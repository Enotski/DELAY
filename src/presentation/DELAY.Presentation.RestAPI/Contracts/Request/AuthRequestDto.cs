namespace DELAY.Presentation.RestAPI.Contracts.Request
{
    public class AuthRequestDto
    {
        public AuthRequestDto()
        {
        }

        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNunmber { get; set; }
        public string Password { get; set; }
    }
}
