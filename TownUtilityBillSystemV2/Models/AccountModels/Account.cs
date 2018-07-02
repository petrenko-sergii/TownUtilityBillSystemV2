using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TownUtilityBillSystemV2.Resources;

namespace TownUtilityBillSystemV2.Models.AccountModels
{
	public class Account
	{
		[Required(ErrorMessageResourceName = "FieldIsRequired", ErrorMessageResourceType = typeof(Localization))]
		public int Id { get; set; }

		[Display(Name = "Number", ResourceType = typeof(Localization))]
		public string Number { get; set; }

		[Display(Name = "Balance", ResourceType = typeof(Localization))]
		public decimal Balance { get; set; }

		public static Account Get(ACCOUNT account)
		{
			return new Account
			{
				Id = account.ID,
				Number = account.NUMBER,
				Balance = account.BALANCE
			};
		}
	}
}