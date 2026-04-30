
using RentEase.API.DTOs.Booking;
using RentEase.API.Helpers;
using RentEase.API.Interfaces;
using RentEase.API.Models;

namespace RentEase.API.Services
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _repo;
        private readonly IPropertyRepository _propRepo;

        public BookingService(IBookingRepository repo, IPropertyRepository propRepo)
        {
            _repo = repo;
            _propRepo = propRepo;
        }

        public async Task<BookingResponse> CreateAsync(BookingRequest request, int renterId)
        {
            var property = await _propRepo.GetByIdAsync(request.PropertyId)
                ?? throw new KeyNotFoundException("Property not found.");

            if (property.Status != "Active")
                throw new InvalidOperationException("Property is not available for booking.");

            var booking = new Booking
            {
                PropertyId = request.PropertyId,
                RenterId = renterId,
                MoveIn = request.MoveIn,
                MoveOut = request.MoveOut,
                Duration = request.Duration,
                Occupants = request.Occupants,
                Parking = request.Parking,
                Message = request.Message,
                Status = "Pending",
            };

            var created = await _repo.CreateAsync(booking);
            var full = await _repo.GetByIdAsync(created.Id);
            return MappingHelper.ToBookingResponse(full!);
        }

        public async Task<List<BookingResponse>> GetMyBookingsAsync(int renterId)
        {
            var bookings = await _repo.GetByRenterIdAsync(renterId);
            return bookings.Select(MappingHelper.ToBookingResponse).ToList();
        }

        public async Task<List<BookingResponse>> GetOwnerBookingsAsync(int ownerId)
        {
            var bookings = await _repo.GetByOwnerIdAsync(ownerId);
            return bookings.Select(MappingHelper.ToBookingResponse).ToList();
        }

        public async Task<List<BookingResponse>> GetHistoryAsync(int renterId)
        {
            var bookings = await _repo.GetByRenterIdAsync(renterId);
            return bookings
                .Where(b => b.Status == "Completed" || b.Status == "Rejected")
                .Select(MappingHelper.ToBookingResponse)
                .ToList();
        }

        public async Task<List<BookingResponse>> GetOwnerHistoryAsync(int ownerId)
        {
            var bookings = await _repo.GetByOwnerIdAsync(ownerId);
            return bookings
                .Where(b => b.Status == "Completed")
                .Select(MappingHelper.ToBookingResponse)
                .ToList();
        }

        public async Task<BookingResponse?> UpdateStatusAsync(int id, string status, int ownerId)
        {
            var booking = await _repo.GetByIdAsync(id);
            if (booking == null) return null;

            // Verify the owner owns the property
            if (booking.Property.OwnerId != ownerId)
                throw new UnauthorizedAccessException("Not your booking to update.");

            var allowed = new[] { "Confirmed", "Rejected", "Completed" };
            if (!allowed.Contains(status))
                throw new ArgumentException("Invalid status.");

            var updated = await _repo.UpdateStatusAsync(id, status);
            return updated == null ? null : MappingHelper.ToBookingResponse(updated);
        }
    }
}