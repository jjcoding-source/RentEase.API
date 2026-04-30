
namespace RentEase.API.Models
{
    public class SavedProperty
    {
        public int UserId { get; set; }
        public int PropertyId { get; set; }

        // Navigation
        public User User { get; set; } = null!;
        public Property Property { get; set; } = null!;
    }
}