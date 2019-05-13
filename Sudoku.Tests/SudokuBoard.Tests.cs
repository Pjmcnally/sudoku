using System.Collections;
using System.Collections.Generic;
using Xunit;

namespace Sudoku.Tests
{
    // TODO: Write tests for ConvertToArray
    // TODO: Write tests for ConvertFromArray
    // TODO: Write tests for GetBoardFromApi
    public class ConvertFromString
    {
        [Theory]
        // 10x10 board 
        [InlineData("[[0,0,0,0,0,0,0,0,2,0],[0,0,0,0,0,0,0,0,2,0],[0,0,0,4,0,0,7,0,9,0],[4,0,0,0,7,0,1,3,5,0],[0,1,4,0,0,5,0,0,0,0],[0,5,8,0,0,1,2,0,4,0],[7,0,0,0,0,0,0,5,1,0],[6,3,0,5,4,0,0,0,8,0],[0,7,0,9,1,0,0,0,6,0],[0,4,2,6,0,7,5,0,0,0]]")]
        // 8x8 board
        [InlineData("[[0,0,0,0,0,0,0,0],[0,0,0,4,0,0,7,0],[4,0,0,0,7,0,1,3],[0,1,4,0,0,5,0,0],[0,5,8,0,0,1,2,0],[7,0,0,0,0,0,0,5],[6,3,0,5,4,0,0,0],[0,7,0,9,1,0,0,0]]")]
        // 3 dimensional board
        [InlineData("[[[4,2,4],[2,5,6],[7,1,2]],[[4,8,2],[6,0,8],[1,6,5]],[[7,5,1],[4,7,4],[8,7,0]]]")]
        // 1 dimensional board
        [InlineData("[0,0,0,4,0,0,7,0]")]
        public void ArgumentException(string board)
        {
            Assert.Throws<System.ArgumentException>(() => SudokuBoard.ConvertFromString(board));
        }
    }


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

        // TODO: Write tests to compare actual solution
    }
}   