namespace DELAY.Core.Application.Contracts.Models.Auth
{
    public class VkUserCredentials : UserExternalServiceCredentials
    {
        public VkUserCredentials(string email, string name, string familyName) : base(name + " " + familyName, email)
        {
            Name = name;
            FamilyName = familyName;
        }

        public string Name { get; set; }
        public string FamilyName { get; set; }
    }
}

