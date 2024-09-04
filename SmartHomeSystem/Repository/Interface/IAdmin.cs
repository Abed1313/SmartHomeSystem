using SmartHomeSystem.Models;
using SmartHomeSystem.Models.DTO.Response;

namespace SmartHomeSystem.Repository.Interface
{
    public interface IAdmin
    {
        Task<IEnumerable<Admin>> GetAllAdminAsync();
        Task<Admin> GetAdminById(int adminId);

        // Methods related to managing houses
        Task<IEnumerable<House>> GetManagedHousesAsync(int adminId);
        Task<House> AddHouseAsync(HouseDto houseDto);
        Task RemoveHouseAsync(int adminId, int houseId);

        // Methods related to managing devices
        Task<IEnumerable<Device>> GetManagedDevicesAsync(int adminId);
        Task AddDeviceAsync(int adminId, Device device);
        Task RemoveDeviceAsync(int adminId, int deviceId);

        // Methods related to managing subscription plans
        Task<IEnumerable<SubscriptionPlan>> GetManagedSubscriptionPlansAsync(int adminId);
        Task AddSubscriptionPlanAsync(int adminId, SubscriptionPlan plan);
        Task RemoveSubscriptionPlanAsync(int adminId, int planId);

        // Methods related to managing alerts
        Task<IEnumerable<Alert>> GetManagedAlertsAsync(int adminId);
        Task AddAlertAsync(int adminId, Alert alert);
        Task RemoveAlertAsync(int adminId, int alertId);

        // Methods related to managing energy usages
        Task<IEnumerable<EnergyUsage>> GetManagedEnergyUsagesAsync(int adminId);
        Task AddEnergyUsageAsync(int adminId, EnergyUsage energyUsage);
        Task RemoveEnergyUsageAsync(int adminId, int energyUsageId);

        // Methods related to managing access controls
        Task<IEnumerable<AccessControl>> GetAllowedAccessControlsAsync(int adminId);
        Task AddAccessControlAsync(int adminId, AccessControl accessControl);
        Task RemoveAccessControlAsync(int adminId, int accessControlId);

        // Methods related to managing scenes
        Task<IEnumerable<Scene>> GetAccessibleScenesAsync(int adminId);
        Task AddSceneAsync(int adminId, Scene scene);
        Task RemoveSceneAsync(int adminId, int sceneId);

        // Methods related to managing rooms
        Task<IEnumerable<Room>> GetRoomsAsync(int adminId);
        Task AddRoomAsync(int adminId, Room room);
        Task RemoveRoomAsync(int adminId, int roomId);

        // Methods related to managing user subscriptions
        Task<IEnumerable<UserSubscription>> GetUserSubscriptionsAsync(int adminId);
        Task AddUserSubscriptionAsync(int adminId, UserSubscription userSubscription);
        Task RemoveUserSubscriptionAsync(int adminId, int subscriptionId);

        // Methods related to managing automation rules
        Task<IEnumerable<AutomationRule>> GetAutomationRulesAsync(int adminId);
        Task AddAutomationRuleAsync(int adminId, AutomationRule automationRule);
        Task RemoveAutomationRuleAsync(int adminId, int ruleId);

        // Methods related to managing notifications
        Task<IEnumerable<Notification>> GetNotificationsAsync(int adminId);
        Task AddNotificationAsync(int adminId, Notification notification);
        Task RemoveNotificationAsync(int adminId, int notificationId);
    }
}
