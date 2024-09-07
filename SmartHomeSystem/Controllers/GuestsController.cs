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
    [Authorize(Roles = "Guest")]
    public class GuestsController : ControllerBase
    {
        private readonly IGuest _context;

        public GuestsController(IGuest userManager)
        {
            _context = userManager;
        }

        // GET: api/Guests
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Guest>>> GetGuests()
        {
          var guest = await _context.GetAllGuestAsync();
            return Ok(guest);
        }

        // GET: api/Guests/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Guest>> GetGuest(int id)
        {
            var guest = await _context.GetGuestById(id);

            if (guest == null)
            {
                return NotFound();
            }

            var guestDto = new GuestDto
            {
                Name = guest.Name,
            };

            return Ok(guestDto);
        }
        // Add an access control for the guest
        [HttpPost("{guestId}/accesscontrols")]
        public async Task<IActionResult> AddAccessControl( [FromBody] AccessControlDto controlDto)
        {
            await _context.AddAccessControlAsync(controlDto);
            return Ok(new { message = "Access control added successfully." });
        }

        // Remove an access control from the guest
        [HttpDelete("{guestId}/accesscontrols/{accessControlId}")]
        public async Task<IActionResult> RemoveAccessControl( int accessControlId)
        {
            await _context.RemoveAccessControlAsync( accessControlId);
            return Ok(new { message = "Access control removed successfully." });
        }

        // Get all access controls for the guest
        [HttpGet("{guestId}/accesscontrols")]
        public async Task<IActionResult> GetAllowedAccessControls(int guestId)
        {
            var accessControls = await _context.GetAllowedAccessControlsAsync(guestId);
            return Ok(accessControls);
        }

        // Add a device to the guest's accessible devices
        [HttpPost("{guestId}/devices")]
        public async Task<IActionResult> AddDevice( [FromBody] DeviceDto deviceDto)
        {
            await _context.AddDeviceAsync(deviceDto);
            return Ok(new { message = "Device added successfully." });
        }

        // Remove a device from the guest's accessible devices
        [HttpDelete("{guestId}/devices/{deviceId}")]
        public async Task<IActionResult> RemoveDevice( int deviceId)
        {
            await _context.RemoveDeviceAsync( deviceId);
            return Ok(new { message = "Device removed successfully." });
        }

        // Get all devices accessible by the guest
        [HttpGet("{guestId}/devices")]
        public async Task<IActionResult> GetAccessibleDevices(int guestId)
        {
            var devices = await _context.GetAccessibleDevicesAsync(guestId);
            return Ok(devices);
        }

        // Add a scene to the guest's accessible scenes
        [HttpPost("{guestId}/scenes")]
        public async Task<IActionResult> AddScene( [FromBody] SceneDto sceneDto)
        {
            await _context.AddSceneAsync(sceneDto);
            return Ok(new { message = "Scene added successfully." });
        }

        // Remove a scene from the guest's accessible scenes
        [HttpDelete("{guestId}/scenes/{sceneId}")]
        public async Task<IActionResult> RemoveScene( int sceneId)
        {
            await _context.RemoveSceneAsync( sceneId);
            return Ok(new { message = "Scene removed successfully." });
        }

        // Get all scenes accessible by the guest
        [HttpGet("{guestId}/scenes")]
        public async Task<IActionResult> GetAccessibleScenes(int guestId)
        {
            var scenes = await _context.GetAccessibleScenesAsync(guestId);
            return Ok(scenes);
        }

    }
}
