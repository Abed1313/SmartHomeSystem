using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartHomeSystem.Models.DTO.Request;
using SmartHomeSystem.Models.DTO.Response;
using SmartHomeSystem.Repository.Interface;

namespace SmartHomeSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAcountUser _userManager;

        public AccountController(IAcountUser userManager)
        {
            _userManager = userManager;
        }

        [HttpPost("Register")]
        public async Task<ActionResult> Register(RegisterUserDTO registerEmployeeDTO)
        {
            var employee = await _userManager.Register(registerEmployeeDTO, this.ModelState);
            if (ModelState.IsValid)
            {
                return Ok(employee);
            }
            return BadRequest(ModelState);
        }

        [HttpPost("Login")]
        public async Task<ActionResult<LogDTO>> Login(LoginDTO loginDto)
        {
            var user = await _userManager.LoginUser(loginDto.Username, loginDto.Password);

            if (user == null)
            {
                return Unauthorized();
            }
            return user;
        }

        [HttpPost("Logout")]
        public async Task<LogDTO> Logout(string Username)
        {
            var user = await _userManager.LogoutUser(Username);
            return user;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("Profile")]
        public async Task<ActionResult<LogDTO>> Profile()
        {
            return await _userManager.UserProfile(User);
        }

        [HttpDelete("DeleteAccount")]
       public async Task DeleteAccount(string username)
        {
            var user = await _userManager.DeleteAccount(username);
        }
    }
}
