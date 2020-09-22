using System;
using System.Linq;
using System.Text.Json;

namespace Sudoku.ViewModel.Helpers
{
    public class boardResponse
    {
        public int[][] board { get; set; }
    }

    public class SugokuHelper
    {
        private static string baseUrl = "https://sugoku.herokuapp.com";

        public enum Difficulty
        {
            random,
            easy,
            medium,
            hard
        }

        public static int[,] GetBoard(Difficulty difficulty)
        {
            // Gets raw data
            string url = $"{baseUrl}/board?difficulty={difficulty}";
            var response = App.client.GetStringAsync(url);

            // Deserialize data.
            var board = JsonSerializer.Deserialize<boardResponse>(response.Result);
            return To2D(board.board);
        }

        private static T[,] To2D<T>(T[][] source)
        {
            try
            {
                int FirstDim = source.Length;
                int SecondDim = source.GroupBy(row => row.Length).Single().Key; // throws InvalidOperationException if source is not rectangular

                var result = new T[FirstDim, SecondDim];
                for (int i = 0; i < FirstDim; ++i)
                    for (int j = 0; j < SecondDim; ++j)
                        result[i, j] = source[i][j];

                return result;
            }
            catch (InvalidOperationException)
            {
                throw new InvalidOperationException("The given jagged array is not rectangular.");
            }
        }

        //private int[,] GetSolutionFromApi(int[,] board)
        //{
        //    // Make post request with current board to get solution
        //    string url = $"{baseUrl}/solve";

        //    var data = new Dictionary<string, string> { { "board", JsonConvert.SerializeObject(board) } };
        //    var encodedData = new FormUrlEncodedContent(data);
        //    var content = App.client.PostAsync(url, encodedData).Result.Content.ReadAsStringAsync().Result;

        //    // Deserialize data and save results.
        //    var rawSolution = JsonConvert.DeserializeObject<RawSolution>(content);

        //    return rawSolution;
        //}
    }
}
