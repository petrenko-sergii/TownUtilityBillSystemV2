using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TownUtilityBillSystemV2.Models.Currency;
using TownUtilityBillSystemV2.Models.HelperMethods;
using TownUtilityBillSystemV2.Models.UtilityModels;
using TownUtilityBillSystemV2.Resources;
using static TownUtilityBillSystemV2.Models.InitialDB.InitialDBEnums;

namespace TownUtilityBillSystemV2.Models.Calculator
{
	public class CalculatorItemModel
	{
		#region Properties

		public int Id { get; set; }

		[Required(ErrorMessageResourceName = "FieldIsRequired", ErrorMessageResourceType = typeof(Localization))]
		[Range(0, int.MaxValue, ErrorMessageResourceName = "UsageCanNotBeNegative", ErrorMessageResourceType = typeof(Localization))]
		[Display(Name = "Electricity", ResourceType = typeof(Localization))]
		public float ElectricUsage { get; set; }

		[Required(ErrorMessageResourceName = "FieldIsRequired", ErrorMessageResourceType = typeof(Localization))]
		[Range(0, int.MaxValue, ErrorMessageResourceName = "UsageCanNotBeNegative", ErrorMessageResourceType = typeof(Localization))]
		[Display(Name = "Water", ResourceType = typeof(Localization))]
		public float WaterUsage { get; set; }

		[Required(ErrorMessageResourceName = "FieldIsRequired", ErrorMessageResourceType = typeof(Localization))]
		[Range(0, int.MaxValue, ErrorMessageResourceName = "UsageCanNotBeNegative", ErrorMessageResourceType = typeof(Localization))]
		[Display(Name = "Heating", ResourceType = typeof(Localization))]
		public float HeatUsage { get; set; }

		[Required(ErrorMessageResourceName = "FieldIsRequired", ErrorMessageResourceType = typeof(Localization))]
		[Range(0, int.MaxValue, ErrorMessageResourceName = "UsageCanNotBeNegative", ErrorMessageResourceType = typeof(Localization))]
		[Display(Name = "Gas", ResourceType = typeof(Localization))]
		public float GasUsage { get; set; }

		public float ElCharges { get; set; }
		public float WaterCharges { get; set; }
		public float HeatCharges { get; set; }
		public float GasCharges { get; set; }

		public float TotalCharges { get; set; }

		public List<Utility> Utilities { get; set; }
		public CurrencyMoney Currency;

		#endregion

		#region Ctor

		public CalculatorItemModel()
		{
			Utilities = new List<Utility>();
			Currency = new CurrencyMoney();
		}

		#endregion

		#region Methods

		public void AddUtilitiesToCalculatorItemModel()
		{
			using (var context = new TownUtilityBillSystemV2Entities())
			{
				var utilitiesDB = context.UTILITYs.ToList();

				foreach (var item in utilitiesDB)
				{
					Utility utility = (item.USAGEFORSTANDARTPRICE != null && item.BIGUSAGEPRICE != null) ?
						Utility.GetUtilityWithBigUsagePrice(item) : Utility.GetUtilityWithOutBigUsagePrice(item);

					Utilities.Add(utility);
				}
			}
		}

		public void CalculateCharges(CalculatorItemModel inputModel)
		{
			using (var context = new TownUtilityBillSystemV2Entities())
			{
				var elUtilityDB = context.UTILITYs.Find((int)InitialDB.InitialDBEnums.Utilities.Electricity);
				var waterUtilityDB = context.UTILITYs.Find((int)InitialDB.InitialDBEnums.Utilities.Water);
				var heatUtilityDB = context.UTILITYs.Find((int)InitialDB.InitialDBEnums.Utilities.Heating);
				var gasUtilityDB = context.UTILITYs.Find((int)InitialDB.InitialDBEnums.Utilities.Gas);

				ElCharges = (inputModel.ElectricUsage > (float)elUtilityDB.USAGEFORSTANDARTPRICE)
					? (float)elUtilityDB.USAGEFORSTANDARTPRICE * (float)elUtilityDB.PRICE + (inputModel.ElectricUsage - (float)elUtilityDB.USAGEFORSTANDARTPRICE) * (float)elUtilityDB.BIGUSAGEPRICE
					: inputModel.ElectricUsage * (float)elUtilityDB.PRICE;

				WaterCharges = inputModel.WaterUsage * (float)waterUtilityDB.PRICE;
				HeatCharges = inputModel.HeatUsage * (float)heatUtilityDB.PRICE;
				GasCharges = inputModel.GasUsage * (float)gasUtilityDB.PRICE;

				ElectricUsage = inputModel.ElectricUsage;
				WaterUsage = inputModel.WaterUsage;
				HeatUsage = inputModel.HeatUsage;
				GasUsage = inputModel.GasUsage;

				TotalCharges = ElCharges + WaterCharges + HeatCharges + GasCharges;
			}
		}

		#endregion
	}
}