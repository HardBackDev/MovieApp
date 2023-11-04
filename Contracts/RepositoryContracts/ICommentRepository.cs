using Shared.Dtos.CommentDtos;
using Shared.RequestFeatures;
using Shared.RequestFeatures.EntitiesParameters;

namespace Contracts.RepositoryContracts
{
    public interface ICommentRepository
    {
        Task<PagedList<CommentDto>> GetCommentsByMovie(Guid movieId, CommentParameters parameters);
        Task<CommentDto> GetCommentById(Guid id);
        Task AddCommentToMovie(Guid movieId, string UserName, string text);
        Task UpdateComment(Guid commentId, string text);
        Task DeleteComment(Guid commentId);
    }
}
