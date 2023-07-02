using System.Net;
using System.Net.Mail;
using VKRproject.Models;
using VKRproject.Tools;

namespace VKRproject.Modules
{
    public class EmailModule
    {
        public async Task SendToursByEmail(List<ShortTour> toursList, string destEmail)
        {
            try
            {
                string sendEmail = ConfigProvider.PrivateConfig["Email:Login"];
                string password = ConfigProvider.PrivateConfig["Email:Password"];
                string name = ConfigProvider.PrivateConfig["Email:Name"];
                MailAddress sendAdress = new MailAddress(sendEmail, name);
                MailAddress destAdress = new MailAddress(destEmail);
                MailMessage message = new MailMessage(sendAdress, destAdress);
                message.Subject = ConfigProvider.PrivateConfig["Email:Subject"];
                message.Body = CreateMessageStr(toursList);
                message.IsBodyHtml = true;
                SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                client.Credentials = new NetworkCredential(sendEmail, password);
                client.EnableSsl = true;
                await client.SendMailAsync(message);
            }
            catch(Exception ex)
            {
                throw new Exception("Send message to email is failed! " + ex.Message);
            }
        }
        private string CreateMessageStr(List<ShortTour> tours)
        {
            string result = "<div><ul>";
            foreach(var t in tours)
            {
                result += $"<li>{t.Name} Город: {t.City.Name} Дата вылета: {t.DateStart} Цена: {t.Price}</li>";
            }
            result += "</li></div>";
            return result;
        }
    }
}
