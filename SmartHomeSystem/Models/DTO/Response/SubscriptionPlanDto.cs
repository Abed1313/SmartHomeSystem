namespace SmartHomeSystem.Models.DTO.Response
{
    public class SubscriptionPlanDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal MonthlyCost { get; set; }
        public int AdminId { get; set; }
        public int ProviderId { get; set; }
    }
}
