﻿namespace DELAY.Core.Application.Contracts.Models.Auth
{
    public class VkAuthRequest
    {
        public VkAuthRequest()
        {
        }

        public string Code { get; set; }
        public string DeviceId { get; set; }
        public string CodeVerifier { get; set; }
    }
}
