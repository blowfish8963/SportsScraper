using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;

namespace SportsScraper;

public class Mailer()
{
    public static void NewMail(string subject, string body)
    {
        var builder = new ConfigurationBuilder().AddJsonFile("appSettingsDevelopment.json");

        string smtpServer = builder.Build().GetSection("Settings")["SmtpServer"];
        int port = Convert.ToInt32(builder.Build().GetSection("Settings")["Port"]);
        var sender = builder.Build().GetSection("Settings")["Sender"];
        var recipient = builder.Build().GetSection("Settings")["Recipient"];
        var password = builder.Build().GetSection("Settings")["Password"];

        var mail = new MailMessage();
        mail.From = new MailAddress(sender);
        mail.Subject = subject;
        mail.Body = body;
        mail.IsBodyHtml = true;

        var client = new SmtpClient(smtpServer, port);
        client.Credentials = new NetworkCredential(sender, password);
        client.EnableSsl = true;
        client.Send(mail);
    }
}