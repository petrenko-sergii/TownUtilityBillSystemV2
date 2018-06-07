using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TownUtilityBillSystemV2.Resources;

namespace TownUtilityBillSystemV2.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Remember this browser?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
		[Required(ErrorMessageResourceName = "FieldIsRequired", ErrorMessageResourceType = typeof(Localization))]
		[Display(Name = "LoginForApp", ResourceType = typeof(Localization))]
		[EmailAddress(ErrorMessageResourceName = "EnterValidEmail", ErrorMessageResourceType = typeof(Localization))]
		public string Login { get; set; }

		[Required(ErrorMessageResourceName = "FieldIsRequired", ErrorMessageResourceType = typeof(Localization))]
		[DataType(DataType.Password)]
		[Display(Name = "Password", ResourceType = typeof(Localization))]
		public string Password { get; set; }

		[Display(Name = "RememberMe", ResourceType = typeof(Localization))]
		public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
		[Required(ErrorMessageResourceName = "FieldIsRequired", ErrorMessageResourceType = typeof(Localization))]
		[StringLength(20, ErrorMessageResourceName = "ValueMustBeAtLeastCharactersLong", ErrorMessageResourceType = typeof(Localization), MinimumLength = 2)]
		[Display(Name = "Name", ResourceType = typeof(Localization))]
		public string Name { get; set; }

		[Required(ErrorMessageResourceName = "FieldIsRequired", ErrorMessageResourceType = typeof(Localization))]
		[StringLength(20, ErrorMessageResourceName = "ValueMustBeAtLeastCharactersLong", ErrorMessageResourceType = typeof(Localization), MinimumLength = 2)]
		[Display(Name = "Surname", ResourceType = typeof(Localization))]
		public string Surname { get; set; }

		[Required(ErrorMessageResourceName = "FieldIsRequired", ErrorMessageResourceType = typeof(Localization))]
		[EmailAddress(ErrorMessageResourceName = "EnterValidEmail", ErrorMessageResourceType = typeof(Localization))]
		[Display(Name = "Email", ResourceType = typeof(Localization))]
		public string Email { get; set; }

		[Required(ErrorMessageResourceName = "FieldIsRequired", ErrorMessageResourceType = typeof(Localization))]
		[Phone(ErrorMessageResourceName = "EnterValidPhone", ErrorMessageResourceType = typeof(Localization))]
		[StringLength(13, ErrorMessageResourceName = "ValueMustHaveFromToCharactersLong", ErrorMessageResourceType = typeof(Localization), MinimumLength = 11)]
		[Display(Name = "PhoneNumber", ResourceType = typeof(Localization))]
		public string Number { get; set; }

		[Required(ErrorMessageResourceName = "FieldIsRequired", ErrorMessageResourceType = typeof(Localization))]
		[StringLength(100, ErrorMessageResourceName = "ValueMustBeAtLeastCharactersLong", ErrorMessageResourceType = typeof(Localization), MinimumLength = 6)]
		[DataType(DataType.Password)]
		[Display(Name = "Password", ResourceType = typeof(Localization))]
		public string Password { get; set; }

        [DataType(DataType.Password)]
		[Display(Name = "ConfirmPassword", ResourceType = typeof(Localization))]
		[Compare("Password", ErrorMessageResourceName = "ConfirmPasswordWarning", ErrorMessageResourceType = typeof(Localization))]
		public string ConfirmPassword { get; set; }

		public string FullName
		{
			get { return string.Format("{0} {1}", Surname, Name); }
		}
	}

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
