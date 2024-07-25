public interface IEmailService {
   Task SendEmailAsync(string email, string subject, string htmlMessage);
   void CreateAndSendEmail(string email, string subject, string htmlMessage);
}
