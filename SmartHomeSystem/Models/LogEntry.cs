namespace SmartHomeSystem.Models
{
    public class LogEntry
    {
        public int LogEntryId { get; set; }
        public string Action { get; set; }
        public DateTime Timestamp { get; set; }
        public int DeviceId { get; set; } // Foreign key
        public Device Device { get; set; } // Navigation
        public int ActionTypeId { get; set; } // Foreign key to ActionType
        public ActionType ActionType { get; set; } // Navigation
    }
}
