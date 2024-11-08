using DELAY.Core.Application.Contracts.Models;

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
