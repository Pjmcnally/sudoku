using System;
using System.Windows.Input;

namespace Sudoku.ViewModel.Commands
{
    public class SolveCommand : ICommand
    {
        public SudokuVM VM { get; set; }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public SolveCommand(SudokuVM vm)
        {
            VM = vm;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            VM.SolveBoard();
        }
    }
}
