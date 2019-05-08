using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http;

namespace Sudoku
{
    class Sudoku
    {
        // Create HttpClient - Instantiate as static so only 1 is created and used for the entire application.
        private static HttpClient client = new HttpClient();

        private int[,] _rawBoard;
        private int[,] _solution;
        private string _difficulty;
        private string _status;

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

    }
}
