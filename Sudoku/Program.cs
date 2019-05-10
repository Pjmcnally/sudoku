using System;

namespace Sudoku
{
    class Program
    {
        static void Main(string[] args)
        {
            SudokuBoard game = new SudokuBoard();
            game.GetBoard(SudokuBoard.Difficulty.random);
            game.DisplayBoard();
            Console.WriteLine();
            game.GetSolution();
            game.DisplaySolution();

            Cell test = new Cell(5, 5, 9);
            Console.WriteLine($"\nCell Created:\nRow: {test.Row}\nCol: {test.Col}\nBox: {test.Box}\nValue: {test.Value}\nSolved: {test.Solved}");
        }
    }
}
