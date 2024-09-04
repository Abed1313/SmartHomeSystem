using Microsoft.AspNetCore.Identity;

namespace SmartHomeSystem.Models
{
    public class Characters : IdentityUser
    {
        // Navigation properties for inherited classes
        public Admin Admin { get; set; }
        public Guest Guest { get; set; }
        public Provider Provider { get; set; }
    }
}
