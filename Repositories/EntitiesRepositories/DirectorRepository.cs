using Contracts.RepositoryContracts;
using Dapper;
using Entities.Models;
using Repositories.Queries;
using Repository;
using Shared.Dtos.DirectorDtos;
using Shared.RequestFeatures.EntitiesParameters;

namespace Repositories.EntitiesRepositories
{

    public class DirectorRepository : IDirectorRepository
    {
        readonly DapperContext _context;
        public DirectorRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<(int count, IEnumerable<DirectorDto> directors)> GetDirectors(DirectorParameters parameters)
        {
            var query = DirectorQuery.SelectDirectorsQuery(parameters);
            var param = new DynamicParameters(parameters);
            using var connection = _context.CreateConnection();
            using var multi = await connection.QueryMultipleAsync(query, param);

            var count = await multi.ReadSingleAsync<int>();
            var directors = (await multi.ReadAsync<DirectorDto>()).ToList();

            return (count, directors);
        }

        public async Task<DirectorDto> GetDirector(Guid id)
        {
            var query = DirectorQuery.GetDirectorQuery;
            using var connection = _context.CreateConnection();

            return await connection.QuerySingleOrDefaultAsync<DirectorDto>(query, new {id});
        }

        public async Task<Guid> CreateDirector(DirectorForCreation director)
        {
            var query = DirectorQuery.CreateDirectorQuery;
            using var connection = _context.CreateConnection();
            var param = new DynamicParameters(director);

            return await connection.QuerySingleAsync<Guid>(query, param);
        }

        public async Task UpdateDirector(Guid id, DirectorForUpdate director)
        {
            var query = DirectorQuery.UpdateDirectorQuery;
            using var connection = _context.CreateConnection();
            var param = new DynamicParameters(director);
            param.Add("id", id);

            await connection.ExecuteAsync(query, param);
        }

        public async Task DeleteDirector(Guid id)
        {
            var query = DirectorQuery.DeleteDirectorQuery;
            using var connection = _context.CreateConnection();
            
            await connection.ExecuteAsync(query, new { id });
        }
    }
}
