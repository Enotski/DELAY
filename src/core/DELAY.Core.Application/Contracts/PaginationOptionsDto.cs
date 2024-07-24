namespace DELAY.Core.Application.Contracts
{
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
