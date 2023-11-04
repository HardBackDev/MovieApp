namespace Shared.Dtos.MovieDtos
{
    public record MovieDto
    {
        public Guid Id { get; init; }
        public Guid DirectorId { get; init; }
        public string Title { get; init; }
        public string Description { get; init; }
        public DateTime ReleaseDate { get; init; }
        public IEnumerable<string> Genres { get; init; }
        public string PhotoUrl { get; init; }
        public string VideoPath { get; set; }
        public int GoodRating { get; init; }
        public int BadRating { get; init; }
    }
}
