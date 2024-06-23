namespace IdentityServer.DTOs
{
    public class AuthenticationModel
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public string UserName { get; set; }
        public string Email {  get; set; }
        public string FirstName {  get; set; }
        public string LastName { get; set; }
    }
}
