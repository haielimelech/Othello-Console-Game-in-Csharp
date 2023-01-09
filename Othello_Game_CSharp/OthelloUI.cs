using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Othello_Game_CSharp
{
    public class OthelloUI
    {
        private OthelloGame m_OthelloGame;

        public OthelloUI()
        {
            string player1 = this.AskForPlayerName();
            bool isAgainstComputer = this.AskIfAgainstComputer();
            string player2 = "Computer";

            if (!isAgainstComputer)
            {
                player2 = this.AskForSecondPlayerName();
            }

            int boardSize = this.AskForBoardSize();

            this.m_OthelloGame = new OthelloGame(player1, player2, isAgainstComputer, boardSize);
        }

        public string AskForPlayerName()
        {
            Console.WriteLine("Enter your name:");
            string name = Console.ReadLine();
            return name;
        }

        public string AskForSecondPlayerName()
        {
            Console.WriteLine("Enter the second player's name:");
            string name = Console.ReadLine();
            return name;
        }

        public int AskForBoardSize()
        {
            Console.WriteLine("Enter the size of the board (6 or 8):");
            int size = int.Parse(Console.ReadLine());

            while (size != 6 && size != 8)
            {
                Console.WriteLine("Invalid size. Enter 6 or 8:");
                size = int.Parse(Console.ReadLine());
            }

            return size;
        }

        public bool AskIfAgainstComputer()
        {
            Console.WriteLine("Do you want to play against the computer? (Y/N)");
            string answer = Console.ReadLine();

            while (answer.ToUpper() != "Y" && answer.ToUpper() != "N")
            {
                Console.WriteLine("Invalid input. Please enter Y or N.");
                answer = Console.ReadLine();
            }

            return answer.ToUpper() == "Y";
        }

        public void AskIfPlayerWantsToPlayAgain()
        {
            Console.Write($"{Environment.NewLine}Do you want to play again? (Y/N):");
            string answer = Console.ReadLine();

            while (answer.ToUpper() != "Y" && answer.ToUpper() != "N")
            {
                Console.WriteLine("Invalid input. Please enter Y or N.");
                answer = Console.ReadLine();
            }

            if (answer.ToUpper() == "Y")
            {
                this.m_OthelloGame = new OthelloGame(this.m_OthelloGame.Player1, this.m_OthelloGame.Player2, this.m_OthelloGame.AgainstComputer, this.m_OthelloGame.BoardSize);
                this.Start();
            }
            else
            {
                Console.WriteLine("Thanks for playing!");
            }
        }

        public StringBuilder AskForAMoveOrQuit()
        {
            Console.WriteLine($"It's {this.m_OthelloGame.GetCurrentPlayerName()}'s turn. Enter a row and column number to place a piece: (Q to Quit)");
            StringBuilder input = new StringBuilder();
            input.Append(Console.ReadLine());

            if (this.m_OthelloGame.BoardSize == 6)
            {
                while (input.Length != 2 || (char.ToUpper(input[0]) < 'A' || char.ToUpper(input[0]) > 'F') || (input[1] < '1' || input[1] > '6'))
                {
                    this.CheckForQuit(input);
                    Console.WriteLine("Invalid input. Row should be 1-6, Column should be A-F: (Q to Quit)");
                    input.Clear();
                    input.Append(Console.ReadLine());
                }

                input[0] = char.ToUpper(input[0]);
            }
            else
            {
                while (input.Length != 2 || (char.ToUpper(input[0]) < 'A' || char.ToUpper(input[0]) > 'H') || (input[1] < '1' || input[1] > '8'))
                {
                    this.CheckForQuit(input);
                    Console.WriteLine("Invalid input. Row should be 1-8, Column should be A-H: (Q to Quit)");
                    input.Clear();
                    input.Append(Console.ReadLine());
                }

                input[0] = char.ToUpper(input[0]);
            }

            return input;
        }

        public void CheckForQuit(StringBuilder i_Input)
        {
            if (i_Input.ToString().Equals("Q") || i_Input.ToString().Equals("q"))
            {
                Console.WriteLine("Game is over. Thanks for playing!");
                Environment.Exit(0);
            }
        }

        public void Start()
        {
            this.GreetPlayers();
            Console.WriteLine($"{this.m_OthelloGame.Player1} : {this.m_OthelloGame.GetScore(eCellState.Black)}      {this.m_OthelloGame.Player2} : {this.m_OthelloGame.GetScore(eCellState.White)}");
            this.ShowBoard();

            while (true)
            {
                if (this.m_OthelloGame.GetCurrentPlayer() != eCellState.Black && this.m_OthelloGame.AgainstComputer)
                {
                    Console.Write("Computer is making a move");
                    Thread.Sleep(700);
                    Console.Write(".");
                    Thread.Sleep(700);
                    Console.Write(" .");
                    Thread.Sleep(700);
                    Console.Write(" .");
                    Thread.Sleep(700);
                    this.m_OthelloGame.MakeComputerMove();
                    Console.Clear();
                    Console.WriteLine($"{this.m_OthelloGame.Player1} : {this.m_OthelloGame.GetScore(eCellState.Black)}      {this.m_OthelloGame.Player2} : {this.m_OthelloGame.GetScore(eCellState.White)}");
                    this.ShowBoard();
                    Console.WriteLine("Computer made a move.");
                }
                else
                {
                    StringBuilder newMove = this.AskForAMoveOrQuit();
                    int row = int.Parse(newMove[1].ToString()) - 1;
                    int column = newMove[0] - 'A';
                    bool validMove = this.m_OthelloGame.MakeMove(new Move(row, column));

                    if (!validMove)
                    {
                        Console.WriteLine("Invalid move. Try again.");
                        continue;
                    }

                    Console.Clear();
                    Console.WriteLine($"{this.m_OthelloGame.Player1} : {this.m_OthelloGame.GetScore(eCellState.Black)}      {this.m_OthelloGame.Player2} : {this.m_OthelloGame.GetScore(eCellState.White)}");
                    this.ShowBoard();
                }

                if (this.m_OthelloGame.IsGameOver())
                {
                    eCellState winner = this.m_OthelloGame.GetWinner();

                    if (winner == eCellState.Empty)
                    {
                        Console.WriteLine($"{Environment.NewLine}The game is over. It's a tie.");
                    }
                    else
                    {
                        string winnerName = winner == eCellState.Black ? this.m_OthelloGame.Player1 : this.m_OthelloGame.Player2;
                        Console.WriteLine($"The game is over. The winner is {winnerName}.");
                    }

                    Console.WriteLine($"The score is {this.m_OthelloGame.GetScore(eCellState.Black)}:{this.m_OthelloGame.GetScore(eCellState.White)} for {this.m_OthelloGame.Player1}:{this.m_OthelloGame.Player2}.");
                    this.AskIfPlayerWantsToPlayAgain();
                    break;
                }
            }
        }

        public void GreetPlayers()
        {
            Console.Clear();
            Console.Write("Welcome to Othello!");
            Thread.Sleep(700);
            Console.Write(".");
            Thread.Sleep(700);
            Console.Write(" .");
            Thread.Sleep(700);
            Console.Write(" .");
            Thread.Sleep(700);
            Console.Clear();
        }

        public void ShowBoard()
        {
            Console.WriteLine();
            Console.Write("  ");

            for (int i = 0; i < this.m_OthelloGame.BoardSize; i++)
            {
                Console.Write($"{(char)('A' + i)} ");
            }

            Console.WriteLine();

            for (int i = 0; i < this.m_OthelloGame.BoardSize; i++)
            {
                Console.Write($"{i + 1} ");
                for (int j = 0; j < this.m_OthelloGame.BoardSize; j++)
                {
                    eCellState cell = this.m_OthelloGame.Board[i, j];
                    if (cell == eCellState.Empty)
                    {
                        Console.Write("- ");
                    }
                    else if (cell == eCellState.Black)
                    {
                        Console.Write("X ");
                    }
                    else
                    {
                        Console.Write("O ");
                    }
                }

                Console.WriteLine();
            }

            Console.WriteLine();
        }
    }
}
