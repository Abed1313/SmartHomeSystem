
using Microsoft.EntityFrameworkCore;
using SmartHomeSystem.Data;
using SmartHomeSystem.Models;
using SmartHomeSystem.Models.DTO.Response;
using SmartHomeSystem.Repository.Interface;

namespace SmartHomeSystem.Repository.Services
{
    public class ProviderService : IProvider
    {
        private readonly SmartHomeDbContext _context;

        public ProviderService(SmartHomeDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Provider>> GetAllProviderAsync()
        {
            return await _context.Providers.ToListAsync();
        }
        public async Task<Provider> GetProviderById(int providerId)
        {
            return await _context.Providers
        .Include(p => p.ManagedAlerts)
        .Include(p => p.ManagedEnergyUsages)
        .Include(p => p.ManagedHouses)
        .Include(p => p.ManagedDevices)
        .Include(p => p.ManagedSubscriptionPlans)
        .FirstOrDefaultAsync(p => p.ProviderId == providerId);
        }
        public async Task<IEnumerable<Alert>> GetManagedAlertsAsync(int providerId)
        {
            return await _context.Alerts
                .Where(a => a.ProviderId == providerId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Device>> GetManagedDevicesAsync(int providerId)
        {
            return await _context.Devices
                .Where(d => d.ProviderId == providerId)
                .ToListAsync();
        }

        public async Task<IEnumerable<EnergyUsage>> GetManagedEnergyUsagesAsync(int providerId)
        {
            return await _context.EnergyUsages
                .Where(e => e.ProviderId == providerId)
                .ToListAsync();
        }

        public async Task<IEnumerable<House>> GetManagedHousesAsync(int providerId)
        {
            return await _context.Houses
                .Where(h => h.ProviderId == providerId)
                .ToListAsync();
        }

        public async Task<IEnumerable<SubscriptionPlan>> GetManagedSubscriptionPlansAsync(int providerId)
        {
            return await _context.SubscriptionPlans
                .Where(s =>s.ProviderId == providerId)
                .ToListAsync();
        }

        public async Task<Alert> AddAlertAsync( AlertDto alertDto)
        {
            var ProviderExists = await _context.Providers.AnyAsync(a => a.ProviderId == alertDto.ProviderId);
            if (!ProviderExists)
            {
                throw new ArgumentException("Provider with the specified ID does not exist.");
            }
            // Map the DTO to the Alert entity
            var alert = new Alert
            {
                Message = alertDto.Message,
                Timestamp = alertDto.Timestamp,
                ActionSeverityId = alertDto.ActionSeverityId,
                ProviderId = alertDto.ProviderId,
                AdminId = alertDto.AdminId
            };
            _context.Alerts.Add(alert);

           await _context.SaveChangesAsync();
            return alert;
        }

        public async Task<Device> AddDeviceAsync(DeviceDto deviceDto)
        {
            var ProviderExists = await _context.Providers.AnyAsync(a => a.ProviderId == deviceDto.ProviderId);
            if (!ProviderExists)
            {
                throw new ArgumentException("Provider with the specified ID does not exist.");
            }

            var devise = new Device
            {
                
                Name = deviceDto.Name,
                AdminId = deviceDto.AdminId,
                DeviceTypeId = deviceDto.DeviceTypeId,
                GuestId = deviceDto.GuestId,
                RoomId = deviceDto.RoomId,
                IsOnline = deviceDto.IsOnline,
                LastCommunicationTime = deviceDto.LastCommunicationTime,
                ProviderId = deviceDto.ProviderId,
                ModelNumber = deviceDto.ModelNumber,
                Manufacturer = deviceDto.Manufacturer
            };

            _context.Devices.Add(devise);

            await _context.SaveChangesAsync();
            return devise;
        }

        public async Task<House> AddHouseAsync( HouseDto houseDto)
        {
            var ProviderExists = await _context.Providers.AnyAsync(a => a.ProviderId == houseDto.ProviderId);
            if (!ProviderExists)
            {
                throw new ArgumentException("Provider with the specified ID does not exist.");
            }
            var house = new House
            {
                Address = houseDto.Address,
                AdminId = houseDto.AdminId,
                ProviderId = houseDto.ProviderId,
            };

            _context.Houses.Add(house);
            await _context.SaveChangesAsync();
            return house;
        }

        public async Task<SubscriptionPlan> AddSubscriptionPlanAsync( SubscriptionPlanDto planDto)
        {
            var ProviderExists = await _context.Providers.AnyAsync(a => a.ProviderId == planDto.ProviderId);
            if (!ProviderExists)
            {
                throw new ArgumentException("Provider with the specified ID does not exist.");
            }

            var plan = new SubscriptionPlan
            {
                Name = planDto.Name,
                Description = planDto.Description,
                MonthlyCost = planDto.MonthlyCost,
                AdminId = planDto.AdminId,
                ProviderId = planDto.ProviderId
            };

            _context.SubscriptionPlans.Add(plan);
            await _context.SaveChangesAsync();
            return plan;
        }

    }
}
