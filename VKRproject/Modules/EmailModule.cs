using System.Net;
using System.Net.Mail;
using VKRproject.Models;

namespace VKRproject.Modules
{
    public class EmailModule
    {
        public async Task SendToursByEmail(List<Tour> toursList, string destEmail)
        {
            try
            {
                string sendEmail = "p.smetanin.001@gmail.com";
                string password = "ahbsareluksswsxi";
                string name = "TravelAgency";
                MailAddress sendAdress = new MailAddress(sendEmail, name);
                MailAddress destAdress = new MailAddress(destEmail);
                MailMessage message = new MailMessage(sendAdress, destAdress);
                message.Subject = "Подборка туров";
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
        private string CreateMessageStr(List<Tour> tours)
        {
            string result = "<div><ul>";
            foreach(var t in tours)
            {
                result += $"<li>{t.Name} {t.TourOperator.ID} Направление: {t.Country.ID} Город: {t.City.ID} Дата вылета: {t.DateStart} Цена: {t.Price}</li>";
            }
            result += "</li></div>";
            return result;
        }
    }
}
