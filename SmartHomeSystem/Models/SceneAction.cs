namespace SmartHomeSystem.Models
{
    public class SceneAction
    {
        public int SceneActionId { get; set; }
        public int SceneId { get; set; } // Foreign key
        public Scene Scene { get; set; } // Navigation
        public int ActionTypeId { get; set; } // Foreign key
        public ActionType ActionType { get; set; } // Navigation
    }
}
