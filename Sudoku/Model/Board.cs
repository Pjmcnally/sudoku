using System.Collections.Generic;
using System.ComponentModel;
using System.Text.Json;

namespace Sudoku.Model
{
    public class Board : INotifyPropertyChanged
    {
        // TODO: Add property to contain rows
        // TODO: Add property to contain columns
        // TODO: Add property to contain boxes
        //     I may not do the 3 items above and may instead use peers inside the cell.

        // Defines regions within the board. Each number below represents a 3x3 group of cells.
        // 0 1 2
        // 3 4 5
        // 6 7 8
        public enum Region
        {
            UpperLeft = 0,
            UpperMiddle = 1,
            UpperRight = 2,
            MiddleLeft = 3,
            Center = 4,
            MiddleRight = 5,
            LowerLeft = 6,
            LowerMiddle = 7,
            LowerRight = 8
        }

        private List<Cell> cells;

        public List<Cell> Cells
        {
            get { return cells; }
            set
            {
                cells = value;
                OnPropertyChanged("Cells");
            }
        }

        private List<Cell> solution;

        public List<Cell> Solution
        {
            get { return solution; }
            set
            {
                solution = value;
                OnPropertyChanged("Solution");
            }
        }

        public Board(string board)
        {
            Cells = CreateFromString(board);
            FindAllPeers();
        }

        public Board(int[,] board)
        {
            Cells = CreateFromArray(board);
            FindAllPeers();
        }

        public void FindAllPeers()
        {
            foreach (Cell cell in Cells)
            {
                cell.Peers = Cells.FindAll(x => x != cell && (x.Row == cell.Row || x.Col == cell.Col || x.Region == cell.Region));
            }
        }

        public static List<Cell> CreateFromString(string rawBoard)
        {
            int[,] board = JsonSerializer.Deserialize<int[,]>(rawBoard);
            return CreateFromArray(board);
        }

        public static List<Cell> CreateFromArray(int[,] rawBoard)
        {
            if (rawBoard.Rank != 2 || rawBoard.GetLength(0) != 9 || rawBoard.GetLength(1) != 9)
            {
                throw new System.ArgumentException("rawBoard must be a 9x9 multidimensional array.", "rawBoard");
            }

            List<Cell> temp = new List<Cell>();
            for (int i = 0; i < rawBoard.GetLength(0); i++)
            {
                for (int j = 0; j < rawBoard.GetLength(1); j++)
                {
                    temp.Add(new Cell(i, j, rawBoard[i, j]));
                }
            }

            return temp;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // ToDo Move to viewModel
        /*

        // Gets board from API of specified difficulty
        public Board(Difficulty difficulty)
        {
            int[,] rawBoard = GetBoardFromApi(difficulty);
            _board = ConvertFromArray(rawBoard);
        }

        public static int[,] ConvertToArray(List<Cell> board)
        {
            int[,] temp = new int[9, 9];
            foreach (Cell cell in board)
            {
                temp[cell.Row, cell.Col] = cell.Value;
            }

            return temp;
        }

        public void GetSolutionFromApi()
        {
            _rawSolution = _getSolutionFromApi(_board);
            _boardSolved = ConvertFromArray(_rawSolution.Solution);
        }

        public class RawSolution
        {
            public string Difficulty;
            public string Status;
            public int[,] Solution;
        }

        */
    }
}
