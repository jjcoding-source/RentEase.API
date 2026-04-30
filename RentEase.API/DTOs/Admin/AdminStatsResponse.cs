namespace RentEase.API.DTOs.Admin
{
    public class AdminStatsResponse
    {
        public int TotalUsers { get; set; }
        public int TotalOwners { get; set; }
        public int TotalRenters { get; set; }
        public int TotalProperties { get; set; }
        public int PendingProperties { get; set; }
        public int ActiveBookings { get; set; }
        public decimal MonthlyRevenue { get; set; }
    }
}