using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;

namespace Pic.Infrastructure;

public class SendEmailService : ISendEmailService
{
    private readonly IConfiguration _config;
    private readonly string HOST;
    private readonly string USER;
    private readonly string PASSWORD;
    private readonly int PORT;
    public SendEmailService(IConfiguration config)
    {
        _config = config;
        HOST = _config.GetSection("Smtp:Host").Value;
        USER = _config.GetSection("Smtp:User").Value;
        PASSWORD = _config.GetSection("Smtp:Password").Value;
        PORT = int.Parse(_config.GetSection("Smtp:Port").Value);
    }

    public void SendEmail(string sendto)
    {
        try
        {
            string smtpAddress = HOST;
            int portNumber = PORT; 
            bool enableSSL = true;

            // Credenciais de login do e-mail
            string from = USER;
            string password = PASSWORD;

            // Informações do e-mail
            string to = sendto;
            string subject = "Transfer";
            string body = $"Hi,{sendto}! your transfer has completed sucessfully";

            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress(from);
                mail.To.Add(to);
                mail.Subject = subject;
                mail.Body = body;
                mail.IsBodyHtml = true;

               
                using (SmtpClient smtp = new SmtpClient(smtpAddress, portNumber))
                {
                    smtp.Credentials = new NetworkCredential(from, password);
                    smtp.EnableSsl = enableSSL;
                    smtp.Send(mail);
                }
            }

        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao enviar e-mail: {ex.Message}");
        }
    }
}
