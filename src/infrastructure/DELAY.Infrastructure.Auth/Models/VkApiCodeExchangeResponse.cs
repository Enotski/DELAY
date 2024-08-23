namespace DELAY.Infrastructure.Auth.Models
{
    internal class VkApiCodeExchangeResponse
    {
        public VkApiCodeExchangeResponse()
        {
        }

        public VkApiCodeExchangeResponse(string refresh_token, string access_token, string token_type, string state, string scope, int expires_in, int user_id, string id_token)
        {
            this.refresh_token = refresh_token;
            this.access_token = access_token;
            this.token_type = token_type;
            this.state = state;
            this.scope = scope;
            this.expires_in = expires_in;
            this.user_id = user_id;
            this.id_token = id_token;
        }

        public string refresh_token { get; set; }
        public string access_token { get; set; }
        public string token_type { get; set; }
        public string state { get; set; }
        public string scope { get; set; }
        public int expires_in { get; set; }
        public int user_id { get; set; }
        public string id_token { get; set; }
    }
}
