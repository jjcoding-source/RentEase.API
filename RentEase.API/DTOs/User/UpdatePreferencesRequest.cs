namespace RentEase.API.DTOs.User
{
    public class UpdatePreferencesRequest
    {
        public bool PrefBookingUpdates { get; set; }
        public bool PrefNewListings { get; set; }
        public bool PrefPriceDropAlerts { get; set; }
        public bool PrefMarketingEmails { get; set; }
    }
}