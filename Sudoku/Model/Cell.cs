using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Sudoku.Model
{
    public class Cell : INotifyPropertyChanged
    {
        // TODO: Add property to track possible guesses (HashSet)
        // TODO: Add property to track peers
        // TODO: Add reset method
        // TODO: Add method for constraint propagation
        // TODO: Add method to update peers

        private int row;

        public int Row
        {
            get { return row; }
            set
            {
                if (value < 0 || value > 8)
                {
                    throw new System.ArgumentOutOfRangeException("Row", "Row must be 0-8");
                }
                row = value;
                OnPropertyChanged("Row");
            }
        }

        private int col;

        public int Col
        {
            get { return col; }
            set
            {
                if (value < 0 || value > 8)
                {
                    throw new System.ArgumentOutOfRangeException("Col", "Col must be 0-8");
                }
                col = value;
                OnPropertyChanged("Col");
            }
        }

        private int value;

        public int Value
        {
            get { return value; }
            set
            {
                if (value < 0 || value > 9)
                {
                    throw new System.ArgumentOutOfRangeException("Value", "Value must be integer 1-9");
                }
                this.value = value;
                OnPropertyChanged("Value");
            }
        }

        private Board.Region region;

        public Board.Region Region
        {
            get { return region; }
            set
            {
                region = value;
                OnPropertyChanged("Region");
            }
        }

        private bool solved;

        public bool Solved
        {
            get { return solved; }
            set
            {
                solved = value;
                OnPropertyChanged("Solved");
            }
        }

        private List<Cell> peers;

        public List<Cell> Peers
        {
            get { return peers; }
            set { peers = value; }
        }

        public Cell(int row, int col, int value)
        {
            Col = col;
            Row = row;
            Value = value;

            // Calculate which region the cell is in
            Region = (Board.Region)(col / 3) + ((row / 3) * 3);

            // If a value is provided at instantiation the cell is already solved.
            if (value > 0)
            {
                Solved = true;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public bool UpdateValue()
        {
            // Increase value to next available value if value exceeds max (9) set value to 0 (unknown)
            Value = (Value + 1) % 10;

            // Return true if any number other than 0. Otherwise return false.
            return Convert.ToBoolean(Value);
        }

        public bool IsValid()
        {
            return !(this.Peers.Select(o => o.Value).ToHashSet().Contains(this.value));
        }
    }
}
