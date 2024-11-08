namespace DELAY.Core.Application.Abstractions.Services.Common
{
    /// <summary>
    /// Common mapper service
    /// </summary>
    public interface IModelMapperService
    {
        /// <summary>
        /// Map method
        /// </summary>
        /// <typeparam name="TDestination">To type</typeparam>
        /// <param name="source">Object for mapping</param>
        /// <returns></returns>
        TDestination Map<TDestination>(object source);
    }
}
