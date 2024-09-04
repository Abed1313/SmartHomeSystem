using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SmartHomeSystem.Data;
using SmartHomeSystem.Models.DTO.Request;
using SmartHomeSystem.Models.DTO.Response;
using SmartHomeSystem.Models;
using SmartHomeSystem.Repository.Interface;
using System.Security.Claims;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace SmartHomeSystem.Repository.Services
{
    public class AccountUserService : IAcountUser
    {
        private readonly UserManager<Characters> _userManager;
        private readonly SignInManager<Characters> _signInManager;
        private readonly JwtTokenServeses _jwtTokenServices;
        private readonly SmartHomeDbContext _context;

        // Constructor
        public AccountUserService(UserManager<Characters> userManager,
                                  SignInManager<Characters> signInManager,
                                  JwtTokenServeses jwtTokenServices,
                                  SmartHomeDbContext context)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
            _jwtTokenServices = jwtTokenServices ?? throw new ArgumentNullException(nameof(jwtTokenServices));
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        // Login
        public async Task<LogDTO> LoginUser(string username, string password)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null || !(await _userManager.CheckPasswordAsync(user, password)))
            {
                return null; // or return a custom error indicating invalid credentials
            }

            return new LogDTO
            {
                Id = user.Id,
                UserName = user.UserName,
                Token = await _jwtTokenServices.GenerateToken(user, TimeSpan.FromDays(14)),
                Roles = await _userManager.GetRolesAsync(user)
            };
        }

        // Logout
        public async Task<LogDTO> LogoutUser(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                return null;
            }

            await _signInManager.SignOutAsync();
            return new LogDTO
            {
                Id = user.Id,
                UserName = user.UserName
            };
        }

        // User Profile
        public async Task<LogDTO> UserProfile(ClaimsPrincipal claimsPrincipal)
        {
            var user = await _userManager.GetUserAsync(claimsPrincipal);
            return new LogDTO
            {
                Id = user.Id,
                UserName = user.UserName,
                Token = await _jwtTokenServices.GenerateToken(user, TimeSpan.FromMinutes(7)),
                Roles = await _userManager.GetRolesAsync(user)
            };
        }

        // Register
        public async Task<LogDTO> Register(RegisterUserDTO registerUserDTO, ModelStateDictionary modelState)
        {
            if (!registerUserDTO.Roles.Contains("Guest") && !registerUserDTO.Roles.Contains("Provider") && !registerUserDTO.Roles.Contains("Admin"))
            {
                throw new ArgumentException("User must have either the 'Guest' or 'Provider' role to register.");
            }

            var account = new Characters
            {
                UserName = registerUserDTO.UserName,
                Email = registerUserDTO.Email,
            };

            var result = await _userManager.CreateAsync(account, registerUserDTO.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRolesAsync(account, registerUserDTO.Roles);

                foreach (var role in registerUserDTO.Roles)
                {
                    switch (role)
                    {
                        case "Admin":
                            // Check if there are already 3 admins
                            var adminCount = await _context.Admins.CountAsync();
                            if (adminCount >= 3)
                            {
                                throw new InvalidOperationException("Cannot add more than 3 admins.");
                            }

                            var admin = new Admin
                            {
                                CharactersId = account.Id,
                                Name = account.UserName,
                                Email = account.Email
                            };
                            _context.Admins.Add(admin);
                            break;
                        case "Guest":
                            var guest = new Guest
                            {
                                CharactersId = account.Id,
                                Name = account.UserName
                            };
                            _context.Guests.Add(guest);
                            break;
                        case "Provider":
                            var provider = new Provider
                            {
                                CharactersId = account.Id,
                                Name = account.UserName,
                                Email = account.Email
                            };
                            _context.Providers.Add(provider);
                            break;
                    }
                }
                await _context.SaveChangesAsync(); // Ensure changes are saved

                return new LogDTO
                {
                    Id = account.Id,
                    UserName = account.UserName,
                    Token = await _jwtTokenServices.GenerateToken(account, TimeSpan.FromMinutes(7)),
                    Roles = await _userManager.GetRolesAsync(account)
                };
            }

            throw new Exception("User creation failed: " + string.Join(", ", result.Errors.Select(e => e.Description)));
        }



        // Delete User
        public async Task<LogDTO> DeleteAccount(string username)
        {
            var account = await _userManager.FindByNameAsync(username);
            if (account == null)
            {
                throw new Exception("Account not found.");
            }

            await _userManager.DeleteAsync(account);
            return new LogDTO
            {
                Id = account.Id,
                UserName = account.UserName
            };
        }
    }
}