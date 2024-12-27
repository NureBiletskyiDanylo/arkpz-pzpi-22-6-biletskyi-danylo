namespace MediStoS.Services.SMTPService;

public interface IEmailSender
{
    Task SendEmaiAsync(string email, string subject, string message);
}
