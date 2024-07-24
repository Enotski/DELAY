namespace DELAY.Core.Application.Contracts.Models.Request
{
    public class SearchOptionsDto
    {
        public string Term { get; set; }

        public SearchOptionsDto() { }
    }

    public class KeysSearchOptionsDto : SearchOptionsDto
    {
        public IEnumerable<Guid> Keys { get; set; }

        public KeysSearchOptionsDto() { }
    }
}
