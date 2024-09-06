using SmartHomeSystem.Models;
using SmartHomeSystem.Models.DTO.Response;

namespace SmartHomeSystem.Repository.Interface
{
    public interface IProvider
    {
        Task<IEnumerable<Provider>> GetAllProviderAsync();
        Task<Provider> GetProviderById(int providerId);
        Task<IEnumerable<Alert>> GetManagedAlertsAsync(int providerId);
        Task<IEnumerable<Device>> GetManagedDevicesAsync(int providerId);
        Task<IEnumerable<EnergyUsage>> GetManagedEnergyUsagesAsync(int providerId);
        Task<IEnumerable<House>> GetManagedHousesAsync(int providerId);
        Task<IEnumerable<SubscriptionPlan>> GetManagedSubscriptionPlansAsync(int providerId);

        Task<Alert> AddAlertAsync(AlertDto alertDto);
        Task<Device> AddDeviceAsync(DeviceDto deviceDto);
        Task<House> AddHouseAsync(HouseDto houseDto);
        Task<SubscriptionPlan> AddSubscriptionPlanAsync(SubscriptionPlanDto planDto);

    }
}
