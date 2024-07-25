using DELAY.Core.Application.Abstractions.Mapper;
using MapsterMapper;

namespace DELAY.Core.Application.Mapper
{
    internal class MapsterMapperService : IModelMapperService
    {
        private IMapper Mapper { get; set; }
        public MapsterMapperService(IMapper mapper)
        {
            Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public TDestination Map<TDestination>(object source)
        {
            return Mapper.Map<TDestination>(source);
        }
    }
}
