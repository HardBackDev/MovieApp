using Shared.Dtos.CommentDtos;
using Shared.RequestFeatures;
using Shared.RequestFeatures.EntitiesParameters;

namespace Contracts.ServiceContracts
{
    public interface ICommentService
    {
        Task<PagedList<CommentDto>> GetCommentsByMovie(Guid movieId, CommentParameters parameters);
        Task AddCommentToMovie(Guid movieId, string UserName, string text);
        Task UpdateComment(Guid commentId, string text, string userName);
        Task DeleteComment(Guid commentId, string userName);
    }
}
