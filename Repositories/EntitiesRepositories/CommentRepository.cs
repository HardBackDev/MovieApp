using Contracts.RepositoryContracts;
using Dapper;
using Repositories.Queries;
using Repository;
using Shared.Dtos.CommentDtos;
using Shared.RequestFeatures;
using Shared.RequestFeatures.EntitiesParameters;

namespace Repositories.EntitiesRepositories
{
    public class CommentRepository : ICommentRepository
    {
        readonly DapperContext _context;

        public CommentRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<PagedList<CommentDto>> GetCommentsByMovie(Guid movieId, CommentParameters parameters)
        {
            var query = CommentQuery.GetMovieCommentsByParametersQuery(parameters);
            var param = new DynamicParameters(parameters);
            param.Add("Id", movieId);

            using var connection = _context.CreateConnection();
            using var multi = await connection.QueryMultipleAsync(query, param);

            var count = await multi.ReadSingleAsync<int>();
            var comments = (await multi.ReadAsync<CommentDto>()).ToList();

            return new PagedList<CommentDto>(comments, count, parameters.PageNumber, parameters.PageSize);
        }

        public async Task<CommentDto> GetCommentById(Guid id)
        {
            var query = CommentQuery.GetCommentByIdQuery;
            using var connection = _context.CreateConnection();
            return await connection.QuerySingleOrDefaultAsync<CommentDto>(query, new { Id = id });
        }

        public async Task AddCommentToMovie(Guid movieId, string userName, string text)
        {
            var query = CommentQuery.CreateMovieCommentQuery;
            var param = new
            {
                UserName = userName,
                MovieId = movieId,
                Text = text
            };
            using var connection = _context.CreateConnection();
            await connection.ExecuteAsync(query, param);
        }

        public async Task DeleteComment(Guid commentId)
        {
            var query = CommentQuery.DeleteMovieCommentQuery;
            using var connection = _context.CreateConnection();
            await connection.ExecuteAsync(query, new { Id =  commentId });
        }

        public async Task UpdateComment(Guid commentId, string text)
        {
            var query = CommentQuery.UpdateMovieCommentQuery;
            using var connection = _context.CreateConnection();
            await connection.ExecuteAsync(query, new { Text = text, Id = commentId });
        }
    }
}
