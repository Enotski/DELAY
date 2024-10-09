namespace DELAY.Core.Application.Contracts.Models.Dtos.Response
{
    public class PagedDataDto<T>
    {
        public PagedDataDto()
        {
        }

        public PagedDataDto(int totalCount, IReadOnlyList<T> data)
        {
            TotalCount = totalCount;
            Data = data;
        }

        public int TotalCount { get; set; }

        public IReadOnlyList<T> Data { get; set; }
    }
}
