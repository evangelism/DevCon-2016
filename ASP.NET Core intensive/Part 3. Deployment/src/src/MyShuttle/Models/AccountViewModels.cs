using System.ComponentModel.DataAnnotations;

namespace MyShuttle.Web.Models
{
    public abstract class BasePasswordModel
    {
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
		[Required(ErrorMessage = "Password is required")]
		[StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long  and no more than 100", MinimumLength = 6)]
		public string Password { get; set; }

        [Display(Name = "User name")]
		[Required(ErrorMessage = "User name is required")]
		[StringLength(30, ErrorMessage = "The {0} must be at least {2} characters long and no more than 30", MinimumLength = 3)]
		public string UserName { get; set; }
    }

    public class LoginViewModel : BasePasswordModel
    {
        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel : BasePasswordModel
    {
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
		[Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
		public string ConfirmPassword { get; set; }
    }
}
