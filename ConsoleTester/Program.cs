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

        static void PrintBoard(IBoard board)
        {
            Console.WriteLine("--your status--");
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (board[i,j]!=0)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                       

                    }
                    Console.Write(board[i, j] + " ");
                    Console.ResetColor();
                }
                Console.WriteLine();
            }
            Console.WriteLine("\n--------");
           
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the sudoku Game, enter a number to specify amount of hints");
            int n = int.Parse(Console.ReadLine());
            Game sudokuGame = new Game(n);

            Console.WriteLine("The User Board with {0} amount of hints is",n);
            PrintBoard(sudokuGame.UserBoard);

            Console.WriteLine("This is the solution");
            PrintBoard(sudokuGame.Solution);

            while (true)
            {
                Console.WriteLine("Diga la fila, columna y numero que quiere probar (separados por coma)");
                string[] userInput = Console.ReadLine().Split(',');
                int r = int.Parse(userInput[0]);
                int c = int.Parse(userInput[1]);
                int num = int.Parse(userInput[2]);

                bool wasPosible=sudokuGame.TryUserAttemp(r, c, num);
                if (wasPosible)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Well done");
                    bool isSolved = sudokuGame.IsGameSolved();
                    if (isSolved)
                    {
                        Console.WriteLine("You won the Game");
                        PrintBoard(sudokuGame.Solution);
                        break;
                    }
                    Console.ResetColor();
                }
                else
                {
                    Console.Beep();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid Action\n");
                    Console.ResetColor();
                }
                PrintBoard(sudokuGame.UserBoard);
            }


            
        }
    }
}
