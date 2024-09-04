namespace SmartHomeSystem.Models
{
    public class House
    {
        public int HouseId { get; set; }
        public string Address { get; set; }
        public int AdminId { get; set; }
        public Admin Admin { get; set; }
        public int ProviderId { get; set; } // Use string to match IdentityUser's Id
        public Provider Provider { get; set; }
        public ICollection<Room> Rooms { get; set; }
        public ICollection<AccessControl> AccessControls { get; set; }
    }
}
