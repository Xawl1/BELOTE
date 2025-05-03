namespace DEMO2_ASP.Services;
using DEMO2_ASP.Models.Game;

public class BeloteGameService
{
    private static readonly List<(string symbol, string name, string color)> Suits = new()
    {
        ("♠️", "spade", "black"),
        ("♥️", "heart", "red"),
        ("♦️", "diamond", "red"),
        ("♣️", "club", "black")
    };

    private static readonly List<(string symbol, string name, int rank, int points, int? trumpRank, int? trumpPoints)> Values = new()
    {
        ("7", "seven", 0, 0, null, null),
        ("8", "eight", 1, 0, null, null),
        ("9", "nine", 2, 0, 6, 14),
        ("10", "ten", 5, 10, null, null),
        ("J", "jack", 3, 2, 7, 20),
        ("Q", "queen", 4, 3, null, null),
        ("K", "king", 6, 4, null, null),
        ("A", "ace", 7, 11, null, null)
    };

    public GameState InitializeGame()
    {
        var state = new GameState
        {
            Opponents = new List<Player>
            {
                new Player { Id = "opponent1", Name = "Opponent 1" },
                new Player { Id = "opponent2", Name = "Opponent 2" },
                new Player { Id = "opponent3", Name = "Opponent 3" }
            }
        };

        CreateDeck(state);
        ShuffleDeck(state);
        DealCards(state);

        return state;
    }

    private void CreateDeck(GameState state)
    {
        state.Deck.Clear();
        
        foreach (var suit in Suits)
        {
            foreach (var value in Values)
            {
                state.Deck.Add(new Card
                {
                    Suit = suit.name,
                    SuitSymbol = suit.symbol,
                    Color = suit.color,
                    Value = value.name,
                    ValueSymbol = value.symbol,
                    Rank = value.rank,
                    Points = value.points,
                    TrumpRank = value.trumpRank ?? value.rank,
                    TrumpPoints = value.trumpPoints ?? value.points,
                    Id = $"{value.name}_{suit.name}"
                });
            }
        }
    }

    private void ShuffleDeck(GameState state)
    {
        var rng = new Random();
        int n = state.Deck.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            (state.Deck[k], state.Deck[n]) = (state.Deck[n], state.Deck[k]);
        }
    }
    public void MakeBid(GameState gameState, string suit)
{
    
    if (gameState.GamePhase != "bidding")
    {
        throw new InvalidOperationException("The game is not in the bidding phase.");
    }

    
    if (gameState.BidderIndex == null)
    {
        gameState.BidderIndex = gameState.CurrentPlayerIndex;
        gameState.TrumpSuit = suit;  
    }
    else
    {
       
        gameState.GamePhase = "playing";  
    }

   
    gameState.CurrentPlayerIndex = (gameState.CurrentPlayerIndex + 1) % 4;

    
}


    public void SortHand(List<Card> hand)
{
    hand.Sort((card1, card2) => 
    {
        int suitComparison = card1.Suit.CompareTo(card2.Suit);
        if (suitComparison != 0) return suitComparison;
        return card1.Value.CompareTo(card2.Value);
    });
}
    private void DealCards(GameState state)
    {
        state.HumanPlayer.Hand.Clear();
        foreach (var opponent in state.Opponents)
        {
            opponent.Hand.Clear();
        }

        
        for (int i = 0; i < 5; i++)
        {
            state.HumanPlayer.Hand.Add(state.Deck.Last());
            state.Deck.RemoveAt(state.Deck.Count - 1);
            
            foreach (var opponent in state.Opponents)
            {
                opponent.Hand.Add(state.Deck.Last());
                state.Deck.RemoveAt(state.Deck.Count - 1);
            }
        }

        SortHand(state.HumanPlayer.Hand);
    }

    public void PlayCard(GameState gameState, string cardId)
{
   
     var player = gameState.CurrentPlayer;
    if (player == null) return;

    
    var card = player.Hand.FirstOrDefault(c => c.Id == cardId);
    if (card != null)
    {
        player.Hand.Remove(card);
        gameState.Trick.Add(card);
    }
}


    
}