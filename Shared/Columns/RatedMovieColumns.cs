using Entities.Models;
using System.Xml.Linq;

namespace Shared.Columns
{
    public static class RatedMovieColumns
    {
        public const string UserName = "rated_movie_username";
        public const string MovieId = "rated_movie_movie_id";
        public const string Rate = "rated_movie_rate";

        public static string GetRatedMovieColumns(string alias = null)
        {
            string aliasDot = string.IsNullOrEmpty(alias) ? "" : alias + '.';

            return $"""
            {aliasDot}"{UserName}" AS {nameof(RatedMovie.UserName)},
            {aliasDot}"{MovieId}" AS {nameof(RatedMovie.MovieId)},
            {aliasDot}"{Rate}" AS {nameof(RatedMovie.Rate)}
            """;
        }
    }
}
