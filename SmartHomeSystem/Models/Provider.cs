namespace SmartHomeSystem.Models
{
    public class Provider
    {
        public int ProviderId { get; set; } // Use string to match IdentityUser's Id
        public string CharactersId { get; set; }
        public Characters User { get; set; } // Navigation
        public string Name { get; set; }
        public string Email { get; set; }

        // Provider-specific navigations
        public ICollection<Alert> ManagedAlerts { get; set; }
        public ICollection<EnergyUsage> ManagedEnergyUsages { get; set; }
        public ICollection<House> ManagedHouses { get; set; }
        public ICollection<Device> ManagedDevices { get; set; }
        public ICollection<SubscriptionPlan> ManagedSubscriptionPlans { get; set; }
    }
}
