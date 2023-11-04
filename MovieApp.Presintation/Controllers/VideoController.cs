using Contracts.ServiceContracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Shared.Dtos.MovieDtos;
using System.IO;
using System.Net.Http.Headers;

namespace MovieApp.Presentation.Controllers
{
    [Controller]
    [Route("api/video")]
    public class VideoController : ControllerBase
    {
        private readonly IHostingEnvironment _webHostEnvironment;

        public VideoController(IHostingEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("upload")]
        [RequestSizeLimit(long.MaxValue)]
        [RequestFormLimits(BufferBody = true, BufferBodyLengthLimit = long.MaxValue, MultipartBodyLengthLimit = long.MaxValue)]
        public IActionResult UploadVideo(IFormFile File)
        {
            var fileExtension = Path.GetExtension(File.FileName);
            if (fileExtension != ".mp4")
                return BadRequest("only video with mp4 extension can be uploaded");

            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", File.FileName);
            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                File.CopyTo(fileStream);
            }

            return Ok(new { path });
        }

        [HttpGet("play"), DisableRequestSizeLimit]
        public IActionResult PlayVideo([FromQuery] string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
                return BadRequest("file path is null");

            if (_webHostEnvironment.IsEnvironment("Docker"))
            {
                filePath = filePath.Replace("\\", "/");
            }

            if (!System.IO.File.Exists(filePath))
            {
                return NotFound();
            }

            var videoStream = System.IO.File.OpenRead(filePath);
            var fileStreamResponse = new FileStreamResult(videoStream, "video/mp4");
            fileStreamResponse.EnableRangeProcessing = true;
            return fileStreamResponse;
        }
    }
}
