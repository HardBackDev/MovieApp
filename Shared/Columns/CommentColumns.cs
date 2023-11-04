using Entities.Models;
using System.Xml.Linq;

namespace Shared.Columns
{
    public static class CommentColumns
    {
        public const string Id = "comment_id";
        public const string UserName = "comment_user_name";
        public const string MovieId = "comment_movie_id";
        public const string Text = "comment_text";
        public const string DateAdded = "comment_date_added";

        public static string GetCommentColumns(string alias = null)
        {
            var aliasDot = string.IsNullOrEmpty(alias) ? "" : alias + ".";
            return $"""
                {aliasDot}{Id} AS {nameof(Comment.Id)},
                {aliasDot}{UserName} AS {nameof(Comment.UserName)},
                {aliasDot}{MovieId} AS {nameof(Comment.MovieId)},
                {aliasDot}{Text} AS {nameof(Comment.Text)},
                {aliasDot}{DateAdded}  AS {nameof(Comment.DateAdded)}
                """;
        }
    }
}
