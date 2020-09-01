using Sudoku.Model;
using Sudoku.ViewModel.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace Sudoku.ViewModel
{
    public class SudokuVM : INotifyPropertyChanged
    {
        private Board board;

        public Board Board
        {
            get { return board; }
            set
            {
                board = value;
                OnPropertyChanged("Board");
            }
        }

        private int delayPeriod;

        public int DelayPeriod
        {
            get { return delayPeriod; }
            set
            {
                delayPeriod = value;
                OnPropertyChanged("DelayPeriod");
            }
        }

        private string statusMessage;

        public string StatusMessage
        {
            get { return statusMessage; }
            set
            {
                statusMessage = value;
                OnPropertyChanged("StatusMessage");
            }
        }

        private Random random;

        public Random Random
        {
            get { return random; }
            set { random = value; }
        }

        public LoadCommand LoadCommand { get; set; }
        public SolveCommand SolveCommand { get; set; }

        public SudokuVM()
        {
            SolveCommand = new SolveCommand(this);
            LoadCommand = new LoadCommand(this);

            Random = new Random();
            DelayPeriod = 0;
            Board = new Board(GetEmptyBoard());
        }

        public void LoadBoard()
        {
            Board = new Board(GetHardBoard());
            StatusMessage = "Board Loaded";
        }

        public int[,] GetEmptyBoard()
        {
            return new int[,] {
                    { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                    { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                    { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                    { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                    { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                    { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                    { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                    { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                    { 0, 0, 0, 0, 0, 0, 0, 0, 0 }
            };
        }

        public int[,] GetHardBoard()
        {
            List<int[,]> boardList = new List<int[,]> {
                new int[,] {
                    { 8, 0, 0, 0, 0, 0, 0, 0, 0 },
                    { 0, 0, 3, 6, 0, 0, 0, 0, 0 },
                    { 0, 7, 0, 0, 9, 0, 2, 0, 0 },
                    { 0, 5, 0, 0, 0, 7, 0, 0, 0 },
                    { 0, 0, 0, 0, 4, 5, 7, 0, 0 },
                    { 0, 0, 0, 1, 0, 0, 0, 3, 0 },
                    { 0, 0, 1, 0, 0, 0, 0, 6, 8 },
                    { 0, 0, 8, 5, 0, 0, 0, 1, 0 },
                    { 0, 9, 0, 0, 0, 0, 4, 0, 0 }
                },
                new int[,]
                {
                    { 3, 0, 5, 0, 7, 1, 0, 0, 9 },
                    { 0, 0, 0, 3, 4, 0, 0, 0, 0 },
                    { 0, 9, 0, 2, 0, 0, 0, 0, 0 },
                    { 0, 3, 0, 0, 0, 4, 0, 0, 0 },
                    { 0, 6, 0, 0, 0, 0, 0, 0, 7 },
                    { 0, 0, 0, 0, 0, 2, 8, 5, 0 },
                    { 0, 0, 0, 0, 0, 0, 0, 8, 0 },
                    { 0, 5, 4, 0, 0, 0, 9, 0, 1 },
                    { 0, 0, 7, 0, 0, 0, 4, 0, 0 }
                }
            };

            return boardList[Random.Next(boardList.Count)];
        }

        public int[,] GetVeryHardBoard()
        {
            List<int[,]> boardList = new List<int[,]> {
                new int[,]
                {
                    { 0, 0, 0, 8, 0, 1, 0, 0, 0 },
                    { 0, 0, 0, 0, 0, 0, 4, 3, 0 },
                    { 5, 0, 0, 0, 0, 0, 0, 0, 0 },
                    { 0, 0, 0, 0, 7, 0, 8, 0, 0 },
                    { 0, 0, 0, 0, 0, 0, 1, 0, 0 },
                    { 0, 2, 0, 0, 3, 0, 0, 0, 0 },
                    { 6, 0, 0, 0, 0, 0, 0, 7, 5 },
                    { 0, 0, 3, 4, 0, 0, 0, 0, 0 },
                    { 0, 0, 0, 2, 0, 0, 6, 0, 0 }
                }
            };

            return boardList[Random.Next(boardList.Count)];
        }

        public async void SolveBoard()
        {
            await Task.Run(() =>
            {
                // Method to run backtrack solve algorithm on the board.
                StatusMessage = "Solving Board";
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();

                List<Cell> unsolvedCells = Board.Cells.FindAll(x => x.Solved == false);

                int i = 0;
                while (i >= 0 && i < unsolvedCells.Count)
                {
                    Cell currentCell = unsolvedCells[i];
                    while (currentCell.UpdateValue() && !(currentCell.IsValid()))
                    {
                        if (DelayPeriod > 0)
                        {
                            Thread.Sleep(DelayPeriod);
                        }
                    }

                    if (currentCell.Value == 0)
                    {
                        i -= 1;
                    }
                    else
                    {
                        i += 1;
                    }
                }

                stopwatch.Stop();
                TimeSpan ts = stopwatch.Elapsed;
                string durationString = $"{ts.Hours:00}:{ts.Minutes:00}:{ts.Seconds:00}.{ts.Milliseconds:000}";
                if (i == unsolvedCells.Count)
                {
                    StatusMessage = $"Board has been solved\r\n{durationString}";
                    return true;  // board is solved
                }
                else
                {
                    StatusMessage = "Board cannot be solved\r\n{durationString}";
                    return false;  // board is unsolvable
                }
            });
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
