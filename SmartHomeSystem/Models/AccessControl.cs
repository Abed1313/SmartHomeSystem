namespace SmartHomeSystem.Models
{
    public class AccessControl         // join table
    {
        public int AccessControlId { get; set; }
        public int AdminId { get; set; }
        public Admin Admin { get; set; }
        public int HouseId { get; set; } // Foreign key
        public House House { get; set; } // Navigation
        public int AccessLevelId { get; set; } // Foreign key
        public AccessLevel AccessLevel { get; set; } // Navigation
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
    }
}
