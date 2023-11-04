using Contracts.RepositoryContracts;
using Dapper;
using Entities.Models;
using Repositories.Queries;
using Repository;
using Shared.Dtos.ActorDtos;
using Shared.Dtos.MovieDtos;
using Shared.RequestFeatures;
using Shared.RequestFeatures.EntitiesParameters;

namespace Repositories.EntitiesRepositories
{
    public class ActorRepository : IActorRepository
    {
        readonly DapperContext _context;
        public ActorRepository(DapperContext context)
        {
            _context = context;
        }


        public async Task<PagedList<ActorDto>> GetActors(ActorParameters parameters)
        {
            var query = ActorQuery.GetActorsByParametersQuery(parameters);
            var param = new DynamicParameters(parameters);

            var connection = _context.CreateConnection();
            using var multi = await connection.QueryMultipleAsync(query, param);

            var count = await multi.ReadSingleAsync<int>();
            var actors = (await multi.ReadAsync<ActorDto>()).ToList();

            return new PagedList<ActorDto>(actors, count, parameters.PageNumber, parameters.PageSize);
        }

        public async Task<ActorDto> GetActorById(Guid id)
        {
            var query = ActorQuery.GetActorByIdQuery;

            var connection = _context.CreateConnection();
            var actor = await connection.QuerySingleOrDefaultAsync<ActorDto>(query, new { Id = id });
            return actor;
        }

        public async Task<PagedList<ActorDto>> GetActorsByMovieId(Guid id, ActorParameters parameters)
        {
            var query = ActorQuery.GetActorsByMovieId(parameters);
            var param = new DynamicParameters(parameters);
            param.Add("Id", id);

            using var connection = _context.CreateConnection();
            using var multi = await connection.QueryMultipleAsync(query, param);

            var count = await multi.ReadSingleAsync<int>();
            var actors = (await multi.ReadAsync<ActorDto>()).ToList();

            return new PagedList<ActorDto>(actors, count, parameters.PageNumber, parameters.PageSize);
        }

        public async Task AddMovieRelation(Guid movieId, Guid actorId)
        {
            var query = MovieActorQuery.CreateMovieActorRelationQuery;

            using var connection = _context.CreateConnection();
            await connection.ExecuteAsync(query, new
            {
                movieId,
                actorId
            });
        }

        public async Task<Guid> CreateActor(ActorForCreation actorForCreation)
        {
            var query = ActorQuery.CreateActorQuery;
            var param = new DynamicParameters(actorForCreation);

            var connection = _context.CreateConnection();
            var createdId = await connection.QuerySingleAsync<Guid>(query, param);

            return createdId;
        }

        public async Task DeleteActor(Guid id)
        {
            var query = ActorQuery.DeleteActorQuery;

            var connection = _context.CreateConnection();
            await connection.ExecuteAsync(query, new { Id = id });
        }

        public async Task UpdateActor(Guid id, ActorForUpdate actor)
        {
            var query = ActorQuery.UpdateActorQuery;
            var param = new DynamicParameters(actor);
            param.Add("Id", id);

            var connection = _context.CreateConnection();
            await connection.ExecuteAsync(query, param);
        }
    }
}
