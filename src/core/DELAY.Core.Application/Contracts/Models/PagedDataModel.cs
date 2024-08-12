namespace DELAY.Core.Application.Contracts.Models
{
    public class PagedDataModel<T>
    {
        public PagedDataModel()
        {
        }

        public PagedDataModel(int totalCount, IEnumerable<T> data)
        {
            TotalCount = totalCount;
            Data = data;
        }

        public int TotalCount { get; set; }

        public IEnumerable<T> Data { get; set; }
    }
}
