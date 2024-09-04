namespace SmartHomeSystem.Models
{
    public class Admin 
    {
        public int AdminId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        // Navigation property for the Characters entity
        public string CharactersId { get; set; }
        public Characters Characters { get; set; }

        // Additional navigation for Admin-specific operations
        public ICollection<House> ManagedHouses { get; set; }
        public ICollection<Device> ManagedDevices { get; set; }
        public ICollection<SubscriptionPlan> ManagedSubscriptionPlans { get; set; }
        //
        public ICollection<Alert> ManagedAlerts { get; set; }
        public ICollection<EnergyUsage> ManagedEnergyUsages { get; set; }
        public ICollection<AccessControl> AllowedAccessControls { get; set; }
        public ICollection<Scene> AccessibleScenes { get; set; }
        public ICollection<Room> Rooms { get; set; }
        public ICollection<UserSubscription> UserSubscriptions { get; set; }
        public ICollection<AutomationRule> AutomationRules { get; set; }
        public ICollection<Notification> Notification { get; set; }
    }
}
