using Microsoft.EntityFrameworkCore;
using SmartHomeSystem.Data;
using SmartHomeSystem.Models;
using SmartHomeSystem.Models.DTO.Response;
using SmartHomeSystem.Repository.Interface;
using System.Diagnostics;
using System.Net.Mail;
using System.Net;

namespace SmartHomeSystem.Repository.Services
{
    public class AdminService : IAdmin
    {
        private readonly SmartHomeDbContext _context;
        public AdminService( SmartHomeDbContext context)
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
                imageURL = houseDto.imageURL,
            };

            // Add the House entity to the DbContext
            _context.Houses.Add(house);

            // Save changes to the database
            await _context.SaveChangesAsync();

            // Return the created House entity
            return house;
        }

        public async Task<House> UpdateHouseAsync(HouseDto HouseDto, int houseId)
        {
            var house = await _context.Houses.FindAsync(houseId);
            if (house == null)
            {
                throw new Exception($"House with ID {houseId} not found.");
            }
            house.Address = HouseDto.Address;
            house.AdminId = HouseDto.AdminId;
            house.imageURL = HouseDto.imageURL;
            house.ProviderId = HouseDto.ProviderId;

            await _context.SaveChangesAsync();
            return house;
        }

        public async Task RemoveHouseAsync( int houseId)
        {
            var house = await _context.Houses
                .FirstOrDefaultAsync(h =>h.HouseId == houseId);

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

        public async Task<Device> AddDeviceAsync( DeviceDto deviceDto)
        {
            

            var devise = new Device
            {
               
                Name = deviceDto.Name,
               AdminId = deviceDto.AdminId,
                DeviceTypeId = deviceDto.DeviceTypeId,
                RoomId = deviceDto.RoomId,
                IsOnline = deviceDto.IsOnline,
                LastCommunicationTime = deviceDto.LastCommunicationTime,
                ProviderId = deviceDto.ProviderId,
                ModelNumber = deviceDto.ModelNumber,
                Manufacturer = deviceDto.Manufacturer,
                GuestId = deviceDto.GuestId,
                imageURL = deviceDto.imageURL,
            };
            

            // Add the Device entity to the DbContext
            _context.Devices.Add(devise);
            
            await _context.SaveChangesAsync();

            return devise;
        }

        public async Task<Device> UpdateDeviceAsync(UpdateDeviseDto deviceDto, int deviceId)
        {
            var devise = await _context.Devices.FindAsync(deviceId);
            if (devise == null)
            {
                throw new Exception($"Devise with ID {deviceId} not found.");
            }

            devise.Name = deviceDto.Name;
            devise.DeviceTypeId = deviceDto.DeviceTypeId;
            devise.IsOnline = deviceDto.IsOnline;
            devise.imageURL = deviceDto.imageURL;
            devise.ModelNumber = deviceDto.ModelNumber;

            await _context.SaveChangesAsync();
            return devise;

        }

        public async Task RemoveDeviceAsync( int deviceId)
        {
            var devise = await _context.Devices
                .FirstOrDefaultAsync(d =>  d.DeviceId == deviceId);

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

        public async Task<SubscriptionPlan> AddSubscriptionPlanAsync(SubscriptionPlanDto planDto)
        {
            // Check if the Admin exists
            var adminExists = await _context.Admins.AnyAsync(a => a.AdminId == planDto.AdminId);
            if (!adminExists)
            {
                throw new ArgumentException("Admin with the specified ID does not exist.");
            }

            // Map the DTO to the SubscriptionPlan entity
            var plan = new SubscriptionPlan
            {
                Name = planDto.Name,
                Description = planDto.Description,
                MonthlyCost = planDto.MonthlyCost,
                AdminId = planDto.AdminId,
                ProviderId = planDto.ProviderId
            };

            // Add the SubscriptionPlan entity to the DbContext
            _context.SubscriptionPlans.Add(plan);

            // Save changes to the database
            await _context.SaveChangesAsync();
            return plan;
        }


        public async Task RemoveSubscriptionPlanAsync( int planId)
        {
            var plan = await _context.SubscriptionPlans
                .FirstOrDefaultAsync(s => s.SubscriptionPlanId == planId);

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

        public async Task<Alert> AddAlertAsync(AlertDto alertDto)
        {
            

            // Map the DTO to the Alert entity
            var alert = new Alert
            {
                Message = alertDto.Message,
                Timestamp = alertDto.Timestamp,
                ActionSeverityId = alertDto.ActionSeverityId,
                ProviderId = alertDto.ProviderId,
                AdminId = alertDto.AdminId
            };

            // Add the Alert entity to the DbContext
            _context.Alerts.Add(alert);

            // Save changes to the database
            await _context.SaveChangesAsync();
            
            return alert;   
        }
        

        public async Task RemoveAlertAsync( int alertId)
        {
            var alert = await _context.Alerts
                .FirstOrDefaultAsync(a => a.AlertId == alertId);

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

        public async Task<EnergyUsage> AddEnergyUsageAsync(EnergyUsageDto energyUsageDto)
        {
            // Check if the Admin exists
            var adminExists = await _context.Admins.AnyAsync(a => a.AdminId == energyUsageDto.AdminId);
            if (!adminExists)
            {
                throw new ArgumentException("Admin with the specified ID does not exist.");
            }

            // Map the DTO to the EnergyUsage entity
            var energyUsage = new EnergyUsage
            {
                DeviceId = energyUsageDto.DeviceId,
                Timestamp = energyUsageDto.Timestamp,
                EnergyConsumed = energyUsageDto.EnergyConsumed,
                Cost = energyUsageDto.Cost,
                AdminId = energyUsageDto.AdminId,
                ProviderId = energyUsageDto.ProviderId
            };

            // Add the EnergyUsage entity to the DbContext
            _context.EnergyUsages.Add(energyUsage);

            // Save changes to the database
            await _context.SaveChangesAsync();
            return energyUsage;
        }

        public async Task RemoveEnergyUsageAsync( int energyUsageId)
        {
            var energy = await _context.EnergyUsages
                .FirstOrDefaultAsync(e =>  e.EnergyUsageId == energyUsageId);
            
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

        public async Task<AccessControl> AddAccessControlAsync(AccessControlDto accessControlDto)
        {
            // Check if the Admin exists
            var adminExists = await _context.Admins.AnyAsync(a => a.AdminId == accessControlDto.AdminId);
            if (!adminExists)
            {
                throw new ArgumentException("Admin with the specified ID does not exist.");
            }

            // Map the DTO to the AccessControl entity
            var accessControl = new AccessControl
            {
                AdminId = accessControlDto.AdminId,
                HouseId = accessControlDto.HouseId,
                AccessLevelId = accessControlDto.AccessLevelId,
                StartTime = accessControlDto.StartTime,
                EndTime = accessControlDto.EndTime,
                GuestId = accessControlDto.GuestId,
            };

            // Add the AccessControl entity to the DbContext
            _context.AccessControls.Add(accessControl);

            // Save changes to the database
            await _context.SaveChangesAsync();
            return accessControl;
        }


        public async Task RemoveAccessControlAsync(int accessControlId)
        {
            var access = await _context.AccessControls
                .FirstOrDefaultAsync(a=> a.AccessControlId == accessControlId);

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

        public async Task<Scene> AddSceneAsync(SceneDto sceneDto)
        {
            // Check if the Admin exists
            var adminExists = await _context.Admins.AnyAsync(a => a.AdminId == sceneDto.AdminId);
            if (!adminExists)
            {
                throw new ArgumentException("Admin with the specified ID does not exist.");
            }

            // Map the DTO to the Scene entity
            var scene = new Scene
            {
                AdminId = sceneDto.AdminId,
                Name = sceneDto.Name,
                Description = sceneDto.Description,
                IsActive = sceneDto.IsActive,
                GuestId = sceneDto.GuestId,
            };

            // Add the Scene entity to the DbContext
            _context.Scenes.Add(scene);

            // Save changes to the database
            await _context.SaveChangesAsync();
            return scene;
        }


        public async Task RemoveSceneAsync( int sceneId)
        {
            var scene = await _context.Scenes
                .FirstOrDefaultAsync(s => s.SceneId == sceneId);

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

        public async Task<Room> AddRoomAsync(RoomDto roomDto)
        {
            // Check if the Admin exists
            var adminExists = await _context.Admins.AnyAsync(a => a.AdminId == roomDto.AdminId);
            if (!adminExists)
            {
                throw new ArgumentException("Admin with the specified ID does not exist.");
            }

            // Map the DTO to the Room entity
            var room = new Room
            {
                Name = roomDto.Name,
                HouseId = roomDto.HouseId,
                RoomTypeId = roomDto.RoomTypeId,
                AdminId = roomDto.AdminId,
                imageURL = roomDto.imageURL,
            };

            // Add the Room entity to the DbContext
            _context.Rooms.Add(room);

            // Save changes to the database
            await _context.SaveChangesAsync();
            return room;
        }

        public async Task<Room> UpdateRoomAsync(RoomDto roomDto, int roomId)
        {
            var Room = await _context.Rooms.FindAsync(roomId);
            if (Room == null)
            {
                throw new Exception($"Room with ID {roomId} not found.");
            }

            Room.Name = roomDto.Name;
            Room.AdminId = roomDto.AdminId;
            Room.imageURL = roomDto.imageURL;
            Room.RoomTypeId = roomDto.RoomTypeId;

            await _context.SaveChangesAsync();
            return Room;
        }
        public async Task RemoveRoomAsync( int roomId)
        {
            var room = await _context.Rooms
                .FirstOrDefaultAsync(r =>  r.RoomId == roomId);

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

        public async Task<UserSubscription> AddUserSubscriptionAsync(UserSubscriptionDto userSubscriptionDto)
        {
            // Check if the Admin exists
            var adminExists = await _context.Admins.AnyAsync(a => a.AdminId == userSubscriptionDto.AdminId);
            if (!adminExists)
            {
                throw new ArgumentException("Admin with the specified ID does not exist.");
            }

            // Map the DTO to the UserSubscription entity
            var userSubscription = new UserSubscription
            {
                SubscriptionPlanId = userSubscriptionDto.SubscriptionPlanId,
                StartDate = userSubscriptionDto.StartDate,
                EndDate = userSubscriptionDto.EndDate,
                AdminId = userSubscriptionDto.AdminId
            };

            // Add the UserSubscription entity to the DbContext
            _context.UserSubscriptions.Add(userSubscription);

            // Save changes to the database
            await _context.SaveChangesAsync();
            return userSubscription;
        }


        public async Task RemoveUserSubscriptionAsync(int subscriptionId)
        {
            var user = await _context.UserSubscriptions
                .FirstOrDefaultAsync(u => u.UserSubscriptionId == subscriptionId);

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

        public async Task<AutomationRule> AddAutomationRuleAsync(AutomationRuleDto automationRuleDto)
        {
            // Check if the Admin exists
            var adminExists = await _context.Admins.AnyAsync(a => a.AdminId == automationRuleDto.AdminId);
            if (!adminExists)
            {
                throw new ArgumentException("Admin with the specified ID does not exist.");
            }

            // Map the DTO to the AutomationRule entity
            var automationRule = new AutomationRule
            {
                Name = automationRuleDto.Name,
                Description = automationRuleDto.Description,
                Trigger = automationRuleDto.Trigger,
                Action = automationRuleDto.Action,
                IsActive = automationRuleDto.IsActive,
                AdminId = automationRuleDto.AdminId
            };

            // Add the AutomationRule entity to the DbContext
            _context.AutomationRules.Add(automationRule);

            // Save changes to the database
            await _context.SaveChangesAsync();
            return automationRule;
        }


        public async Task RemoveAutomationRuleAsync( int ruleId)
        {
            var automation = await _context.AutomationRules
                .FirstOrDefaultAsync(a => a.AutomationRuleId == ruleId);

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

        public async Task<Notification> AddNotificationAsync(NotificationDto notificationDto)
        {
            // Check if the Admin exists
            var adminExists = await _context.Admins.AnyAsync(a => a.AdminId == notificationDto.AdminId);
            if (!adminExists)
            {
                throw new ArgumentException("Admin with the specified ID does not exist.");
            }

            // Map the DTO to the Notification entity
            var notification = new Notification
            {
                Message = notificationDto.Message,
                Timestamp = notificationDto.Timestamp,
                IsRead = notificationDto.IsRead,
                AdminId = notificationDto.AdminId
            };

            // Add the Notification entity to the DbContext
            _context.Notifications.Add(notification);

            // Save changes to the database
            await _context.SaveChangesAsync();
            return notification;
        }


        public async Task RemoveNotificationAsync( int notificationId)
        {
            var notification = await _context.Notifications
                .FirstOrDefaultAsync(n =>  n.NotificationId == notificationId);

            if(notification != null)
            {
                _context.Notifications.Remove(notification);
                await _context.SaveChangesAsync();
            }
        }
    }
}
