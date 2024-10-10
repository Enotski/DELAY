using DELAY.Core.Application.Contracts.Models;
using DELAY.Core.Application.Contracts.Models.Dtos.Base;
using DELAY.Core.Domain.Interfaces;
using DELAY.Core.Domain.Models.Base;

namespace DELAY.Core.Application.Abstractions.Services
{
    /// <summary>
    /// Service for named models
    /// </summary>
    public interface INamedService
    {
        /// <summary>
        /// Delete by key
        /// </summary>
        /// <param name="id">Key</param>
        /// <param name="triggeredBy">What invoke operation</param>
        /// <returns></returns>
        Task<int> DeleteAsync(Guid id, OperationUserInfo triggeredBy);
    }
}
