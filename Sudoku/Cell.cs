using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku
{
    class Cell
    {
        public int Row { get; }
        public int Col { get; }
        public int Box { get; }
        public int Value { get; private set; }
        public bool Solved { get; private set; }

        public Cell(int row, int col, int box, int value)
        {
            Row = row;
            Col = col;
            Box = box;
            Value = value;

            // If a value is provided at instantiation the cell is already solved.
            if (value > 0)
            {
                this.Solved = true;
            }
        }

        public bool UpdateValue()
        {
            // Increase value to next available value if value exceeds max (9) set value to 0 (unknown)
            Value++;
            if (Value > 9)
            {
                Value = 0; 
            }

            // Return false if 0. True if any other number.
            return Convert.ToBoolean(Value);

        }
    }
}
