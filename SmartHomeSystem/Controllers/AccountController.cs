using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartHomeSystem.Data;
using SmartHomeSystem.Models;
using SmartHomeSystem.Models.DTO.Request;
using SmartHomeSystem.Models.DTO.Response;
using SmartHomeSystem.Repository.Interface;
using SmartHomeSystem.Repository.Services;
using System.Security.Claims;

namespace SmartHomeSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAcountUser _userManager;
        private readonly UserManager<Characters> _identityUserManager;

        public AccountController(IAcountUser userManager, UserManager<Characters> identityUserManager)
        {
            _userManager = userManager;
            _identityUserManager = identityUserManager;
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
        [HttpPost("ChangePassword")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDTO changePasswordDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = _identityUserManager.GetUserId(User); // Get the currently logged-in user's ID
            var result = await _userManager.ChangePasswordAsync(userId, changePasswordDTO);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return BadRequest(ModelState);
            }

            return Ok("Password changed successfully.");
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
        
        [HttpPost("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDTO forgotPasswordDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = _identityUserManager.GetUserId(User); // Get the currently logged-in user's ID
            var result = await _userManager.ForgetPasswordAsync(forgotPasswordDTO);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return BadRequest(ModelState);
            }

            return Ok("Password changed successfully.");
        }

    }
}
