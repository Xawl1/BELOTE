using DEMO2_ASP.Models.Game;
using DEMO2_ASP.Services;
using Microsoft.AspNetCore.Mvc;

public class GameController : Controller
{
    private readonly BeloteGameService _gameService;
    private const string GameSessionKey = "BeloteGameState";

    public GameController(BeloteGameService gameService)
    {
        _gameService = gameService;
    }

    public IActionResult BeloteGame()
    {
        // Initialize or retrieve game state
        GameState gameState = HttpContext.Session.Get<GameState>(GameSessionKey) ?? _gameService.InitializeGame();
        HttpContext.Session.Set(GameSessionKey, gameState);
        
        return View(gameState);
    }

    [HttpPost]
    public IActionResult PlayCard(string cardId)
    {
        var gameState = HttpContext.Session.Get<GameState>(GameSessionKey);
        
        if (gameState == null || gameState.CurrentPlayerIndex != 0)
            return BadRequest("Invalid game state");

        _gameService.PlayCard(gameState, cardId);
        HttpContext.Session.Set(GameSessionKey, gameState);

        return PartialView("_GameBoard", gameState);
    }

    [HttpPost]
    public IActionResult MakeBid(string suit)
    {
        var gameState = HttpContext.Session.Get<GameState>(GameSessionKey);
        
        if (gameState == null || gameState.GamePhase != "bidding")
            return BadRequest("Invalid bidding state");

        _gameService.MakeBid(gameState, suit);
        HttpContext.Session.Set(GameSessionKey, gameState);

        return Json(new { 
            success = true, 
            gamePhase = gameState.GamePhase,
            trumpSuit = gameState.TrumpSuit,
            currentPlayer = gameState.CurrentPlayerIndex
        });
    }

    [HttpPost]
    public IActionResult NewGame()
    {
        var newState = _gameService.InitializeGame();
        HttpContext.Session.Set(GameSessionKey, newState);
        
        return RedirectToAction("BeloteGame");
    }

    public IActionResult BeloteGuide()
    {
        // Keep your existing guide view
        return View();
    }
}