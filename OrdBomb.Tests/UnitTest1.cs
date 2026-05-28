public class GameServiceTests
{
    // Testar att en lobby skapas korrekt
    [Fact]
    public void CreateLobby_ShouldCreateWaitingGame()
    {
        GameService service = new GameService();

        Game game = service.CreateLobby("Samir");

        Assert.Equal("Samir", game.Player1Name);
        Assert.Equal("Waiting", game.Status);
        Assert.False(string.IsNullOrEmpty(game.Id));
    }

    // Testar att player 2 kan gå med och spelet startar
    [Fact]
    public void JoinLobby_ShouldStartGame()
    {
        GameService service = new GameService();

        Game game = service.CreateLobby("Samir");

        Game? joinedGame = service.JoinLobby(game.Id, "Ali");

        Assert.NotNull(joinedGame);
        Assert.Equal("Ali", joinedGame.Player2Name);
        Assert.Equal("Running", joinedGame.Status);
    }

    // Testar att rätt bokstav ger poäng
    [Fact]
    public void CorrectLetter_ShouldGivePoint()
    {
        GameService service = new GameService();

        Game game = service.CreateLobby("Samir");
        service.JoinLobby(game.Id, "Ali");

        service.GuessLetter(game.Id, 1, game.SecretWord[0]);

        Assert.Equal(1, game.Player1Score);
    }

    // Testar att turen byts efter gissning
    [Fact]
    public void Guess_ShouldChangePlayer()
    {
        GameService service = new GameService();

        Game game = service.CreateLobby("Samir");
        service.JoinLobby(game.Id, "Ali");

        service.GuessLetter(game.Id, 1, game.SecretWord[0]);

        Assert.Equal(2, game.CurrentPlayer);
    }

    // Testar att samma bokstav inte ger extra poäng
    [Fact]
    public void DuplicateLetter_ShouldNotGiveExtraPoint()
    {
        GameService service = new GameService();

        Game game = service.CreateLobby("Samir");
        service.JoinLobby(game.Id, "Ali");

        char letter = game.SecretWord[0];

        service.GuessLetter(game.Id, 1, letter);
        service.GuessLetter(game.Id, 2, letter);

        Assert.Equal(1, game.Player1Score);
        Assert.Equal(0, game.Player2Score);
    }
}