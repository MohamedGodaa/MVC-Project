using System.ComponentModel.DataAnnotations;

namespace Demo.PL.ViewModels
{
	public class ResetPasswordViewModel
	{
        [Required(ErrorMessage ="Password Is Required")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
		[Required(ErrorMessage = "Confirm Password Is Required")]
		[Compare(nameof(NewPassword))]
		[DataType(DataType.Password)]
		public string ConfirmPassword { get; set; }
    }
}
