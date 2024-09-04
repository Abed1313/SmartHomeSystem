﻿namespace SmartHomeSystem.Models.DTO.Response
{
    public class LogDTO
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Token { get; set; }
        public IList<string> Roles { get; set; }
    }
}
