namespace SmartHomeSystem.Models
{
    public class AccessLevel
    {
        public int AccessLevelId { get; set; }
        public string Name { get; set; } // e.g., "FullAccess", "LimitedAccess", "ViewOnly"
        public string Description { get; set; } // Describe what this access level entails
        public ICollection<AccessControl> AccessControl { get; set; }
    }
}
