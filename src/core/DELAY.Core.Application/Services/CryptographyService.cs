using DELAY.Core.Application.Abstractions.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DELAY.Core.Application.Services
{
    internal class CryptographyService : ICryptographyService
    {
        public string GetHash(string plainText)
        {
            throw new NotImplementedException();
        }

        public bool IsEqual(string plainText, string hashText)
        {
            throw new NotImplementedException();
        }
    }
}
