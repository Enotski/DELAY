namespace DELAY.Core.Application.Contracts.Models.SelectOptions
{
    /// <summary>
    /// Pagination options for records select
    /// </summary>
    public sealed class PaginationOptions
    {
        public int? Skip { get; set; }

        public int? Take { get; set; }

        public PaginationOptions() { }

        public PaginationOptions(int? take)
        {
            Take = take;
        }

        public PaginationOptions(int skip, int take)
        {
            Skip = skip;
            Take = take;
        }
    }
}
