namespace SmartHomeSystem.Models
{
    public class SecuritySystem
    {
        public int SecuritySystemId { get; set; }
        public int HouseId { get; set; } // Foreign key
        public House House { get; set; } // Navigation
        public bool IsArmed { get; set; }
        public DateTime LastArmedTime { get; set; }
        public DateTime LastDisarmedTime { get; set; }
    }
}
