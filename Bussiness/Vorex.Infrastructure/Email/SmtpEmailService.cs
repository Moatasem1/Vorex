using Microsoft.Extensions.Options;
using MailKit;
using Vorex.Domain.lib.Interfaces;
using MimeKit;
using MailKit.Net.Smtp;
using System.Runtime;
using static Vorex.Domain.lib.Interfaces.IEmailService;

namespace Vorex.Infrastructure.Email;

public class SmtpEmailService(IOptions<EmailConfig> _emailConfig, EmailTemplateBuilder _emailTemplateBuilder) : IEmailService
{

    public async Task SendEmailAsync(string to, string subject, string htmlBody)
    {
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress(_emailConfig.Value.SenderName, _emailConfig.Value.SenderEmail));
        message.To.Add(MailboxAddress.Parse(to));
        message.Subject = subject;

        var bodyBuilder = new BodyBuilder { HtmlBody = htmlBody };
        message.Body = bodyBuilder.ToMessageBody();

        using var smtp = new SmtpClient();
        try
        {
            await smtp.ConnectAsync(_emailConfig.Value.SmtpServer, _emailConfig.Value.SmtpPort, MailKit.Security.SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(_emailConfig.Value.SenderEmail, _emailConfig.Value.EmailPassword);
            await smtp.SendAsync(message);
            await smtp.DisconnectAsync(true);
        }catch(Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

    public async Task SendVerificationEmail(string userEmail, string userName, string verificationLink)
    {
        var body = _emailTemplateBuilder.BuildEmailBody(EmailTemplatePaths.EmailVerificationTemplate,
             new Dictionary<string, string>
             {
                { "UserName", userName },
                { "VerificationLink", verificationLink }
             }
           );

       await SendEmailAsync(userEmail, "Verify your email", body);
    }
}
