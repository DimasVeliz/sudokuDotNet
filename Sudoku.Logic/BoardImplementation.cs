using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.Logic
{
    public class Board : IBoard
    {

        private int[,] UserBoard;
        public Board()
        {
            UserBoard = new int[9, 9];
        }

        public int this[int r, int c] { get => UserBoard[r,c]; set => UserBoard[r,c] = value; }

        public int BoardSize { get => 9;  }

        
    }
}
