using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DELAY.Core.Application.Contracts.Models.Auth
{
    public class GoogleApiSecrets
    {
        public const string SectionName = "Authentication:Google";

        public GoogleApiSecrets()
        {
        }

        public GoogleApiSecrets(string clientSecret, string clientId)
        {
            ClientSecret = clientSecret;
            ClientId = clientId;
        }

        public string ClientSecret { get; set; }
        public string ClientId { get; set; }
    }
}
