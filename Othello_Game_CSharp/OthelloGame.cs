using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Othello_Game_CSharp
{
    public class OthelloGame
    {
        private readonly string r_Player1;
        private readonly string r_Player2;
        private readonly bool r_AgainstComputer;
        private readonly int r_BoardSize;
        private eCellState[,] m_Board;
        private eCellState m_CurrentPlayer;

        public OthelloGame(string i_Player1, string i_Player2, bool i_AgainstComputer, int i_Size)
        {
            this.Board = new eCellState[i_Size, i_Size];

            for (int i = 0; i < i_Size; i++)
            {
                for (int j = 0; j < i_Size; j++)
                {
                    this.m_Board[i, j] = eCellState.Empty;
                }
            }

            int middle = i_Size / 2;
            this.Board[middle - 1, middle - 1] = eCellState.White;
            this.Board[middle, middle] = eCellState.White;
            this.Board[middle - 1, middle] = eCellState.Black;
            this.Board[middle, middle - 1] = eCellState.Black;
            this.CurrentPlayer = eCellState.Black;

            this.r_Player1 = i_Player1;
            this.r_Player2 = i_Player2;
            this.r_AgainstComputer = i_AgainstComputer;
            this.r_BoardSize = i_Size;
        }

        public eCellState[,] Board
        {
            get { return this.m_Board; }
            set { this.m_Board = value; }
        }

        public eCellState CurrentPlayer
        {
            get { return this.m_CurrentPlayer; }
            set { this.m_CurrentPlayer = value; }
        }

        public string Player1
        {
            get { return this.r_Player1; }
        }

        public string Player2
        {
            get { return this.r_Player2; }
        }

        public bool AgainstComputer
        {
            get { return this.r_AgainstComputer; }
        }

        public int BoardSize
        {
            get { return this.r_BoardSize; }
        }

        public eCellState GetCurrentPlayer()
        {
            return this.CurrentPlayer;
        }

        public string GetCurrentPlayerName()
        {
            return this.CurrentPlayer == eCellState.Black ? this.r_Player1 : this.r_Player2;
        }

        public int GetScore(eCellState i_Player)
        {
            int score = 0;

            for (int i = 0; i < this.r_BoardSize; i++)
            {
                for (int j = 0; j < this.r_BoardSize; j++)
                {
                    if (this.m_Board[i, j] == i_Player)
                    {
                        score++;
                    }
                }
            }

            return score;
        }

        public bool MakeMove(Move i_Move)
        {
            bool validMove = false;

            if (this.m_Board[i_Move.Row, i_Move.Column] != eCellState.Empty)
            {
                validMove = false;
            }
            else
            {
                for (int i = -1; i <= 1; i++)
                {
                    for (int j = -1; j <= 1; j++)
                    {
                        if (i == 0 && j == 0)
                        {
                            continue;
                        }

                        int row = i_Move.Row + i;
                        int col = i_Move.Column + j;

                        while (row >= 0 && row < this.r_BoardSize && col >= 0 && col < this.r_BoardSize && this.m_Board[row, col] == this.GetOpponent(this.CurrentPlayer))
                        {
                            row += i;
                            col += j;
                        }

                        if (row >= 0 && row < this.r_BoardSize && col >= 0 && col < this.r_BoardSize && this.m_Board[row, col] == this.CurrentPlayer)
                        {
                            validMove = true;

                            row -= i;
                            col -= j;

                            while (!(row == i_Move.Row && col == i_Move.Column))
                            {
                                this.m_Board[row, col] = this.CurrentPlayer;
                                row -= i;
                                col -= j;
                            }
                        }
                    }
                }
            }

            if (validMove)
            {
                this.m_Board[i_Move.Row, i_Move.Column] = this.CurrentPlayer;
                this.CurrentPlayer = this.GetOpponent(this.CurrentPlayer);
            }

            return validMove;
        }

        public eCellState GetWinner()
        {
            int black = 0;
            int white = 0;
            eCellState winner = eCellState.Empty;

            for (int i = 0; i < this.r_BoardSize; i++)
            {
                for (int j = 0; j < this.r_BoardSize; j++)
                {
                    if (this.m_Board[i, j] == eCellState.Black)
                    {
                        black++;
                    }
                    else if (this.m_Board[i, j] == eCellState.White)
                    {
                        white++;
                    }
                }
            }

            if (black == white)
            {
                winner = eCellState.Empty;
            }
            else if (black > white)
            {
                winner = eCellState.Black;
            }
            else
            {
                winner = eCellState.White;
            }

            return winner;
        }

        public bool IsGameOver()
        {
            bool gameOver;

            if (this.GetValidMoves().Any())
            {
                gameOver = false;
            }
            else
            {
                this.CurrentPlayer = this.GetOpponent(this.CurrentPlayer);

                gameOver = this.GetValidMoves().Any() ? false : true;
            }

            return gameOver;
        }

        public Move[] GetValidMoves()
        {
            List<Move> validMoves = new List<Move>();

            for (int i = 0; i < this.r_BoardSize; i++)
            {
                for (int j = 0; j < this.r_BoardSize; j++)
                {
                    if (this.m_Board[i, j] == eCellState.Empty)
                    {
                        bool validMove = false;
                        for (int k = -1; k <= 1; k++)
                        {
                            for (int l = -1; l <= 1; l++)
                            {
                                if (k == 0 && l == 0)
                                {
                                    continue;
                                }

                                int row = i + k;
                                int col = j + l;

                                while (row >= 0 && row < this.r_BoardSize && col >= 0 && col < this.r_BoardSize && this.m_Board[row, col] == this.GetOpponent(this.CurrentPlayer))
                                {
                                    row += k;
                                    col += l;
                                }

                                if (row >= 0 && row < this.r_BoardSize && col >= 0 && col < this.r_BoardSize && this.m_Board[row, col] == this.CurrentPlayer)
                                {
                                    validMove = true;
                                    break;
                                }
                            }

                            if (validMove)
                            {
                                break;
                            }
                        }

                        if (validMove)
                        {
                            validMoves.Add(new Move(i, j));
                        }
                    }
                }
            }

            return validMoves.ToArray();
        }

        public eCellState GetOpponent(eCellState i_Player)
        {
            return i_Player == eCellState.Black ? eCellState.White : eCellState.Black;
        }

        public void MakeComputerMove()
        {
            Move[] validMoves = this.GetValidMoves();
            Move move = validMoves[new Random().Next(validMoves.Length)];

            this.MakeMove(move);
        }
    }
}
