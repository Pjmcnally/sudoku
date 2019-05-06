using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;

namespace Sudoku
{
    class Program
    {
        // Create HttpClient - Instantiate as static so only 1 is created and used for the entire application.
        private static HttpClient client = new HttpClient();

        static void Main(string[] args)
        {   
            // Get Sudoku board of random difficulty
            var randResponse = client.GetStringAsync("https://sugoku.herokuapp.com/board?difficulty=random");
            var rawBoard = JsonConvert.DeserializeObject<RawBoard>(randResponse.Result);

            // Get solved version of random b
            var data = new Dictionary<string, string> { { "board", JsonConvert.SerializeObject(rawBoard.board) } };
            var temp = new FormUrlEncodedContent(data);
            var solvedResponse = client.PostAsync("https://sugoku.herokuapp.com/solve", temp);

            var solvedContent = solvedResponse.Result.Content.ReadAsStringAsync();
            var rawSolution = JsonConvert.DeserializeObject<RawSolution>(solvedContent.Result);

            Console.WriteLine("Board: ");
            Print2dArray((int[,])rawBoard.board);

            Console.WriteLine("\nRaw Solution: ");
            Print2dArray((int[,])rawSolution.Solution);
        }

        static void Print2dArray(int[,] array)
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

        public class RawBoard
        {
            public int[,] board { get; set; }
        }

        public class RawSolution
        {
            public string Difficulty { get; set; }
            public int[,] Solution { get; set; }
            public string Status { get; set; }
        }
    }
}
