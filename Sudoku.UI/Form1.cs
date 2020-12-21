using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Sudoku.Logic;


namespace Sudoku.UI
{
    public partial class Form1 : Form
    {
        Game sudokuGame;
        public bool UserWantsSolution { get; set; }
        public Form1()
        {
            InitializeComponent();
            sudokuGame = new Game(17);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnStartGame_Click(object sender, EventArgs e)
        {
            sudokuGame = new Game((int)numUpDwHints.Value);
            
            pictureBox1.Refresh();
        }

        private void DrawBoard(Graphics g)
        {
            
            int numOfCells = sudokuGame.GameSize;
            int cellSize = 40;
            Pen p = new Pen(Color.Black);

            for (int y = 0; y < numOfCells; ++y)
            {
                g.DrawLine(p, 0, y * cellSize, numOfCells * cellSize, y * cellSize);
            }

            for (int x = 0; x < numOfCells; ++x)
            {
                g.DrawLine(p, x * cellSize, 0, x * cellSize, numOfCells * cellSize);
            }
        }

        private void UpdateNumbers(Graphics g, bool solution)
        {
            Brush brush = solution?Brushes.Blue:Brushes.Black;
            int numOfCells = sudokuGame.GameSize;
            int cellSize = 40;
            int stride = 10;
            Pen p = new Pen(Color.Black);
            for (int i = 0; i < sudokuGame.GameSize; i++)
            {
                for (int j = 0; j < sudokuGame.GameSize; j++)
                {
                    string valueStr = sudokuGame.UserBoard[i, j]!=0? sudokuGame.UserBoard[i, j].ToString() : " ";
                    Font font = new Font("Arial", 12, FontStyle.Bold);
                    g.DrawString(valueStr, font,brush, new Point(i*cellSize+stride,j*cellSize+stride));
                }
            }
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            DrawBoard(e.Graphics);
            UpdateNumbers(e.Graphics, this.UserWantsSolution);
        }

        private void btnSolve_Click(object sender, EventArgs e)
        {
            if (!sudokuGame.IsSolved)
            {
                this.UserWantsSolution = true;
                sudokuGame.SolvePlease();
                pictureBox1.Refresh();
            }
            else
            {
                MessageBox.Show("The Game is already solved");
            }
            
        }

        private void btnHint_Click(object sender, EventArgs e)
        {
            if (!sudokuGame.IsSolved)
            {
                bool isThereHint= sudokuGame.GiveHint();
                if (!isThereHint)
                {
                    MessageBox.Show("The game was solved with the last hint");
                }
                else
                {
                    MessageBox.Show("Hint given!");
                }
                pictureBox1.Refresh();
            }

            else
            {
                MessageBox.Show("The Game is already solved");
            }
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            int xCoordinate = e.X;
            int yCoordinate = e.Y;

        }
    }
}
