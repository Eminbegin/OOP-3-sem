using MailKit.Net.Smtp;
using MimeKit;

namespace Banks.Clients;

public class EmailMessage : IMessageSender
{
    private readonly string _email;

    public EmailMessage(string email)
    {
        _email = email;
    }

    public async Task SendMessage(string message)
    {
        await SendMail(message);
    }

    private async Task SendMail(string value)
    {
        var mail = new MimeMessage();
        mail.From.Add(new MailboxAddress("ABOBUS", "testingbanks@mail.ru"));
        mail.To.Add(new MailboxAddress("ASS", _email));
        mail.Subject = "Banks";
        mail.Body = new TextPart(value);

        using (var client = new SmtpClient())
        {
            await client.ConnectAsync("smtp.mail.ru", 587, false);

            await client.AuthenticateAsync("testingbanks@mail.ru", "wj2-Mbj-WtM-v6m");
            await client.SendAsync(mail);

            await client.DisconnectAsync(true);
        }

// 47Brum0cacW8ZW5gygwD

        // var from = new MailAddress("testingbanks@mail.ru", "Emin");
        // var to = new MailAddress(_email);
        // var m = new MailMessage(from, to);
        // m.Subject = "Banks";
        // m.Body = value;
        // m.IsBodyHtml = false;
        // using var smtp = new SmtpClient("smtp.mail.ru", 25);
        // smtp.UseDefaultCredentials = false;
        // smtp.TargetName = "s";
        // smtp.Credentials = new NetworkCredential("testingbanks@mail.ru", "wj2-Mbj-WtM-v6m");
        // smtp.EnableSsl = true;
        // smtp.Timeout = 10000000;
        // smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
        // smtp.DeliveryFormat = SmtpDeliveryFormat.International;
        // smtp.Send(m);
        //
        // // smtp.Dispose();
        // // return Task.CompletedTask;
    }
}