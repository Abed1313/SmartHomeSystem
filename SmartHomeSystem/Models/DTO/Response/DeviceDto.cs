namespace SmartHomeSystem.Models.DTO.Response
{
    public class DeviceDto
    {
        public int DeviceId { get; set; }
        public string Name { get; set; }
        public int DeviceTypeId { get; set; }
        public int RoomId { get; set; }
        public bool IsOnline { get; set; }
        public DateTime LastCommunicationTime { get; set; }
        public string Manufacturer { get; set; }
        public string ModelNumber { get; set; }
        public int AdminId { get; set; }
        public int ProviderId { get; set; }
    }
}
