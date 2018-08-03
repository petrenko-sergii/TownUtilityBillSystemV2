using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using TownUtilityBillSystemV2.Resources;
using TownUtilityBillSystemV2.Models.AddressModels;
using TownUtilityBillSystemV2.Models.Chart;
using TownUtilityBillSystemV2.Models.UtilityModels;

namespace TownUtilityBillSystemV2.Models.MeterModels
{
	public class Meter
	{
		public int Id { get; set; }

		[Required(ErrorMessageResourceName = "FieldIsRequired", ErrorMessageResourceType = typeof(Localization))]
		[StringLength(20, ErrorMessageResourceName = "ValueMustBeAtLeastCharactersLong", ErrorMessageResourceType = typeof(Localization), MinimumLength = 6)]
		[Display(Name = "SerialNumber", ResourceType = typeof(Localization))]
		public string SerialNumber { get; set; }

		[Required(ErrorMessageResourceName = "FieldIsRequired", ErrorMessageResourceType = typeof(Localization))]
		[Display(Name = "ReleaseDate", ResourceType = typeof(Localization))]
		public DateTime ReleaseDate { get; set; }

		[Required(ErrorMessageResourceName = "FieldIsRequired", ErrorMessageResourceType = typeof(Localization))]
		[Display(Name = "VarificationDate", ResourceType = typeof(Localization))]
		public DateTime VarificationDate { get; set; }

		public string VerificationNote { get; set; }

		public float ConsumedMonthValue { get; set; }

		public Address Address { get; set; }

		public MeterType MeterType { get; set; }

		public List<MeterType> MeterTypes { get; set; }

		public List<MeterItemModel> MeterItemModels { get; set; }

		public List<ChartData> ChartData { get; set; }

		internal void GetMeterForEdit(int meterId)
		{
			using (var context = new TownUtilityBillSystemV2Entities())
			{
				var meterDB = context.METERs.Find(meterId);

				if (meterDB != null)
				{
					var meterTypeDB = context.METER_TYPEs.Where(mt => mt.ID == meterDB.METER_TYPE_ID).FirstOrDefault();
					var utility = context.UTILITYs.Where(u => u.ID == meterTypeDB.UTILITY_ID).Select(Utility.GetUtilityWithIdAndResourceName).FirstOrDefault();

					var meterType = new MeterType() { Id = meterTypeDB.ID, Name = meterTypeDB.NAME, VarificationPeriod = meterTypeDB.VARIFICATION_PERIOD_YEARS, Utility = utility };

					Id = meterDB.ID;
					MeterType = meterType;
					SerialNumber = meterDB.SERIAL_NUMBER;
					ReleaseDate = meterDB.RELEASE_DATE;
					VarificationDate = meterDB.VARIFICATION_DATE;

					MeterTypes = context.METER_TYPEs.Select(MeterType.Get).ToList();
				}
			}
		}

		internal void UpdateMeter(Meter meter)
		{
			using (var context = new TownUtilityBillSystemV2Entities())
			{
				var meterDB = context.METERs.Find(meter.Id);

				meterDB.SERIAL_NUMBER = meter.SerialNumber;
				meterDB.RELEASE_DATE = meter.ReleaseDate;
				meterDB.VARIFICATION_DATE = meter.VarificationDate;
				meterDB.METER_TYPE_ID = meter.MeterType.Id;

				context.SaveChanges();
			}
		}
	}
}