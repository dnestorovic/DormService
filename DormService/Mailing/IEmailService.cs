using Mailing.Data;

namespace Mailing
{
    public interface IEmailService
    {
        Task<bool> SendEmail(Email emailRequest);
    }
}