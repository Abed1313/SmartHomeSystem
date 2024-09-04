namespace SmartHomeSystem.Models
{
    public class Scene
    {
        public int SceneId { get; set; }
        public int AdminId { get; set; }
        public Admin Admin { get; set; }
        public string Name { get; set; }
        public int GuestId { get; set; }
        public Guest Guest { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public ICollection<SceneAction> SceneActions { get; set; }
    }
}
