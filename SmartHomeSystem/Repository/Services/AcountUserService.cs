﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SmartHomeSystem.Data;
using SmartHomeSystem.Models.DTO.Request;
using SmartHomeSystem.Models.DTO.Response;
using SmartHomeSystem.Models;
using SmartHomeSystem.Repository.Interface;
using System.Security.Claims;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Net.Mail;
using System.Net;
using Microsoft.AspNetCore.Http.HttpResults;

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

            // Generate the token and roles
            var token = await _jwtTokenServices.GenerateToken(user, TimeSpan.FromDays(14));
            var roles = await _userManager.GetRolesAsync(user);

            // Prepare the OTP message
            string otpMessage = $"Logged in on time {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} , Welcome {user.UserName}! " +
                                $"Thank you for using Smart Home System, where your home is at your fingertips. " +
                                "Manage your devices, control access, and experience the comfort and security that technology brings to your living space. " +
                                "Stay connected, stay secure, and enjoy the convenience of a smarter home!";
            string subject = "A13";

            // Send OTP via email 
            SendOtpViaEmail(otpMessage, user.Email, subject);

            return new LogDTO
            {
                Id = user.Id,
                UserName = user.UserName,
                Token = await _jwtTokenServices.GenerateToken(user, TimeSpan.FromDays(14)),
                Roles = await _userManager.GetRolesAsync(user)
            };

        }
      public async Task SendOtpViaEmail(string mess, string email, string subject)
        {
            // Create a new instance of MailMessage class
            MailMessage message = new MailMessage();
            // Set subject of the message, body and sender information
            message.Subject = subject;
            message.Body = mess;
            message.From = new MailAddress("Example@outlook.com", "Admin"); 
            // Add To recipients and CC recipients
            message.To.Add(new MailAddress(email, "Recipient 1"));
            // Create an instance of SmtpClient class
            SmtpClient client = new SmtpClient();
            // Specify your mailing Host, Username, Password, Port # and Security option
            client.Host = "smtp.office365.com";
            client.Credentials = new NetworkCredential("Example@outlook.com", "Password");
            client.Port = 587;
            client.EnableSsl = true;
            try
            {
                // Send this email
                client.Send(message);
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.ToString());
            }
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
                await _context.SaveChangesAsync(); 

                //// Generate OTP or confirmation message
                //string otp = GenerateOtp(); // You can implement this method to generate an OTP.
                //string emailSubject = "Welcome to the platform - Confirm your account";
                //string emailBody = $"Dear {registerUserDTO.UserName},\n\nYour OTP code is: {otp}\n\nUse this to confirm your registration.";

                //// Send OTP via email
                //SendOtpViaEmail(emailBody, registerUserDTO.Email, emailSubject);

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

        //private string GenerateOtp()
        //{
        //    Random random = new Random();
        //    return random.Next(100000, 999999).ToString();
        //}

        public async Task<IdentityResult> ChangePasswordAsync(string userId, ChangePasswordDTO model)
        {                                                        // To ChangePassword you must be Loged in 
            // Get the user by Id
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return IdentityResult.Failed(new IdentityError
                {
                    Description = "User not found."
                });
            }

            // Ensure the new password matches the confirmation
            if (model.NewPassword != model.ConfirmNewPassword)
            {
                return IdentityResult.Failed(new IdentityError
                {
                    Description = "New password and confirmation do not match."
                });
            }

            // Change the user's password
            var result = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
            if (result.Succeeded)
            {
                // Sign in the user again to refresh the security token
                await _signInManager.RefreshSignInAsync(user);
            }

            return result;
        }
        public async Task<IdentityResult> ForgetPasswordAsync(ForgotPasswordDTO model)
        {
            // Check if user exists by Email and Name
            var user = await _userManager.Users
                .FirstOrDefaultAsync(u => u.Email == model.Email && u.UserName == model.Name);

            if (user == null)
            {
                return IdentityResult.Failed(new IdentityError
                {
                    Description = "User with the specified email and name not found."
                });
            }

            // Ensure the new password matches the confirmation
            if (model.NewPassword != model.ConfirmNewPassword)
            {
                return IdentityResult.Failed(new IdentityError
                {
                    Description = "New password and confirmation do not match."
                });
            }

            // Generate a password reset token
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            // Reset the user's password using the token
            var result = await _userManager.ResetPasswordAsync(user, token, model.NewPassword);

            if (result.Succeeded)
            {
                // Optionally: Sign in the user again to refresh the security token
                await _signInManager.RefreshSignInAsync(user);
            }

            return result;
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