using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace PasswordHandlling.Services.EmailSettings
{
	public class EmailSender : IEmailSender
	{
		//go to appSetting To show Why I used Configration and what i added
		private readonly IConfiguration _configuration;

		public EmailSender(IConfiguration configuration)
        {
			_configuration = configuration;
		}
        public async Task SendAsync(string from, string recipients, string subject, string body)
		{
			var senderEmail = _configuration["EmailSettings:SenderEmail"];  //<==========
			var senderPassword = _configuration["EmailSettings:SenderPassword"]; //<========

			var emailMessage = new MailMessage();
			emailMessage.From=new MailAddress(from);
			emailMessage.To.Add(recipients);
			emailMessage.Subject=subject;
			emailMessage.Body=$"<html><body>{body}</body></html>";
			emailMessage.IsBodyHtml=true;


			var smtpClient=new SmtpClient(_configuration["EmailSettings:SmtpClientServer"],int.Parse(_configuration["EmailSettings:SmtpClientPort"])) 
			{
				Credentials=new NetworkCredential(senderEmail,senderPassword),
				EnableSsl=true
			};

			await smtpClient.SendMailAsync(emailMessage);
		}
	}
}
