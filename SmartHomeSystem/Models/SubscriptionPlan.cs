namespace SmartHomeSystem.Models
{
        public class SubscriptionPlan
        {
            public int SubscriptionPlanId { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public decimal MonthlyCost { get; set; }
        public int AdminId { get; set; }
        public Admin Admin { get; set; }
        public int ProviderId { get; set; } // Use string to match IdentityUser's Id
        public Provider Provider { get; set; }
        public ICollection<UserSubscription> UserSubscriptions { get; set; }
        }
}