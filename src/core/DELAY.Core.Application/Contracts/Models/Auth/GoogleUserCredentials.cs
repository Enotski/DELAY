namespace DELAY.Core.Application.Contracts.Models.Auth
{
    /// <summary>
    /// Google profile info
    /// </summary>
    public class GoogleUserCredentials : UserExternalServiceCredentials
    {
        public GoogleUserCredentials(string email, string name, string givenName, string familyName) : base(givenName, email, name, familyName)
        {
            Name = name;
            FamilyName = familyName;
        }
    }
}
