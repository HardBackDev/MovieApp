namespace Entities.Models
{
    public class RatedMovie
    {
        public string UserName { get; set; }
        public Guid MovieId { get; set; }
        public string Rate { get; set; }
    }
}
