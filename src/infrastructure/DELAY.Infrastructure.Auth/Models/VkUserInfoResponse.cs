namespace DELAY.Infrastructure.Auth.Services
{
    internal class VkUserInfoResponse
    {
        public VkUserInfoResponse()
        {
        }

        public VkUserInfoResponse(VkUser user)
        {
            this.user = user;
        }

        public VkUser user { get; set; }
    }

    internal class VkUser
    {
        public VkUser()
        {
        }

        public VkUser(string user_id, string first_name, string last_name, string phone, string email)
        {
            this.user_id = user_id;
            this.first_name = first_name;
            this.last_name = last_name;
            this.email = email;
        }

        public string user_id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string email { get; set; }
    }


}
