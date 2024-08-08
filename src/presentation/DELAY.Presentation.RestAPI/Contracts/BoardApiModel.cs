using DELAY.Core.Application.Contracts.Models.Base;
using DELAY.Core.Domain.Models.Base;

namespace DELAY.Core.Application.Contracts.Models
{
    public class BoardApiModel : KeyNameApiModel
    {
        public string Description { get; set; }
        public ICollection<KeyNameApiModel> BoardUsers { get; set; } = new List<KeyNameApiModel>();
    }
}
