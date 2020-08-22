using System;

namespace Sudoku.Model
{
    internal class Cell
    {
        private int _row;

        public int Row
        {
            get { return _row; }
            private set
            {
                if (value < 0 || value > 8)
                {
                    throw new System.ArgumentOutOfRangeException("Row", "Row must be 0-8");
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
                    throw new System.ArgumentOutOfRangeException("Col", "Col must be 0-8");
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
                if (value < 1 || value > 9)
                {
                    throw new System.ArgumentOutOfRangeException("Value", "Value must be integer 1-9");
                }
                _value = value;
            }
        }

        public Board.Region Region { get; }

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

            // Calculate which region the cell is in
            Region = (Board.Region)(col / 3) + ((row / 3) * 3);

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
