using System;
using GameStore.Frontend.Models;

namespace GameStore.Frontend.Clients;

public class GenreClient
{
    private readonly Genre[] genres =
    {
        new () { Id = 1, Name = "Action" },
        new () { Id = 2, Name = "Adventure" },
        new () { Id = 3, Name = "RPG" },
        new () { Id = 4, Name = "Simulation" },
        new () { Id = 5, Name = "Strategy" }
    };

    public Genre[] GetGenres() => genres;
}
