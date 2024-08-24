namespace DELAY.Core.Application.Contracts.Models.Auth
{
    public class GoogleUserCredentials : UserExternalServiceCredentials
    {
        public GoogleUserCredentials(string email, string name, string givenName, string familyName) : base(givenName, email)
        {
            GivenName = givenName;
            FamilyName = familyName;
        }

        public string Name { get; set; }
        public string FamilyName { get; set; }
    }
}
