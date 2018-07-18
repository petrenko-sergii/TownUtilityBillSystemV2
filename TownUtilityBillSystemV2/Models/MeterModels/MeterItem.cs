using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using TownUtilityBillSystemV2.Resources;

namespace TownUtilityBillSystemV2.Models.MeterModels
{
	public class MeterItem
	{
		public int Id { get; set; }
		public Meter Meter { get; set; }
		public DateTime Date { get; set; }

		[Required(ErrorMessageResourceName = "FieldIsRequired", ErrorMessageResourceType = typeof(Localization))]
		[Range(0.1, int.MaxValue, ErrorMessageResourceName = "ValueMustBeBiggerThanZero", ErrorMessageResourceType = typeof(Localization))]
		[Display(Name = "NewValue", ResourceType = typeof(Localization))]
		public float Value { get; set; }

		public static MeterItem GetMeterItemWithOutMeter(METER_ITEM meterItem)
		{
			return new MeterItem
			{
				Id = meterItem.ID,
				Date = meterItem.DATE,
				Value = meterItem.VALUE
			};
		}

		internal void UpdateMeterData(MeterItem meterItem)
		{
			using (var context = new TownUtilityBillSystemV2Entities())
			{
				var meterItemDB = context.METER_ITEMs.Find(meterItem.Id);

				meterItemDB.VALUE = meterItem.Value;
				context.SaveChanges();
			}

		}

		internal int GetMeterId(int meterItemId)
		{
			using (var context = new TownUtilityBillSystemV2Entities())
			{
				return context.METER_ITEMs.Find(meterItemId).METER_ID;
			}
		}
	}
}