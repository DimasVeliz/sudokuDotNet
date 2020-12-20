using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sudoku.Logic
{
    

    public class Game
    {
        private Board InternalBoard {  get;  set; }
        public Board UserBoard { get; private set; }
        public Game(int amountOfHints)
        {
            InternalBoard = new Board();
            UserBoard = new Board();
            CreateValidBoard();
            AmountOfHints(amountOfHints);
        }

        private bool CheckCrash(IBoard board,int row, int col, int num)
        {

            // Check if we find the same num
            // in the similar row , we
            // return false
            for (int x = 0; x <= 8; x++)
                if (board[row, x] == num)
                    return false;

            // Check if we find the same num
            // in the similar column ,
            // we return false
            for (int x = 0; x <= 8; x++)
                if (board[x, col] == num)
                    return false;

            // Check if we find the same num
            // in the particular 3*3
            // UserBoard, we return false
            int startRow = row - row % 3, startCol
                                          = col - col % 3;
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    if (board[i + startRow, j + startCol] == num)
                        return false;

            return true;
        }

        public bool TryUserAttemp(int r, int c, int num)
        {
            if (num<=0 || num>9)
            {
                return false;
            }

            if (r < 0 || r > 9)
            {
                return false;
            }

            if (c < 0 || c > 9)
            {
                return false;

            }

            bool wereNoCrash = CheckCrash(this.UserBoard,r, c, num);
            if (wereNoCrash)
            {
                UserBoard[r, c] = num;
                return true;
            }
            return false;
        }

        private bool SolveSudoku(bool randomize = false)
        {
            int[] arr = { 9, 2, 3, 4, 5, 6, 7, 8, 1 };
            if (randomize)
            {
               arr = arr.ToList().Shuffle().ToArray();

            }
            return SolveSudokuHelper( 0, 0, arr);
        }

        private bool SolveSudokuHelper( int i, int j, int[] numbers)
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
            if (InternalBoard[i, j] != 0)
                return SolveSudokuHelper( i, j + 1, numbers);


            for (int k = 0; k < numbers.Length; k++)
            {
                int num = numbers[k];
                bool nohayCrash = CheckCrash(this.InternalBoard, i, j, num);
                if (nohayCrash)
                {
                    InternalBoard[i, j] = num;
                    bool foundSolution = SolveSudokuHelper(i, j + 1, numbers);
                    if (foundSolution)
                    {
                        return true;
                    }

                }
                InternalBoard[i, j] = 0;

            }
            return false;


        }

        public bool IsGameSolved()
        {
            for (int i = 0; i < InternalBoard.BoardSize; i++)
            {
                for (int j = 0; j < InternalBoard.BoardSize; j++)
                {
                    if (UserBoard[i,j] != InternalBoard[i,j])
                        return false;
                }
            }
            return true;
        }

        private void CreateValidBoard()
        {
            SolveSudoku(true);
        }

        private void AmountOfHints(int n)
        {
            List<Pair> availablePositions = new List<Pair>();
            for (int i = 0; i < InternalBoard.BoardSize; i++)
            {
                for (int j = 0; j < InternalBoard.BoardSize; j++)
                {
                    availablePositions.Add(new Pair() { X = i, Y = j });
                }
            }

            for (int k = 0; k < n; k++)
            {
                int randomPos = ThreadSafeRandom.ThisThreadsRandom.Next(availablePositions.Count);
                var selectedPair = availablePositions[randomPos];
                UserBoard[selectedPair.X, selectedPair.Y] = InternalBoard[selectedPair.X, selectedPair.Y];
                availablePositions.RemoveAt(randomPos);
            }
        }


        public Board Solution { get => InternalBoard; }
        

    }
}
