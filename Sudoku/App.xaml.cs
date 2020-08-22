using System.Net.Http;
using System.Windows;

namespace Sudoku
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        // Create HttpClient - Instantiate as static so only 1 is created and used for the entire application.
        public static HttpClient client = new HttpClient();
    }
}
