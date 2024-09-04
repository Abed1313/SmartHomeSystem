namespace SmartHomeSystem.Models
{
    public class RoomType
    {
        public int RoomTypeId { get; set; }
        public string Name { get; set; } // e.g., "LivingRoom", "Kitchen", "Bedroom"
        public string Description { get; set; } // Optional: Describe the room type
        public ICollection<Room> Room { get; set; }


    }
}
