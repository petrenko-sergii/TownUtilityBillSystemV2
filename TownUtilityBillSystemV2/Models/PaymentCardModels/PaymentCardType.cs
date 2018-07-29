using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using TownUtilityBillSystemV2.Models.BaseModels;
using TownUtilityBillSystemV2.Resources;

namespace TownUtilityBillSystemV2.Models.PaymentCardModels
{
	public class PaymentCardType : ObjectWithName
	{
		[Required]
		[Display(Name = "CardType", ResourceType = typeof(Localization))]
		public new int Id { get; set; }
	}
}