namespace DELAY.Core.Application.Contracts.Models.Request
{
    /// <summary>
    /// Pagination options for records select
    /// </summary>
    public sealed class PaginationOptionsDto
    {
        public int? Skip { get; set; }

        public int? Take { get; set; }

        public PaginationOptionsDto() { }

        public PaginationOptionsDto(int? take)
        {
            Take = take;
        }

        public PaginationOptionsDto(int skip, int take)
        {
            Skip = skip;
            Take = take;
        }
    }
}
