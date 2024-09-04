namespace SmartHomeSystem.Models
{
    public class UserSubscription
    {
        public int UserSubscriptionId { get; set; }
        public int SubscriptionPlanId { get; set; } // Foreign key
        public SubscriptionPlan SubscriptionPlan { get; set; } // Navigation
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int AdminId { get; set; }
        public Admin Admin { get; set; }
    }
}