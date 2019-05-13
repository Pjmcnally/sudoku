using System;
using Newtonsoft.Json;

namespace Sudoku
{
    class Program
    {
        static void Main(string[] args)
        {
            SudokuBoard game = new SudokuBoard(SudokuBoard.Difficulty.random);
            // game.DisplayBoard();
            // Console.WriteLine();
            // game.DisplaySolution();

            game = new SudokuBoard("[[0,0,0,0,0,0,0,0,2],[0,0,0,4,0,0,7,0,9],[4,0,0,0,7,0,1,3,5],[0,1,4,0,0,5,0,0,0],[0,5,8,0,0,1,2,0,4],[7,0,0,0,0,0,0,5,1],[6,3,0,5,4,0,0,0,8],[0,7,0,9,1,0,0,0,6],[0,4,2,6,0,7,5,0,0]]");
            // game.DisplayBoard();
            // Console.WriteLine();
            // game.DisplaySolution();
            game = new SudokuBoard("[[1,1,1,1,1,1,1,1,1],[0,0,0,4,0,0,7,0,9],[4,0,0,0,7,0,1,3,5],[0,1,4,0,0,5,0,0,0],[0,5,8,0,0,1,2,0,4],[7,0,0,0,0,0,0,5,1],[6,3,0,5,4,0,0,0,8],[0,7,0,9,1,0,0,0,6],[0,4,2,6,0,7,5,0,0]]");

            Cell test = new Cell(5, 5, 9);
            Console.WriteLine($"\nCell Created:\nRow: {test.Row}\nCol: {test.Col}\nBox: {test.Box}\nValue: {test.Value}\nSolved: {test.Solved}");
        }
    }
}
