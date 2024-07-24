namespace DELAY.Core.Application.Contracts
{
    public class SearchOptionsDto
    {
        public string Term { get; set; }

        public PaginationOptionsDto PaginatedOption { get; set; }

        public SearchOptionsDto() { }
    }

    public class KeysSearchOptionsDto : SearchOptionsDto
    {
        public IEnumerable<Guid> Keys { get; set; }

        public KeysSearchOptionsDto() { }
    }
}
