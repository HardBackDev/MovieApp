namespace Shared.Dtos.CommentDtos
{
    public record CommentDto
    {
        public Guid Id { get; init; }
        public string UserName { get; init; }
        public Guid MovieId { get; init; }
        public string Text { get; init; }
        public DateTime DateAdded { get; init; }
    }
}
