using Xunit;

namespace Sudoku.Tests
{
    public class CellRow
    {
        [Theory]
        [InlineData(-1)]
        [InlineData(9)]
        // Row values may not be below 0 or above 8.
        public void Invalid(int row)
        {
            int col = 0;
            int val = 0;
            Assert.Throws<System.ArgumentException>(() => new Cell(row, col, val));
        }
    }

    public class CellCol
    {
        [Theory]
        [InlineData(-1)]
        [InlineData(9)]
        // Column values may not be below 0 or above 8.
        public void Invalid(int col)
        {
            int row = 0;
            int val = 0;
            Assert.Throws<System.ArgumentException>(() => new Cell(row, col, val));
        }
    }
    public class CellVal
    {
        [Theory]
        [InlineData(-1)]
        [InlineData(10)]
        // Value values may not be below 0 or above 9.
        public void Invalid(int val)
        {
            int row = 0;
            int col = 0;
            Assert.Throws<System.ArgumentException>(() => new Cell(row, col, val));
        }

        [Theory]
        [InlineData(0)]
        // Value of 0 means cell is not solved.
        public void UnSolved(int val)
        {
            int row = 0;
            int col = 0;
            Cell cell = new Cell(row, col, val);
            Assert.False(cell.Solved);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        [InlineData(6)]
        [InlineData(7)]
        [InlineData(8)]
        [InlineData(9)]
        // Value of 1-9 means cell is solved.
        public void Solved(int val)
        {
            int row = 0;
            int col = 0;
            Cell cell = new Cell(row, col, val);
            Assert.True(cell.Solved);
        }
    }

    public class CellBox
    {
        [Theory]
        [InlineData(0, 0, 0)]
        [InlineData(0, 1, 0)]
        [InlineData(0, 2, 0)]
        [InlineData(0, 3, 1)]
        [InlineData(0, 4, 1)]
        [InlineData(0, 5, 1)]
        [InlineData(0, 6, 2)]
        [InlineData(0, 7, 2)]
        [InlineData(0, 8, 2)]
        [InlineData(1, 0, 0)]
        [InlineData(1, 1, 0)]
        [InlineData(1, 2, 0)]
        [InlineData(1, 3, 1)]
        [InlineData(1, 4, 1)]
        [InlineData(1, 5, 1)]
        [InlineData(1, 6, 2)]
        [InlineData(1, 7, 2)]
        [InlineData(1, 8, 2)]
        [InlineData(2, 0, 0)]
        [InlineData(2, 1, 0)]
        [InlineData(2, 2, 0)]
        [InlineData(2, 3, 1)]
        [InlineData(2, 4, 1)]
        [InlineData(2, 5, 1)]
        [InlineData(2, 6, 2)]
        [InlineData(2, 7, 2)]
        [InlineData(2, 8, 2)]
        [InlineData(3, 0, 3)]
        [InlineData(3, 1, 3)]
        [InlineData(3, 2, 3)]
        [InlineData(3, 3, 4)]
        [InlineData(3, 4, 4)]
        [InlineData(3, 5, 4)]
        [InlineData(3, 6, 5)]
        [InlineData(3, 7, 5)]
        [InlineData(3, 8, 5)]
        [InlineData(4, 0, 3)]
        [InlineData(4, 1, 3)]
        [InlineData(4, 2, 3)]
        [InlineData(4, 3, 4)]
        [InlineData(4, 4, 4)]
        [InlineData(4, 5, 4)]
        [InlineData(4, 6, 5)]
        [InlineData(4, 7, 5)]
        [InlineData(4, 8, 5)]
        [InlineData(5, 0, 3)]
        [InlineData(5, 1, 3)]
        [InlineData(5, 2, 3)]
        [InlineData(5, 3, 4)]
        [InlineData(5, 4, 4)]
        [InlineData(5, 5, 4)]
        [InlineData(5, 6, 5)]
        [InlineData(5, 7, 5)]
        [InlineData(5, 8, 5)]
        [InlineData(6, 0, 6)]
        [InlineData(6, 1, 6)]
        [InlineData(6, 2, 6)]
        [InlineData(6, 3, 7)]
        [InlineData(6, 4, 7)]
        [InlineData(6, 5, 7)]
        [InlineData(6, 6, 8)]
        [InlineData(6, 7, 8)]
        [InlineData(6, 8, 8)]
        [InlineData(7, 0, 6)]
        [InlineData(7, 1, 6)]
        [InlineData(7, 2, 6)]
        [InlineData(7, 3, 7)]
        [InlineData(7, 4, 7)]
        [InlineData(7, 5, 7)]
        [InlineData(7, 6, 8)]
        [InlineData(7, 7, 8)]
        [InlineData(7, 8, 8)]
        [InlineData(8, 0, 6)]
        [InlineData(8, 1, 6)]
        [InlineData(8, 2, 6)]
        [InlineData(8, 3, 7)]
        [InlineData(8, 4, 7)]
        [InlineData(8, 5, 7)]
        [InlineData(8, 6, 8)]
        [InlineData(8, 7, 8)]
        [InlineData(8, 8, 8)]
        public void Calculate(int row, int col, int box)
        {
            int val = 0;
            Cell cell = new Cell(row, col, val);
            Assert.Equal(cell.Box, box);
        }
    }
}