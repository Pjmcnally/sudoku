using System;

namespace Sudoku
{
    public class Cell
    {
        private int _row;
        public int Row {
            get { return _row; }
            private set
            {
                if (value < 0 || value > 8)
                {
                    throw new System.ArgumentException("Parameter must be integer 0 - 8", "Row");
                }
                _row = value;
            }
        }

        private int _col;
        public int Col
        {
            get { return _col; }
            private set
            {
                if (value < 0 || value > 8)
                {
                    throw new System.ArgumentException("Parameter must be integer 0 - 8", "Col");
                }
                _col = value;
            }
        }

        private int _value;
        public int Value
        {
            get { return _value; }
            private set
            {
                if (value < 0 || value > 9)
                {
                    throw new System.ArgumentException("Parameter must be integer 0 - 9", "Value");
                }
                _value = value;
            }
        }

        public int Box { get; }
        
        public bool Solved { get; private set; }

        // TODO: Add property to track possible guesses (HashSet)
        // TODO: Add property to track peers
        // TODO: Add reset method
        // TODO: Add method for constraint propagation
        // TODO: Add method to update peers

        public Cell(int row, int col, int value)
        {
            Col = col;
            Row = row;
            Value = value;

            // Calculate which 3x3 box the cell is in (listed from 0-8)
            // Where each number below represents a 3x3 group of cells.
            // 0 1 2
            // 3 4 5
            // 6 7 8
            Box = (col / 3) + ((row / 3) * 3);

            // If a value is provided at instantiation the cell is already solved.
            if (value > 0)
            {
                Solved = true;
            }
        }

        public bool UpdateValue()
        {
            // Increase value to next available value if value exceeds max (9) set value to 0 (unknown)
            Value = (Value + 1) % 10;

            // Return true if any number other than 0. Otherwise return false.
            return Convert.ToBoolean(Value);
        }
    }
}
