using Microsoft.EntityFrameworkCore;
using SmartHomeSystem.Data;
using SmartHomeSystem.Models;
using SmartHomeSystem.Models.DTO.Response;
using SmartHomeSystem.Repository.Interface;

namespace SmartHomeSystem.Repository.Services
{
    public class AdminService : IAdmin
    {
        private readonly SmartHomeDbContext _context;

        public AdminService(SmartHomeDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Admin>> GetAllAdminAsync()
        {
            return await _context.Admins.ToListAsync();
        }

        public async Task<Admin> GetAdminById(int adminId)
        {
            return await _context.Admins
         .Include(a => a.ManagedHouses)
         .Include(a => a.ManagedDevices)
         .Include(a => a.ManagedSubscriptionPlans)
         .Include(a => a.ManagedAlerts)
         .Include(a => a.ManagedEnergyUsages)
         .Include(a => a.AllowedAccessControls)
         .Include(a => a.AccessibleScenes)
         .Include(a => a.Rooms)
         .Include(a => a.UserSubscriptions)
         .Include(a => a.AutomationRules)
         .Include(a => a.Notification)
         .FirstOrDefaultAsync(a => a.AdminId == adminId);
        }

        // managing houses //
        public async Task<IEnumerable<House>> GetManagedHousesAsync(int adminId)
        {
            return await _context.Houses
            .Where(h => h.AdminId == adminId)
            .ToListAsync();
        }
        public async Task<House> AddHouseAsync(HouseDto houseDto)
        {
            // Check if the Admin exists
            var adminExists = await _context.Admins.AnyAsync(a => a.AdminId == houseDto.AdminId);
            if (!adminExists)
            {
                throw new ArgumentException("Admin with the specified ID does not exist.");
            }

            // Create the House entity
            var house = new House
            {
                Address = houseDto.Address,
                AdminId = houseDto.AdminId,
                ProviderId = houseDto.ProviderId,
            };

            // Add the House entity to the DbContext
            _context.Houses.Add(house);

            // Save changes to the database
            await _context.SaveChangesAsync();

            // Return the created House entity
            return house;
        }


        public async Task RemoveHouseAsync(int adminId, int houseId)
        {
            var house = await _context.Houses
                .FirstOrDefaultAsync(h => h.AdminId == adminId && h.HouseId == houseId);

            if (house != null)
            {
                _context.Houses.Remove(house);
                await _context.SaveChangesAsync();
            }
        }
                                     // Managed Devices //
        public async Task<IEnumerable<Device>> GetManagedDevicesAsync(int adminId)
        {
            return await _context.Devices
                .Where(d => d.AdminId == adminId)
                .ToListAsync();
        }

        public async Task AddDeviceAsync(int adminId, Device device)
        {
            device.AdminId = adminId;
            _context.Devices.Add(device);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveDeviceAsync(int adminId, int deviceId)
        {
            var devise = await _context.Devices
                .FirstOrDefaultAsync(d => d.AdminId == adminId && d.DeviceId == deviceId);

            if(devise != null) 
            {
                _context.Devices.Remove(devise);
                await _context.SaveChangesAsync();
            }
        }
                                  //  managing subscription //
        public async Task<IEnumerable<SubscriptionPlan>> GetManagedSubscriptionPlansAsync(int adminId)
        {
            return await _context.SubscriptionPlans
                .Where(s => s.AdminId == adminId)
                .ToListAsync();
        }

        public async Task AddSubscriptionPlanAsync(int adminId, SubscriptionPlan plan)
        {
            plan.AdminId = adminId;
            _context.SubscriptionPlans.Add(plan);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveSubscriptionPlanAsync(int adminId, int planId)
        {
            var plan = await _context.SubscriptionPlans
                .FirstOrDefaultAsync(s => s.AdminId == adminId && s.SubscriptionPlanId == planId);

            if (plan != null)
            {
                _context.SubscriptionPlans.Remove(plan);
                await _context.SaveChangesAsync();
            }
        }
                                         // managing alerts //
        public async Task<IEnumerable<Alert>> GetManagedAlertsAsync(int adminId)
        {
            return await _context.Alerts
                .Where(a => a.AdminId == adminId)
                .ToListAsync();
        }

        public async Task AddAlertAsync(int adminId, Alert alert)
        {
            alert.AdminId = adminId;
            _context.Alerts.Add(alert);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveAlertAsync(int adminId, int alertId)
        {
            var alert = await _context.Alerts
                .FirstOrDefaultAsync(a => a.AdminId == adminId && a.AlertId == alertId);

            if(alert != null)
            {
                _context.Alerts.Remove(alert);
                await _context.SaveChangesAsync();
            }
        }
                                  // managing energy usages //
        public async Task<IEnumerable<EnergyUsage>> GetManagedEnergyUsagesAsync(int adminId)
        {
            return await _context.EnergyUsages
                .Where(e => e.AdminId == adminId)
                .ToListAsync();
        }

        public async Task AddEnergyUsageAsync(int adminId, EnergyUsage energyUsage)
        {
            energyUsage.AdminId = adminId;
            _context.EnergyUsages.Add(energyUsage);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveEnergyUsageAsync(int adminId, int energyUsageId)
        {
            var energy = await _context.EnergyUsages
                .FirstOrDefaultAsync(e => e.AdminId == adminId &&  e.EnergyUsageId == energyUsageId);
            
            if (energy != null)
            {
                _context.EnergyUsages.Remove(energy);
                await _context.SaveChangesAsync();
            }
        }
                               // managing access controls //
        public async Task<IEnumerable<AccessControl>> GetAllowedAccessControlsAsync(int adminId)
        {
           return await _context.AccessControls
                .Where(a => a.AdminId  == adminId)
                .ToListAsync();
        }

        public async Task AddAccessControlAsync(int adminId, AccessControl accessControl)
        {
            accessControl.AdminId = adminId;
            _context.AccessControls.Add(accessControl);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveAccessControlAsync(int adminId, int accessControlId)
        {
            var access = await _context.AccessControls
                .FirstOrDefaultAsync(a=>a.AdminId == adminId && a.AccessControlId == accessControlId);

            if (access != null)
            {
                _context.AccessControls.Remove(access);
                await _context.SaveChangesAsync();
            }
        }
                                 // managing scenes //
        public async Task<IEnumerable<Scene>> GetAccessibleScenesAsync(int adminId)
        {
            return await _context.Scenes
                .Where(s => s.AdminId == adminId)
                .ToListAsync();
        }

        public async Task AddSceneAsync(int adminId, Scene scene)
        {
            scene.AdminId = adminId;
            _context.Scenes.Add(scene);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveSceneAsync(int adminId, int sceneId)
        {
            var scene = await _context.Scenes
                .FirstOrDefaultAsync(s => s.AdminId == adminId &&  s.SceneId == sceneId);

            if(scene != null)
            {
                _context.Scenes.Remove(scene);
                await _context.SaveChangesAsync();
            }
        }
                                  // managing rooms //
        public async Task<IEnumerable<Room>> GetRoomsAsync(int adminId)
        {
            return await _context.Rooms
                .Where(r => r.AdminId == adminId)
                .ToListAsync();
        }

        public async Task AddRoomAsync(int adminId, Room room)
        {
            room.AdminId = adminId;
            _context.Rooms.Add(room);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveRoomAsync(int adminId, int roomId)
        {
            var room = await _context.Rooms
                .FirstOrDefaultAsync(r => r.AdminId == adminId &&  r.RoomId == roomId);

            if(room != null)
            {
                _context.Rooms.Remove(room);
                await _context.SaveChangesAsync();
            }
        }
                                // managing user subscriptions //
        public async Task<IEnumerable<UserSubscription>> GetUserSubscriptionsAsync(int adminId)
        {
            return await _context.UserSubscriptions
                .Where(u => u.AdminId == adminId)
                .ToListAsync();
        }

        public async Task AddUserSubscriptionAsync(int adminId, UserSubscription userSubscription)
        {
            userSubscription.AdminId = adminId;
            _context.UserSubscriptions.Add(userSubscription);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveUserSubscriptionAsync(int adminId, int subscriptionId)
        {
            var user = await _context.UserSubscriptions
                .FirstOrDefaultAsync(u => u.AdminId == adminId && u.UserSubscriptionId == subscriptionId);

            if(user != null)
            {
                _context.UserSubscriptions.Remove(user);
                await _context.SaveChangesAsync();
            }
        }
                                // managing automation rules // 
        public async Task<IEnumerable<AutomationRule>> GetAutomationRulesAsync(int adminId)
        {
            return await _context.AutomationRules
                .Where(a => a.AdminId == adminId)
                .ToListAsync();
        }

        public async Task AddAutomationRuleAsync(int adminId, AutomationRule automationRule)
        {
            automationRule.AdminId = adminId;
            _context.AutomationRules.Add(automationRule);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveAutomationRuleAsync(int adminId, int ruleId)
        {
            var automation = await _context.AutomationRules
                .FirstOrDefaultAsync(a => a.AdminId == adminId && a.AutomationRuleId == ruleId);

            if (automation != null)
            {
                _context.AutomationRules.Remove(automation);
                await _context.SaveChangesAsync();
            }
        }
                             // managing notifications //
        public async Task<IEnumerable<Notification>> GetNotificationsAsync(int adminId)
        {
            return await _context.Notifications
                .Where(n => n.AdminId == adminId)
                .ToListAsync();
        }

        public async Task AddNotificationAsync(int adminId, Notification notification)
        {
            notification.AdminId = adminId;
            _context.Notifications.Add(notification);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveNotificationAsync(int adminId, int notificationId)
        {
            var notification = await _context.Notifications
                .FirstOrDefaultAsync(n => n.AdminId == adminId &&  n.NotificationId == notificationId);

            if(notification != null)
            {
                _context.Notifications.Remove(notification);
                await _context.SaveChangesAsync();
            }
        }

        
    }
}
