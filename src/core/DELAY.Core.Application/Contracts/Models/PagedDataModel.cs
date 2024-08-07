namespace DELAY.Core.Application.Contracts.Models
{
    public class PagedDataModel<T>
    {
        public PagedDataModel()
        {
        }

        public PagedDataModel(int totalCount, IReadOnlyList<T> data)
        {
            TotalCount = totalCount;
            Data = data;
        }

        public int TotalCount { get; set; }

        public IReadOnlyList<T> Data { get; set; }
    }
}
