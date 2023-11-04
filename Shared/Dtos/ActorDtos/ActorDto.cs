namespace Shared.Dtos.ActorDtos
{
    public record ActorDto
    {
        public Guid Id { get; init; }
        public string Name { get; init; }
        public string Bio { get; init; }
        public string PhotoUrl { get; init; }
    }
}
