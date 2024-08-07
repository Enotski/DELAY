namespace DELAY.Core.Application.Contracts.Models
{
    public class PagedDataApiModel<T>
    {
        public PagedDataApiModel()
        {
        }

        public PagedDataApiModel(int totalCount, IReadOnlyList<T> data)
        {
            TotalCount = totalCount;
            Data = data;
        }

        public int TotalCount { get; set; }

        public IReadOnlyList<T> Data { get; set; }
    }
}
