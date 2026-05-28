var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<GameService>();

var app = builder.Build();

GameService gameService = app.Services.GetRequiredService<GameService>();

app.UseDefaultFiles();
app.UseStaticFiles();



app.MapPost("/lobby/create/{playerName}", (string playerName) =>
{
    return gameService.CreateLobby(playerName);
});

app.MapPost("/lobby/join/{gameId}/{playerName}", (string gameId, string playerName) =>
{
    Game? game = gameService.JoinLobby(gameId, playerName);

    if (game == null)
    {
        return Results.NotFound("Lobby not found");
    }

    return Results.Ok(gameService.ConvertToDto(game));
});

app.MapGet("/game/{gameId}", (string gameId) =>
{
    Game? game = gameService.GetGame(gameId);

    if (game == null)
    {
        return Results.NotFound("Game not found");
    }

    return Results.Ok(gameService.ConvertToDto(game));
});

app.MapPost("/game/{gameId}/guess/{playerNumber}/{letter}", (string gameId, int playerNumber, char letter) =>
{
    Game? game = gameService.GuessLetter(gameId, playerNumber, letter);

    if (game == null)
    {
        return Results.NotFound("Game not found");
    }

    return Results.Ok(gameService.ConvertToDto(game));
});

app.Run();