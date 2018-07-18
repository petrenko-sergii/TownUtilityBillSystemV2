using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TownUtilityBillSystemV2.Models.BaseModels;
using TownUtilityBillSystemV2.Models.UtilityModels;
using TownUtilityBillSystemV2.Resources;

namespace TownUtilityBillSystemV2.Models.MeterModels
{
	public class MeterType : ObjectWithName
	{
		[Required(ErrorMessageResourceName = "FieldIsRequired", ErrorMessageResourceType = typeof(Localization))]
		public new int Id { get; set; }

		[Required(ErrorMessageResourceName = "FieldIsRequired", ErrorMessageResourceType = typeof(Localization))]
		[Range(1, int.MaxValue, ErrorMessageResourceName = "ValueMustBeBiggerThan", ErrorMessageResourceType = typeof(Localization))]
		[Display(Name = "VarificationPeriod", ResourceType = typeof(Localization))]
		public int VarificationPeriod { get; set; }

		[Required(ErrorMessageResourceName = "FieldIsRequired", ErrorMessageResourceType = typeof(Localization))]
		public Utility Utility { get; set; }

		public List<Utility> Utilities { get; set; }

		public static MeterType Get(METER_TYPE meterType)
		{
			return new MeterType
			{
				Id = meterType.ID,
				Name = meterType.NAME,
				VarificationPeriod = meterType.VARIFICATION_PERIOD_YEARS
			};
		}

		internal MeterType GetMeterType(int meterTypeId)
		{
			using (var context = new TownUtilityBillSystemV2Entities())
			{
				MeterType meterType;
				var meterTypeDB = context.METER_TYPEs.Find(meterTypeId);

				if (meterTypeDB != null)
				{
					var utilityDB = context.UTILITYs.Where(u => u.ID == meterTypeDB.UTILITY_ID).FirstOrDefault();
					var utility = Utility.GetUtilityWithIdAndResourceName(utilityDB);

					meterType = MeterType.Get(meterTypeDB);
					meterType.Utility = utility;

					meterType.Utilities = context.UTILITYs.Select(Utility.GetUtilityWithIdAndResourceName).ToList();
				}
				else
				{
					meterType = null;
				}

				return meterType;
			}
		}

		internal void UpdateMeterType(MeterType meterType)
		{
			using (var context = new TownUtilityBillSystemV2Entities())
			{
				var meterTypeDB = context.METER_TYPEs.Find(meterType.Id);

				meterTypeDB.NAME = meterType.Name;
				meterTypeDB.UTILITY_ID = meterType.Utility.Id;
				meterTypeDB.VARIFICATION_PERIOD_YEARS = meterType.VarificationPeriod;

				context.SaveChanges();
			}
		}
	}
}