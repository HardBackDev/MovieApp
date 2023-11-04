using Entities.Models;
using Shared;
using Shared.Columns;
using Shared.RequestFeatures.EntitiesParameters;

namespace Repositories.Queries
{
    public static class CommentQuery
    {
        public static string GetMovieCommentsByParametersQuery(CommentParameters parameters)
        {
            var orderByStatement = SharedQuery.GetOrderByQuery(typeof(CommentColumns), parameters.OrderDirection, parameters.OrderBy);
            var whereStatement = $"""
                WHERE ({CommentColumns.Text} ILIKE ('%' || @{nameof(CommentParameters.SearchedText)} || '%') OR @{nameof(CommentParameters.SearchedText)} IS NULL)
                AND {CommentColumns.MovieId} = @Id
                """;

            return $"""
                SELECT COUNT({CommentColumns.Id})
                FROM "{Tables.CommentTable}"
                {whereStatement};

                SELECT {CommentColumns.GetCommentColumns()} FROM "{Tables.CommentTable}"
                {whereStatement}
                {orderByStatement}
                {SharedQuery.PaggingQuery};
                """;
        }

        public static string GetCommentByIdQuery = $"""
            SELECT {CommentColumns.GetCommentColumns()} FROM "{Tables.CommentTable}"
            WHERE "{CommentColumns.Id}" = @Id
            """;    

        public const string CreateMovieCommentQuery = $"""
            INSERT INTO "{Tables.CommentTable}"
            ("{CommentColumns.Id}", "{CommentColumns.Text}", "{CommentColumns.UserName}", "{CommentColumns.MovieId}", "{CommentColumns.DateAdded}")
            VALUES (gen_random_uuid(), @{nameof(Comment.Text)}, @{nameof(Comment.UserName)}, @{nameof(Comment.MovieId)}, NOW())
            """;

        public const string UpdateMovieCommentQuery = $"""
            UPDATE "{Tables.CommentTable}" SET
            "{CommentColumns.Text}" = @Text
            WHERE "{CommentColumns.Id}" = @Id;
            """;

        public const string DeleteMovieCommentQuery = $"""
            DELETE FROM "{Tables.CommentTable}"
            WHERE {CommentColumns.Id} = @Id;
            """;
    }
}
