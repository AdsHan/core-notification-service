﻿namespace SNR.Core.Settings
{
    public class TokenSettings
    {
        public string SecretJWTKey { get; set; }
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public int ExpireHours { get; set; }
    }
}
