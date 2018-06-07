using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TownUtilityBillSystemV2.Models.BaseModels;
using TownUtilityBillSystemV2.Resources;

namespace TownUtilityBillSystemV2.Models.Utility
{
	public class Utility : ObjectWithImage
	{
		[Required(ErrorMessageResourceName = "FieldIsRequired", ErrorMessageResourceType = typeof(Localization))]
		[StringLength(20, ErrorMessageResourceName = "ValueMustBeAtLeastCharactersLong", ErrorMessageResourceType = typeof(Localization), MinimumLength = 2)]
		[Display(Name = "Name", ResourceType = typeof(Localization))]
		public string Name { get; set; }

		public string ResourceName { get; set; }

		[Required(ErrorMessageResourceName = "FieldIsRequired", ErrorMessageResourceType = typeof(Localization))]
		[Range(0.1, int.MaxValue, ErrorMessageResourceName = "ValueMustBeBiggerThanZero", ErrorMessageResourceType = typeof(Localization))]
		[Display(Name = "Price", ResourceType = typeof(Localization))]
		public decimal Price { get; set; }

		[Required(ErrorMessageResourceName = "FieldIsRequired", ErrorMessageResourceType = typeof(Localization))]
		[Range(0.1, int.MaxValue, ErrorMessageResourceName = "ValueMustBeBiggerThanZero", ErrorMessageResourceType = typeof(Localization))]
		[Display(Name = "UsageForStandartPrice", ResourceType = typeof(Localization))]
		public decimal UsageForStandartPrice { get; set; }

		[Required(ErrorMessageResourceName = "FieldIsRequired", ErrorMessageResourceType = typeof(Localization))]
		[Range(0.1, int.MaxValue, ErrorMessageResourceName = "ValueMustBeBiggerThanZero", ErrorMessageResourceType = typeof(Localization))]
		[Display(Name = "HigherPrice", ResourceType = typeof(Localization))]
		public decimal BigUsagePrice { get; set; }

		public Unit Unit { get; set; }
	}
}
