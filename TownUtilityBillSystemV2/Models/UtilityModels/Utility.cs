using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TownUtilityBillSystemV2.Models.BaseModels;
using TownUtilityBillSystemV2.Models.HelperMethods;
using TownUtilityBillSystemV2.Resources;

namespace TownUtilityBillSystemV2.Models.UtilityModels
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
		//[RegularExpression(@"[0-9]+(\.[0-9][0-9]?)?",
		//	ErrorMessageResourceName = "FieldMustBeNonNegativeNumber2WithDecDigits", 
		//	ErrorMessageResourceType = typeof(Localization))]
		public decimal Price { get; set; }

		[Required(ErrorMessageResourceName = "FieldIsRequired", ErrorMessageResourceType = typeof(Localization))]
		[Range(0.1, int.MaxValue, ErrorMessageResourceName = "ValueMustBeBiggerThanZero", ErrorMessageResourceType = typeof(Localization))]
		[Display(Name = "UsageForStandartPrice", ResourceType = typeof(Localization))]
		//[RegularExpression(@"[0-9]+(\.[0-9][0-9]?)?",
		//	ErrorMessageResourceName = "FieldMustBeNonNegativeNumber2WithDecDigits",
		//	ErrorMessageResourceType = typeof(Localization))]
		public decimal UsageForStandartPrice { get; set; }

		[Required(ErrorMessageResourceName = "FieldIsRequired", ErrorMessageResourceType = typeof(Localization))]
		[Range(0.1, int.MaxValue, ErrorMessageResourceName = "ValueMustBeBiggerThanZero", ErrorMessageResourceType = typeof(Localization))]
		[Display(Name = "HigherPrice", ResourceType = typeof(Localization))]
		//[RegularExpression(@"[0-9]+(\.[0-9][0-9]?)?",
		//	ErrorMessageResourceName = "FieldMustBeNonNegativeNumber2WithDecDigits",
		//	ErrorMessageResourceType = typeof(Localization))]
		public decimal BigUsagePrice { get; set; }

		public Unit Unit { get; set; }

		public static Utility GetUtilityWithIdAndResourceName(UTILITY utility)
		{
			return new Utility
			{
				Id = utility.ID,
				ResourceName = UtilityModel.GetResourceNameForUtility(utility.NAME)
			};
		}

		public static Utility GetUtilityWithOutBigUsagePrice(UTILITY utility)
		{
			return new Utility
			{
				Id = utility.ID,
				ResourceName = UtilityModel.GetResourceNameForUtility(utility.NAME),
				Name = utility.NAME,
				Price = utility.PRICE,
				ImagePath = HelperMethod.GetUtilityImage(utility.ID),
				Unit = Unit.Get(utility.UNIT)
			};
		}

		public static Utility GetUtilityWithBigUsagePrice(UTILITY utility)
		{
			return new Utility
			{
				Id = utility.ID,
				ResourceName = UtilityModel.GetResourceNameForUtility(utility.NAME),
				Name = utility.NAME,
				Price = utility.PRICE,
				BigUsagePrice = (decimal)utility.BIGUSAGEPRICE,
				UsageForStandartPrice = Math.Round((decimal)utility.USAGEFORSTANDARTPRICE, 0),
				ImagePath = HelperMethod.GetUtilityImage(utility.ID),
				Unit = Unit.Get(utility.UNIT)
			};
		}
	}
}
