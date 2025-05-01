namespace DEMO2_ASP.Models.Game;
public class Card
{
    public string Id { get; set; }
    public string Suit { get; set; }
    public string SuitSymbol { get; set; }
    public string Color { get; set; }
    public string Value { get; set; }
    public string ValueSymbol { get; set; }
    public int Rank { get; set; }
    public int Points { get; set; }
    public int TrumpRank { get; set; }
    public int TrumpPoints { get; set; }
}