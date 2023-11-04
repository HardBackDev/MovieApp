using Entities.Models;
using Shared.RequestFeatures.EntitiesParameters;
using Shared.RequestFeatures;
using Shared;
using Shared.Columns;

namespace Repositories.Queries
{
    public class ActorQuery
    {
        public static string GetActorsByParametersQuery(ActorParameters parameters)
        {
            var orderByStatement = SharedQuery.GetOrderByQuery(typeof(ActorColumns), parameters.OrderDirection, parameters.OrderBy);

            return $"""
                SELECT COUNT({ActorColumns.Id})
                FROM "{Tables.ActorTable}"
                WHERE {GetActorParametersFilter(null)};

                SELECT {ActorColumns.GetActorColumns(null)} FROM "{Tables.ActorTable}"
                WHERE {GetActorParametersFilter(null)}
                {orderByStatement}
                {SharedQuery.PaggingQuery}
                """;
        }


        public static string GetActorsByMovieId(ActorParameters parameters)
        {
            var orderByStatement = SharedQuery.GetOrderByQuery(typeof(ActorColumns), parameters.OrderDirection, parameters.OrderBy, "a");
            var joinStatement = $"""
                FROM "{Tables.MoviesActorsTable}" AS mv
                JOIN "{Tables.MovieTable}" AS m 
                ON m."{MovieColumns.Id}" = mv."{MoviesActorsColumns.MovieId}" AND m."{MovieColumns.Id}" = @Id
                JOIN "{Tables.ActorTable}" AS a
                ON a."{ActorColumns.Id}" = mv."{MoviesActorsColumns.ActorId}"
                """;

            var query = $"""
                SELECT COUNT(a."{ActorColumns.Id}")
                {joinStatement}
                WHERE {GetActorParametersFilter("a")};

                SELECT {ActorColumns.GetActorColumns("a")}
                {joinStatement}
                WHERE {GetActorParametersFilter("a")}
                {orderByStatement}
                {SharedQuery.PaggingQuery}
                """;

            return query;
        }

        public static string GetActorByIdQuery => $"""
            SELECT {ActorColumns.GetActorColumns()} FROM "{Tables.ActorTable}"
            WHERE "{ActorColumns.Id}" = @Id
            """;

        public const string CreateActorQuery = $"""
            INSERT INTO "{Tables.ActorTable}"
            ("{ActorColumns.Id}", "{ActorColumns.Name}", "{ActorColumns.Bio}", "{ActorColumns.PhotoUrl}")
            VALUES (gen_random_uuid(), @{nameof(Actor.Name)}, @{nameof(Actor.Bio)}, @{nameof(Actor.PhotoUrl)})
            RETURNING "{ActorColumns.Id}"
            """;

        public const string UpdateActorQuery = $"""
            UPDATE "{Tables.ActorTable}" SET
            "{ActorColumns.Name}" = @{nameof(Actor.Name)},
            "{ActorColumns.Bio}" = @{nameof(Actor.Bio)},
            "{ActorColumns.PhotoUrl}" = @{nameof(Actor.PhotoUrl)}
            WHERE "{ActorColumns.Id}" = @Id
            """;

        public const string DeleteActorQuery = $"""
            DELETE FROM "{Tables.ActorTable}"
            WHERE "{ActorColumns.Id}" = @Id
            """;

        public static string GetActorParametersFilter(string alias = null)
        {
            string aliasDot = string.IsNullOrEmpty(alias) ? "" : alias + '.';

            return $"""
            ({aliasDot}{ActorColumns.Name} ILIKE ('%' || @{nameof(ActorParameters.SearchedName)} || '%') OR @{nameof(ActorParameters.SearchedName)} IS NULL)
            """;
        }
    }
}
