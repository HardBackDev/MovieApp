using Contracts.ServiceContracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieApp.Presentation.Extensions;
using Shared.Dtos.ActorDtos;
using Shared.RequestFeatures;
using Shared.RequestFeatures.EntitiesParameters;
using System.Text.Json;

namespace MovieApp.Presentation.Controllers
{
    [ApiController]
    [Route("api/actors")]
    public class ActorController : ControllerBase
    {
        readonly IServiceManager _service;

        public ActorController(IServiceManager service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetActors([FromQuery] ActorParameters parameters)
        {
            var pagedResult = await _service.ActorService.GetActors(parameters);

            Response.AddPaginationHeader(pagedResult.MetaData);
            return Ok(pagedResult);
        }

        [HttpGet("{id:Guid}", Name = "GetActorById")]
        public async Task<IActionResult> GetActorById(Guid id)
        {
            return Ok(await _service.ActorService.GetActorById(id));
        }

        [HttpGet("byMovie/{id:Guid}", Name = "GetActorMovies")]
        public async Task<IActionResult> GetActorsByMovie(Guid id, [FromQuery] ActorParameters parameters)
        {
            var pagedResult = await _service.ActorService.GetActorsByMovieId(id, parameters);
            Response.AddPaginationHeader(pagedResult.MetaData);
            return Ok(pagedResult);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateActor([FromBody] ActorForCreation actor)
        {
            var createdActor = await _service.ActorService.CreateActor(actor);
            return CreatedAtRoute("GetActorById", new { Id = createdActor.Id }, createdActor);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("{actorId:Guid}/{movieId:Guid}")]
        public async Task<IActionResult> AddMovieRelation(Guid actorId, Guid movieId)
        {
            await _service.ActorService.AddMovieRelation(movieId, actorId);
            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{Id:Guid}")]
        public async Task<IActionResult> UpdateActor(Guid id, [FromBody] ActorForUpdate actor)
        {
            await _service.ActorService.UpdateActor(id, actor);
            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{Id:Guid}")]
        public async Task<IActionResult> DeleteActor(Guid id)
        {
            await _service.ActorService.DeleteActor(id);
            return Ok();
        }
    }
}
