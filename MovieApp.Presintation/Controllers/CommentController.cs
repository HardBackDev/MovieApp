using Contracts.ServiceContracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieApp.Presentation.Extensions;
using Shared.RequestFeatures.EntitiesParameters;
using System.Text;

namespace MovieApp.Presentation.Controllers
{
    [ApiController]
    [Route("api/comments")]
    public class CommentController : ControllerBase
    {
        readonly IServiceManager _service;

        public CommentController(IServiceManager service)
        {
            _service = service;
        }

        [HttpGet("{movieId:Guid}", Name = "GetMovieComments")]
        public async Task<IActionResult> GetMovieComments(Guid movieId, [FromQuery] CommentParameters parameters)
        {
            var pagedResult = await _service.CommentService.GetCommentsByMovie(movieId, parameters);

            Response.AddPaginationHeader(pagedResult.MetaData);
            return Ok(pagedResult);
        }

        [Authorize]
        [HttpPost("{movieId:Guid}")]
        public async Task<IActionResult> AddCommentToMovie(Guid movieId)
        {
            string text;
            using (StreamReader reader = new StreamReader(Request.Body, Encoding.UTF8))
            {
                text = await reader.ReadToEndAsync();
            }
            if (text is null)
                BadRequest("empty comment text!");
            if (text.Length > 500)
                BadRequest("maximum comment length is 500!");

            var userName = User.Identity.Name;
            await _service.CommentService.AddCommentToMovie(movieId, userName, text);
            return RedirectToAction("GetMovieComments", new { movieId });
        }

        [Authorize]
        [HttpPut("{commentId:Guid}")]
        public async Task<IActionResult> EditComment(Guid commentId)
        {
            string text;
            using (StreamReader reader = new StreamReader(Request.Body, Encoding.UTF8))
            {
                text = await reader.ReadToEndAsync();
            }
            if (text is null)
                BadRequest("comment text is empty");
            if (text.Length > 500)
                BadRequest("maximum comment length is 500!");

            await _service.CommentService.UpdateComment(commentId, text, User.Identity.Name);
            return Ok();
        }

        [Authorize]
        [HttpDelete("{commentId:Guid}")]
        public async Task<IActionResult> DeleteComment(Guid commentId)
        {
            await _service.CommentService.DeleteComment(commentId, User.Identity.Name);
            return Ok();
        }
    }
}
