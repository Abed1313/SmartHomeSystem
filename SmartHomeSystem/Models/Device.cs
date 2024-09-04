namespace SmartHomeSystem.Models
{
    public class Device
    {
        public int DeviceId { get; set; }
        public string Name { get; set; }
        public int DeviceTypeId { get; set; } // Foreign key
        public DeviceType DeviceType { get; set; } // Navigation
        public int RoomId { get; set; } // Foreign key
        public Room Room { get; set; } // Navigation
        public bool IsOnline { get; set; }
        public DateTime LastCommunicationTime { get; set; }
        public string Manufacturer { get; set; }
        public string ModelNumber { get; set; }
        public int AdminId { get; set; }
        public Admin Admin { get; set; }
        public int GuestId { get; set; }
        public Guest Guest { get; set; }
        public int ProviderId { get; set; } // Use string to match IdentityUser's Id
        public Provider Provider { get; set; }
        public ICollection<Alert> Alerts { get; set; }
        public ICollection<EnergyUsage> EnergyUsages { get; set; }
        public ICollection<LogEntry> LogEntries { get; set; }
        public ICollection<SceneAction> SceneActions { get; set; }
    }
}
