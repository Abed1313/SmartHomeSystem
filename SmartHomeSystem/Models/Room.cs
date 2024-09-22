namespace SmartHomeSystem.Models
{
    public class Room
    {
        public int RoomId { get; set; }
        public string Name { get; set; }
        public string imageURL { get; set; }
        public int HouseId { get; set; } // Foreign key
        public House House { get; set; } // Navigation
        public int RoomTypeId { get; set; } // Foreign key
        public RoomType RoomType { get; set; } // Navigation
        public int AdminId { get; set; }
        public Admin Admin { get; set; }
        public ICollection<Device> Devices { get; set; }
    }
}
