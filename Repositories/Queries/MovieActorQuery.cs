using Entities.Models;
using Shared;
using Shared.Columns;

namespace Repositories.Queries
{
    internal static class MovieActorQuery
    {
        public const string CreateMovieActorRelationQuery = $"""
            INSERT INTO "{Tables.MoviesActorsTable}"
            ("{MoviesActorsColumns.ActorId}", "{MoviesActorsColumns.MovieId}")
            VALUES (@{nameof(MoviesActors.ActorId)}, @{nameof(MoviesActors.MovieId)})
            """;
    }
}