﻿namespace RealEstateAnalysis.Domain.DTOs.Writes
{
    public class WritePasswordReset
    {
        public string UserId { get; set; }

        public string Token { get; set; }

        public string NewPassword { get; set; }
    }
}