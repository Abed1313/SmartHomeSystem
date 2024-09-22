namespace SmartHomeSystem.Models.DTO.Response
{
    public class UpdateDeviseDto
    {
        public int DeviceId { get; set; }
        public string Name { get; set; }
        public int DeviceTypeId { get; set; }
        public string ModelNumber { get; set; }
        public string imageURL { get; set; }
        public bool IsOnline { get; set; }
    }
}
