namespace SmartHomeSystem.Models.DTO.Response
{
    public class EnergyUsageDto
    {
        public int EnergyUsageId { get; set; }
        public int DeviceId { get; set; }
        public DateTime Timestamp { get; set; }
        public float EnergyConsumed { get; set; }
        public decimal Cost { get; set; }
        public int AdminId { get; set; }
        public int ProviderId { get; set; }
    }
}
