using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.Logic
{
    public interface IBoard
    {
        int this[int r, int c] { get; set; }
        int BoardSize { get; }
        
    }
}
