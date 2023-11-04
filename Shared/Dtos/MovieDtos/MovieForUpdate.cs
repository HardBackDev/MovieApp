namespace Shared.Dtos.MovieDtos
{
    public class MovieForUpdate
    {
        public string Title { get; init; }
        public Guid DirectorId { get; init; }
        public string Description { get; init; }
        public DateTime ReleaseDate { get; init; }
        public string PhotoUrl { get; init; }
        public string VideoPath { get; set; }
        public IEnumerable<string> Genres { get; init; }

    }
}
