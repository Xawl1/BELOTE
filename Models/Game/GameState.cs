namespace DEMO2_ASP.Models.Game;
public class GameState
{
    public List<Card> Deck { get; set; } = new List<Card>();
    public Player HumanPlayer { get; set; } = new Player { IsHuman = true, Name = "You" };
    public List<Player> Opponents { get; set; } = new List<Player>();
    public List<Card> Trick { get; set; } = new List<Card>();

    
    public int CurrentPlayerIndex { get; set; }
    public string TrumpSuit { get; set; } = string.Empty;
    public int? BidderIndex { get; set; }
    public int TeamPoints { get; set; }
    public int OpponentTeamPoints { get; set; }
    public int BidPasses { get; set; }
    public string GamePhase { get; set; } = "bidding"; // 'bidding', 'playing', 'roundOver'
    
    public Player CurrentPlayer => CurrentPlayerIndex == 0 ? HumanPlayer : Opponents[CurrentPlayerIndex - 1];
}