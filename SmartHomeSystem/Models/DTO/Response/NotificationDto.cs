namespace SmartHomeSystem.Models.DTO.Response
{
    public class NotificationDto
    {
        public string Message { get; set; }
        public DateTime Timestamp { get; set; }
        public bool IsRead { get; set; }
        public int AdminId { get; set; }
    }
}
