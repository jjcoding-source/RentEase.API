
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentEase.API.DTOs.Booking;
using RentEase.API.Interfaces;
using System.Security.Claims;

namespace RentEase.API.Controllers
{
    [ApiController]
    [Route("api/bookings")]
    [Authorize]
    public class BookingsController : ControllerBase
    {
        private readonly IBookingService _service;
        public BookingsController(IBookingService service) { _service = service; }

        private int GetUserId() => int.Parse(User.FindFirstValue("id") ?? "0");
        private string GetRole() => User.FindFirstValue(ClaimTypes.Role) ?? string.Empty;

        // POST /api/bookings
        [HttpPost]
        [Authorize(Roles = "Renter")]
        public async Task<IActionResult> Create([FromBody] BookingRequest request)
        {
            try
            {
                var result = await _service.CreateAsync(request, GetUserId());
                return CreatedAtAction(nameof(GetMyBookings), result);
            }
            catch (KeyNotFoundException ex) { return NotFound(new { message = ex.Message }); }
            catch (InvalidOperationException ex) { return BadRequest(new { message = ex.Message }); }
        }

        // GET /api/bookings/my
        [HttpGet("my")]
        [Authorize(Roles = "Renter")]
        public async Task<IActionResult> GetMyBookings()
        {
            var result = await _service.GetMyBookingsAsync(GetUserId());
            return Ok(result);
        }

        // GET /api/bookings/owner
        [HttpGet("owner")]
        [Authorize(Roles = "Owner")]
        public async Task<IActionResult> GetOwnerBookings()
        {
            var result = await _service.GetOwnerBookingsAsync(GetUserId());
            return Ok(result);
        }

        // GET /api/bookings/history
        [HttpGet("history")]
        [Authorize(Roles = "Renter")]
        public async Task<IActionResult> GetHistory()
        {
            var result = await _service.GetHistoryAsync(GetUserId());
            return Ok(result);
        }

        // GET /api/bookings/owner/history
        [HttpGet("owner/history")]
        [Authorize(Roles = "Owner")]
        public async Task<IActionResult> GetOwnerHistory()
        {
            var result = await _service.GetOwnerHistoryAsync(GetUserId());
            return Ok(result);
        }

        // PATCH /api/bookings/{id}/status
        [HttpPatch("{id}/status")]
        [Authorize(Roles = "Owner")]
        public async Task<IActionResult> UpdateStatus(int id, [FromBody] UpdateBookingStatusRequest request)
        {
            try
            {
                var result = await _service.UpdateStatusAsync(id, request.Status, GetUserId());
                return result == null ? NotFound() : Ok(result);
            }
            catch (UnauthorizedAccessException ex) { return Forbid(); }
            catch (ArgumentException ex) { return BadRequest(new { message = ex.Message }); }
        }
    }
}