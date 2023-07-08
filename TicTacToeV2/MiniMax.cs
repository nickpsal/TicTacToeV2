using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToeV2
{
    internal class MiniMax
    {
        public static Tuple<int, int> GetComputerMove(TicTacToeBoard Board, Player Player1, Player Computer)
        {
            int bestScore = int.MinValue;
            Tuple<int, int> bestMove = Tuple.Create(-1, -1);
            for (int row = 0; row < Board.Board.GetLength(0); row++)
            {
                for (int col = 0; col < Board.Board.GetLength(1); col++)
                {
                    if (Board.Board[row, col] != Player1.PlayerSymbol && Board.Board[row, col] != Computer.PlayerSymbol)
                    {
                        char temp = Board.Board[row, col];
                        Board.Board[row, col] = Computer.PlayerSymbol; // Simulate the computer's move
                        int score = Minimax(Board, 0, int.MinValue, int.MaxValue, false, Player1.PlayerSymbol, Computer.PlayerSymbol);
                        Board.Board[row, col] = temp; // Undo the computer's move
                        if (score > bestScore)
                        {
                            bestScore = score;
                            bestMove = Tuple.Create(row, col);
                        }
                    }
                }
            }
            return bestMove;
        }

        private static int Minimax(TicTacToeBoard Board, int depth, int alpha, int beta, bool isMaximizingPlayer, char Player1Symbol, char ComputerSymbol)
        {
            if (Board.CheckIfWin())
            {
                if (isMaximizingPlayer)
                {
                    return -1; // The computer loses
                }
                else
                {
                    return 1; // The computer wins
                }
            }
            if (Board.checkIfBoardisFull())
            {
                return 0; // It's a tie
            }
            if (isMaximizingPlayer)
            {
                int bestScore = int.MinValue;
                for (int row = 0; row < Board.Board.GetLength(0); row++)
                {
                    for (int col = 0; col < Board.Board.GetLength(1); col++)
                    {
                        if (Board.Board[row, col] != Player1Symbol && Board.Board[row, col] != ComputerSymbol)
                        {
                            char temp = Board.Board[row, col];
                            Board.Board[row, col] = ComputerSymbol; // Simulate the computer's move
                            int score = Minimax(Board, depth + 1, alpha, beta, false, Player1Symbol, ComputerSymbol);
                            Board.Board[row, col] = temp; // Undo the computer's move
                            bestScore = Math.Max(score, bestScore);
                            alpha = Math.Max(alpha, score);
                            if (beta <= alpha)
                            {
                                break; // Beta cut-off
                            }
                        }
                    }
                }
                return bestScore;
            }
            else
            {
                int bestScore = int.MaxValue;
                for (int row = 0; row < Board.Board.GetLength(0); row++)
                {
                    for (int col = 0; col < Board.Board.GetLength(1); col++)
                    {
                        if (Board.Board[row, col] != Player1Symbol && Board.Board[row, col] != ComputerSymbol)
                        {
                            char temp = Board.Board[row, col];
                            Board.Board[row, col] = Player1Symbol; // Simulate the player's move
                            int score = Minimax(Board, depth + 1, alpha, beta, true, Player1Symbol, ComputerSymbol);
                            Board.Board[row, col] = temp; // Undo the player's move
                            bestScore = Math.Min(score, bestScore);
                            beta = Math.Min(beta, score);
                            if (beta <= alpha)
                            {
                                break; // Alpha cut-off
                            }
                        }
                    }
                }
                return bestScore;
            }
        }
    }
}
