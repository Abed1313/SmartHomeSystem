namespace SmartHomeSystem.Models
{
    public class DeviceType
    {
        public int DeviceTypeId { get; set; }
        public string Name { get; set; } // e.g., "Light", "Thermostat"
        public string Description { get; set; }
        public ICollection<Device> Devices { get; set; }
    }
}
