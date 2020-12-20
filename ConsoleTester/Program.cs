using Sudoku.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTester
{
    class Program
    {
        static void Main(string[] args)
        {
            Game sudokuGame = new Game(17);

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    Console.WriteLine(sudokuGame.UserBoard[i, j]);
                }
            }



            Console.ReadKey();
        }
    }
}
