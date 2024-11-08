namespace DELAY.Core.Application.Contracts.Models.Auth
{
    /// <summary>
    /// External auth profile info
    /// </summary>
    public abstract class UserExternalServiceCredentials
    {
        protected UserExternalServiceCredentials(string givenName, string email, string name, string familyName) : this(givenName, email)
        {
            Name = name;
            FamilyName = familyName;
        }

        public string GivenName { get; set; }

        public string Email { get; set; }

        public string Name { get; set; }

        public string FamilyName { get; set; }
    }
}
