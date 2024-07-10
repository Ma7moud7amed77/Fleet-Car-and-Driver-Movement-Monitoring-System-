namespace PasswordHandlling.Dto
{
	public class ResetPasswordDto
	{
        public string NewPassword { get; set; }
        public string ComfirmNewPassword { get; set; }
		public string Token { get; set; }
	}
}
