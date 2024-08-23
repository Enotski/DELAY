namespace DELAY.Core.Application.Contracts.Models.Auth
{
    public class GoogleUserCredentials
    {
        public GoogleUserCredentials(string email, string name, string givenName, string familyName)
        {
            Email = email;
            Name = name;
            GivenName = givenName;
            FamilyName = familyName;
        }

        public string Email { get; set; }
        public string Name { get; set; }
        public string GivenName { get; set; }
        public string FamilyName { get; set; }
    }
}
