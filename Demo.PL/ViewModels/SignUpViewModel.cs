using System.ComponentModel.DataAnnotations;

namespace Demo.PL.ViewModels
{
	public class SignUpViewModel
	{
		[Required(ErrorMessage = "FName Is Required")]
		public string FName { get; set; }
		[Required(ErrorMessage = "LName Is Required")]
		public string LName { get; set; }
        [Required(ErrorMessage = "Email Is Required")]
        [EmailAddress(ErrorMessage ="InValed Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password Is Required")]
		[DataType(DataType.Password)]
        public string Password { get; set; }

		[Required(ErrorMessage = "Confim Password Is Required")]
		[Compare(nameof(Password),ErrorMessage = "Confim Password doesn't match Password")]
		[DataType(DataType.Password)]
		public string ConfimPassword { get; set; }

		[Required(ErrorMessage = "Required To Agree")]
		public bool IsAgree { get; set; }
    }
}
