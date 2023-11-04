using Shared.Dtos.DirectorDtos;
using Shared.RequestFeatures;
using Shared.RequestFeatures.EntitiesParameters;

namespace Contracts.ServiceContracts
{
    public interface IDirectorService
    {
        Task<PagedList<DirectorDto>> GetDirectors(DirectorParameters parameters);
        Task<DirectorDto> GetDirector(Guid id);
        Task<DirectorDto> CreateDirector(DirectorForCreation director);
        Task UpdateDirector(Guid id, DirectorForUpdate director);
        Task DeleteDirector(Guid id);

    }
}
