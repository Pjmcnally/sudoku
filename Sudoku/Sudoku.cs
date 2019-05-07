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

        private int[,] RawBoard;
        private int[,] Solution;
        private string Difficulty;
        private string Status;

        public enum difficulty
        {
            random,
            easy,
            medium,
            hard
        }

        public void GetBoard(difficulty difficulty)
        {
            string url = "https://sugoku.herokuapp.com/board?difficulty=";
            
            var randResponse = client.GetStringAsync($"{url}{difficulty}");
            var rawBoard = JsonConvert.DeserializeObject<Dictionary<string, int[,]>>(randResponse.Result);
            RawBoard = rawBoard["board"];
        }

        public void DisplayBoard()
        {
            Console.WriteLine("Board: ");
            Print2dArray(RawBoard);
        }

        public void GetSolution()
        {
            // Make post request with current board to get solution
            string url = "https://sugoku.herokuapp.com/solve";
            var data = new Dictionary<string, string> { { "board", JsonConvert.SerializeObject(RawBoard) } };
            var temp = new FormUrlEncodedContent(data);
            var solvedResponse = client.PostAsync(url, temp);

            // Parse request data into dynamic dictionary
            var solvedContent = solvedResponse.Result.Content.ReadAsStringAsync();
            var rawSolution = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(solvedContent.Result);

            // Save dictionary values to object values
            Solution = rawSolution["solution"].ToObject<int[,]>();
            Difficulty = rawSolution["difficulty"];
            Status = rawSolution["status"];
        }

        public void DisplaySolution()
        {
            Console.WriteLine("Solution: ");
            Print2dArray(Solution);
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
