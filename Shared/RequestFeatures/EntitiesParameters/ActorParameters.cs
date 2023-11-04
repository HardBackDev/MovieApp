using Shared.Dtos.ActorDtos;

namespace Shared.RequestFeatures.EntitiesParameters
{
    public class ActorParameters : RequestParameters
    {
        public ActorParameters() 
        {
            OrderBy = nameof(ActorDto.Name);
        }
        public string? SearchedName { get; set; } = "";
        
    }
}
