using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TownUtilityBillSystemV2.Models.Currency;
using TownUtilityBillSystemV2.Models.HelperMethods;
using TownUtilityBillSystemV2.Resources;

namespace TownUtilityBillSystemV2.Models.UtilityModels
{
	public class UtilityModel
	{
		#region Properties

		public Utility Utility;

		public Unit Unit;

		public List<Utility> Utilities { get; set; }

		public List<Unit> Units { get; set; }

		public CurrencyMoney Currency;

		public int MeterQty;

		#endregion

		#region Ctor

		public UtilityModel()
		{
			Utilities = new List<Utility>();
			Units = new List<Unit>();
			Utility = new Utility();
			Unit = new Unit();
			Currency = new CurrencyMoney();
			MeterQty = 0;
		}

		#endregion

		#region Methods

		#region Public 

		public void GetUtilitiesName()
		{
			using (var context = new TownUtilityBillSystemV2Entities())
			{
				var utilitiesDB = context.UTILITYs.ToList();

				foreach (var item in utilitiesDB)
					Utilities.Add(new Utility { Id = item.ID, Name = item.NAME, ResourceName = GetResourceNameForUtility(item.NAME) });
			}
		}

		public void GetAllUtilitiesPrices()
		{
			using (var context = new TownUtilityBillSystemV2Entities())
			{
				var utilitiesDB = context.UTILITYs.ToList();

				foreach (var item in utilitiesDB)
				{
					var unitDB = context.UNITs.Where(u => u.ID == item.UNIT_ID).FirstOrDefault();
					var unit = new Unit() { Id = unitDB.ID, Name = unitDB.NAME };

					if (item.USAGEFORSTANDARTPRICE != null && item.BIGUSAGEPRICE != null)
						Utilities.Add(new Utility { Id = item.ID, Name = item.NAME, Price = item.PRICE, BigUsagePrice = (decimal)item.BIGUSAGEPRICE,
							UsageForStandartPrice = Math.Round((decimal)item.USAGEFORSTANDARTPRICE, 0),
							ImagePath = HelperMethod.GetUtilityImage(item.ID), Unit = unit, ResourceName = GetResourceNameForUtility(item.NAME)});
					else
						Utilities.Add(new Utility { Id = item.ID, Name = item.NAME, Price = item.PRICE,
							ImagePath = HelperMethod.GetUtilityImage(item.ID), Unit = unit, ResourceName = GetResourceNameForUtility(item.NAME) });
				}
			}
		}

		public void GetDataForUtility(string utilityName)
		{
			using (var context = new TownUtilityBillSystemV2Entities())
			{
				var utilityDB = context.UTILITYs.Where(u => u.NAME == utilityName).FirstOrDefault();
				var unitDB = context.UNITs.Find(utilityDB.UNIT_ID);
				var utilitiesDB = context.UTILITYs.ToList();


				Unit.Id = unitDB.ID;
				Unit.Name = unitDB.NAME;

				Utility.Id = utilityDB.ID;
				Utility.Name = utilityDB.NAME;
				Utility.Price = utilityDB.PRICE;
				Utility.ResourceName = GetResourceNameForUtility(utilityDB.NAME);
				Utility.ImagePath = HelperMethod.GetUtilityImage(utilityDB.ID);

				if (utilityDB.USAGEFORSTANDARTPRICE != null)
					Utility.UsageForStandartPrice = (decimal)utilityDB.USAGEFORSTANDARTPRICE;
				if (utilityDB.BIGUSAGEPRICE != null)
					Utility.BigUsagePrice = (decimal)utilityDB.BIGUSAGEPRICE;

				Utility.Unit = Unit;

				foreach (var u in utilitiesDB)
				{
					if (u.NAME != Utility.Name)
						Utilities.Add(new Utility() { Name = u.NAME, ResourceName = GetResourceNameForUtility(u.NAME) });
				}

				var meterTypesDB = context.METER_TYPEs.Where(mt => mt.UTILITY_ID == Utility.Id).ToList();
				var metersDB = context.METERs.ToList();
				HashSet<int> meterTypesIds = new HashSet<int>();

				foreach (var m in meterTypesDB)
					meterTypesIds.Add(m.ID);

				foreach (var m in metersDB)
				{
					if (meterTypesIds.Contains(m.METER_TYPE_ID))
						MeterQty += 1;
				}
			}
		}

		#endregion

		#region Private

		public static string GetResourceNameForUtility(string name)
		{
			string resourceName;

			if (name == InitialDB.InitialDBEnums.Utilities.Electricity.ToString())
				resourceName = Localization.Electricity;
			else if (name == InitialDB.InitialDBEnums.Utilities.Water.ToString())
				resourceName = Localization.Water;
			else if (name == InitialDB.InitialDBEnums.Utilities.Heating.ToString())
				resourceName = Localization.Heating;
			else if (name == InitialDB.InitialDBEnums.Utilities.Gas.ToString())
				resourceName = Localization.Gas;
			else
				resourceName = name;

			return resourceName;
		}

		#endregion

		#endregion
	}
}
