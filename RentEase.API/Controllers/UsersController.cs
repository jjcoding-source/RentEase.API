
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentEase.API.DTOs.User;
using RentEase.API.Interfaces;
using System.Security.Claims;

namespace RentEase.API.Controllers
{
    [ApiController]
    [Route("api/users")]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _service;
        public UsersController(IUserService service) { _service = service; }

        private int GetUserId() => int.Parse(User.FindFirstValue("id") ?? "0");

        // GET /api/users/me
        [HttpGet("me")]
        public async Task<IActionResult> GetMe()
        {
            var result = await _service.GetByIdAsync(GetUserId());
            return result == null ? NotFound() : Ok(result);
        }

        // PUT /api/users/me
        [HttpPut("me")]
        public async Task<IActionResult> UpdateMe([FromBody] UpdateUserRequest request)
        {
            var result = await _service.UpdateAsync(GetUserId(), request);
            return result == null ? NotFound() : Ok(result);
        }

        // POST /api/users/me/change-password
        [HttpPost("me/change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest request)
        {
            try
            {
                await _service.ChangePasswordAsync(GetUserId(), request);
                return Ok(new { message = "Password changed successfully." });
            }
            catch (UnauthorizedAccessException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // PATCH /api/users/me/preferences
        [HttpPatch("me/preferences")]
        public async Task<IActionResult> UpdatePreferences([FromBody] UpdatePreferencesRequest request)
        {
            await _service.UpdatePreferencesAsync(GetUserId(), request);
            return Ok(new { message = "Preferences updated." });
        }

        // DELETE /api/users/me
        [HttpDelete("me")]
        public async Task<IActionResult> DeleteMe()
        {
            await _service.DeleteAsync(GetUserId());
            return NoContent();
        }
    }
}