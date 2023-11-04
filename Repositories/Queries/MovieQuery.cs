using Entities.Models;
using Shared;
using Shared.Columns;
using Shared.Dtos.MovieDtos;
using Shared.RequestFeatures.EntitiesParameters;
using System.Text;

namespace Repositories.Queries
{
    public static class MovieQuery
    {

        public static string GetMoviesByParametersQuery(MovieParameters parameters)
        {
            var orderByStatement = SharedQuery.GetOrderByQuery(typeof(MovieColumns), parameters.OrderDirection, parameters.OrderBy);
            var filterStatement = GetMovieParametersFilter(countSearchingGenres: parameters.SearchedGenresList.Count);

            return $"""
                SELECT COUNT("{MovieColumns.Id}")
                FROM "{Tables.MovieTable}"
                WHERE {filterStatement};

                SELECT {MovieColumns.GetMovieColumns()} FROM "{Tables.MovieTable}"
                WHERE {filterStatement}
                {orderByStatement}
                {SharedQuery.PaggingQuery};
                """;
        }

        public static string GetMoviesByActorIdQuery(MovieParameters parameters)
        {
            var orderByStatement = SharedQuery.GetOrderByQuery(typeof(MovieColumns), parameters.OrderDirection, parameters.OrderBy, "m");

            var joinStatement = $"""
                FROM "{Tables.MoviesActorsTable}" AS mv
                JOIN "{Tables.ActorTable}" AS a
                ON a.{ActorColumns.Id} = mv.{MoviesActorsColumns.ActorId} AND a.{ActorColumns.Id} = @Id
                JOIN "{Tables.MovieTable}" AS m
                ON m.{MovieColumns.Id} = mv.{MoviesActorsColumns.MovieId}
                """;
            var filterStatement = GetMovieParametersFilter("m", parameters.SearchedGenresList.Count);

            return $"""
                SELECT COUNT(m.{MovieColumns.Id})
                {joinStatement}
                WHERE {filterStatement};

                SELECT {MovieColumns.GetMovieColumns("m")}
                {joinStatement}
                WHERE {filterStatement}
                {orderByStatement}
                {SharedQuery.PaggingQuery};
                """;
        }

        public static string GetMoviesByDirectorId(MovieParameters parameters)
        {
            var orderByStatement = SharedQuery.GetOrderByQuery(typeof(MovieColumns), parameters.OrderDirection, parameters.OrderBy);;
            var filterStatement = GetMovieParametersFilter(countSearchingGenres: parameters.SearchedGenresList.Count);

            return $"""
                SELECT COUNT({MovieColumns.Id}) 
                FROM "{Tables.MovieTable}"
                WHERE {filterStatement} AND
                {MovieColumns.DirectorId} = @id;

                SELECT {MovieColumns.GetMovieColumns()} FROM "{Tables.MovieTable}"
                WHERE {filterStatement} AND
                {MovieColumns.DirectorId} = @id
                {orderByStatement}
                {SharedQuery.PaggingQuery}
                """;
        }

        public static string GetMoviesWithActors(MovieParameters parameters, int countActors)
        {
            var orderByStatement = SharedQuery.GetOrderByQuery(typeof(MovieColumns), parameters.OrderDirection, parameters.OrderBy);
            var movieColumns = MovieColumns.GetMovieColumns("m");
            var actorColumns = ActorColumns.GetActorColumns("a");
            var filterStatement = GetMovieParametersFilter("m", parameters.SearchedGenresList.Count);


            return $"""
                SELECT COUNT(m.{MovieColumns.Id}) 
                FROM "{Tables.MovieTable}" AS m
                WHERE {filterStatement};

                WITH MoviesWithActors AS (
                    SELECT
                    {movieColumns},
                    {actorColumns},
                    ROW_NUMBER() OVER (PARTITION BY m.{MovieColumns.Id}) AS row_number
                    FROM "{Tables.MoviesActorsTable}" AS mv
                    JOIN "{Tables.ActorTable}" AS a ON a.{ActorColumns.Id} = mv.{MoviesActorsColumns.ActorId}
                    RIGHT JOIN "{Tables.MovieTable}" AS m ON m.{MovieColumns.Id} = mv.{MoviesActorsColumns.MovieId}
                    WHERE {MovieColumns.Id} IN (
                        SELECT {MovieColumns.Id} 
                        FROM "{Tables.MovieTable}" AS m
                        WHERE {filterStatement}
                        {orderByStatement}
                        {SharedQuery.PaggingQuery}
                    )
                    {orderByStatement}
                )
                SELECT * FROM MoviesWithActors 
                WHERE row_number <= {countActors}
                """;
        }

        public static string GetMoviesToWatch(MovieParameters parameters)
        {
            var orderByStatement = SharedQuery.GetOrderByQuery(typeof(MovieColumns), parameters.OrderDirection, parameters.OrderBy);
            var filterStatement = GetMovieParametersFilter(countSearchingGenres: parameters.SearchedGenresList.Count);
            var joinStatement = $"""
                FROM "{Tables.MovieToWatchTable}"
                JOIN "{Tables.MovieTable}" ON {MovieColumns.Id} = {MovieToWatchColumns.MovieId}
                WHERE {MovieToWatchColumns.UserName} = @username 
                """;

            return $"""
                SELECT COUNT({MovieColumns.Id}) 
                {joinStatement} AND
                {filterStatement};
                
                SELECT {MovieColumns.GetMovieColumns()}
                {joinStatement} AND
                {filterStatement}
                {orderByStatement}
                {SharedQuery.PaggingQuery}
                """;
        }

