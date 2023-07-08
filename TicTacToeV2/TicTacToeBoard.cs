namespace TicTacToeV2
{
    internal class TicTacToeBoard
    {
        public char[,] Board 
        {
            get;

            set;
        }
        
        public TicTacToeBoard(Player player1, Player computer)
        {
            Player Player1 = player1;
            Player Computer = computer;
            this.Board = new char[3, 3]{
            {'1','2','3'},
            {'4', '5', '6'},
            {'7', '8', '9'}
            };
        }

        public void printBoard()
        {
            Console.WriteLine("-------------------------------------------------------------");
            for (int row = 0; row < Board.GetLength(0); row++)
            {
                Console.WriteLine("|                   |                   |                   |");
                Console.WriteLine("|         {0}         |         {1}         |         {2}         |", Board[row, 0], Board[row, 1], Board[row, 2]);
                Console.WriteLine("|                   |                   |                   |");
                Console.WriteLine("-------------------------------------------------------------");
            }
        }

        public void MakeMove(Player CurrentPlayer)
        {
            int row, col;
            Console.WriteLine("Παίζει ο {0} με το Σύμβολο {1}", CurrentPlayer.PlayerName, CurrentPlayer.PlayerSymbol);
            Tuple<int, int> PlayerChoise = CheckInput(CurrentPlayer);
            row = PlayerChoise.Item1;
            col = PlayerChoise.Item2;
            Board[row, col] = CurrentPlayer.PlayerSymbol;
            Console.WriteLine("Επέλεξες τα {0},{1}", row, col);
            printBoard();
        }

        public bool checkifGameisFinished(Player CurrentPlayer)
        {
            if (checkIfBoardisFull())
            {
                Console.WriteLine("Ισοπαλια Κανείς δεν Κέρδισε");
                return true;
            }
            if (CheckIfWin())
            {
                CurrentPlayer.PlayerScore++;
                Console.WriteLine("Το παιχνίδι Τελείωσε. Νίκησε ο {0}", CurrentPlayer.PlayerName);
                return true;
            }
            return false;
        }

        private Tuple<int, int> CheckInput(Player CurrentPlayer)
        {
            Tuple<int, int> zero = Tuple.Create(-1, -1);
            while (true)
            {
                Console.WriteLine("Δώσε το νούμερο του σημείου που θές να παίξεις 1 - 9 και e ή E για έξοδο");
                string? Choise = Console.ReadLine();
                if (Choise == "e" || Choise == "E")
                {
                    TicTacToeGame.GameInfo();
                    Console.Read();
                    Environment.Exit(0);
                }
                if (!int.TryParse(Choise, out int playerChoise) || playerChoise < 1 || playerChoise > 9)
                {
                    Console.WriteLine("Λανθασμένη Επιλογή Δεν έδωσες Νούμερο ή έδωσες νούμερο εκτος ευρους");
                    continue;
                }
                else
                {
                    Tuple<int, int> rowcol = Checkifisfree(playerChoise);
                    if (zero.Equals(rowcol))
                    {
                        Console.WriteLine("Εδώσες αριθμό τετραγώνου που είναι ήδη κατελλημένο");
                        continue;
                    }
                    else
                    {
                        return rowcol;
                    }
                }
            }
        }

        public Tuple<int, int> Checkifisfree(int playerChoise)
        {
            for (int row = 0; row < Board.GetLength(0); row++)
            {
                for (int col = 0; col < Board.GetLength(1); col++)
                {
                    if (Board[row, col] == (char)(playerChoise + '0'))
                    {
                        return Tuple.Create(row, col);
                    }
                }
            }
            return Tuple.Create(-1, -1);
        }

        public bool checkIfBoardisFull()
        {
            for (int row = 0; row < Board.GetLength(0); row++)
            {
                for (int col = 0; col < Board.GetLength(1); col++)
                {

                    if (!Board[row,col].Equals('X')  && !Board[row, col].Equals('O'))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public bool CheckIfWin()
        {
            //check Horizontal
            for (int row = 0; row < Board.GetLength(0); row++)
            {
                if (Board[row, 0] == Board[row, 1] && Board[row, 0] == Board[row, 2])
                {
                    return true;
                }
            }
            //check vertical
            for (int col = 0; col < Board.GetLength(1); col++)
            {
                if (Board[0, col] == Board[1, col] && Board[0, col] == Board[2, col])
                {
                    return true;
                }
            }
            //check diagonal
            if (Board[0, 0] == Board[1, 1] && Board[0, 0] == Board[2, 2])
            {
                return true;
            }
            if (Board[0, 2] == Board[1, 1] && Board[0, 2] == Board[2, 0])
            {
                return true;
            }
            return false;
        }
    }
}
