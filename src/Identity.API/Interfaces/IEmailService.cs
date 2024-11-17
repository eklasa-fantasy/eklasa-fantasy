namespace Identity.API.Interfaces
{
    public interface IEmailService
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage);
    }
}