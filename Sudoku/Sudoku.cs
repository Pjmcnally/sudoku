using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Net.Http;

namespace Sudoku
{
    public class SudokuBoard
    {
        // Create HttpClient - Instantiate as static so only 1 is created and used for the entire application.
        private static HttpClient client = new HttpClient();

        private int[,] _rawBoard;
        private int[,] _solution;
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

        public void GetBoard(Difficulty difficulty)
        {
            // Make request for raw data.
            string url = "https://sugoku.herokuapp.com/board?difficulty=";
            var response = client.GetStringAsync($"{url}{difficulty}");

            // Deserialize data and save results. 
            var jsonSchema = new {
                board = new int[9, 9]
            };
            var content = JsonConvert.DeserializeAnonymousType(response.Result, jsonSchema);
            _rawBoard = content.board;
        }

        public void DisplayBoard()
        {
            Console.WriteLine("Board: ");
            Print2dArray(_rawBoard);
        }

        public void GetSolution()
        {
            // Make post request with current board to get solution
            string url = "https://sugoku.herokuapp.com/solve";
            var data = new Dictionary<string, string> { { "board", JsonConvert.SerializeObject(_rawBoard) } };
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
            _solution = rawSolution.solution;
            _difficulty = rawSolution.difficulty;
            _status = rawSolution.status;
        }

        public void DisplaySolution()
        {
            Console.WriteLine("Solution: ");
            Print2dArray(_solution);
        }

        private void Print2dArray(int[,] array)
        {
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    Console.Write($"{array[i, j]} ");
                }
                Console.WriteLine();
            }
        }

        public bool BacktrackSolve()
        {
            // Method to run backtrack solve algorithm on the board.

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
