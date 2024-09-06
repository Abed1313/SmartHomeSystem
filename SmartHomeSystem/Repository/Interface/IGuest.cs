using SmartHomeSystem.Models;
using SmartHomeSystem.Models.DTO.Response;

namespace SmartHomeSystem.Repository.Interface
{
    public interface IGuest
    {
        Task<IEnumerable<Guest>> GetAllGuestAsync();
        Task<Guest> GetGuestById(int guestId);
        Task<AccessControl> AddAccessControlAsync(AccessControlDto controlDto); // Method to add an access control to a guest
        Task RemoveAccessControlAsync( int accessControlId); // Method to remove an access control from a guest
        Task<IEnumerable<AccessControl>> GetAllowedAccessControlsAsync(int guestId);  // Method to get all access controls for a guest
        Task<Device> AddDeviceAsync(DeviceDto deviceDto); // Method to add a device to the guest's accessible devices

        Task RemoveDeviceAsync( int deviceId);// Method to remove a device from the guest's accessible devices
        
        Task<IEnumerable<Device>> GetAccessibleDevicesAsync(int guestId);// Method to get all devices accessible by the guest

        Task<Scene> AddSceneAsync(SceneDto sceneDto);// Method to add a scene to the guest's accessible scenes

        Task RemoveSceneAsync( int sceneId);// Method to remove a scene from the guest's accessible scenes

        Task<IEnumerable<Scene>> GetAccessibleScenesAsync(int guestId);// Method to get all scenes accessible by the guest
    }
}
