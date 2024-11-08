namespace DELAY.Core.Application.Contracts.Models.Auth
{
    /// <summary>
    /// Vk profile info
    /// </summary>
    public class VkUserCredentials : UserExternalServiceCredentials
    {
        public VkUserCredentials(string email, string name, string familyName) : base(name + " " + familyName, email, name, familyName)
        {
            Name = name;
            FamilyName = familyName;
        }
    }
}

