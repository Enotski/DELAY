using DELAY.Core.Application.Contracts.Models.SelectOptions;

namespace DELAY.Core.Application.Contracts.Models
{
    public class PagedDataRequest
    {
        public PagedDataRequest()
        {
        }

        public IEnumerable<SearchOptions> Searchs { get; set; }
        public IEnumerable<SortOptions> Sorts { get; set; }
        public PaginationOptions Pagination { get; set; }
    }
}
