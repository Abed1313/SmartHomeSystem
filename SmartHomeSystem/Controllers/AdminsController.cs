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
    [Authorize(Roles = "Admin")]
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
            var adminDto = new AdminDto
            {
                Name = admin.Name,
                Email = admin.Email,
            };
            return Ok(adminDto);
           
            
        }

        // Houses
        [HttpGet("houses")]
        public async Task<ActionResult<IEnumerable<House>>> GetManagedHouses(int adminId)
        {

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
        [HttpPut("houses")]
        public async Task<IActionResult> UpdateHouse(HouseDto houseDto, int houseId)
        {
            if(houseId != houseDto.HouseId)
            {
                return BadRequest();
            }
            try
            {
                var UpdateHouse = await _context.UpdateHouseAsync(houseDto, houseId);
                if(UpdateHouse == null)
                {
                    return NotFound();
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return Ok();
        }
        [HttpDelete("houses/{houseId}")]
        public async Task<ActionResult> RemoveHouse(int houseId)
        {

            await _context.RemoveHouseAsync( houseId);
            return Ok();
        }

        // Devices
        [HttpGet("devices")]
        public async Task<ActionResult<IEnumerable<Device>>> GetManagedDevices(int adminId)
        {

            var devices = await _context.GetManagedDevicesAsync(adminId);
            return Ok(devices);
        }

        [HttpPost("devices")]
        public async Task<ActionResult> AddDevice([FromBody] DeviceDto deviceDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _context.AddDeviceAsync(deviceDto);
            return Ok();
        }
        [HttpPut("devices")]
        public async Task<IActionResult> UpdateDevices(UpdateDeviseDto deviceDto, int deviceId)
        {
            if (deviceId != deviceDto.DeviceId)
            {
                return BadRequest();
            }
            try
            {
                var UpdateHouse = await _context.UpdateDeviceAsync(deviceDto, deviceId);
                if (UpdateHouse == null)
                {
                    return NotFound();
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return Ok();
        }

        [HttpDelete("devices/{deviceId}")]
        public async Task<ActionResult> RemoveDevice(int deviceId)
        {
            

            await _context.RemoveDeviceAsync( deviceId);
            return Ok();
        }

        // Subscription Plans
        [HttpGet("subscription-plans")]
        public async Task<ActionResult<IEnumerable<SubscriptionPlan>>> GetManagedSubscriptionPlans(int adminId)
        {

            var subscriptionPlans = await _context.GetManagedSubscriptionPlansAsync(adminId);
            return Ok(subscriptionPlans);
        }

        [HttpPost("subscription-plans")]
        public async Task<IActionResult> AddSubscriptionPlan([FromBody] SubscriptionPlanDto planDto )
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _context.AddSubscriptionPlanAsync(planDto);
            return Ok();
        }

        [HttpDelete("subscription-plans/{planId}")]
        public async Task<ActionResult> RemoveSubscriptionPlan(int planId)
        {

            await _context.RemoveSubscriptionPlanAsync( planId);
            return Ok();
        }

        // Alerts
        [HttpGet("alerts")]
        public async Task<ActionResult<IEnumerable<Alert>>> GetManagedAlerts(int adminId)
        {
            var alerts = await _context.GetManagedAlertsAsync(adminId);
            return Ok(alerts);
        }

        [HttpPost("alerts")]
        public async Task<IActionResult> AddAlert([FromBody] AlertDto alertDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _context.AddAlertAsync( alertDto);
            return Ok();
        }

        [HttpDelete("alerts/{alertId}")]
        public async Task<ActionResult> RemoveAlert(int alertId)
        {

            await _context.RemoveAlertAsync( alertId);
            return Ok();
        }

        // Energy Usages
        [HttpGet("energy-usages")]
        public async Task<ActionResult<IEnumerable<EnergyUsage>>> GetManagedEnergyUsages(int adminId)
        {
            

            var energyUsages = await _context.GetManagedEnergyUsagesAsync(adminId);
            return Ok(energyUsages);
        }

        [HttpPost("energy-usages")]
        public async Task<IActionResult> AddEnergyUsage([FromBody] EnergyUsageDto energyUsageDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _context.AddEnergyUsageAsync(energyUsageDto);
            return Ok();
        }

        [HttpDelete("energy-usages/{usageId}")]
        public async Task<ActionResult> RemoveEnergyUsage(int usageId)
        {

            await _context.RemoveEnergyUsageAsync( usageId);
            return Ok();
        }

        // Access Controls
        [HttpGet("access-controls")]
        public async Task<ActionResult<IEnumerable<AccessControl>>> GetAllowedAccessControls(int adminId)
        {

            var accessControls = await _context.GetAllowedAccessControlsAsync(adminId);
            return Ok(accessControls);
        }

        [HttpPost("access-controls")]
        public async Task<IActionResult> AddAccessControl([FromBody] AccessControlDto accessControlDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _context.AddAccessControlAsync(accessControlDto);
            return Ok();
        }

        [HttpDelete("access-controls/{accessControlId}")]
        public async Task<ActionResult> RemoveAccessControl(int accessControlId)
        {
            await _context.RemoveAccessControlAsync(accessControlId);
            return Ok();
        }

        // Scenes
        [HttpGet("scenes")]
        public async Task<ActionResult<IEnumerable<Scene>>> GetAccessibleScenes(int adminId)
        {

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

            await _context.RemoveSceneAsync(sceneId);
            return Ok();
        }

        // Rooms
        [HttpGet("rooms")]
        public async Task<ActionResult<IEnumerable<Room>>> GetRooms(int adminId)
        {

            var rooms = await _context.GetRoomsAsync(adminId);
            return Ok(rooms);
        }

        [HttpPost("rooms")]
        public async Task<ActionResult> AddRoom([FromBody] RoomDto roomDto)
        {
            
            await _context.AddRoomAsync(roomDto);
            return Ok();
        }

        [HttpPut("rooms")]
        public async Task<ActionResult> UpdateRoom(RoomDto roomDto, int roomId)
        {
            if (roomId != roomDto.RoomId)
            {
                return BadRequest();
            }
            try
            {
                var room = await _context.UpdateRoomAsync(roomDto, roomId);
                if (room == null)
                {
                    return NotFound();
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return Ok();
        }

            [HttpDelete("rooms/{roomId}")]
        public async Task<ActionResult> RemoveRoom(int roomId)
        {

            await _context.RemoveRoomAsync(roomId);
            return Ok();
        }

        // User Subscriptions
        [HttpGet("user-subscriptions")]
        public async Task<ActionResult<IEnumerable<UserSubscription>>> GetUserSubscriptions(int adminId)
        {

            var subscriptions = await _context.GetUserSubscriptionsAsync(adminId);
            return Ok(subscriptions);
        }

        [HttpPost("user-subscriptions")]
        public async Task<IActionResult> AddUserSubscription([FromBody] UserSubscriptionDto userSubscriptionDto)
        {
            
            await _context.AddUserSubscriptionAsync(userSubscriptionDto);
            return Ok();
        }

        [HttpDelete("user-subscriptions/{subscriptionId}")]
        public async Task<ActionResult> RemoveUserSubscription(int subscriptionId)
        {
           

            await _context.RemoveUserSubscriptionAsync( subscriptionId);
            return Ok();
        }

        // Automation Rules
        [HttpGet("automation-rules")]
        public async Task<ActionResult<IEnumerable<AutomationRule>>> GetAutomationRules(int adminId)
        {

            var rules = await _context.GetAutomationRulesAsync(adminId);
            return Ok(rules);
        }

        [HttpPost("automation-rules")]
        public async Task<IActionResult> AddAutomationRule([FromBody] AutomationRuleDto automationRuleDto)
        {

            await _context.AddAutomationRuleAsync(automationRuleDto);
            return Ok();
        }

        [HttpDelete("automation-rules/{ruleId}")]
        public async Task<ActionResult> RemoveAutomationRule(int ruleId)
        {
           

            await _context.RemoveAutomationRuleAsync(ruleId);
            return Ok();
        }

        // Notifications
        [HttpGet("notifications")]
        public async Task<ActionResult<IEnumerable<Notification>>> GetNotifications(int adminId)
        {
            var notifications = await _context.GetNotificationsAsync(adminId);
            return Ok(notifications);
        }

        [HttpPost("notifications")]
        public async Task<ActionResult> AddNotification([FromBody] NotificationDto notificationDto)
        {
            

            await _context.AddNotificationAsync(notificationDto);
            return Ok();
        }

        [HttpDelete("notifications/{notificationId}")]
        public async Task<ActionResult> RemoveNotification(int notificationId)
        {
           

            await _context.RemoveNotificationAsync(notificationId);
            return Ok();
        }
    }
}
