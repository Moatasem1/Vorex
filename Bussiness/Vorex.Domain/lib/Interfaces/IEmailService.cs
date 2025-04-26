namespace Vorex.Domain.lib.Interfaces;

public interface IEmailService
{
    public static class EmailTemplatePaths
    {
        public static readonly string EmailVerificationTemplate = "VerifyEmail";
    }

    Task SendEmailAsync(string to, string subject, string htmlBody);

    Task SendVerificationEmail(string userEmail, string userName, string verificationLink);
}
