using Contracts.ServiceContracts;
using MovieApp.Presentation.Filtres;
using Microsoft.AspNetCore.Mvc;
using Shared.Dtos.MovieDtos;
using System.Text.Json;
using Shared.RequestFeatures.EntitiesParameters;
using Microsoft.AspNetCore.Authorization;
using Shared.RequestFeatures;
using MovieApp.Presentation.Extensions;

namespace MovieApp.Presentation.Controllers
{
    [ApiController]
    [Route("api/movies")]
    public class MovieController : ControllerBase
    {
        readonly IServiceManager _service;

        public MovieController(IServiceManager service)
        {
            _service = service;
        }

        [HttpGet]
        [ServiceFilter(typeof(ValidationFilter))]
        public async Task<IActionResult> GetMovies([FromQuery] MovieParameters parameters)
        {
            var pagedResult = await _service.MovieService.GetMovies(parameters);

            Response.AddPaginationHeader(pagedResult.MetaData);
            return Ok(pagedResult);
        }

        [HttpGet("withActors")]
        [ServiceFilter(typeof(ValidationFilter))]
        public async Task<IActionResult> GetMoviesWithActors([FromQuery] MovieParameters parameters, [FromQuery] int actorsCount)
        {
            var pagedResult = await _service.MovieService.GetMoviesWithActors(parameters, actorsCount);

            Response.AddPaginationHeader(pagedResult.MetaData);
            return Ok(pagedResult);
        }

        [HttpGet("byActor/{id:Guid}", Name = "GetMovieActors")]
        [ServiceFilter(typeof(ValidationFilter))]
        public async Task<IActionResult> GetMoviesByActor(Guid id, [FromQuery] MovieParameters parameters)
        {
            var pagedResult = await _service.MovieService.GetMoviesByActorId(id, parameters);

            Response.AddPaginationHeader(pagedResult.MetaData);
            return Ok(pagedResult);
        }

        [HttpGet("byDirector/{id:Guid}")]
        [ServiceFilter(typeof(ValidationFilter))]
        public async Task<IActionResult> GetMoviesByDirector(Guid id, [FromQuery] MovieParameters parameters)
        {
            var pagedResult = await _service.MovieService.GetMoviesByDirector(id, parameters);

            Response.AddPaginationHeader(pagedResult.MetaData);
            return Ok(pagedResult);
        }

        [Authorize]
        [HttpGet("toWatch")]
        [ServiceFilter(typeof(ValidationFilter))]
        public async Task<IActionResult> GetMoviesToWatch([FromQuery] MovieParameters parameters)
        {
            var pagedResult = await _service.MovieService.GetMoviesToWatch(parameters, User.Identity.Name);

            Response.AddPaginationHeader(pagedResult.MetaData);
            return Ok(pagedResult);
        }

        [HttpGet("{id:Guid}", Name = "MovieById")]
        public async Task<IActionResult> GetMovieById(Guid id)
        {
            return Ok(await _service.MovieService.GetMovieById(id));
        }

        [Authorize]
        [HttpGet("checkInWatching/{id:Guid}")]
        public async Task<IActionResult> CheckMovieInWatching(Guid id)
        {
            return Ok(await _service.MovieService.CheckMovieInWatching(id, User.Identity.Name));
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ServiceFilter(typeof(ValidationFilter))]
        public async Task<IActionResult> CreateMovie([FromBody] MovieForCreation movie)
        {
            var createdMovie = await _service.MovieService.CreateMovie(movie);
            
            return CreatedAtRoute("MovieById", new { id = createdMovie.Id }, createdMovie);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id:Guid}")]
        [ServiceFilter(typeof(ValidationFilter))]
        public async Task<IActionResult> UpdateMovie(Guid id, [FromBody] MovieForUpdate movie)
        {
            await _service.MovieService.UpdateMovie(id, movie);
            return Ok();
        }

        [Authorize]
        [HttpPost("addToWatching/{movieId:Guid}")]
        [ServiceFilter(typeof(ValidationFilter))]
        public async Task<IActionResult> AddMovieToWatching(Guid movieId)
        {
            await _service.MovieService.AddMovieToWatching(movieId, User.Identity.Name);
            return Ok();
        }


        [Authorize(Roles = "Admin")]
        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> DeleteMovie(Guid id)
        {
            await _service.MovieService.DeleteMovie(id);
            Console.WriteLine("is");
            return Ok();
        }

        [Authorize]
        [HttpDelete("deleteFromWatching/{id:Guid}")]
        public async Task<IActionResult> DeleteMovieFromWatching(Guid id)
        {
            await _service.MovieService.DeleteMovieFromWatching(id, User.Identity.Name);
            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("addActor/{movieId:Guid}/{actorId:Guid}")]
        public async Task<IActionResult> AddActorRelation(Guid movieId, Guid actorId)
        {
            await _service.MovieService.AddActorRelation(movieId, actorId);
            return Ok();
        }

        [Authorize]
        [HttpPost("rate/{id:Guid}/{rate}")]
        public async Task<IActionResult> RateMovie(Guid id, string rate)
        {
            await _service.MovieService.RateMovie(id, User.Identity.Name, rate);
            return Ok();
        }

        [Authorize]
        [HttpDelete("unRate")]
        public async Task<IActionResult> UnRateMovie([FromQuery] Guid id)
        {
            await _service.MovieService.UnRateMovie(id, User.Identity.Name);
            return Ok();
        }

        [Authorize]
        [HttpGet("getRate/{id:Guid}")]
        public async Task<IActionResult> GetMovieRate(Guid id)
        {
            return Ok(await _service.MovieService.GetMovieRate(id, User.Identity.Name));
        }
    }
}
