using Shared.Dtos.DirectorDtos;
using Shared.RequestFeatures.EntitiesParameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.RepositoryContracts
{
    public interface IDirectorRepository
    {
        Task<(int count, IEnumerable<DirectorDto> directors)> GetDirectors(DirectorParameters parameters);
        Task<DirectorDto> GetDirector(Guid id);
        Task<Guid> CreateDirector(DirectorForCreation director);
        Task UpdateDirector(Guid id, DirectorForUpdate director);
        Task DeleteDirector(Guid id);
    }
}
