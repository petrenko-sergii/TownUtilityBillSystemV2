using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TownUtilityBillSystemV2.Models.HelperMethods;
using TownUtilityBillSystemV2.Models.UtilityModels;

namespace TownUtilityBillSystemV2.Models.MeterModels
{
	public class MeterTypeModel
	{
		#region Properties

		public MeterType MeterType;

		public List<MeterType> MeterTypes;

		public Utility Utility;

		#endregion

		#region Ctor

		public MeterTypeModel()
		{
			MeterType = new MeterType();
			MeterTypes = new List<MeterType>();
			Utility = new Utility();
		}

		#endregion


		public void GetAllMeterTypes()
		{
			using (var context = new TownUtilityBillSystemV2Entities())
			{
				var meterTypesDB = context.METER_TYPEs.ToList();

				foreach (var mt in meterTypesDB)
				{
					var utilityDB = context.UTILITYs.Where(u => u.ID == mt.UTILITY_ID).FirstOrDefault();
					var utility = new Utility() { Id = utilityDB.ID, Name = utilityDB.NAME, ResourceName = UtilityModel.GetResourceNameForUtility(utilityDB.NAME), ImagePath = HelperMethod.GetUtilityImage(utilityDB.ID) };

					MeterTypes.Add(new MeterType() { Id = mt.ID, Name = mt.NAME, VarificationPeriod = mt.VARIFICATION_PERIOD_YEARS, Utility = utility });
				}
			}
		}

		
	}
}
