namespace SmartHomeSystem.Models
{
    public class EnergyUsage
    {
        public int EnergyUsageId { get; set; }
        public int DeviceId { get; set; } // Foreign key
        public Device Device { get; set; } // Navigation
        public DateTime Timestamp { get; set; }
        public float EnergyConsumed { get; set; }
        public decimal Cost { get; set; } // Cost associated with energy usage
        public int AdminId { get; set; }
        public Admin Admin { get; set; }
        public int ProviderId { get; set; } // Use string to match IdentityUser's Id
        public Provider Provider { get; set; }
    }
}
