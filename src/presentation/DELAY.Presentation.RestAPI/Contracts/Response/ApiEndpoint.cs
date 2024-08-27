namespace DELAY.Presentation.RestAPI.Contracts.Response
{
    public class ApiEndpoint
    {
        public ApiEndpoint()
        {
        }

        public ApiEndpoint(string path, string title)
        {
            Path = path;
            Title = title;
        }

        public string Path { get; set; }
        public string Title { get; set; }
    }
}
