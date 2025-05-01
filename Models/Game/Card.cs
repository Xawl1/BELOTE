namespace DEMO2_ASP.Models.Game;
public class Card
{
    public string Id { get; set; } = string.Empty;
    public string Suit { get; set; } = string.Empty;
    public string SuitSymbol { get; set; } = string.Empty;
    public string Color { get; set; } = string.Empty;
    public string Value { get; set; } = string.Empty;
    public string ValueSymbol { get; set; } = string.Empty;
    public int Rank { get; set; }
    public int Points { get; set; }
    public int TrumpRank { get; set; }
    public int TrumpPoints { get; set; }
}