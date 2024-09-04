namespace SmartHomeSystem.Models.DTO.Response
{
    public class AlertDto
    {
        public int AlertId { get; set; }
        public int AdminId { get; set; }
        public string Message { get; set; }
        public DateTime Timestamp { get; set; }
        public int ActionSeverityId { get; set; }
        public int ProviderId { get; set; }
    }
}
