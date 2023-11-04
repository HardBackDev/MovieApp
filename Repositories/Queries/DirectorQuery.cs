using Entities.Models;
using Shared;
using Shared.Columns;
using Shared.RequestFeatures.EntitiesParameters;

namespace Repositories.Queries
{
    public static class DirectorQuery
    {
        public static string SelectDirectorsQuery(DirectorParameters parameters)
        {
            var orderByStatement = SharedQuery.GetOrderByQuery(typeof(DirectorColumns), parameters.OrderDirection, parameters.OrderBy);
            
            return $"""
                SELECT COUNT("{DirectorColumns.Id}") FROM "{Tables.DirectorTable}"
                WHERE {DirectorParametersFilter};

                SELECT {DirectorColumns.GetDirectorColumns()} FROM "{Tables.DirectorTable}"
                WHERE {DirectorParametersFilter}
                {orderByStatement}
                {SharedQuery.PaggingQuery};
                """;
        }

        public static string GetDirectorQuery = $"""
            SELECT {DirectorColumns.GetDirectorColumns()} FROM "{Tables.DirectorTable}"
            WHERE "{DirectorColumns.Id}" = @id
            """;

        public const string CreateDirectorQuery = $"""
            INSERT INTO "{Tables.DirectorTable}"
            ("{DirectorColumns.Id}", "{DirectorColumns.Bio}", "{DirectorColumns.Age}", "{DirectorColumns.Name}", "{DirectorColumns.PhotoUrl}")
            VALUES (gen_random_uuid(), @{nameof(Director.Bio)}, @{nameof(Director.Age)}, @{nameof(Director.Name)}, @{nameof(Director.PhotoUrl)})
            RETURNING "{DirectorColumns.Id}"
            """;

        public const string UpdateDirectorQuery = $"""
            UPDATE "{Tables.DirectorTable}" SET
            "{DirectorColumns.Age}" = @{nameof(Director.Age)},
            "{DirectorColumns.Bio}" = @{nameof(Director.Bio)},
            "{DirectorColumns.Name}" = @{nameof(Director.Name)},
            "{DirectorColumns.PhotoUrl}" = @{nameof(Director.PhotoUrl)}
            WHERE "{DirectorColumns.Id}" = @id
            """;

        public const string DeleteDirectorQuery = $"""
            DELETE FROM "{Tables.DirectorTable}"
            WHERE "{DirectorColumns.Id}" = @id
            """;

        public const string DirectorParametersFilter = $"""
            "{DirectorColumns.Name}" ILIKE ('%' || @{nameof(DirectorParameters.SearchedName)} || '%')
            """;

        
    }
}
