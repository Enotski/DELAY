namespace DELAY.Presentation.RestAPI.Contracts
{
    public class ApiEndpointDto
    {
        public ApiEndpointDto()
        {
        }

        public ApiEndpointDto(string path, string title)
        {
            Path = path;
            Title = title;
        }

        public string Path { get; set; }
        public string Title { get; set; }
    }
}
