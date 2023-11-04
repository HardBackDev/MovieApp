using Entities.Models;

namespace Shared.Columns
{
    public static class DirectorColumns
    {
        public const string Id = "director_id";
        public const string Name = "director_name";
        public const string Age = "director_age";
        public const string Bio = "director_bio";
        public const string PhotoUrl = "director_photo_url";

        public static string GetDirectorColumns(string alias = null)
        {
            var aliasDot = string.IsNullOrEmpty(alias) ? "" : alias + ".";
            return $"""
                {aliasDot}{Id} AS {nameof(Director.Id)},
                {aliasDot}{Age} AS {nameof(Director.Age)},
                {aliasDot}{Bio} AS {nameof(Director.Bio)},
                {aliasDot}{Name} AS {nameof(Director.Name)},
                {aliasDot}{PhotoUrl}  AS {nameof(Director.PhotoUrl)}
                """;
        }
    }
}
