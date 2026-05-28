public class GameDto
{
  public string Id { get; set; } = "";

  public string Player1Name { get; set; } = "";

  public string Player2Name { get; set; } = "";

  public List<char> GuessedLetters { get; set; } = new();

  public int Player1Score { get; set; }

  public int Player2Score { get; set; }

  public int CurrentPlayer { get; set; }

  public string Status { get; set; } = "";

  public string DisplayWord { get; set; } = "";
}