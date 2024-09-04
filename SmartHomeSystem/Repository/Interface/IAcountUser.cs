﻿using Microsoft.AspNetCore.Mvc.ModelBinding;
using SmartHomeSystem.Models;
using SmartHomeSystem.Models.DTO.Request;
using SmartHomeSystem.Models.DTO.Response;
using System.Security.Claims;

namespace SmartHomeSystem.Repository.Interface
{
    public interface IAcountUser
    {
        //Add regester
        Task<LogDTO> Register(RegisterUserDTO registerEmployeeDTO, ModelStateDictionary modelState);

        // Add Login 
        public Task<LogDTO> LoginUser(string Username, string Password);

        public Task<LogDTO> LogoutUser(string Username);
        //Task AddRoleSpecificEntity(string role, Characters account);
        public Task<LogDTO> DeleteAccount(string username);

        // add user profile 
        public Task<LogDTO> UserProfile(ClaimsPrincipal claimsPrincipal);
    }
}
