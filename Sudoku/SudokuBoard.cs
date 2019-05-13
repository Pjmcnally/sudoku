using System;
using System.Text;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Net.Http;

namespace Sudoku
{
    public class SudokuBoard
    {
        // Create HttpClient - Instantiate as static so only 1 is created and used for the entire application.
        private static HttpClient client = new HttpClient();

        private List<Cell> _board;
        private List<Cell> _solution;
        private string _difficulty;
        private string _status;
        
        // TODO: Add property to contain all cells
        
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

        public SudokuBoard(Difficulty difficulty)
        {
            // Make request for raw data.
            string url = "https://sugoku.herokuapp.com/board?difficulty=";
            var response = client.GetStringAsync($"{url}{difficulty}");

            // Deserialize data and save results. 
            var jsonSchema = new {
                board = new int[9, 9]
            };
            var content = JsonConvert.DeserializeAnonymousType(response.Result, jsonSchema);
            _board = ConvertFromArray(content.board);
        }

        public SudokuBoard(string board)
        {
            // Deserialize string and save results. 
            var content = JsonConvert.DeserializeObject<int[,]>(board);
            _board = ConvertFromArray(content);
        }

        private List<Cell> ConvertFromArray(int[,] rawBoard)
        {
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

        private int[,] ConvertToArray(List<Cell> board)
        {
            int[,] temp = new int[9,9];
            foreach (Cell cell in board)
            {
                temp[cell.Row, cell.Col] = cell.Value;
            }

            return temp;
        }

        public void GetSolution()
        {
            // Make post request with current board to get solution
            string url = "https://sugoku.herokuapp.com/solve";
            int[,] arrayBoard = ConvertToArray(_board);
            var data = new Dictionary<string, string> { { "board", JsonConvert.SerializeObject(arrayBoard) } };
            var encodedData = new FormUrlEncodedContent(data);
            var content = client.PostAsync(url, encodedData).Result.Content.ReadAsStringAsync().Result;

            // Deserialize data and save results. 
            var jsonSchema = new
            {
                difficulty = "",
                status = "",
                solution = new int[9, 9]
            };
            var rawSolution = JsonConvert.DeserializeAnonymousType(content, jsonSchema);

            // Save dictionary values to object values
            _solution = ConvertFromArray(rawSolution.solution);
            _difficulty = rawSolution.difficulty;
            _status = rawSolution.status;
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
}
