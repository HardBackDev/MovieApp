using Contracts.RepositoryContracts;
using Contracts.ServiceContracts;
using Entities.Models;
using Shared.Dtos.ActorDtos;
using Shared.Exceptions;
using Shared.RequestFeatures.EntitiesParameters;
using Shared.RequestFeatures;

namespace Service
{
    public class ActorService : IActorService
    {
        readonly IRepositoryManager _repo;

        public ActorService(IRepositoryManager repo)
        {
            _repo = repo;
        }

        public async Task<PagedList<ActorDto>> GetActors(ActorParameters parameters)
        {
            return await _repo.ActorRepo.GetActors(parameters);
        }

        public async Task<PagedList<ActorDto>> GetActorsByMovieId(Guid id, ActorParameters parameters)
        {
            if(await _repo.MovieRepo.GetMovieById(id) is null)
                throw new NotFoundException(nameof(Movie), id);
            return await _repo.ActorRepo.GetActorsByMovieId(id, parameters);
        }

        public async Task<ActorDto> GetActorById(Guid id)
        {
            var actor = await TryGetActor(id);

            return actor;
        }

        public async Task UpdateActor(Guid id, ActorForUpdate Actor)
        {
            await TryGetActor(id);
            await _repo.ActorRepo.UpdateActor(id, Actor);
        }

        public async Task<ActorDto> CreateActor(ActorForCreation actor)
        {
            var createdId = await _repo.ActorRepo.CreateActor(actor);
            return new ActorDto
            {
                Id = createdId,
                Bio = actor.Bio,
                Name = actor.Name,
                PhotoUrl = actor.PhotoUrl,
            };
        }

        public async Task DeleteActor(Guid id)
        {
            await TryGetActor(id);
            await _repo.ActorRepo.DeleteActor(id);
        }

        public async Task AddMovieRelation(Guid movieId, Guid actorId)
        {
            await TryGetActor(actorId);
            if (await _repo.MovieRepo.GetMovieById(movieId) is null)
                throw new NotFoundException(nameof(Movie), movieId);
            await _repo.ActorRepo.AddMovieRelation(movieId, actorId);
        }

        private async Task<ActorDto> TryGetActor(Guid id)
        {
            return await _repo.ActorRepo.GetActorById(id)
                ?? throw new NotFoundException(nameof(Actor), id);
        }
    }
}
