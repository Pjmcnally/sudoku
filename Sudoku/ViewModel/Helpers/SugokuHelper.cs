namespace Sudoku.ViewModel.Helpers
{
    public class SugokuHelper
    {
        //private string baseUrl = "https://sugoku.herokuapp.com";

        //public enum Difficulty
        //{
        //    random,
        //    easy,
        //    medium,
        //    hard
        //}

        //private int[,] GetBoard(Difficulty difficulty)
        //{
        //    // Gets raw data
        //    string url = $"{baseUrl}/board?difficulty={difficulty}";
        //    var response = App.client.GetStringAsync(url);

        //    // Deserialize data.
        //    var jsonSchema = new { board = new int[9, 9] };
        //    var content = JsonConvert.DeserializeAnonymousType(response.Result, jsonSchema);
        //    return content.board;
        //}

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
