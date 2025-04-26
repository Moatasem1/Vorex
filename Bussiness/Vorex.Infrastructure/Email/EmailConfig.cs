namespace Vorex.Infrastructure.Email;

public record EmailConfig
{
    public required string SenderName { get; init; }
    public required string SenderEmail { get; init; }
    public required string SmtpServer { get; init; }
    public required int SmtpPort { get; set; }
    public required string EmailPassword { get; set; }
}
