
using System.Net;
using System.Net.Mail;

namespace MediStoS.Services.SMTPService;

public class EmailSender(IConfiguration configuration) : IEmailSender
{
    public Task SendEmaiAsync(string email, string subject, string message)
    {
        string serverEmail = configuration["SMTP:email"];
        string serverPassword = configuration["SMTP:pw"];

        var client = new SmtpClient("smtp.gmail.com", 587)
        {
            EnableSsl = true,
            Credentials = new NetworkCredential(serverEmail, serverPassword)
        };

        return client.SendMailAsync(new MailMessage(from: serverEmail, to: email, subject, message));
    }
}
