using Contracts.ServiceContracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieApp.Presentation.Extensions;
using Shared.Dtos.DirectorDtos;
using Shared.RequestFeatures.EntitiesParameters;

namespace MovieApp.Presentation.Controllers
{
    [ApiController]
    [Route("api/directors")]
    public class DirectorController : ControllerBase
    {
        readonly IServiceManager _service;

        public DirectorController(IServiceManager service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetDirectors([FromQuery] DirectorParameters parameters)
        {
            var pagedResult = await _service.DirectorService.GetDirectors(parameters);
            Response.AddPaginationHeader(pagedResult.MetaData);
            return Ok(pagedResult);
        }

        [HttpGet("{id:Guid}", Name = "GetById")]
        public async Task<IActionResult> GetDirector(Guid id)
        {
            return Ok(await _service.DirectorService.GetDirector(id));
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateDirector([FromBody] DirectorForCreation director)
        {
            var created = await _service.DirectorService.CreateDirector(director);
            return CreatedAtRoute("GetById", new { id = created.Id }, created);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id:Guid}")]
        public async Task<IActionResult> UpdateDirector(Guid id, [FromBody] DirectorForUpdate director)
        {
            await _service.DirectorService.UpdateDirector(id, director);
            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> DeleteDirector(Guid id)
        {
            await _service.DirectorService.DeleteDirector(id);
            return Ok();
        }
    }
}
