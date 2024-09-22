using Microsoft.EntityFrameworkCore;
using SmartHomeSystem.Data;
using SmartHomeSystem.Models;
using SmartHomeSystem.Models.DTO.Response;
using SmartHomeSystem.Repository.Interface;

namespace SmartHomeSystem.Repository.Services
{
    public class GuestService : IGuest
    {
        private readonly SmartHomeDbContext _context;

        public GuestService(SmartHomeDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Guest>> GetAllGuestAsync()
        {
            return await _context.Guests.ToListAsync();
        }
        public async Task<Guest> GetGuestById(int guestId)
        {
            var guest = await _context.Guests
                .Include(g => g.AllowedAccessControls)
                .Include(g => g.AccessibleDevices)
                .Include(g => g.AccessibleScenes)
                .FirstOrDefaultAsync(g => g.GuestId == guestId);

            if (guest == null)
            {
                throw new KeyNotFoundException($"Guest with ID {guestId} was not found.");
            }

            return guest;
        }
        public async Task<AccessControl> AddAccessControlAsync(AccessControlDto controlDto)
        {
            var accessControl = new AccessControl
            {
                AdminId = controlDto.AdminId,
                HouseId = controlDto.HouseId,
                AccessLevelId = controlDto.AccessLevelId,
                StartTime = controlDto.StartTime,
                EndTime = controlDto.EndTime,
                GuestId = controlDto.GuestId,
            };

            // Add the AccessControl entity to the DbContext
            _context.AccessControls.Add(accessControl);

            // Save changes to the database
            await _context.SaveChangesAsync();
            return accessControl;
        }

        public async Task<Device> AddDeviceAsync(DeviceDto deviceDto)
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

            _context.Devices.Add(devise);
            await _context.SaveChangesAsync();
            return devise;
        }

        public async Task<Scene> AddSceneAsync(SceneDto sceneDto)
        {
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

        public async Task<IEnumerable<Device>> GetAccessibleDevicesAsync(int guestId)
        {
            return await _context.Devices
                .Where(d => d.GuestId == guestId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Scene>> GetAccessibleScenesAsync(int guestId)
        {
            return await _context.Scenes
                .Where(s =>s.GuestId == guestId)
                .ToListAsync();
        }

        

        public async Task<IEnumerable<AccessControl>> GetAllowedAccessControlsAsync(int guestId)
        {
            return await _context.AccessControls
                .Where(a => a.GuestId == guestId)
                .ToListAsync();
        }


        public async Task RemoveAccessControlAsync( int accessControlId)
        {
            var access = await _context.AccessControls
                .FirstOrDefaultAsync(a =>a.AccessControlId == accessControlId);

            if(access != null)
            {
                _context.AccessControls.Remove(access);
                await _context.SaveChangesAsync();
            }
        }

        public async Task RemoveDeviceAsync( int deviceId)
        {
            var device = await _context.Devices
                .FirstOrDefaultAsync (a => a.DeviceId == deviceId);

            if (device != null)
            {
                _context.Devices.Remove(device);
                await _context.SaveChangesAsync();
            }
        }

        public async Task RemoveSceneAsync( int sceneId)
        {
            var scane = await _context.Scenes
                .FirstOrDefaultAsync(s =>s.SceneId == sceneId);

            if(scane != null)
            {
                _context.Scenes.Remove(scane);
                await _context.SaveChangesAsync();
            }
        }
    }
}
