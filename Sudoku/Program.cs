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
        static void Main(string[] args)
        {
            Sudoku game = new Sudoku();
            game.GetBoard(Sudoku.Difficulty.random);
            game.DisplayBoard();
            game.GetSolution();
            game.DisplaySolution();
        }
    }
}
