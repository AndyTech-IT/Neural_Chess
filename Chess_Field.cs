using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Neural_Chess
{
    public partial class Chess_Field : UserControl
    {
        class ChessFigure_Images
        {
            Image Pawn_Image;
            Image Rook_Image;
            Image Knight_Image;
            Image Bishop_Image;
            Image Queen_Image;
            Image King_Image;

            public Image this[ChessFigure figure]
            {
                get
                {
                    switch (figure)
                    {
                        case ChessFigure.PAWN   :   return Pawn_Image;
                        case ChessFigure.ROOK   :   return Rook_Image;
                        case ChessFigure.KNIGHT :   return Knight_Image;
                        case ChessFigure.BISHOP :   return Bishop_Image;
                        case ChessFigure.QUEEN  :   return Queen_Image;
                        case ChessFigure.KING   :   return King_Image;
                    }
                    throw new ArgumentException("Данный тип фигуры не имеет изображения!", "figure");
                }
                set
                {
                    switch (figure)
                    {
                        case ChessFigure.PAWN   :   Pawn_Image      = value; break;
                        case ChessFigure.ROOK   :   Rook_Image      = value; break;
                        case ChessFigure.KNIGHT :   Knight_Image    = value; break;
                        case ChessFigure.BISHOP :   Bishop_Image    = value; break;
                        case ChessFigure.QUEEN  :   Queen_Image     = value; break;
                        case ChessFigure.KING   :   King_Image      = value; break;
                    }
                    throw new ArgumentException("Данный тип фигуры не имеет изображения!", "figure");
                }
            }

            public ChessFigure_Images(Image[] images)
            {
                if (images.Length == 6)
                {
                    Pawn_Image = images[0];
                    Rook_Image = images[1];
                    Knight_Image = images[2];
                    Bishop_Image = images[3];
                    Queen_Image = images[4];
                    King_Image = images[5];
                }
                else
                    throw new ArgumentException("Необходим массив из 7-ми изображений!", "images");
            }
        }

        class BoardSelections_Images
        {
            Image ME_Image;
            Image Select_Image;
            Image Enemy_Image;

            public Image this[BoardSelection selection]
            {
                get
                {
                    switch (selection)
                    {
                        case BoardSelection.ME: return ME_Image;
                        case BoardSelection.SELECT: return Select_Image;
                        case BoardSelection.ENEMY: return Enemy_Image;

                    }
                    throw new ArgumentException("Данный тип выделения не имеет изображения!", "figure");
                }
                set
                {
                    switch (selection)
                    {
                        case BoardSelection.ME: ME_Image = value; break;
                        case BoardSelection.SELECT: Select_Image = value; break;
                        case BoardSelection.ENEMY: Enemy_Image = value; break;
                    }
                    throw new ArgumentException("Данный тип выделения не имеет изображения!", "figure");
                }
            }

            public BoardSelections_Images(Image[] images)
            {
                if (images.Length == 3)
                {
                    ME_Image = images[0];
                    Select_Image = images[1];
                    Enemy_Image = images[2];
                }
                else
                    throw new ArgumentException("Необходим массив из 3-x изображений!", "images");
            }
        }

        class BoardSelections_Map
        {
            BoardSelection[,] map;

            public BoardSelection this[BoardPosition position]
            {
                get => map[position.Row, position.Col];
                set => map[position.Row, position.Col] = value;
            }
            public BoardSelection this[int row, int col]
            {
                get => map[row, col];
                set => map[row, col] = value;
            }

            public BoardSelections_Map()
            {
                map = new BoardSelection[8, 8];
                for (int row = 0; row < 8; row++)
                    for (int col = 0; col < 8; col++)
                        map[row, col] = BoardSelection.VOID;
            }
        }

        public Image WhiteCell_Image { get; set; }
        public Image BlackCell_Image { get; set; }
        ChessFigure_Images WhiteFigures_Images { get; set; }
        ChessFigure_Images BlackFigures_Images { get; set; }
        BoardSelections_Images Selections_Images { get; set; }
        Chess_Board Board { get; set; }
        float CellSize { get { return (float)Field_PictureBox.Width / 8F; } }
        BoardSelections_Map Selections_Map { get; set; }
        public Chess_Field(string images_Path)
        {
            InitializeComponent();
            Board = new Chess_Board();
            Selections_Map = new BoardSelections_Map();
            SetImage_Pack(images_Path);
            Field_PictureBox.Image = GetNewFieldImage();
        }

        private void Label_Paint(object sender, PaintEventArgs e)
        {
            Label label = (Label)sender;
            e.Graphics.Clear(this.BackColor);
            e.Graphics.RotateTransform(-180);
            SizeF textSize = e.Graphics.MeasureString(label.Text, label.Font);
            e.Graphics.TranslateTransform(-(label.Height / 2), -(label.Width / 2));
            e.Graphics.DrawString(label.Text, label.Font, Brushes.Black, -(textSize.Width / 2), -(textSize.Height / 2));
        }

        private void Chess_Field_Resize(object sender, EventArgs e)
        {
            if (Width < Height)
                Size = new Size(Width, Width);
            else
                Size = new Size(Height, Height);
        }

        Bitmap GetNewFieldImage()
        {
            Bitmap bmp = new Bitmap(Field_PictureBox.Width, Field_PictureBox.Height);
            Graphics g = Graphics.FromImage(bmp);
            DrawField(g);
            return bmp;
        }

        void DrawField(Graphics g)
        {
            for (int row=0; row<8; row++)
                for (int col=0; col<8; col++)
                    DrawCell(g, Board[row, col], Selections_Map[row, col], row, col);
        }

        void DrawCell(Graphics g, Chess_Figure figure, BoardSelection selection, int row, int col)
        {
            if ((col + row) % 2 != 0)
                g.DrawImageUnscaledAndClipped(BlackCell_Image, new Rectangle((int)(col * CellSize), (int)(row * CellSize), (int)CellSize, (int)CellSize));
            else
                g.DrawImageUnscaledAndClipped(WhiteCell_Image, new Rectangle((int)(col * CellSize), (int)(row * CellSize), (int)CellSize, (int)CellSize));

            if (selection != BoardSelection.VOID)
                g.DrawImageUnscaledAndClipped(Selections_Images[selection], new Rectangle((int)(col * CellSize), (int)(row * CellSize), (int)CellSize, (int)CellSize));

            if (figure.Figure != ChessFigure.VOID)
            {
                Image figure_image =
                    figure.Team == ChessTeam.WHITE ? WhiteFigures_Images[figure.Figure] :
                        figure.Team == ChessTeam.BLACK ? BlackFigures_Images[figure.Figure] :
                            throw new ArgumentException("Ожидалась белая или чёрная фигура!", "figure.Team");

                g.DrawImageUnscaledAndClipped(figure_image, new Rectangle((int)(col * CellSize), (int)(row * CellSize), (int)CellSize, (int)CellSize));
            }
            
        }

        public void ClearField()
        {
            Board.ClearField();
            Selections_Map = new BoardSelections_Map();
        }
        public void SetImage_Pack(string images_Path)
        {
            string whiteFigures_Path = $"{images_Path}\\white_figures";
            string blackFigures_Path = $"{images_Path}\\black_figures";
            string selections_Path = $"{images_Path}\\selections";
            string cells_Path = $"{images_Path}\\cells";

            Image[] white_figures = new Image[6];
            Image[] black_figures = new Image[6];
            Image[] selections = new Image[3];
            FileStream stream;
            string path;
            int i;

            i = 0;
            foreach (string filename in new string[] { "pawn", "rook", "knight", "bishop", "queen", "king"})
            {
                path = $"{whiteFigures_Path}\\{filename}.png";
                stream = new FileStream(path, FileMode.Open, FileAccess.Read);
                white_figures[i] = Image.FromStream(stream);
                stream.Close();

                path = $"{blackFigures_Path}\\{filename}.png";
                stream = new FileStream(path, FileMode.Open, FileAccess.Read);
                black_figures[i] = Image.FromStream(stream);
                stream.Close();

                i++;
            }
            WhiteFigures_Images = new ChessFigure_Images(white_figures);
            BlackFigures_Images = new ChessFigure_Images(black_figures);

            i = 0;
            foreach (string filename in new string[] { "me", "select", "enemy" })
            {
                path = $"{selections_Path}\\{filename}.png";
                stream = new FileStream(path, FileMode.Open, FileAccess.Read);
                selections[i] = Image.FromStream(stream);
                stream.Close();

                i++;
            }
            Selections_Images = new BoardSelections_Images(selections);

            path = $"{cells_Path}\\white.png";
            stream = new FileStream(path, FileMode.Open, FileAccess.Read);
            WhiteCell_Image = Image.FromStream(stream);
            stream.Close();

            path = $"{cells_Path}\\black.png";
            stream = new FileStream(path, FileMode.Open, FileAccess.Read);
            BlackCell_Image = Image.FromStream(stream);
            stream.Close();

        }
    }
}
