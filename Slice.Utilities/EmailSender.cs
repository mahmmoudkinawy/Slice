namespace Slice.Utilities;
public class EmailSender : IEmailSender
{
    private readonly IConfiguration _config;

    public string SendGridKey { get; set; }

    public EmailSender(IConfiguration config) => _config = config;

    public Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        //There is a fucking bug here!! I do not know what is it!!!


        SendGridKey = _config.GetValue<string>("SendGrid:SecretKey");

        var client = new SendGridClient(SendGridKey);
        var from = new EmailAddress("mahmmoudkinawy@gmail.com", "Slice Pizza");
        var to = new EmailAddress(email);
        var message = MailHelper.CreateSingleEmail(from, to, subject, "", htmlMessage);

        return client.SendEmailAsync(message);
    }
}