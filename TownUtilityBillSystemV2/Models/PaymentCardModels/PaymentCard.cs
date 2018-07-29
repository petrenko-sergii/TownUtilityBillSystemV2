using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TownUtilityBillSystemV2.Models.BaseModels;
using TownUtilityBillSystemV2.Resources;

namespace TownUtilityBillSystemV2.Models.PaymentCardModels
{
	public class PaymentCard 
	{
		public int Id { get; set; }

		[Required(ErrorMessageResourceName = "FieldIsRequired", ErrorMessageResourceType = typeof(Localization))]
		[StringLength(19, ErrorMessageResourceName = "ValueMustHaveCountDigits", ErrorMessageResourceType = typeof(Localization), MinimumLength = 16)]
		[Display(Name = "PaymentCardNumber", ResourceType = typeof(Localization))]
		public string Number { get; set; }

		[Required(ErrorMessageResourceName = "FieldIsRequired", ErrorMessageResourceType = typeof(Localization))]
		[StringLength(20, ErrorMessageResourceName = "ValueMustBeAtLeastCharactersLong", ErrorMessageResourceType = typeof(Localization), MinimumLength = 2)]
		[Display(Name = "CardOwnerSurname", ResourceType = typeof(Localization))]
		public string Surname { get; set; }

		[Required(ErrorMessageResourceName = "FieldIsRequired", ErrorMessageResourceType = typeof(Localization))]
		[StringLength(20, ErrorMessageResourceName = "ValueMustBeAtLeastCharactersLong", ErrorMessageResourceType = typeof(Localization), MinimumLength = 2)]
		[Display(Name = "CardOwnerName", ResourceType = typeof(Localization))]
		public string Name { get; set; }

		[Required(ErrorMessageResourceName = "FieldIsRequired", ErrorMessageResourceType = typeof(Localization))]
		[Display(Name = "ExpireDate", ResourceType = typeof(Localization))]
		public DateTime ExpireDate { get; set; }

		[Required(ErrorMessageResourceName = "FieldIsRequired", ErrorMessageResourceType = typeof(Localization))]
		[StringLength(3, ErrorMessageResourceName = "ValueMustHaveCountDigits", ErrorMessageResourceType = typeof(Localization), MinimumLength = 3)]
		[Display(Name = "CVVnumber", ResourceType = typeof(Localization))]
		public string CVV { get; set; }

		[Required(ErrorMessageResourceName = "FieldIsRequired", ErrorMessageResourceType = typeof(Localization))]
		[Display(Name = "CardType", ResourceType = typeof(Localization))]
		public PaymentCardType Type { get; set; }
	}
}