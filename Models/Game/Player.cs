namespace DEMO2_ASP.Models.Game;
public class Player
{
    public string Id { get; set; }
    public string Name { get; set; }
    public List<Card> Hand { get; set; } = new List<Card>();
    public int Points { get; set; }
    public bool IsHuman { get; set; }
}