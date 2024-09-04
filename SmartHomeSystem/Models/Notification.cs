namespace SmartHomeSystem.Models
{
    public class Notification
    {
        public int NotificationId { get; set; }
        public string Message { get; set; }
        public DateTime Timestamp { get; set; }
        public bool IsRead { get; set; }
        public int AdminId { get; set; }
        public Admin Admin { get; set; }
    }
}
