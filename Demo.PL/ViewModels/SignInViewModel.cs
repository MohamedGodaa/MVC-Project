﻿using System.ComponentModel.DataAnnotations;

namespace Demo.PL.ViewModels
{
	public class SignInViewModel
	{
        [Required(ErrorMessage = "Email Is Required")]
        [EmailAddress(ErrorMessage ="InValid Email")]
        public string Email { get; set; }
		[Required(ErrorMessage = "Email Is Password")]
        [DataType(DataType.Password)]
		public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
