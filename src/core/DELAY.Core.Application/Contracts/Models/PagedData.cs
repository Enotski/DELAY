namespace DELAY.Core.Application.Contracts.Models
{
    public class PagedData<T>
    {
        public PagedData()
        {
        }

        public PagedData(int totalCount, IEnumerable<T> data)
        {
            TotalCount = totalCount;
            Data = data;
        }

        public int TotalCount { get; set; }

        public IEnumerable<T> Data { get; set; }
    }
}
