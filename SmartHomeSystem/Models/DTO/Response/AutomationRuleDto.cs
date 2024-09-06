namespace SmartHomeSystem.Models.DTO.Response
{
    public class AutomationRuleDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Trigger { get; set; }
        public string Action { get; set; }
        public bool IsActive { get; set; }
        public int AdminId { get; set; }
    }
}
