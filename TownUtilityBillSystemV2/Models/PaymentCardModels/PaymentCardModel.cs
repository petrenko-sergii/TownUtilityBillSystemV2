using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using TownUtilityBillSystemV2.Resources;
using TownUtilityBillSystemV2.Models.BillModels;
using TownUtilityBillSystemV2.Models.Currency;
using System.Data;

namespace TownUtilityBillSystemV2.Models.PaymentCardModels
{
	public class PaymentCardModel
	{
		#region Properties

		[Required(ErrorMessageResourceName = "FieldIsRequired", ErrorMessageResourceType = typeof(Localization))]
		public PaymentCard PaymentCard { get; set; }

		public Bill Bill { get; set; }
		public List<PaymentCardType> PaymentCardTypes;
		public CurrencyMoney Currency;

		[Required(ErrorMessageResourceName = "FieldIsRequired", ErrorMessageResourceType = typeof(Localization))]
		[Range(0, int.MaxValue, ErrorMessageResourceName = "PayingSumCanNotBeNegative", ErrorMessageResourceType = typeof(Localization))]
		[Display(Name = "PayingSum", ResourceType = typeof(Localization))]
		public decimal PayingSum { get; set; }

		#endregion

		#region Ctor

		public PaymentCardModel()
		{
			PaymentCard = new PaymentCard();
			Bill = new Bill();

			PaymentCardTypes = new List<PaymentCardType>()
			{
				new PaymentCardType(){Id = 1, Name = "MasterCard" },
				new PaymentCardType(){Id = 2, Name = "Maestro" },
				new PaymentCardType(){Id = 2, Name = "Visa" },
				new PaymentCardType(){Id = 2, Name = "Visa Electron" }
			};
			Currency = new CurrencyMoney();
		}

		#endregion
	}
}