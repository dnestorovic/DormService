using IdentiryServer.DTOs;
using IdentiryServer.Entities;

namespace IdentiryServer.Services
{
    public interface IAuthenticationService
    {
        Task<User> ValidateUser(UserCredentialsDTO userCredentials);
        Task<AuthenticationModel> CreateAuthenticationModel(User user);
    }
}
