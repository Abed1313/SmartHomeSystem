namespace SmartHomeSystem.Models
{
    public class ActionType
    {
        public int ActionTypeId { get; set; }
        public string Name { get; set; } // e.g., "TurnOn", "TurnOff"
        public string Parameters { get; set; } // e.g., "75°F", "50% Brightness"
        public ICollection<LogEntry> LogEntries { get; set; }
        public ICollection<SceneAction> SceneActions { get; set; }
    }
}
