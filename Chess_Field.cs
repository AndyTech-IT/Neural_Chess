using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Neural_Chess
{
    public partial class Chess_Field : UserControl
    {
        public Chess_Field()
        {
            InitializeComponent();
            ClearField();
            SetImagePack(new List<Image>(2), new List<Image>(13));
            Field_PictureBox.Image = GetNewFieldImage();
        }
        List<Image> Figures { get; set; } = new List<Image>(14);
        List<Image> Selections { get; set; } = new List<Image>(3);
        public int[,] Field_Figures { get; set; } = new int[8, 8];
        public int[,] Field_Selections { get; set; } = new int[8, 8];
        public Color WhiteCell_Color { get; set; } = Color.White;
        public Color BlackCell_Color { get; set; } = Color.Black;
        float CellSize { get { return (float)Field_PictureBox.Width / 8F; } }

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
                    DrawCell(g, Field_Figures[row, col], Field_Selections[row, col], row, col);
        }

        void DrawCell(Graphics g, int figureID, int selectionID, int row, int col)
        {
            if ((col + row) % 2 != 0)
                g.FillRectangle(new SolidBrush(BlackCell_Color), col * CellSize, row * CellSize, CellSize, CellSize);
            else
                g.FillRectangle(new SolidBrush(WhiteCell_Color), col * CellSize, row * CellSize, CellSize, CellSize);

            g.DrawImage(Selections[selectionID], col * CellSize, row * CellSize, CellSize, CellSize);

            g.DrawImage(Figures[figureID], col*CellSize, row*CellSize, CellSize, CellSize);
        }

        public void ClearField()
        {
            for (int row = 0; row < 8; row++)
                for (int col = 0; col < 8; col++)
                    Field_Selections[row, col] = new int();

            Field_Figures = new int[8, 8] {
                { 2+6, 3+6, 4+6, 5+6, 6+6, 4+6, 3+6, 2+6 },
                { 1+6, 1+6, 1+6, 1+6, 1+6, 1+6, 1+6, 1+6 },
                { 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0 },
                { 1, 1, 1, 1, 1, 1, 1, 1 },
                { 2, 3, 4, 5, 6, 4, 3, 2 }
            };
        }

        public void SetImagePack(List<Image> Selections_Images, List<Image> Figures_Images)
        {
            Selections = new List<Image>();
            Figures = new List<Image>();

            Selections.Add(new Bitmap(1, 1));
            Figures.Add(new Bitmap(1, 1));

            for (int i = 0; i < 2; i++)
                if (Selections_Images.Count <= i)
                {
                    Selections.Add(new Bitmap(Convert.ToInt32(CellSize), Convert.ToInt32(CellSize)));
                    Graphics.FromImage(Selections[i + 1]).FillRectangle(Brushes.Red, 5, 5, CellSize - 10, CellSize - 10);
                }
                else
                    Selections.Add(Selections_Images[i]);
                
            for (int i=0; i<13; i++)
                if (Figures_Images.Count <= i)
                {
                    Figures.Add(new Bitmap(Convert.ToInt32(CellSize), Convert.ToInt32(CellSize)));
                    Graphics.FromImage(Figures[i + 1]).DrawString((i + 1).ToString(), A_TopLabel.Font, Brushes.Red, CellSize/2, CellSize/2);
                }
                else
                    Figures.Add(Figures_Images[i]);
        }
    }
}
