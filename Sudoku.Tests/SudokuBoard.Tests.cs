using System.Collections;
using System.Collections.Generic;
using Xunit;

namespace Sudoku.Tests
{
    public class BackTrackSolve
    {
        [Theory]
        [InlineData(true, "[[0,0,0,0,0,0,0,0,2],[0,0,0,4,0,0,7,0,9],[4,0,0,0,7,0,1,3,5],[0,1,4,0,0,5,0,0,0],[0,5,8,0,0,1,2,0,4],[7,0,0,0,0,0,0,5,1],[6,3,0,5,4,0,0,0,8],[0,7,0,9,1,0,0,0,6],[0,4,2,6,0,7,5,0,0]]")]
        [InlineData(false, "[[1,1,1,1,1,1,1,1,1],[0,0,0,4,0,0,7,0,9],[4,0,0,0,7,0,1,3,5],[0,1,4,0,0,5,0,0,0],[0,5,8,0,0,1,2,0,4],[7,0,0,0,0,0,0,5,1],[6,3,0,5,4,0,0,0,8],[0,7,0,9,1,0,0,0,6],[0,4,2,6,0,7,5,0,0]]")]
        public void ReturnValue(bool expectedReturnValue, string board)
        {
            SudokuBoard testBoard = new SudokuBoard(board);
            Assert.Equal(expectedReturnValue, testBoard.BacktrackSolve());
        }
    }
}   