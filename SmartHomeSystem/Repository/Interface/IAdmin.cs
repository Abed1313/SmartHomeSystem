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
        Task RemoveHouseAsync(int houseId);

        // Methods related to managing devices
        Task<IEnumerable<Device>> GetManagedDevicesAsync(int adminId);
        Task<Device> AddDeviceAsync( DeviceDto deviceDto);
        Task RemoveDeviceAsync( int deviceId);

        // Methods related to managing subscription plans
        Task<IEnumerable<SubscriptionPlan>> GetManagedSubscriptionPlansAsync(int adminId);
        Task<SubscriptionPlan> AddSubscriptionPlanAsync(SubscriptionPlanDto planDto);
        Task RemoveSubscriptionPlanAsync( int planId);

        // Methods related to managing alerts
        Task<IEnumerable<Alert>> GetManagedAlertsAsync(int adminId);
        Task<Alert> AddAlertAsync(AlertDto alertDto);
        Task RemoveAlertAsync( int alertId);

        // Methods related to managing energy usages
        Task<IEnumerable<EnergyUsage>> GetManagedEnergyUsagesAsync(int adminId);
        Task<EnergyUsage> AddEnergyUsageAsync(EnergyUsageDto energyUsageDto);
        Task RemoveEnergyUsageAsync( int energyUsageId);

        // Methods related to managing access controls
        Task<IEnumerable<AccessControl>> GetAllowedAccessControlsAsync(int adminId);
        Task<AccessControl> AddAccessControlAsync(AccessControlDto accessControlDto);
        Task RemoveAccessControlAsync( int accessControlId);

        // Methods related to managing scenes
        Task<IEnumerable<Scene>> GetAccessibleScenesAsync(int adminId);
        Task<Scene> AddSceneAsync(SceneDto sceneDto);
        Task RemoveSceneAsync( int sceneId);

        // Methods related to managing rooms
        Task<IEnumerable<Room>> GetRoomsAsync(int adminId);
        Task<Room> AddRoomAsync(RoomDto roomDto);
        Task RemoveRoomAsync( int roomId);

        // Methods related to managing user subscriptions
        Task<IEnumerable<UserSubscription>> GetUserSubscriptionsAsync(int adminId);
        Task<UserSubscription> AddUserSubscriptionAsync(UserSubscriptionDto userSubscriptionDto);
        Task RemoveUserSubscriptionAsync( int subscriptionId);

        // Methods related to managing automation rules
        Task<IEnumerable<AutomationRule>> GetAutomationRulesAsync(int adminId);
        Task<AutomationRule> AddAutomationRuleAsync(AutomationRuleDto automationRuleDto);
        Task RemoveAutomationRuleAsync( int ruleId);

        // Methods related to managing notifications
        Task<IEnumerable<Notification>> GetNotificationsAsync(int adminId);
        Task<Notification> AddNotificationAsync(NotificationDto notificationDto);
        Task RemoveNotificationAsync(int notificationId);
    }
}
