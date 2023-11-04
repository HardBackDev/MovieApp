
namespace Entities.Models
{
    public class Movie
    {
        public Guid Id { get; set; }
        public Guid DirectorId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Genres { get; set; }
        public string PhotoUrl { get; set; }
        public string? VideoPath { get; set; }
        public int GoodRating { get; set; }
        public int BadRating { get; set; }
    }
}
