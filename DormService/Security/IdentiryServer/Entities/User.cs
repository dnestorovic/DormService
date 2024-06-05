using Microsoft.AspNetCore.Identity;

namespace IdentiryServer.Entities
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
