using System;
using System.Collections.Generic;
using System.Threading;
using System.Linq;

namespace sudoku
{
    public static class ThreadSafeRandom
  {
      [ThreadStatic] private static Random Local;

      public static Random ThisThreadsRandom
      {
          get { return Local ?? (Local = new Random(unchecked(Environment.TickCount * 31 + Thread.CurrentThread.ManagedThreadId))); }
      }
  }

  static class MyExtensions
  {
    public static IEnumerable<T> Shuffle<T>(this IList<T> list)
    {
      int n = list.Count;
      while (n > 1)
      {
        n--;
        int k = ThreadSafeRandom.ThisThreadsRandom.Next(n + 1);
        T value = list[k];
        list[k] = list[n];
        list[n] = value;
      }
      return list;
    }
  }

    class Program
    {

        public static bool SolveSudoku(int[,] UserBoard, bool randomize = false)
        {
            int[] arr = { 9, 2, 3, 4, 5, 6, 7, 8, 1 };
            var numbers =arr.ToList().Shuffle().ToArray();
            return SolveSudokuHelper(UserBoard, 0, 0, numbers);
        }

        private static bool SolveSudokuHelper(int[,] UserBoard, int i, int j, int[] numbers)
        {
            
            if (i == 8 && j == 9)
            {

                return true;
            }

            if (j == 9)
            {
                i++;
                j = 0;
            }
            if (UserBoard[i, j] != 0)
                return SolveSudokuHelper(UserBoard, i, j + 1, numbers);


            for (int k = 0; k < numbers.Length; k++)
            {
                int num = numbers[k];
                bool nohayCrash = CheckCrash(UserBoard, i, j, num);
                if (nohayCrash)
                {
                    UserBoard[i, j] = num;
                    bool foundSolution = SolveSudokuHelper(UserBoard, i, j + 1, numbers);
                    if (foundSolution)
                    {
                        return true;
                    }

                }
                UserBoard[i, j] = 0;

            }
            return false;


        }

        private static void PrintBoard(int[,] UserBoard)
        {
            for (int i = 0; i < UserBoard.GetLength(0); i++)
            {
                for (int j = 0; j < UserBoard.GetLength(1); j++)
                {
                    System.Console.Write(UserBoard[i, j] + " ");
                }
                System.Console.WriteLine();
            }

            Console.WriteLine("\n---------------\n");
        }

        static bool CheckCrash(int[,] grid, int row, int col, int num)
        {

            // Check if we find the same num
            // in the similar row , we
            // return false
            for (int x = 0; x <= 8; x++)
                if (grid[row, x] == num)
                    return false;

            // Check if we find the same num
            // in the similar column ,
            // we return false
            for (int x = 0; x <= 8; x++)
                if (grid[x, col] == num)
                    return false;

            // Check if we find the same num
            // in the particular 3*3
            // UserBoard, we return false
            int startRow = row - row % 3, startCol
                                          = col - col % 3;
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    if (grid[i + startRow, j + startCol] == num)
                        return false;

            return true;
        }

        static void Main(string[] args)
        {
            int[,] UserBoard =  {
                         { 3, 0, 6, 5, 0, 8, 4, 0, 0 },
                         { 5, 2, 0, 0, 0, 0, 0, 0, 0 },
                         { 0, 8, 7, 0, 0, 0, 0, 3, 1 },
                         { 0, 0, 3, 0, 1, 0, 0, 8, 0 },
                         { 9, 0, 0, 8, 6, 3, 0, 0, 5 },
                         { 0, 5, 0, 0, 9, 0, 6, 0, 0 },
                         { 1, 3, 0, 0, 0, 0, 2, 5, 0 },
                         { 0, 0, 0, 0, 0, 0, 0, 7, 4 },
                         { 0, 0, 5, 2, 0, 6, 3, 0, 0 }
            };
            UserBoard = new int[9, 9];
            if (SolveSudoku(UserBoard))
            {
                PrintBoard(UserBoard);
            }
        }
    }
}
