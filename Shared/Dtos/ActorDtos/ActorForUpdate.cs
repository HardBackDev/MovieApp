namespace Shared.Dtos.ActorDtos
{
    public record ActorForUpdate
    {
        public string Name { get; init; }
        public string Bio { get; init; }
        public string PhotoUrl { get; init; }
    }
}
