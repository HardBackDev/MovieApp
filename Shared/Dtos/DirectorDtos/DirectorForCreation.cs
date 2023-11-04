namespace Shared.Dtos.DirectorDtos
{
    public record DirectorForCreation
    {
        public string Name { get; init; }
        public int Age { get; init; }
        public string Bio { get; init; }
        public string PhotoUrl { get; init; }
    }
}
