namespace TicTacToeV2
{
    internal class Player
    {
        public string PlayerName { get; set; }
        public char PlayerSymbol { get; set; }
        public int PlayerScore { get; set; }
        public bool IsPlaying { get; set; }

        public Player(string playerName, char playerSymbol)
        {
            this.PlayerName = playerName;
            this.PlayerSymbol = playerSymbol;
            this.PlayerScore = 0;
            this.IsPlaying = false;
        }
    }
}
