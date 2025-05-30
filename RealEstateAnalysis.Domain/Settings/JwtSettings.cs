﻿namespace RealEstateAnalysis.Domain.Settings
{
    public class JwtSettings
    {
        public string ValidIssuer { get; set; }

        public string ValidAudience { get; set; }

        public string PrivateKey { get; set; }

        public int JwtTokenExpiresInHours { get; set; }
    }
}