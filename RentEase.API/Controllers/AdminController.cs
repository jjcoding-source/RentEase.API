
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentEase.API.Interfaces;

namespace RentEase.API.Controllers
{
    [ApiController]
    [Route("api/admin")]
    [Authorize(Roles = "Admin")]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _service;
        public AdminController(IAdminService service) { _service = service; }

        // GET /api/admin/stats
        [HttpGet("stats")]
        public async Task<IActionResult> GetStats()
        {
            var result = await _service.GetStatsAsync();
            return Ok(result);
        }

        // GET /api/admin/users
        [HttpGet("users")]
        public async Task<IActionResult> GetUsers()
        {
            var result = await _service.GetAllUsersAsync();
            return Ok(result);
        }

        // GET /api/admin/properties
        [HttpGet("properties")]
        public async Task<IActionResult> GetProperties()
        {
            var result = await _service.GetAllPropertiesAsync();
            return Ok(result);
        }

        // GET /api/admin/bookings
        [HttpGet("bookings")]
        public async Task<IActionResult> GetBookings()
        {
            var result = await _service.GetAllBookingsAsync();
            return Ok(result);
        }

        // PATCH /api/admin/properties/{id}/approve
        [HttpPatch("properties/{id}/approve")]
        public async Task<IActionResult> ApproveProperty(int id)
        {
            var success = await _service.ApprovePropertyAsync(id);
            return success ? Ok(new { message = "Property approved." }) : NotFound();
        }

        // PATCH /api/admin/properties/{id}/reject
        [HttpPatch("properties/{id}/reject")]
        public async Task<IActionResult> RejectProperty(int id)
        {
            var success = await _service.RejectPropertyAsync(id);
            return success ? Ok(new { message = "Property rejected." }) : NotFound();
        }

        // PATCH /api/admin/users/{id}/deactivate
        [HttpPatch("users/{id}/deactivate")]
        public async Task<IActionResult> DeactivateUser(int id)
        {
            var success = await _service.SetUserActiveAsync(id, false);
            return success ? Ok(new { message = "User deactivated." }) : NotFound();
        }

        // PATCH /api/admin/users/{id}/activate
        [HttpPatch("users/{id}/activate")]
        public async Task<IActionResult> ActivateUser(int id)
        {
            var success = await _service.SetUserActiveAsync(id, true);
            return success ? Ok(new { message = "User activated." }) : NotFound();
        }
    }
}