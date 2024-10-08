﻿namespace SmartHomeSystem.Models.DTO.Response
{
    public class AccessControlDto
    {
        public int AdminId { get; set; }
        public int HouseId { get; set; }
        public int GuestId { get; set; }
        public int AccessLevelId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
    }
}
