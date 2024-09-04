namespace SmartHomeSystem.Models
{
    public class Guest
    {
        public int GuestId { get; set; } // Use string to match IdentityUser's Id
        public string CharactersId { get; set; }
        public Characters User { get; set; } // Navigation
        public string Name { get; set; }

        // Guest-specific navigations
        public ICollection<AccessControl> AllowedAccessControls { get; set; }
        public ICollection<Device> AccessibleDevices { get; set; }
        public ICollection<Scene> AccessibleScenes { get; set; }
    }
}
