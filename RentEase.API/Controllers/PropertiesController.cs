
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentEase.API.DTOs.Property;
using RentEase.API.Interfaces;
using System.Security.Claims;

namespace RentEase.API.Controllers
{
    [ApiController]
    [Route("api/properties")]
    public class PropertiesController : ControllerBase
    {
        private readonly IPropertyService _service;
        public PropertiesController(IPropertyService service) { _service = service; }

        private int GetUserId() =>
            int.Parse(User.FindFirstValue("id") ?? "0");

        // GET /api/properties?search=&minPrice=&maxPrice=&types=&bedrooms=&sort=&owner=
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll([FromQuery] PropertyFilterParams filters)
        {
            int? userId = User.Identity?.IsAuthenticated == true ? GetUserId() : null;
            var result = await _service.GetAllAsync(filters, userId);
            return Ok(result);
        }

        // GET /api/properties/{id}
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _service.GetByIdAsync(id);
            return result == null ? NotFound() : Ok(result);
        }

        // POST /api/properties
        [HttpPost]
        [Authorize(Roles = "Owner")]
        public async Task<IActionResult> Create([FromBody] PropertyRequest request)
        {
            var result = await _service.CreateAsync(request, GetUserId());
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        // PUT /api/properties/{id}
        [HttpPut("{id}")]
        [Authorize(Roles = "Owner")]
        public async Task<IActionResult> Update(int id, [FromBody] PropertyRequest request)
        {
            var result = await _service.UpdateAsync(id, request, GetUserId());
            return result == null ? NotFound() : Ok(result);
        }

        // DELETE /api/properties/{id}
        [HttpDelete("{id}")]
        [Authorize(Roles = "Owner")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _service.DeleteAsync(id, GetUserId());
            return success ? NoContent() : NotFound();
        }

        // GET /api/properties/saved
        [HttpGet("saved")]
        [Authorize(Roles = "Renter")]
        public async Task<IActionResult> GetSaved()
        {
            var result = await _service.GetSavedAsync(GetUserId());
            return Ok(result);
        }

        // POST /api/properties/{id}/save
        [HttpPost("{id}/save")]
        [Authorize(Roles = "Renter")]
        public async Task<IActionResult> ToggleSave(int id)
        {
            var saved = await _service.ToggleSaveAsync(id, GetUserId());
            return Ok(new { saved });
        }

        // DELETE /api/properties/{id}/save
        [HttpDelete("{id}/save")]
        [Authorize(Roles = "Renter")]
        public async Task<IActionResult> Unsave(int id)
        {
            await _service.ToggleSaveAsync(id, GetUserId());
            return NoContent();
        }
    }
}