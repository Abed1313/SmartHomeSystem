namespace SmartHomeSystem.Models
{
    public class AutomationRule
    {
        public int AutomationRuleId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Trigger { get; set; }
        public string Action { get; set; }
        public bool IsActive { get; set; }
        public int AdminId { get; set; }
        public Admin Admin { get; set; }
    }
}
