using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartHomeSystem.Data;
using SmartHomeSystem.Models;
using SmartHomeSystem.Models.DTO.Response;
using SmartHomeSystem.Repository.Interface;
using SmartHomeSystem.Repository.Services;

namespace SmartHomeSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class AdminsController : ControllerBase
    {
        private readonly IAdmin _context;

        public AdminsController(IAdmin userManager)
        {
            _context = userManager;
        }

        // GET: api/Admins
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Admin>>> GetAdmins()
        {
            var admins = await _context.GetAllAdminAsync();
            return Ok(admins);
        }

        // GET: api/Admin/{id}
        [HttpGet("{adminId}")]
        public async Task<ActionResult<Admin>> GetAdminById(int adminId)
        {
            var admin = await _context.GetAdminById(adminId);
            if (admin == null)
            {
                return NotFound();
            }
            return Ok(admin);
        }

        // Houses
        [HttpGet("houses")]
        public async Task<ActionResult<IEnumerable<House>>> GetManagedHouses()
        {
            if (!int.TryParse(User.Identity.Name, out int adminId))
            {
                return BadRequest("Invalid admin ID.");
            }

            var houses = await _context.GetManagedHousesAsync(adminId);
            return Ok(houses);
        }

        [HttpPost("houses")]
        public async Task<IActionResult> AddHouse([FromBody] HouseDto houseDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var house = await _context.AddHouseAsync(houseDto);
            return Ok();
        }

        [HttpDelete("houses/{houseId}")]
        public async Task<ActionResult> RemoveHouse(int houseId)
        {
            if (!int.TryParse(User.Identity.Name, out int adminId))
            {
                return BadRequest("Invalid admin ID.");
            }

            await _context.RemoveHouseAsync(adminId, houseId);
            return Ok();
        }

        // Devices
        [HttpGet("devices")]
        public async Task<ActionResult<IEnumerable<Device>>> GetManagedDevices()
        {
            if (!int.TryParse(User.Identity.Name, out int adminId))
            {
                return BadRequest("Invalid admin ID.");
            }

            var devices = await _context.GetManagedDevicesAsync(adminId);
            return Ok(devices);
        }

        [HttpPost("devices")]
        public async Task<ActionResult> AddDevice([FromBody] DeviceDto deviceDto)
        {
            if (!int.TryParse(User.Identity.Name, out int adminId))
            {
                return BadRequest("Invalid admin ID.");
            }

            await _context.AddDeviceAsync(deviceDto);
            return Ok();
        }

        [HttpDelete("devices/{deviceId}")]
        public async Task<ActionResult> RemoveDevice(int deviceId)
        {
            if (!int.TryParse(User.Identity.Name, out int adminId))
            {
                return BadRequest("Invalid admin ID.");
            }

            await _context.RemoveDeviceAsync(adminId, deviceId);
            return Ok();
        }

        // Subscription Plans
        [HttpGet("subscription-plans")]
        public async Task<ActionResult<IEnumerable<SubscriptionPlan>>> GetManagedSubscriptionPlans()
        {
            if (!int.TryParse(User.Identity.Name, out int adminId))
            {
                return BadRequest("Invalid admin ID.");
            }

            var subscriptionPlans = await _context.GetManagedSubscriptionPlansAsync(adminId);
            return Ok(subscriptionPlans);
        }

        [HttpPost("subscription-plans")]
        public async Task<IActionResult> AddSubscriptionPlan([FromBody] SubscriptionPlanDto planDto )
        {
            if (!int.TryParse(User.Identity.Name, out int adminId))
            {
                return BadRequest("Invalid admin ID.");
            }

            await _context.AddSubscriptionPlanAsync(planDto);
            return Ok();
        }

        [HttpDelete("subscription-plans/{planId}")]
        public async Task<ActionResult> RemoveSubscriptionPlan(int planId)
        {
            if (!int.TryParse(User.Identity.Name, out int adminId))
            {
                return BadRequest("Invalid admin ID.");
            }

            await _context.RemoveSubscriptionPlanAsync(adminId, planId);
            return Ok();
        }

        // Alerts
        [HttpGet("alerts")]
        public async Task<ActionResult<IEnumerable<Alert>>> GetManagedAlerts()
        {
            if (!int.TryParse(User.Identity.Name, out int adminId))
            {
                return BadRequest("Invalid admin ID.");
            }

            var alerts = await _context.GetManagedAlertsAsync(adminId);
            return Ok(alerts);
        }

        [HttpPost("alerts")]
        public async Task<IActionResult> AddAlert([FromBody] AlertDto alertDto)
        {
            if (!int.TryParse(User.Identity.Name, out int adminId))
            {
                return BadRequest("Invalid admin ID.");
            }

            await _context.AddAlertAsync( alertDto);
            return Ok();
        }

        [HttpDelete("alerts/{alertId}")]
        public async Task<ActionResult> RemoveAlert(int alertId)
        {
            if (!int.TryParse(User.Identity.Name, out int adminId))
            {
                return BadRequest("Invalid admin ID.");
            }

            await _context.RemoveAlertAsync(adminId, alertId);
            return Ok();
        }

        // Energy Usages
        [HttpGet("energy-usages")]
        public async Task<ActionResult<IEnumerable<EnergyUsage>>> GetManagedEnergyUsages()
        {
            if (!int.TryParse(User.Identity.Name, out int adminId))
            {
                return BadRequest("Invalid admin ID.");
            }

            var energyUsages = await _context.GetManagedEnergyUsagesAsync(adminId);
            return Ok(energyUsages);
        }

        [HttpPost("energy-usages")]
        public async Task<IActionResult> AddEnergyUsage([FromBody] EnergyUsageDto energyUsageDto)
        {
            if (!int.TryParse(User.Identity.Name, out int adminId))
            {
                return BadRequest("Invalid admin ID.");
            }

            await _context.AddEnergyUsageAsync(energyUsageDto);
            return Ok();
        }

        [HttpDelete("energy-usages/{usageId}")]
        public async Task<ActionResult> RemoveEnergyUsage(int usageId)
        {
            if (!int.TryParse(User.Identity.Name, out int adminId))
            {
                return BadRequest("Invalid admin ID.");
            }

            await _context.RemoveEnergyUsageAsync(adminId, usageId);
            return Ok();
        }

        // Access Controls
        [HttpGet("access-controls")]
        public async Task<ActionResult<IEnumerable<AccessControl>>> GetAllowedAccessControls()
        {
            if (!int.TryParse(User.Identity.Name, out int adminId))
            {
                return BadRequest("Invalid admin ID.");
            }

            var accessControls = await _context.GetAllowedAccessControlsAsync(adminId);
            return Ok(accessControls);
        }

        [HttpPost("access-controls")]
        public async Task<IActionResult> AddAccessControl([FromBody] AccessControlDto accessControlDto)
        {
            if (!int.TryParse(User.Identity.Name, out int adminId))
            {
                return BadRequest("Invalid admin ID.");
            }

            await _context.AddAccessControlAsync(accessControlDto);
            return Ok();
        }

        [HttpDelete("access-controls/{accessControlId}")]
        public async Task<ActionResult> RemoveAccessControl(int accessControlId)
        {
            if (!int.TryParse(User.Identity.Name, out int adminId))
            {
                return BadRequest("Invalid admin ID.");
            }

            await _context.RemoveAccessControlAsync(adminId, accessControlId);
            return Ok();
        }

        // Scenes
        [HttpGet("scenes")]
        public async Task<ActionResult<IEnumerable<Scene>>> GetAccessibleScenes()
        {
            if (!int.TryParse(User.Identity.Name, out int adminId))
            {
                return BadRequest("Invalid admin ID.");
            }

            var scenes = await _context.GetAccessibleScenesAsync(adminId);
            return Ok(scenes);
        }

        [HttpPost("scenes")]
        public async Task<ActionResult> AddScene([FromBody] SceneDto sceneDto)
        {
            

            await _context.AddSceneAsync(sceneDto);
            return Ok();
        }

        [HttpDelete("scenes/{sceneId}")]
        public async Task<ActionResult> RemoveScene(int sceneId)
        {
            if (!int.TryParse(User.Identity.Name, out int adminId))
            {
                return BadRequest("Invalid admin ID.");
            }

            await _context.RemoveSceneAsync(adminId, sceneId);
            return Ok();
        }

        // Rooms
        [HttpGet("rooms")]
        public async Task<ActionResult<IEnumerable<Room>>> GetRooms()
        {
            if (!int.TryParse(User.Identity.Name, out int adminId))
            {
                return BadRequest("Invalid admin ID.");
            }

            var rooms = await _context.GetRoomsAsync(adminId);
            return Ok(rooms);
        }

        [HttpPost("rooms")]
        public async Task<ActionResult> AddRoom([FromBody] RoomDto roomDto)
        {
            if (!int.TryParse(User.Identity.Name, out int adminId))
            {
                return BadRequest("Invalid admin ID.");
            }

            await _context.AddRoomAsync(roomDto);
            return Ok();
        }

        [HttpDelete("rooms/{roomId}")]
        public async Task<ActionResult> RemoveRoom(int roomId)
        {
            if (!int.TryParse(User.Identity.Name, out int adminId))
            {
                return BadRequest("Invalid admin ID.");
            }

            await _context.RemoveRoomAsync(adminId, roomId);
            return Ok();
        }

        // User Subscriptions
        [HttpGet("user-subscriptions")]
        public async Task<ActionResult<IEnumerable<UserSubscription>>> GetUserSubscriptions()
        {
            if (!int.TryParse(User.Identity.Name, out int adminId))
            {
                return BadRequest("Invalid admin ID.");
            }

            var subscriptions = await _context.GetUserSubscriptionsAsync(adminId);
            return Ok(subscriptions);
        }

        [HttpPost("user-subscriptions")]
        public async Task<IActionResult> AddUserSubscription([FromBody] UserSubscriptionDto userSubscriptionDto)
        {
            if (!int.TryParse(User.Identity.Name, out int adminId))
            {
                return BadRequest("Invalid admin ID.");
            }

            await _context.AddUserSubscriptionAsync(userSubscriptionDto);
            return Ok();
        }

        [HttpDelete("user-subscriptions/{subscriptionId}")]
        public async Task<ActionResult> RemoveUserSubscription(int subscriptionId)
        {
            if (!int.TryParse(User.Identity.Name, out int adminId))
            {
                return BadRequest("Invalid admin ID.");
            }

            await _context.RemoveUserSubscriptionAsync(adminId, subscriptionId);
            return Ok();
        }

        // Automation Rules
        [HttpGet("automation-rules")]
        public async Task<ActionResult<IEnumerable<AutomationRule>>> GetAutomationRules()
        {
            if (!int.TryParse(User.Identity.Name, out int adminId))
            {
                return BadRequest("Invalid admin ID.");
            }

            var rules = await _context.GetAutomationRulesAsync(adminId);
            return Ok(rules);
        }

        [HttpPost("automation-rules")]
        public async Task<IActionResult> AddAutomationRule([FromBody] AutomationRuleDto automationRuleDto)
        {
            if (!int.TryParse(User.Identity.Name, out int adminId))
            {
                return BadRequest("Invalid admin ID.");
            }

            await _context.AddAutomationRuleAsync(automationRuleDto);
            return Ok();
        }

        [HttpDelete("automation-rules/{ruleId}")]
        public async Task<ActionResult> RemoveAutomationRule(int ruleId)
        {
            if (!int.TryParse(User.Identity.Name, out int adminId))
            {
                return BadRequest("Invalid admin ID.");
            }

            await _context.RemoveAutomationRuleAsync(adminId, ruleId);
            return Ok();
        }

        // Notifications
        [HttpGet("notifications")]
        public async Task<ActionResult<IEnumerable<Notification>>> GetNotifications()
        {
            if (!int.TryParse(User.Identity.Name, out int adminId))
            {
                return BadRequest("Invalid admin ID.");
            }

            var notifications = await _context.GetNotificationsAsync(adminId);
            return Ok(notifications);
        }

        [HttpPost("notifications")]
        public async Task<ActionResult> AddNotification([FromBody] NotificationDto notificationDto)
        {
            if (!int.TryParse(User.Identity.Name, out int adminId))
            {
                return BadRequest("Invalid admin ID.");
            }

            await _context.AddNotificationAsync(notificationDto);
            return Ok();
        }

        [HttpDelete("notifications/{notificationId}")]
        public async Task<ActionResult> RemoveNotification(int notificationId)
        {
            if (!int.TryParse(User.Identity.Name, out int adminId))
            {
                return BadRequest("Invalid admin ID.");
            }

            await _context.RemoveNotificationAsync(adminId, notificationId);
            return Ok();
        }
    }
}
