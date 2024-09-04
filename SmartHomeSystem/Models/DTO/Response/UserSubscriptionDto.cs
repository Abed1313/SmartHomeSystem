namespace SmartHomeSystem.Models.DTO.Response
{
    public class UserSubscriptionDto
    {
        public int UserSubscriptionId { get; set; }
        public int SubscriptionPlanId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int AdminId { get; set; }
    }
}
