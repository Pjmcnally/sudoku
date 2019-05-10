using System;

namespace Sudoku
{
    class Cell
    {
        public int Row { get; }
        public int Col { get; }
        public int Box { get; private set; }
        public int Value { get; private set; }
        public bool Solved { get; private set; }

        // TODO: Add property to track possible guesses (HashSet)
        // TODO: Add property to track peers
        // TODO: Add reset method
        // TODO: Add method for constraint propagation
        // TODO: Add method to update peers

        public Cell(int row, int col, int value)
        {
            // Check for invalid values and throw exception.
            if (row < 0 || row > 8)
            {
                throw new System.ArgumentException("Parameter must be integer 0 - 8", "Row");
            }
            else if (col < 0 || col > 8)
            {
                throw new System.ArgumentException("Parameter must be integer 0 - 8", "Col");
            }
            else if (value < 0 || value > 9)
            {
                throw new System.ArgumentException("Parameter must be integer 0 - 9", "Value");
            }

            // Save values 
            Row = row;
            Col = col;
            Value = value;

            // If a value is provided at instantiation the cell is already solved.
            if (value > 0)
            {
                Solved = true;
            }

            // Calculate which 3x3 box the cell is in (listed from 0-8)
            // Where each number below represents a 3x3 group of cells.
            // 0 1 2
            // 3 4 5
            // 6 7 8
            Box = (Row / 3) + ((Col / 3) * 3);
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
