using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neural_Chess
{
    public enum ChessFigure
    {
        VOID = 0,
        PAWN = 1,
        ROOK = 2,
        KNIGHT = 3,
        BISHOP = 4,
        QUEEN = 5,
        KING = 6
    }

    public enum ChessTeam
    {
        NONE = 0,
        BLACK = 1,
        WHITE = 2
    }

    public enum BoardSelection
    {
        VOID = 0,
        SELECT = 1,
        ENEMY = 2,
        ME = 3
    }

    public class BoardPosition
    {
        int row;
        int col;
        public int Row
        {
            get => row;
            set
            {
                if (value >= 0 && value <= 7)
                    row = value;
                else
                    throw new ArgumentOutOfRangeException("value", value, "Значение должно быть в диапозоне [0 : 7]!");
            }
        }
        public int Col
        {
            get => col;
            set
            {
                if (value >= 0 && value <= 7)
                    col = value;
                else
                    throw new ArgumentOutOfRangeException("value", value, "Значение должно быть в диапозоне [0 : 7]!");
            }
        }

        public BoardPosition(int row, int col)
        {
            Row = row;
            Col = col;
        }
    }

    public class AvailableMove
    {
        public BoardPosition Pos;
        public BoardSelection Selection;

        public AvailableMove(int row, int col, BoardSelection selection)
        {
            Pos = new BoardPosition(row, col);
            Selection = selection;
        }

        public AvailableMove(BoardPosition position, BoardSelection selection)
        {
            Pos = position;
            Selection = selection;
        }
    }

    class Chess_Figure
    {
        public ChessFigure Figure;
        public ChessTeam Team;
        public bool First_Step;
        public List<AvailableMove> AvailableMoves_List;

        public Chess_Figure(ChessFigure figure, ChessTeam team)
        {
            Figure = figure;
            Team = team;
            First_Step = true;
            AvailableMoves_List = new List<AvailableMove>();
        }

        public override string ToString()
        {
            return $"{Figure,-7}{Team.ToString()[0],-2}";
        }
    }

    class Figures_List
    {
        Chess_Figure[,] list = new Chess_Figure[8, 8];
        public Chess_Figure this[int row, int col]
        {
            get => list[row, col];
            set { list[row, col] = value; }
        }

        public Chess_Figure this[BoardPosition position]
        {
            get => list[position.Row, position.Col];
            set { list[position.Row, position.Col] = value; }
        }

        public Figures_List(Chess_Figure[,] list)
        {
            this.list = list;
        }
        public Figures_List(ChessFigure[,] figures_map, ChessTeam[,] teams_map)
        {
            if (figures_map.Length != 64)
                throw new ArgumentException("Не верный аргумент!", "figures_map", new RankException("Массив должен хранить 64 элемента!"));
            else if (teams_map.Length != 64)
                throw new ArgumentException("Не верный аргумент!", "teams_map", new RankException("Массив должен хранить 64 элемента!"));
            else
            {
                for (int row = 0; row < 8; row++)
                    for (int col = 0; col < 8; col++)
                        this[row, col] = new Chess_Figure(figures_map[row, col], teams_map[row, col]);
            }
        }
    }

    public class Chess_Core
    {
        Figures_List field;
        ChessTeam StepTeam;

        bool check_W;
        bool check_B;

        List<Chess_Figure> DangerousFigures_WhiteTeam;
        List<Chess_Figure> DangerousFigures_BlackTeam;

        List<Chess_Figure> Get_DangerousFigures(ChessTeam king_team)
        {
            return king_team == ChessTeam.BLACK ? DangerousFigures_BlackTeam : king_team == ChessTeam.WHITE ? DangerousFigures_WhiteTeam : throw new ArgumentException("Угроза может быть только для белых или чёрных королей!", "king_team");
        }

        public bool Get_Check(ChessTeam team)
        {
            return team == ChessTeam.BLACK ? check_B : team == ChessTeam.WHITE ? check_W : throw new ArgumentException("Шах может поставлен толлько белым или чёрным!", "team");
        }

        public Chess_Core()
        {
            ClearField();

        }

        public void ClearField()
        {
            ChessFigure[,] figures_map = new ChessFigure[8, 8]
                {
                    { ChessFigure.ROOK, ChessFigure.KNIGHT, ChessFigure.BISHOP, ChessFigure.QUEEN,  ChessFigure.KING,   ChessFigure.BISHOP, ChessFigure.KNIGHT, ChessFigure.ROOK },
                    { ChessFigure.PAWN, ChessFigure.PAWN,   ChessFigure.PAWN,   ChessFigure.PAWN,   ChessFigure.PAWN,   ChessFigure.PAWN,   ChessFigure.PAWN,   ChessFigure.PAWN },
                    { ChessFigure.VOID, ChessFigure.VOID,   ChessFigure.VOID,   ChessFigure.VOID,   ChessFigure.VOID,   ChessFigure.VOID,   ChessFigure.VOID,   ChessFigure.VOID },
                    { ChessFigure.VOID, ChessFigure.VOID,   ChessFigure.VOID,   ChessFigure.VOID,   ChessFigure.VOID,   ChessFigure.VOID,   ChessFigure.VOID,   ChessFigure.VOID },
                    { ChessFigure.VOID, ChessFigure.VOID,   ChessFigure.VOID,   ChessFigure.VOID,   ChessFigure.VOID,   ChessFigure.VOID,   ChessFigure.VOID,   ChessFigure.VOID },
                    { ChessFigure.VOID, ChessFigure.VOID,   ChessFigure.VOID,   ChessFigure.VOID,   ChessFigure.VOID,   ChessFigure.VOID,   ChessFigure.VOID,   ChessFigure.VOID },
                    { ChessFigure.PAWN, ChessFigure.PAWN,   ChessFigure.PAWN,   ChessFigure.PAWN,   ChessFigure.PAWN,   ChessFigure.PAWN,   ChessFigure.PAWN,   ChessFigure.PAWN },
                    { ChessFigure.ROOK, ChessFigure.KNIGHT, ChessFigure.BISHOP, ChessFigure.QUEEN,  ChessFigure.KING,   ChessFigure.BISHOP, ChessFigure.KNIGHT, ChessFigure.ROOK }
                };
            ChessTeam[,] teams_map = new ChessTeam[8, 8]
                {
                    { ChessTeam.BLACK,  ChessTeam.BLACK,    ChessTeam.BLACK,    ChessTeam.BLACK,    ChessTeam.BLACK,    ChessTeam.BLACK,    ChessTeam.BLACK,    ChessTeam.BLACK },
                    { ChessTeam.BLACK,  ChessTeam.BLACK,    ChessTeam.BLACK,    ChessTeam.BLACK,    ChessTeam.BLACK,    ChessTeam.BLACK,    ChessTeam.BLACK,    ChessTeam.BLACK },
                    { ChessTeam.NONE,   ChessTeam.NONE,     ChessTeam.NONE,     ChessTeam.NONE,     ChessTeam.NONE,     ChessTeam.NONE,     ChessTeam.NONE,     ChessTeam.NONE },
                    { ChessTeam.NONE,   ChessTeam.NONE,     ChessTeam.NONE,     ChessTeam.NONE,     ChessTeam.NONE,     ChessTeam.NONE,     ChessTeam.NONE,     ChessTeam.NONE },
                    { ChessTeam.NONE,   ChessTeam.NONE,     ChessTeam.NONE,     ChessTeam.NONE,     ChessTeam.NONE,     ChessTeam.NONE,     ChessTeam.NONE,     ChessTeam.NONE },
                    { ChessTeam.NONE,   ChessTeam.NONE,     ChessTeam.NONE,     ChessTeam.NONE,     ChessTeam.NONE,     ChessTeam.NONE,     ChessTeam.NONE,     ChessTeam.NONE },
                    { ChessTeam.WHITE,  ChessTeam.WHITE,    ChessTeam.WHITE,    ChessTeam.WHITE,    ChessTeam.WHITE,    ChessTeam.WHITE,    ChessTeam.WHITE,    ChessTeam.WHITE },
                    { ChessTeam.WHITE,  ChessTeam.WHITE,    ChessTeam.WHITE,    ChessTeam.WHITE,    ChessTeam.WHITE,    ChessTeam.WHITE,    ChessTeam.WHITE,    ChessTeam.WHITE }
                };
            field = new Figures_List(figures_map, teams_map);
            StepTeam = ChessTeam.WHITE;
            Prepear_BeforeMove(StepTeam);
        }

        public override string ToString()
        {
            string field_str = "";
            for (int row = 0; row < 8; row++)
            {
                for (int col = 0; col < 8; col++)
                    field_str += field[row, col];
                field_str += '\n';
            }
            return field_str;
        }

        List<AvailableMove> Get_AvailableMoves(BoardPosition position, ChessFigure figure_type = ChessFigure.VOID, bool king_check = false)
        {
            Chess_Figure figure = field[position];
            List<AvailableMove> availableMoves = new List<AvailableMove>();
            BoardPosition attaked_position;
            int row = position.Row, col = position.Col;

            if (figure_type == ChessFigure.VOID)
                figure_type = figure.Figure;

            int k = figure.Team == ChessTeam.BLACK ? 1 : -1;

            ChessTeam enemy_team =
                figure.Team == ChessTeam.WHITE ? ChessTeam.BLACK :
                    figure.Team == ChessTeam.BLACK ? ChessTeam.BLACK :
                        throw new ArgumentException("Перемещать можно только белые или чёрные фигуры!", "figure.Team");

            Console.WriteLine($"{figure}\n");

            switch (figure_type)
            {
                case ChessFigure.PAWN:
                    {
                        // Шаг вперёд
                        attaked_position = null;
                        try { attaked_position = new BoardPosition(row + k, col); }
                        catch { }
                        if (attaked_position != null)
                            if (field[attaked_position].Team == ChessTeam.NONE)
                                availableMoves.Add(new AvailableMove(attaked_position, BoardSelection.SELECT));

                        // 2 шага вперёд 
                        attaked_position = null;
                        try { attaked_position = new BoardPosition(row + 2 * k, col); }
                        catch { }
                        if (attaked_position != null)
                            if (field[attaked_position].Team == ChessTeam.NONE)
                                availableMoves.Add(new AvailableMove(attaked_position, BoardSelection.SELECT));

                        // Атака направо-вперёд
                        attaked_position = null;
                        try { attaked_position = new BoardPosition(row + k, col + 1); }
                        catch { }
                        if (attaked_position != null)
                            if (field[attaked_position].Team == enemy_team)
                                availableMoves.Add(new AvailableMove(attaked_position, BoardSelection.ENEMY));

                        // Атака налево-вперёд
                        attaked_position = null;
                        try { attaked_position = new BoardPosition(row + k, col - 1); }
                        catch { }
                        if (attaked_position != null)
                            if (field[attaked_position].Team == enemy_team)
                                availableMoves.Add(new AvailableMove(attaked_position, BoardSelection.ENEMY));

                        break;
                    }

                case ChessFigure.ROOK:
                    {
                        // Прямая вниз
                        for (int x = row + 1; x < 8; x++)
                        {
                            attaked_position = null;
                            bool breaker = false;
                            try { attaked_position = new BoardPosition(x, col); }
                            catch { }
                            if (attaked_position != null)
                                if (field[attaked_position].Team == ChessTeam.NONE)
                                    availableMoves.Add(new AvailableMove(attaked_position, BoardSelection.SELECT));
                                else
                                {
                                    if (field[attaked_position].Team == enemy_team)
                                        availableMoves.Add(new AvailableMove(attaked_position, BoardSelection.ENEMY));
                                    else if (field[attaked_position].Team != figure.Team)
                                        throw new TaskSchedulerException("Ошибка! Ожидалась союзная фигурв!");
                                    breaker = true;

                                    if (king_check && field[attaked_position].Figure == ChessFigure.KING && field[attaked_position].Team == figure.Team)
                                        breaker = false;
                                }
                            if (breaker) break;
                        }

                        // Прямая вверх
                        for (int x = row - 1; x > 0; x--)
                        {
                            attaked_position = null;
                            bool breaker = false;
                            try { attaked_position = new BoardPosition(x, col); }
                            catch { }
                            if (attaked_position != null)
                                if (field[attaked_position].Team == ChessTeam.NONE)
                                    availableMoves.Add(new AvailableMove(attaked_position, BoardSelection.SELECT));
                                else
                                {
                                    if (field[attaked_position].Team == enemy_team)
                                        availableMoves.Add(new AvailableMove(attaked_position, BoardSelection.ENEMY));
                                    else if (field[attaked_position].Team != figure.Team)
                                        throw new TaskSchedulerException("Ошибка! Ожидалась союзная фигурв!");
                                    breaker = true;

                                    if (king_check && field[attaked_position].Figure == ChessFigure.KING && field[attaked_position].Team == figure.Team)
                                        breaker = false;
                                }
                            if (breaker) break;
                        }

                        // Прямая вправо
                        for (int y = col + 1; y < 8; y++)
                        {
                            attaked_position = null;
                            bool breaker = false;
                            try { attaked_position = new BoardPosition(row, y); }
                            catch { }
                            if (attaked_position != null)
                                if (field[attaked_position].Team == ChessTeam.NONE)
                                    availableMoves.Add(new AvailableMove(attaked_position, BoardSelection.SELECT));
                                else
                                {
                                    if (field[attaked_position].Team == enemy_team)
                                        availableMoves.Add(new AvailableMove(attaked_position, BoardSelection.ENEMY));
                                    else if (field[attaked_position].Team != figure.Team)
                                        throw new TaskSchedulerException("Ошибка! Ожидалась союзная фигурв!");
                                    breaker = true;

                                    if (king_check && field[attaked_position].Figure == ChessFigure.KING && field[attaked_position].Team == figure.Team)
                                        breaker = false;
                                }
                            if (breaker) break;
                        }

                        // Прямая влево
                        for (int y = col - 1; y > 0; y--)
                        {
                            attaked_position = null;
                            bool breaker = false;
                            try { attaked_position = new BoardPosition(row, y); }
                            catch { }
                            if (attaked_position != null)
                                if (field[attaked_position].Team == ChessTeam.NONE)
                                    availableMoves.Add(new AvailableMove(attaked_position, BoardSelection.SELECT));
                                else
                                {
                                    if (field[attaked_position].Team == enemy_team)
                                        availableMoves.Add(new AvailableMove(attaked_position, BoardSelection.ENEMY));
                                    else if (field[attaked_position].Team != figure.Team)
                                        throw new TaskSchedulerException("Ошибка! Ожидалась союзная фигурв!");
                                    breaker = true;

                                    if (king_check && field[attaked_position].Figure == ChessFigure.KING && field[attaked_position].Team == figure.Team)
                                        breaker = false;
                                }
                            if (breaker) break;
                        }

                        break;
                    }

                /// \todo Оптимизировать по возможности!!
                case ChessFigure.KNIGHT:
                    {
                        // Вниз 1. Вправо 2
                        attaked_position = null;
                        try { attaked_position = new BoardPosition(row + 1, col + 2); }
                        catch { }
                        if (attaked_position != null)
                            if (field[attaked_position].Team != figure.Team)
                            {
                                BoardSelection selection = field[attaked_position].Team == ChessTeam.NONE ? BoardSelection.SELECT :
                                        field[attaked_position].Team == enemy_team ? BoardSelection.ENEMY :
                                            throw new TaskSchedulerException("Ошибка! Ожидалась вражеская фигурв!");

                                availableMoves.Add(new AvailableMove(attaked_position, selection));
                            }

                        // Вниз 2. Вправо 1
                        attaked_position = null;
                        try { attaked_position = new BoardPosition(row + 2, col + 1); }
                        catch { }
                        if (attaked_position != null)
                            if (field[attaked_position].Team != figure.Team)
                            {
                                BoardSelection selection = field[attaked_position].Team == ChessTeam.NONE ? BoardSelection.SELECT :
                                        field[attaked_position].Team == enemy_team ? BoardSelection.ENEMY :
                                            throw new TaskSchedulerException("Ошибка! Ожидалась вражеская фигурв!");

                                availableMoves.Add(new AvailableMove(attaked_position, selection));
                            }

                        // Вниз 2. Влево 1
                        attaked_position = null;
                        try { attaked_position = new BoardPosition(row + 2, col - 1); }
                        catch { }
                        if (attaked_position != null)
                            if (field[attaked_position].Team != figure.Team)
                            {
                                BoardSelection selection = field[attaked_position].Team == ChessTeam.NONE ? BoardSelection.SELECT :
                                        field[attaked_position].Team == enemy_team ? BoardSelection.ENEMY :
                                            throw new TaskSchedulerException("Ошибка! Ожидалась вражеская фигурв!");

                                availableMoves.Add(new AvailableMove(attaked_position, selection));
                            }

                        // Вниз 1. Влево 2
                        attaked_position = null;
                        try { attaked_position = new BoardPosition(row + 1, col - 2); }
                        catch { }
                        if (attaked_position != null)
                            if (field[attaked_position].Team != figure.Team)
                            {
                                BoardSelection selection = field[attaked_position].Team == ChessTeam.NONE ? BoardSelection.SELECT :
                                        field[attaked_position].Team == enemy_team ? BoardSelection.ENEMY :
                                            throw new TaskSchedulerException("Ошибка! Ожидалась вражеская фигурв!");

                                availableMoves.Add(new AvailableMove(attaked_position, selection));
                            }

                        // Вверх 1. Влево 2
                        attaked_position = null;
                        try { attaked_position = new BoardPosition(row - 1, col - 2); }
                        catch { }
                        if (attaked_position != null)
                            if (field[attaked_position].Team != figure.Team)
                            {
                                BoardSelection selection = field[attaked_position].Team == ChessTeam.NONE ? BoardSelection.SELECT :
                                        field[attaked_position].Team == enemy_team ? BoardSelection.ENEMY :
                                            throw new TaskSchedulerException("Ошибка! Ожидалась вражеская фигурв!");

                                availableMoves.Add(new AvailableMove(attaked_position, selection));
                            }

                        // Вверх 2. Влево 1
                        attaked_position = null;
                        try { attaked_position = new BoardPosition(row - 2, col - 1); }
                        catch { }
                        if (attaked_position != null)
                            if (field[attaked_position].Team != figure.Team)
                            {
                                BoardSelection selection = field[attaked_position].Team == ChessTeam.NONE ? BoardSelection.SELECT :
                                        field[attaked_position].Team == enemy_team ? BoardSelection.ENEMY :
                                            throw new TaskSchedulerException("Ошибка! Ожидалась вражеская фигурв!");

                                availableMoves.Add(new AvailableMove(attaked_position, selection));
                            }

                        // Вверх 2. Вправо 1
                        attaked_position = null;
                        try { attaked_position = new BoardPosition(row - 2, col + 1); }
                        catch { }
                        if (attaked_position != null)
                            if (field[attaked_position].Team != figure.Team)
                            {
                                BoardSelection selection = field[attaked_position].Team == ChessTeam.NONE ? BoardSelection.SELECT :
                                        field[attaked_position].Team == enemy_team ? BoardSelection.ENEMY :
                                            throw new TaskSchedulerException("Ошибка! Ожидалась вражеская фигурв!");

                                availableMoves.Add(new AvailableMove(attaked_position, selection));
                            }

                        // Вверх 1. Вправо 2
                        attaked_position = null;
                        try { attaked_position = new BoardPosition(row - 1, col + 2); }
                        catch { }
                        if (attaked_position != null)
                            if (field[attaked_position].Team != figure.Team)
                            {
                                BoardSelection selection = field[attaked_position].Team == ChessTeam.NONE ? BoardSelection.SELECT :
                                        field[attaked_position].Team == enemy_team ? BoardSelection.ENEMY :
                                            throw new TaskSchedulerException("Ошибка! Ожидалась вражеская фигурв!");

                                availableMoves.Add(new AvailableMove(attaked_position, selection));
                            }

                        break;
                    }
                case ChessFigure.BISHOP:
                    {
                        // Правые диагонали
                        bool stopper_top = false;
                        bool stopper_bottom = false;
                        for (int y = col + 1; y < 8; y++)
                        {
                            // Правая. Нижняя
                            if (!stopper_bottom)
                            {
                                attaked_position = null;
                                try { attaked_position = new BoardPosition(row + y - col, y); }
                                catch { }
                                if (attaked_position != null)
                                    if (field[attaked_position].Team == ChessTeam.NONE)
                                        availableMoves.Add(new AvailableMove(attaked_position, BoardSelection.SELECT));
                                    else
                                    {
                                        if (field[attaked_position].Team == enemy_team)
                                            availableMoves.Add(new AvailableMove(attaked_position, BoardSelection.ENEMY));
                                        else if (field[attaked_position].Team != figure.Team)
                                            throw new TaskSchedulerException("Ошибка! Ожидалась союзная фигурв!");
                                        stopper_bottom = true;

                                        if (king_check && field[attaked_position].Figure == ChessFigure.KING && field[attaked_position].Team == figure.Team)
                                            stopper_bottom = false;
                                    }
                            }

                            // Правая. Верхняя
                            if (!stopper_top)
                            {
                                attaked_position = null;
                                try { attaked_position = new BoardPosition(row - y - col, y); }
                                catch { }
                                if (attaked_position != null)
                                    if (field[attaked_position].Team == ChessTeam.NONE)
                                        availableMoves.Add(new AvailableMove(attaked_position, BoardSelection.SELECT));
                                    else
                                    {
                                        if (field[attaked_position].Team == enemy_team)
                                            availableMoves.Add(new AvailableMove(attaked_position, BoardSelection.ENEMY));
                                        else if (field[attaked_position].Team != figure.Team)
                                            throw new TaskSchedulerException("Ошибка! Ожидалась союзная фигурв!");
                                        stopper_top = true;

                                        if (king_check && field[attaked_position].Figure == ChessFigure.KING && field[attaked_position].Team == figure.Team)
                                            stopper_top = false;
                                    }
                            }
                        }

                        // Левые диагонали
                        stopper_top = false;
                        stopper_bottom = false;
                        for (int y = col - 1; y > 0; y--)
                        {
                            // Правая. Нижняя
                            if (!stopper_bottom)
                            {
                                attaked_position = null;
                                try { attaked_position = new BoardPosition(row + y - col, y); }
                                catch { }
                                if (attaked_position != null)
                                    if (field[attaked_position].Team == ChessTeam.NONE)
                                        availableMoves.Add(new AvailableMove(attaked_position, BoardSelection.SELECT));
                                    else
                                    {
                                        if (field[attaked_position].Team == enemy_team)
                                            availableMoves.Add(new AvailableMove(attaked_position, BoardSelection.ENEMY));
                                        else if (field[attaked_position].Team != figure.Team)
                                            throw new TaskSchedulerException("Ошибка! Ожидалась союзная фигурв!");
                                        stopper_bottom = true;

                                        if (king_check && field[attaked_position].Figure == ChessFigure.KING && field[attaked_position].Team == figure.Team)
                                            stopper_bottom = false;
                                    }
                            }

                            // Правая. Верхняя
                            if (!stopper_top)
                            {
                                attaked_position = null;
                                try { attaked_position = new BoardPosition(row - y - col, y); }
                                catch { }
                                if (attaked_position != null)
                                    if (field[attaked_position].Team == ChessTeam.NONE)
                                        availableMoves.Add(new AvailableMove(attaked_position, BoardSelection.SELECT));
                                    else
                                    {
                                        if (field[attaked_position].Team == enemy_team)
                                            availableMoves.Add(new AvailableMove(attaked_position, BoardSelection.ENEMY));
                                        else if (field[attaked_position].Team != figure.Team)
                                            throw new TaskSchedulerException("Ошибка! Ожидалась союзная фигурв!");
                                        stopper_top = true;

                                        if (king_check && field[attaked_position].Figure == ChessFigure.KING && field[attaked_position].Team == figure.Team)
                                            stopper_top = false;
                                    }
                            }
                        }

                        break;
                    }
                case ChessFigure.QUEEN:
                    {
                        // Горизонтали и вертикали
                        availableMoves.AddRange(Get_AvailableMoves(position, ChessFigure.BISHOP, king_check));

                        // Диагонали
                        availableMoves.AddRange(Get_AvailableMoves(position, ChessFigure.ROOK, king_check));

                        break;
                    }
                case ChessFigure.KING:
                    {
                        for (int x = row - 1; x < row + 1; x++)
                            for (int y = col - 1; y < col + 1; y++)
                            {
                                attaked_position = null;
                                try { attaked_position = new BoardPosition(x, y); }
                                catch { }
                                if (attaked_position != null)
                                    if (field[attaked_position].Team != figure.Team)
                                    {
                                        BoardSelection select =
                                            field[attaked_position].Team == ChessTeam.NONE ? BoardSelection.SELECT :
                                                field[attaked_position].Team == enemy_team ? BoardSelection.ENEMY :
                                                    throw new TaskSchedulerException("Ошибка! Ожидалась вражеская фигурв!");

                                        if (king_check)
                                            availableMoves.Add(new AvailableMove(attaked_position, select));

                                        else if (Cell_IsSafe(attaked_position, figure.Team))
                                            availableMoves.Add(new AvailableMove(attaked_position, select));
                                    }
                            }

                        break;
                    }
            }

            return availableMoves;
        }

        bool Cell_IsSafe(BoardPosition position, ChessTeam team)
        {
            if (Get_AvailableMoves(position, ChessFigure.PAWN).Find(move => move.Selection == BoardSelection.ENEMY) == null)                // Клетка не атакована пешкой
                if (Get_AvailableMoves(position, ChessFigure.KNIGHT).Find(move => move.Selection == BoardSelection.ENEMY) == null)          // Клктка не атакованна конём
                    if (Get_AvailableMoves(position, ChessFigure.QUEEN).Find(move => move.Selection == BoardSelection.ENEMY) == null)       // Клетка не атакованна ладьёй, слоном, королевой
                        if (Get_AvailableMoves(position, ChessFigure.KING).Find(move => move.Selection == BoardSelection.ENEMY) == null)    // Клетка не атакованна королём
                            return true;
            return false;
        }

        public void MoveFigure(BoardPosition firstPos, BoardPosition secondPos)
        {
            Chess_Figure movingFigure = field[firstPos];
            Chess_Figure attackedFigure = field[secondPos];
            BoardSelection selection = attackedFigure.Team == ChessTeam.NONE ? BoardSelection.SELECT : BoardSelection.ENEMY;
            AvailableMove move = movingFigure.AvailableMoves_List.Find(fmove => fmove.Pos.Row == secondPos.Row && fmove.Pos.Col == secondPos.Col);

            if (movingFigure.Team == StepTeam && move != null)
            {
                field[firstPos] = new Chess_Figure(ChessFigure.VOID, ChessTeam.NONE);
                field[secondPos] = movingFigure;
                StepTeam = StepTeam == ChessTeam.WHITE ? ChessTeam.BLACK : ChessTeam.WHITE;
                Prepear_BeforeMove(StepTeam);
            }
        }

        void Prepear_BeforeMove(ChessTeam prepearing_team)
        {
            BoardPosition king_position;
            for (int row = 0; row < 8; row++)
                for (int col = 0; col < 8; col++)
                    if (field[row, col].Team == prepearing_team && field[row, col].Figure == ChessFigure.KING)
                    {
                        king_position = new BoardPosition(row, col);
                        break;
                    }

            for (int row = 0; row < 8; row++)
                for (int col = 0; col < 8; col++)
                {
                    if (field[row, col].Team == prepearing_team)
                        field[row, col].AvailableMoves_List = Get_AvailableMoves(new BoardPosition(row, col));
                }
        }
    }
}
