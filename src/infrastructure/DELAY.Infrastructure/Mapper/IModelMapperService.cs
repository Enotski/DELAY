namespace DELAY.Core.Application.Abstractions.Mapper
{
    public interface IModelMapperService
    {
        TDestination Map<TDestination>(object source);
    }
}
