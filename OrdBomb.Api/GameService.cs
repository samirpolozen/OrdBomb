public class GameService
{
  // Här sparar vi alla matcher/lobbies i minnet
  private Dictionary<string, Game> games = new Dictionary<string, Game>();

  // Ord som servern kan välja mellan
  private List<string> words = new List<string>()
    {
        "backend",
        "frontend",
        "testing",
        "pipeline",
        "network",
        "coding"
    };

  // Skapar en ny lobby
  public Game CreateLobby(string playerName)
  {
    Game game = new Game();

    // Skapar ett kort unikt lobby-id
    game.Id = Guid.NewGuid().ToString().Substring(0, 6);

    // Sparar player 1 namn
    game.Player1Name = playerName;

    // Väljer hemligt ord
    Random random = new Random();
    game.SecretWord = words[random.Next(words.Count)];

    // Väntar på player 2
    game.Status = "Waiting";

    // Player 1 börjar
    game.CurrentPlayer = 1;

    // Sparar spelet i dictionaryn
    games.Add(game.Id, game);

    return game;
  }

  // Player 2 går med i en lobby
  public Game? JoinLobby(string gameId, string playerName)
  {
    if (!games.ContainsKey(gameId))
    {
      return null;
    }

    Game game = games[gameId];

    game.Player2Name = playerName;

    game.Status = "Running";

    return game;
  }

  // Hämtar en specifik lobby
  public Game? GetGame(string gameId)
  {
    if (!games.ContainsKey(gameId))
    {
      return null;
    }

    return games[gameId];
  }

  // Gissar bokstav i en specifik lobby
  public Game? GuessLetter(string gameId, int playerNumber, char letter)
  {
    if (!games.ContainsKey(gameId))
    {
      return null;
    }

    Game game = games[gameId];

    if (game.Status != "Running")
    {
      return game;
    }

    if (game.CurrentPlayer != playerNumber)
    {
      return game;
    }

    letter = char.ToLower(letter);

    if (game.GuessedLetters.Contains(letter))
    {
      return game;
    }

    game.GuessedLetters.Add(letter);

    if (game.SecretWord.Contains(letter))
    {
      if (playerNumber == 1)
      {
        game.Player1Score++;
      }
      else
      {
        game.Player2Score++;
      }
    }

    ChangePlayer(game);

    if (IsGameFinished(game))
    {
      game.Status = "Finished";
    }

    return game;
  }

  private void ChangePlayer(Game game)
  {
    if (game.CurrentPlayer == 1)
    {
      game.CurrentPlayer = 2;
    }
    else
    {
      game.CurrentPlayer = 1;
    }
  }

  private bool IsGameFinished(Game game)
  {
    foreach (char letter in game.SecretWord)
    {
      if (!game.GuessedLetters.Contains(letter))
      {
        return false;
      }
    }

    return true;
  }

  public GameDto ConvertToDto(Game game)
  {
    GameDto dto = new GameDto();

    dto.Id = game.Id;
    dto.Player1Name = game.Player1Name;
    dto.Player2Name = game.Player2Name;
    dto.GuessedLetters = game.GuessedLetters;
    dto.Player1Score = game.Player1Score;
    dto.Player2Score = game.Player2Score;
    dto.CurrentPlayer = game.CurrentPlayer;
    dto.Status = game.Status;
    dto.DisplayWord = GetDisplayWord(game);

    return dto;
  }

  private string GetDisplayWord(Game game)
  {
    string displayWord = "";

    foreach (char letter in game.SecretWord)
    {
      if (game.GuessedLetters.Contains(letter))
      {
        displayWord += letter + " ";
      }
      else
      {
        displayWord += "_ ";
      }
    }

    return displayWord;
  }
}