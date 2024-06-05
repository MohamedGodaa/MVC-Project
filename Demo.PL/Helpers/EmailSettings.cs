using Demo.DAL.Models;

using System;
using System.Net;
using System.Net.Mail;

namespace Demo.PL.Helpers
{
	public class EmailSettings
	{
		public static void SendEmail(Email email)
		{
			//var client = new SmtpClient("smtp.gmail.com", 587);
			//client.EnableSsl = true;
			//client.Credentials = new NetworkCredential("mohamed.goda1852@gmail.com", "185253Jihad");
			//client.Send("mohamed.goda1852@gmail.com",email.Recipients,email.Subject,email.Body);

			MailMessage message = new MailMessage("mohamed.goda1852@gmail.com", email.Recipients);

			message.Subject = email.Body;
			message.Body = email.Body;

			SmtpClient client = new SmtpClient();
			client.Port = 587;
			client.EnableSsl = true;
			client.DeliveryMethod = SmtpDeliveryMethod.Network;
			client.UseDefaultCredentials = false;
			client.Host = "smtp.google.com";

			try
			{
				client.Send(message);
			}
			catch (Exception ex)
			{
				string t = ex.Message;
			}
		}
	}
}
