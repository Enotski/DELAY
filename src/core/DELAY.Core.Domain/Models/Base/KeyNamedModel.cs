using DELAY.Core.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DELAY.Core.Domain.Models.Base
{
    public class KeyNamedModel : KeyModel, IName
    {
        public string Name { get; set; }
    }
}
