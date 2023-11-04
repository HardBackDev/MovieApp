using Entities.Models;
using Shared;
using Shared.Columns;

namespace Repositories.Queries
{
    public static class RatedMovieQuery
    {
        public const string CreateRatedMovieRelation = $"""
            INSERT INTO "{Tables.RatedMovieTable}"
            ("{RatedMovieColumns.UserName}", "{RatedMovieColumns.MovieId}", "{RatedMovieColumns.Rate}")
            VALUES (@userName, @movieId, @rate)
            """;

        public static string GetMovieRateQuery = $"""
            SELECT {RatedMovieColumns.GetRatedMovieColumns()} FROM "{Tables.RatedMovieTable}"
            WHERE "{RatedMovieColumns.MovieId}" = @movieId AND "{RatedMovieColumns.UserName}" = @username
            """;

        public const string UnRateMovieQuery = $"""
            DELETE FROM "{Tables.RatedMovieTable}"
            WHERE "{RatedMovieColumns.MovieId}" = @movieId AND "{RatedMovieColumns.UserName}" = @username
            """;

        public static string LikeMovieQuery(int grade) => $"""
            UPDATE "{Tables.MovieTable}" SET
            "{MovieColumns.GoodRating}" = {grade}
            WHERE "{MovieColumns.Id}" = @Id
            """;

        public static string DisLikeMovieQuery(int grade) => $"""
            UPDATE "{Tables.MovieTable}" SET
            "{MovieColumns.BadRating}" = {grade}
            WHERE "{MovieColumns.Id}" = @Id
            """;

    }
}
