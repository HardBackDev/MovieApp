using Entities.Models;

namespace Shared.Columns
{
    public static class ActorColumns
    {
        public const string Id = "actor_id";
        public const string Name = "actor_name";
        public const string Bio = "actor_bio";
        public const string PhotoUrl = "actor_photo_url";

        public static string GetActorColumns(string alias = null)
        {
            string aliasDot = string.IsNullOrEmpty(alias) ? "" : alias + '.';

            return $"""
            {aliasDot}"{Id}" AS {nameof(Actor.Id)},
            {aliasDot}"{Bio}" AS {nameof(Actor.Bio)},
            {aliasDot}"{Name}" AS {nameof(Actor.Name)},
            {aliasDot}"{PhotoUrl}" AS {nameof(Actor.PhotoUrl)}
            """;
        }
    }
}
