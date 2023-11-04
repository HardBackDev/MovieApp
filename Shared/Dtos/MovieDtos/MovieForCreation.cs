namespace Shared.Dtos.MovieDtos
{
    public record MovieForCreation
    {
        public string Title { get; init; }
        public string Description { get; init; }
        public DateTime ReleaseDate { get; init; }
        public string PhotoUrl { get; init; }
        public IEnumerable<string> Genres { get; init; }
        public Guid DirectorId { get; init; }
        public string VideoPath { get; init; }
    }
}
