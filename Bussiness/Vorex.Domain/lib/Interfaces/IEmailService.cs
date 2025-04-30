namespace Vorex.Domain.lib.Interfaces;

public interface IEmailService
{
    public static class EmailTemplatePaths
    {
        public static readonly string EmailVerificationTemplate = "VerifyEmail";
        public static readonly string ResetPasswordTemplate = "ResetPassword";
    }

    Task SendEmailAsync(string to, string subject, string htmlBody);

    Task SendVerificationEmail(string userEmail, string userName, string verificationLink);

    Task SendResetPasswordEmaill(string userEmail, string userName, string verificationLink);
}
