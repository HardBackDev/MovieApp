using Contracts.RepositoryContracts;
using Contracts.ServiceContracts;
using Entities.Models;
using Identity.Dapper.Postgres.Models;
using Microsoft.AspNetCore.Identity;
using Shared.Dtos.CommentDtos;
using Shared.Exceptions;
using Shared.RequestFeatures;
using Shared.RequestFeatures.EntitiesParameters;

namespace Service
{
    public class CommentService : ICommentService
    {
        readonly IRepositoryManager _repo;
        readonly UserManager<ApplicationUser> _userManager;

        public CommentService(IRepositoryManager repo, UserManager<ApplicationUser> userManager)
        {
            _repo = repo;
            _userManager = userManager;
            
        }

        public async Task<PagedList<CommentDto>> GetCommentsByMovie(Guid movieId, CommentParameters parameters)
        {
            await CheckMovieExist(movieId);
            return await _repo.CommentRepo.GetCommentsByMovie(movieId, parameters);
        }

        public async Task AddCommentToMovie(Guid movieId, string userName, string text)
        {
            await CheckMovieExist(movieId);

            await _repo.CommentRepo.AddCommentToMovie(movieId, userName, text);
        }

        public async Task DeleteComment(Guid commentId, string userName)
        {
            var comment = await CheckCommentExist(commentId);
            var user = await CheckUserExist(userName);
            var userRoles = await _userManager.GetRolesAsync(user);
            var role = userRoles.FirstOrDefault();
            
            if(user.UserName != comment.UserName && role != "Admin")
            {
                throw new BadRequestException("user without admin role cannot edit other comments");
            }
            await _repo.CommentRepo.DeleteComment(commentId);
        }

        public async Task UpdateComment(Guid commentId, string text, string userName)
        {
            await CheckCommentExist(commentId);
            var comment = await CheckCommentExist(commentId);
            var user = await CheckUserExist(userName);
            var userRoles = await _userManager.GetRolesAsync(user);
            var role = userRoles.FirstOrDefault();

            if (user.UserName != comment.UserName && role != "Admin")
            {
                throw new BadRequestException("user without admin role cannot edit other comments");
            }

            await _repo.CommentRepo.UpdateComment(commentId, text);
        }

        private async Task<CommentDto> CheckCommentExist(Guid commentId)
        {
            return await _repo.CommentRepo.GetCommentById(commentId) ??
                throw new NotFoundException(nameof(Comment), commentId);
        }

        private async Task CheckMovieExist(Guid movieId)
        {
            if(await _repo.MovieRepo.GetMovieById(movieId) is null)
                throw new NotFoundException(nameof(Movie), movieId);
        }

        private async Task<ApplicationUser> CheckUserExist(string userName)
        {
            
            return await _userManager.FindByNameAsync(userName) ??
                throw new NotFoundException("User", userName);
        }
    }
}
