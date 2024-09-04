namespace SmartHomeSystem.Models
{
    public class Alert
    {
        public int AlertId { get; set; }
        public int AdminId { get; set; }
        public Admin Admin { get; set; }
        public string Message { get; set; }
        public DateTime Timestamp { get; set; }
        public int ActionSeverityId { get; set; } // Foreign key to ActionSeverity
        public ActionSeverity ActionSeverity { get; set; } // Navigation 
        public int ProviderId { get; set; } // Use string to match IdentityUser's Id
        public Provider Provider { get; set; }
    }
}
