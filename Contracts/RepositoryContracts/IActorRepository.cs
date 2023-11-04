using Entities.Models;
using Shared.Dtos.ActorDtos;
using Shared.Dtos.MovieDtos;
using Shared.RequestFeatures;
using Shared.RequestFeatures.EntitiesParameters;

namespace Contracts.RepositoryContracts
{
    public interface IActorRepository
    {
        Task<PagedList<ActorDto>> GetActors(ActorParameters parameters);
        Task<ActorDto> GetActorById(Guid id);
        Task<PagedList<ActorDto>> GetActorsByMovieId(Guid id, ActorParameters parameters);
        Task<Guid> CreateActor(ActorForCreation movie);
        Task AddMovieRelation(Guid movieId, Guid actorId);
        Task UpdateActor(Guid id, ActorForUpdate movie);
        Task DeleteActor(Guid id);

    }
}
