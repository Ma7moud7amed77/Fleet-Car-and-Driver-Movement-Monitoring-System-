using System.Threading.Tasks;

namespace PasswordHandlling.Services.EmailSettings
{
	public interface IEmailSender
	{
		Task SendAsync(string from,string recipients,string subject, string body);
	}
}
