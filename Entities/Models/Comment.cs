namespace Entities.Models
{
    public class Comment
    {
        public Guid Id { get; set; }
        public Guid UserName { get; set; }
        public Guid MovieId { get; set; }
        public string Text { get; set; }
        public DateTime DateAdded { get; set; }
    }
}
