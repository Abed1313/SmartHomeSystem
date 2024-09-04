namespace SmartHomeSystem.Models.DTO.Response
{
    public class SceneDto
    {
        public int SceneId { get; set; }
        public int AdminId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        
    }
}
