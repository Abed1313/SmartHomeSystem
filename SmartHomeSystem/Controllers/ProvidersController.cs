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
    [Authorize(Roles = "Provider")]
    public class ProvidersController : ControllerBase
    {
        private readonly IProvider _context;

        public ProvidersController(IProvider userManager)
        {
            _context = userManager;
        }

        // GET: api/Providers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Provider>>> GetProviders()
        {
          var provider = await _context.GetAllProviderAsync();
            return Ok(provider);
        }

        // GET: api/Providers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Provider>> GetProvider(int id)
        {
            var provider = await _context.GetProviderById(id);
            if(provider == null)
            {
                return NotFound();
            }
            var providerDto = new ProviderDto
            {
                Name = provider.Name,
                Email = provider.Email,
            };

            return Ok(providerDto);
        }
        // GET: api/provider/{id}/alerts
        [HttpGet("{id}/alerts")]
        public async Task<ActionResult<IEnumerable<Alert>>> GetManagedAlerts(int id)
        {
            var alert = await _context.GetManagedAlertsAsync(id);
            return Ok(alert);
        }
        // GET: api/provider/{id}/devices
        [HttpGet("{id}/devices")]
        public async Task<ActionResult<IEnumerable<Device>>> GetManagedDevices(int id)
        {
            var devise = await _context.GetManagedDevicesAsync(id);
            return Ok(devise);
        }
        // GET: api/provider/{id}/energyusages
        [HttpGet("{id}/energyusages")]
        public async Task<ActionResult<IEnumerable<EnergyUsage>>> GetManagedEnergyUsages(int id)
        {
            var energe = await _context.GetManagedEnergyUsagesAsync(id);
            return Ok(energe);
        }
        // GET: api/provider/{id}/houses
        [HttpGet("{id}/houses")]
        public async Task<ActionResult<IEnumerable<House>>> GetManagedHouses(int id)
        {
            var Houses = await _context.GetManagedHousesAsync(id);
            return Ok(Houses);
        }
        // GET: api/provider/{id}/subscriptionplans
        [HttpGet("{id}/subscriptionplans")]
        public async Task<ActionResult<IEnumerable<SubscriptionPlan>>> GetManagedSubscriptionPlans(int id)
        {
            var plan = await _context.GetManagedSubscriptionPlansAsync(id);
            return Ok(plan);
        }
        // POST: api/provider/alerts
        [HttpPost("alerts")]
        public async Task<ActionResult<Alert>> AddAlert(AlertDto alertDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var alert = await _context.AddAlertAsync(alertDto);
            return Ok(alert);

        }
        // POST: api/provider/devices
        [HttpPost("devices")]
        public async Task<ActionResult<Device>> AddDevice(DeviceDto deviceDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var devise = await _context.AddDeviceAsync(deviceDto);
            return Ok(devise);
        }

        // POST: api/provider/houses
        [HttpPost("houses")]
        public async Task<ActionResult<House>> AddHouse(HouseDto houseDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var house = await _context.AddHouseAsync(houseDto);
            return Ok(house);
        }

        // POST: api/provider/subscriptionplans
        [HttpPost("subscriptionplans")]
        public async Task<ActionResult<SubscriptionPlan>> AddSubscriptionPlan(SubscriptionPlanDto planDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var plan = await _context.AddSubscriptionPlanAsync(planDto);
            return Ok(plan);
        }
    }

}

