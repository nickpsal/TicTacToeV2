namespace TicTacToeV2
{
    internal class TicTacToeGame
    {
        public TicTacToeGame()
        {
            char Player1Symbol = ' ';
            char ComputerSymbol = ' ';
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Θες να παίξουμε y/n");
                string? choise = Console.ReadLine();
                if (choise == "y" || choise == "Y")
                {
                    Random random = new();
                    int num = random.Next(1, 100);
                    if (num % 2 == 0)
                    {
                        Player1Symbol = 'X';
                        ComputerSymbol = '0';
                    }
                    else
                    {
                        Player1Symbol = 'O';
                        ComputerSymbol = 'X';
                    }
                    Player player1 = new Player("Player1", Player1Symbol);
                    Player Computer = new Player("Computer", ComputerSymbol);
                    TicTacToeBoard NewBoard = new TicTacToeBoard(player1, Computer);
                    Console.WriteLine("-".PadLeft(58, '-').PadRight(58, '-'));
                    Console.WriteLine("| Player1 Human     : {0}                                  |", player1.PlayerSymbol);
                    Console.WriteLine("| Player2 Computer  : {0}                                  |", Computer.PlayerSymbol);
                    Console.WriteLine("| Το σκορ Είναι Player1 - Computer : {0} - {1}               |", player1.PlayerScore, Computer.PlayerScore);
                    Console.WriteLine("| Ας Παίξουμε                                            |");
                    Console.WriteLine("-".PadLeft(58, '-').PadRight(58, '-'));
                    NewBoard.printBoard();
                    PlayGame(NewBoard, player1, Computer);
                    break;
                }
                else if (choise == "n" || choise == "N")
                {
                    GameInfo();
                    Console.Read();
                    Environment.Exit(0);
                }
                else
                {
                    Console.WriteLine("Λάθος Επιλογή");
                    continue;
                }
            }

        }

        public TicTacToeGame(Player Player1, Player Computer)
        {
            Random random = new();
            int num = random.Next(1, 100);
            if (num % 2 == 0)
            {
                Player1.PlayerSymbol = 'X';
                Computer.PlayerSymbol = '0';
            }
            else
            {
                Player1.PlayerSymbol = 'O';
                Computer.PlayerSymbol = 'X';
            }
            TicTacToeBoard NewBoard = new TicTacToeBoard(Player1, Computer);
            Console.WriteLine("-".PadLeft(58, '-').PadRight(58, '-'));
            Console.WriteLine("| Player1 Human     : {0}                                  |", Player1.PlayerSymbol);
            Console.WriteLine("| Player2 Computer  : {0}                                  |", Computer.PlayerSymbol);
            Console.WriteLine("| Το σκορ Είναι Player1 - Computer : {0} - {1}               |", Player1.PlayerScore, Computer.PlayerScore);
            Console.WriteLine("| Ας Παίξουμε                                            |");
            Console.WriteLine("-".PadLeft(58, '-').PadRight(58, '-'));
            NewBoard.printBoard();
            PlayGame(NewBoard, Player1, Computer);
        }

        public void PlayGame(TicTacToeBoard Board, Player Player1, Player Computer)
        {
            Player1.IsPlaying = true;
            while (true)
            {
                Computer.IsPlaying = false;
                Player1.IsPlaying = true;
                Board.MakeMove(Player1);
                if (Board.checkifGameisFinished(Player1))
                {
                    Console.WriteLine("-".PadLeft(58, '-').PadRight(58, '-'));
                    Console.WriteLine("| Το σκορ Είναι Player1 - Computer : {0} - {1}               |", Player1.PlayerScore, Computer.PlayerScore);
                    Console.WriteLine("-".PadLeft(58, '-').PadRight(58, '-'));
                    TicTacToeGame NewGame = new TicTacToeGame(Player1, Computer);                    
                }
                Computer.IsPlaying = true;
                Player1.IsPlaying = false;
                ComputerMove(Board, Player1, Computer);
            }
        }

        private void ComputerMove(TicTacToeBoard Board, Player Player1, Player Computer)
        {
            Tuple<int, int> move = MiniMax.GetComputerMove(Board, Player1, Computer);
            int row = move.Item1;
            int col = move.Item2;
            Board.Board[row, col] = Computer.PlayerSymbol;
            Console.WriteLine("Ο Υπολογιστης επέλεξε τα {0},{1}", row, col);
            if (Board.checkifGameisFinished(Computer))
            {
                Board.printBoard();
                Console.WriteLine("-".PadLeft(58, '-').PadRight(58, '-'));
                Console.WriteLine("| Το σκορ Είναι Player1 - Computer : {0} - {1}               |", Player1.PlayerScore, Computer.PlayerScore);
                Console.WriteLine("-".PadLeft(58, '-').PadRight(58, '-'));
                TicTacToeGame NewGame = new TicTacToeGame(Player1, Computer);
            }
        }

        private Player getCurrentPlayer(Player Player1, Player Computer)
        {
            if (Player1.IsPlaying)
            {
                return Player1;
            }
            else
            {
                return Computer;
            }
        }

        private Player PlayerTurn(Player Player1, Player Computer)
        {
            if (Player1.IsPlaying)
            {
                Player1.IsPlaying = false;
                Computer.IsPlaying = true;
                return Computer;
            }
            else
            {
                Player1.IsPlaying = true;
                Computer.IsPlaying = false;
                return Player1;
            }
        }

        public static void GameInfo()
        {
            Console.WriteLine("Το παίχνιδι αυτό φτιάχτηκε για εκπαιδευτικούς σκοπούς");
            Console.WriteLine("Όνομα Προγραμματιστή : Ψαλτάκης Νικόλαος");
            Console.WriteLine("(C) 06/2023 using .NET");
        }
    }
}