        public static string CheckMovieInWatchinQuery = $"""
            SELECT COUNT(*) FROM "{Tables.MovieToWatchTable}" 
            WHERE {MovieToWatchColumns.UserName} = @username AND {MovieToWatchColumns.MovieId} = @id
            """;

        public static string GetMovieByIdQuery = $"""
            SELECT {MovieColumns.GetMovieColumns()} FROM "{Tables.MovieTable}"
            WHERE {MovieColumns.Id} = @Id
            """;

        public const string CreateMovieQuery = $"""
            INSERT INTO "{Tables.MovieTable}"
            ({MovieColumns.Id}, {MovieColumns.Genres}, {MovieColumns.Title}, 
            {MovieColumns.Description}, {MovieColumns.PhotoUrl}, {MovieColumns.ReleaseDate}, {MovieColumns.DirectorId}, 
            {MovieColumns.VideoPath}, {MovieColumns.GoodRating}, {MovieColumns.BadRating})
            VALUES (gen_random_uuid(), @{nameof(MovieForCreation.Genres)}, @{nameof(MovieForCreation.Title)},
            @{nameof(MovieForCreation.Description)}, @{nameof(MovieForCreation.PhotoUrl)}, @{nameof(MovieForCreation.ReleaseDate)},
            @{nameof(MovieForCreation.DirectorId)}, @{nameof(MovieForCreation.VideoPath)}, 0, 0)
            RETURNING {MovieColumns.Id}
            """;

        public const string UpdateMovieQuery = $"""
            UPDATE "{Tables.MovieTable}" SET
            {MovieColumns.Genres} = @{nameof(MovieForUpdate.Genres)},
            {MovieColumns.Title} = @{nameof(MovieForUpdate.Title)},
            {MovieColumns.Description} = @{nameof(MovieForUpdate.Description)},
            {MovieColumns.ReleaseDate} = @{nameof(MovieForUpdate.ReleaseDate)}, 
            {MovieColumns.PhotoUrl} = @{nameof(MovieForUpdate.PhotoUrl)},
            {MovieColumns.VideoPath} = @{nameof(MovieForUpdate.VideoPath)},
            {MovieColumns.DirectorId} = @{nameof(MovieForUpdate.DirectorId)}
            WHERE {MovieColumns.Id} = @Id
            """;

        public const string AddMovieToWatchingQuery = $"""
            INSERT INTO "{Tables.MovieToWatchTable}"
            ({MovieToWatchColumns.MovieId}, {MovieToWatchColumns.UserName})
            VALUES (@movieId, @username)
            """;

        public const string DeleteMovieQuery = $"""
            DELETE FROM "{Tables.MovieTable}"
            WHERE "{MovieColumns.Id}" = @Id
            """;

        public const string DeleteMovieFromWatching = $"""
            DELETE FROM "{Tables.MovieToWatchTable}"
            WHERE {MovieToWatchColumns.MovieId} = @id AND {MovieToWatchColumns.UserName} = @username
            """;

        public static string GetMovieParametersFilter(string alias = null, int countSearchingGenres = 0)
        {
            var aliasDot = string.IsNullOrEmpty(alias) ? "" : alias + '.';
            var movieFilterQuery = $"""
                ({aliasDot}{MovieColumns.Title} ILIKE ('%' || @{nameof(MovieParameters.SearchedTitle)} || '%') OR @{nameof(MovieParameters.SearchedTitle)} IS NULL) AND 
                ({aliasDot}{MovieColumns.GoodRating} BETWEEN @{nameof(MovieParameters.MinGoodRating)} AND @{nameof(MovieParameters.MaxGoodRating)} 
                OR @{nameof(MovieParameters.MaxGoodRating)} IS NULL) AND
                ({aliasDot}{MovieColumns.ReleaseDate} BETWEEN @{nameof(MovieParameters.MinDateRelease)} AND @{nameof(MovieParameters.MaxDateRelease)} 
                OR  @{nameof(MovieParameters.MinDateRelease)} IS NULL)
                """;
            if(countSearchingGenres > 0)
            {
                var stringBuilder = new StringBuilder();
                for (int i = 0; i < countSearchingGenres; i++)
                {
                    stringBuilder.AppendLine($"({aliasDot}{MovieColumns.Genres} ILIKE ('%' || @genre{i} || '%') OR @genre{i} IS NULL) AND");
                }
                string genreFilterQuery = stringBuilder.ToString();
                genreFilterQuery = genreFilterQuery.Remove(genreFilterQuery.LastIndexOf("AND"));
                return $"""
                {genreFilterQuery} AND
                {movieFilterQuery}
                """;
            }

            return movieFilterQuery;
        }
    }
}
