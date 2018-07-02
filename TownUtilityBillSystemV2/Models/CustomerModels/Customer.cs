using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TownUtilityBillSystemV2.Models.AccountModels;
using TownUtilityBillSystemV2.Models.AddressModels;
using TownUtilityBillSystemV2.Models.BaseModels;
using TownUtilityBillSystemV2.Resources;

namespace TownUtilityBillSystemV2.Models.CustomerModels
{
	public class Customer : ObjectWithName
	{
		[Required(ErrorMessageResourceName = "FieldIsRequired", ErrorMessageResourceType = typeof(Localization))]
		[StringLength(20, ErrorMessageResourceName = "ValueMustBeAtLeastCharactersLong", ErrorMessageResourceType = typeof(Localization), MinimumLength = 2)]
		[Display(Name = "Surname", ResourceType = typeof(Localization))]
		public string Surname { get; set; }

		[Required(ErrorMessageResourceName = "FieldIsRequired", ErrorMessageResourceType = typeof(Localization))]
		[StringLength(20, ErrorMessageResourceName = "ValueMustBeAtLeastCharactersLong", ErrorMessageResourceType = typeof(Localization), MinimumLength = 2)]
		[Display(Name = "Name", ResourceType = typeof(Localization))]
		public new string Name { get; set; }

		[Required(ErrorMessageResourceName = "FieldIsRequired", ErrorMessageResourceType = typeof(Localization))]
		[EmailAddress(ErrorMessageResourceName = "EnterValidEmail", ErrorMessageResourceType = typeof(Localization))]
		[Display(Name = "Email", ResourceType = typeof(Localization))]
		public string Email { get; set; }

		[Required(ErrorMessageResourceName = "FieldIsRequired", ErrorMessageResourceType = typeof(Localization))]
		[Phone(ErrorMessageResourceName = "EnterValidPhone", ErrorMessageResourceType = typeof(Localization))]
		[StringLength(13, ErrorMessageResourceName = "ValueMustHaveFromToCharactersLong", ErrorMessageResourceType = typeof(Localization), MinimumLength = 11)]
		[Display(Name = "PhoneNumber", ResourceType = typeof(Localization))]
		public string Phone { get; set; }

		public Address Address { get; set; }

		[Required(ErrorMessageResourceName = "FieldIsRequired", ErrorMessageResourceType = typeof(Localization))]
		public CustomerType CustomerType { get; set; }

		[Required(ErrorMessageResourceName = "FieldIsRequired", ErrorMessageResourceType = typeof(Localization))]
		public Account Account { get; set; }

		public List<CustomerType> CustomerTypes { get; set; }
	}
}