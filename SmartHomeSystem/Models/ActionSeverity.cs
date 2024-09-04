namespace SmartHomeSystem.Models
{
    public class ActionSeverity
    {
        public int ActionSeverityId { get; set; }
        public string Name { get; set; } // e.g., "Info", "Warning", "Critical"
        public string Description { get; set; } // Describe the severity level
        public ICollection<Alert> Alerts { get; set; }
    }
}
