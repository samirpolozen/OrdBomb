public class Game
{
  // Unikt ID för lobbyn/matchen
  public string Id { get; set; } = "";

  // Namn på spelare 1
  public string Player1Name { get; set; } = "";

  // Namn på spelare 2
  public string Player2Name { get; set; } = "";

  // Hemliga ordet
  public string SecretWord { get; set; } = "";

  // Bokstäver som redan har gissats
  public List<char> GuessedLetters { get; set; } = new();

  // Poäng för spelare 1
  public int Player1Score { get; set; }

  // Poäng för spelare 2
  public int Player2Score { get; set; }

  // Vilken spelare som har tur
  public int CurrentPlayer { get; set; } = 1;

  // Spelets status: Waiting, Running eller Finished
  public string Status { get; set; } = "Waiting";

  // Visar ordet med gissade bokstäver
  public string DisplayWord { get; set; } = "";
}