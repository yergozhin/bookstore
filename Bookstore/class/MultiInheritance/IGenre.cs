using System;
using System.Collections.Generic;

namespace Bookstore.@class
{
    public interface IGenre
    {
        string GenreName { get; }
    }

    public interface IHorror : IGenre
    {
        int LevelOfScariness { get; set; }
    }

    public interface IFantasy : IGenre
    {
        List<string> MagicalCreatures { get; set; }
    }

    public interface IRomance : IGenre
    {
        string RelationshipsType { get; set; }
    }
}