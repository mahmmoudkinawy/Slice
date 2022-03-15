namespace Slice.Utilities;
public class EmailSender : IEmailSender
{
    public Task SendEmailAsync(string email, string subject, string htmlMessage)
        => Task.CompletedTask;
}