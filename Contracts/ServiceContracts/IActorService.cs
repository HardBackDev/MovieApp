using Shared.RequestFeatures.EntitiesParameters;
using Shared.RequestFeatures;
using Shared.Dtos.ActorDtos;
using Shared.Dtos.MovieDtos;

namespace Contracts.ServiceContracts
{
    public interface IActorService
    {
        Task<PagedList<ActorDto>> GetActors(ActorParameters parameters);
        Task<ActorDto> GetActorById(Guid id);
        Task<PagedList<ActorDto>> GetActorsByMovieId(Guid id, ActorParameters parameters);
        Task AddMovieRelation(Guid movieId, Guid actorId); 
        Task<ActorDto> CreateActor(ActorForCreation Actor);
        Task UpdateActor(Guid id, ActorForUpdate Actor);
        Task DeleteActor(Guid id);

    }
}
