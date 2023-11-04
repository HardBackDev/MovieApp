using Contracts.RepositoryContracts;
using Contracts.ServiceContracts;
using Entities.Models;
using Shared.Dtos.DirectorDtos;
using Shared.Dtos.MovieDtos;
using Shared.Exceptions;
using Shared.RequestFeatures;
using Shared.RequestFeatures.EntitiesParameters;

namespace Service
{
    public class DirectorService : IDirectorService
    {
        readonly IRepositoryManager _repo;

        public DirectorService(IRepositoryManager repositoryManager)
        {
            _repo = repositoryManager;
        }

        public async Task<PagedList<DirectorDto>> GetDirectors(DirectorParameters parameters)
        {
            var pagedResult = await _repo.DirectorRepo.GetDirectors(parameters);
            return new PagedList<DirectorDto>(pagedResult.directors.ToList(), pagedResult.count, parameters.PageNumber, parameters.PageSize);
        }
        public async Task<DirectorDto> GetDirector(Guid id) => await TryGetDirector(id);

        public async Task<DirectorDto> CreateDirector(DirectorForCreation director)
        {
            var createdId = await _repo.DirectorRepo.CreateDirector(director);
            return new DirectorDto
            {
                Id = createdId,
                Age = director.Age,
                Bio = director.Bio,
                Name = director.Name,
                PhotoUrl = director.PhotoUrl,
            };
        }

        public async Task DeleteDirector(Guid id)
        {
            await TryGetDirector(id);
            await _repo.DirectorRepo.DeleteDirector(id);
        }

        public async Task UpdateDirector(Guid id, DirectorForUpdate director)
        {
            await TryGetDirector(id);
            await _repo.DirectorRepo.UpdateDirector(id, director);
        }

        public async Task<DirectorDto> TryGetDirector(Guid id) => 
            await _repo.DirectorRepo.GetDirector(id) ??
                throw new NotFoundException(nameof(Director), id);
        
    }
}
