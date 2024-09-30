namespace DELAY.Core.Application.Abstractions.Services.Common
{
    public interface IModelMapperService
    {
        TDestination Map<TDestination>(object source);
    }
}
