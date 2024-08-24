namespace DELAY.Core.Application.Contracts.Models.Auth
{
    public abstract class UserExternalServiceCredentials
    {
        protected UserExternalServiceCredentials(string givenName, string email)
        {
            GivenName = givenName;
            Email = email;
        }

        public string GivenName { get; set; }

        public string Email { get; set;}
    }
}
