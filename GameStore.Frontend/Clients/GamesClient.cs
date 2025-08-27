using System;
using GameStore.Frontend.Models;

namespace GameStore.Frontend.Clients;

public class GamesClient
{
        private List<GameSummary> games = [
            new(){
Id=1, Name="The Legend of Code", Genre="Adventure", Price=59.99M, ReleaseDate=new DateOnly(2023,5,1)
},
new(){
Id=2, Name="Bug Hunter", Genre="Action", Price=49.99M, ReleaseDate=new DateOnly(2023,6,15)
},
new(){
Id=3, Name="Refactor Quest", Genre="RPG", Price=39.99M, ReleaseDate=new DateOnly(2023,7,20)
}
            ];

        private readonly Genre[] genres = new GenreClient().GetGenres();
        public GameSummary[] GetGames() => [.. games];

        public void AddGame(GameDetails game)
        {
                Genre genre = GetGenreById(game.GenreId);
                var gameSummary = new GameSummary
                {
                        Id = games.Count + 1,
                        Name = game.Name,
                        Genre = genre.Name ?? "Unknown",
                        Price = game.Price,
                        ReleaseDate = game.ReleaseDate
                };
                games.Add(gameSummary);
        }

        public void UpdateGame(GameDetails updatedGame)
        {
                var genre = GetGenreById(updatedGame.GenreId);
                GameSummary game = GetGameSummaryById(updatedGame.Id);
                game.Name = updatedGame.Name;
                game.Genre = genre.Name ?? "Unknown";
                game.Price = updatedGame.Price;
                game.ReleaseDate = updatedGame.ReleaseDate;
        }
        public GameDetails GetGame(int id)
        {
                GameSummary game = GetGameSummaryById(id);
                var genre = genres.Single(genre => string.Equals(genre.Name, game.Genre, StringComparison.OrdinalIgnoreCase));
                return new GameDetails
                {
                        Id = game.Id,
                        Name = game.Name,
                        GenreId = genre.Id.ToString(),
                        Price = game.Price,
                        ReleaseDate = game.ReleaseDate
                };
        }

        public async Task DeleteGame(int Id)
        {
                GameSummary? game = GetGameSummaryById(Id);
                if (game != null)
                {
                        games.Remove(game);
                }
                await Task.Delay(1);
        }

        private GameSummary GetGameSummaryById(int id)
        {
                GameSummary? game = games.Find(game => game.Id == id);
                ArgumentNullException.ThrowIfNull(game);
                return game;
        }

        private Genre GetGenreById(string? Id)
        {
                ArgumentException.ThrowIfNullOrWhiteSpace(Id);
                var genre = genres.Single(genre => genre.Id == int.Parse(Id));
                return genre;
        }
}
