using System.Net;
using System.Net.Mail;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace server.services;

public class EmailService : IEmailSender, IEmailService
{
    private readonly IConfiguration Configuration;
    private readonly ILogger<EmailService> Logger;

    public EmailService(
        IConfiguration configuration,
        ILogger<EmailService> logger
    )
    {
        Configuration = configuration;
        Logger = logger;
    }


    public async Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        CreateAndSendEmail(email, subject, htmlMessage);
        await Task.CompletedTask;
    }

    public void CreateAndSendEmail(string email, string subject, string htmlMessage)
    {

        string smtpServer = Environment.GetEnvironmentVariable("SMTP_HOST")!;
        string smtpPort = Environment.GetEnvironmentVariable("SMTP_PORT")!;
        string smtpUsername = Environment.GetEnvironmentVariable("SMTP_USERNAME")!;
        string smtpPassword = Environment.GetEnvironmentVariable("SMTP_PASSWORD")!;

        SmtpClient smtpClient = new SmtpClient(smtpServer)
        {
            Port = 587,
            Credentials = new NetworkCredential(smtpUsername, smtpPassword),
            EnableSsl = true,
        };

        MailMessage message = new MailMessage
        {
            From = new MailAddress("system@craigsau.dev"),
            Subject = subject,
            Body = htmlMessage,
        };

        message.To.Add(email);

        try
        {
            smtpClient.Send(message);
            Console.WriteLine("Email sent successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to send email : {ex.Message}");
        }
    }
}
