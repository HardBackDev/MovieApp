namespace Shared.Dtos.ActorDtos
{
    public record ActorForCreation
    {
        public string Name { get; init; }
        public string Bio { get; init; }
        public string PhotoUrl { get; init; }
    }
}
