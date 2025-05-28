namespace Malikinden.Utility
{
    using System.Net;
    using System.Net.Mail;


    public class EpostaHelper
    {
        private readonly string _smtpServer = "smtp.gmail.com"; // Hotmail SMTP server
        private readonly int _smtpPort = 587; // SMTP port for Hotmail
        private readonly string _username; // Hotmail email address
        private readonly string _password; // Hotmail password

        public EpostaHelper(string username, string password)
        {
            _username = username;
            _password = password;
        }

        /// <summary>
        /// Sends an email using the configured Hotmail account.
        /// </summary>
        /// <param name="to">Recipient email address</param>
        /// <param name="subject">Email subject</param>
        /// <param name="body">Email body</param>
        /// <param name="isHtml">Whether the email body is in HTML format</param>
        public void SendEmail(string to, string subject, string body, bool isHtml = true)
        {
            try
            {
                using var smtpClient = new SmtpClient(_smtpServer, _smtpPort)
                {
                    Credentials = new NetworkCredential(_username, _password),
                    EnableSsl = true // Hotmail requires SSL
                };

                using var mailMessage = new MailMessage
                {
                    From = new MailAddress(_username),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = isHtml
                };

                mailMessage.To.Add(to);

                smtpClient.Send(mailMessage);
                Console.WriteLine("Email sent successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending email: {ex.Message}");
            }
        }
    }


}
