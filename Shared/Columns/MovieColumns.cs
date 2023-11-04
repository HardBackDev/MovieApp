using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Columns
{
    public static class MovieColumns
    {
        public const string Id = "movie_id";
        public const string DirectorId = "movie_director_id";
        public const string Title = "movie_title";
        public const string Description = "movie_description";
        public const string ReleaseDate = "movie_release_date";
        public const string Genres = "movie_genres";
        public const string PhotoUrl = "movie_photo_url";
        public const string VideoPath = "movie_video_path";
        public const string GoodRating = "movie_good_rating";
        public const string BadRating = "movie_bad_rating";

        public static string GetMovieColumns(string alias = null)
        {
            var aliasDot = string.IsNullOrEmpty(alias) ? "" : alias + '.';
            var movieColumns = $"""
                {aliasDot}{Id} AS {nameof(Movie.Id)},
                {aliasDot}{Genres} AS {nameof(Movie.Genres)},
                {aliasDot}{Title} AS {nameof(Movie.Title)}, 
                {aliasDot}{Description} AS {nameof(Movie.Description)},
                {aliasDot}{ReleaseDate} AS {nameof(Movie.ReleaseDate)},
                {aliasDot}{GoodRating} AS {nameof(Movie.GoodRating)},
                {aliasDot}{PhotoUrl} AS {nameof(Movie.PhotoUrl)},
                {aliasDot}{DirectorId} AS {nameof(Movie.DirectorId)},
                {aliasDot}{BadRating} AS {nameof(Movie.BadRating)},
                {aliasDot}{VideoPath} AS {nameof(Movie.VideoPath)}
                """;

            return movieColumns;
        }
    }
}
