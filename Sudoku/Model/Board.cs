using System;
using System.Collections.Generic;

namespace Sudoku.Model
{
    internal class Board
    {
        private List<Cell> _board;
        private List<Cell> _boardSolved;
        private RawSolution _rawSolution;

        // TODO: Add property to contain rows
        // TODO: Add property to contain columns
        // TODO: Add property to contain boxes
        //     I may not do the 3 items above and may instead use peers inside the cell.

        public enum Difficulty
        {
            random,
            easy,
            medium,
            hard
        }

        // Gets board from API of specified difficulty
        public Board(Difficulty difficulty)
        {
            int[,] rawBoard = GetBoardFromApi(difficulty);
            _board = ConvertFromArray(rawBoard);
        }

        public Board(string board)
        {
            _board = ConvertFromString(board);
        }

        public Board(int[,] board)
        {
            _board = ConvertFromArray(board);
        }

        private int[,] GetBoardFromApi(Difficulty difficulty)
        {
            // Gets raw data
            string url = "https://sugoku.herokuapp.com/board?difficulty=";
            var response = client.GetStringAsync($"{url}{difficulty}");

            // Deserialize data.
            var jsonSchema = new { board = new int[9, 9] };
            var content = JsonConvert.DeserializeAnonymousType(response.Result, jsonSchema);
            return content.board;
        }

        public static List<Cell> ConvertFromString(string rawBoard)
        {
            try
            {
                int[,] board = JsonConvert.DeserializeObject<int[,]>(rawBoard);
                return ConvertFromArray(board);
            }
            catch (Exception ex) when (
                ex is Newtonsoft.Json.JsonSerializationException ||
                ex is Newtonsoft.Json.JsonReaderException
            )
            {
                throw new System.ArgumentException("rawBoard not readable as JSON string. rawBoard must be a valid json string of a 9x9 multidimensional array.", "rawBoard");
            }
        }

        public static List<Cell> ConvertFromArray(int[,] rawBoard)
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

        private RawSolution _getSolutionFromApi(List<Cell> board)
        {
            // Make post request with current board to get solution
            string url = "https://sugoku.herokuapp.com/solve";
            int[,] arrayBoard = ConvertToArray(board);
            var data = new Dictionary<string, string> { { "board", JsonConvert.SerializeObject(arrayBoard) } };
            var encodedData = new FormUrlEncodedContent(data);
            var content = client.PostAsync(url, encodedData).Result.Content.ReadAsStringAsync().Result;

            // Deserialize data and save results.
            var rawSolution = JsonConvert.DeserializeObject<RawSolution>(content);

            return rawSolution;
        }

        public bool BacktrackSolve()
        {
            // Method to run backtrack solve algorithm on the board.

            List<Cell> unsolved = new List<Cell>();
            // list = list of unsolved cells
            // i = 0
            // while (i > 0 && i < length of unsolved list):
            //  if (list[i] can update guess && new guess creates a valid board)
            //    i++
            //  else
            //    reset current cell
            //    i--
            //
            //  if (i = length of unsolved list)
            //    return true  // board is solved
            //  else
            //    return false  // board is unsolvable

            return false;
        }
    }

    public class RawSolution
    {
        public string Difficulty;
        public string Status;
        public int[,] Solution;
    }
}
