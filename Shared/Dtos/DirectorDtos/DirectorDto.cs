namespace Shared.Dtos.DirectorDtos
{
    public record DirectorDto
    {
        public Guid Id { get; init; }
        public string Name { get; init; }
        public int Age { get; init; }
        public string Bio { get; init; }
        public string PhotoUrl { get; init; }
    }
}
