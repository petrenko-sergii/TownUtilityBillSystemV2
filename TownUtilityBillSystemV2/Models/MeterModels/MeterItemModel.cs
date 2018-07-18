using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TownUtilityBillSystemV2.Models.AddressModels;
using TownUtilityBillSystemV2.Models.Chart;
using TownUtilityBillSystemV2.Models.HelperMethods;
using TownUtilityBillSystemV2.Models.UtilityModels;
using TownUtilityBillSystemV2.Resources;
using static TownUtilityBillSystemV2.Models.InitialDB.InitialDBEnums;

namespace TownUtilityBillSystemV2.Models.MeterModels
{
	public class MeterItemModel
	{
		#region Properties

		public List<MeterItem> MeterItems;
		public Meter Meter;
		public MeterItem MeterItem;
		public List<ChartData> ChartData;

		public string Period { get; set; }
		public float Value { get; set; }

		#endregion

		#region Ctor

		public MeterItemModel()
		{
			MeterItems = new List<MeterItem>();
			ChartData = new List<ChartData>();
			Meter = new Meter();
			MeterItem = new MeterItem();
			Period = "";
			Value = 0;
		}

		#endregion

		#region Methods

		internal void GetMeterData(int meterId)
		{
			using (var context = new TownUtilityBillSystemV2Entities())
			{
				InitializeMeterProperty(meterId, context);

				MeterItems = context.METER_ITEMs.Where(mi => mi.METER_ID == meterId).Select(MeterItem.GetMeterItemWithOutMeter).ToList();
			}
		}

		private void InitializeMeterProperty(int meterId, TownUtilityBillSystemV2Entities context)
		{
			var meterDB = context.METERs.Where(m => m.ID == meterId).FirstOrDefault();
			var meterTypeDB = context.METER_TYPEs.Where(mt => mt.ID == meterDB.METER_TYPE_ID).FirstOrDefault();
			var utilityDB = context.UTILITYs.Where(u => u.ID == meterTypeDB.UTILITY_ID).FirstOrDefault();
			var addressDB = context.ADDRESSes.Where(a => a.ID == meterDB.ADDRESS_ID).FirstOrDefault();
			var flatPartDB = context.FLAT_PARTs.Where(fp => fp.ID == addressDB.FLAT_PART_ID).FirstOrDefault();

			var unit = context.UNITs.Where(u => u.ID == utilityDB.UNIT_ID).Select(Unit.Get).FirstOrDefault(); ;
			var utility = new Utility() { Id = utilityDB.ID, Name = utilityDB.NAME, Unit = unit, ResourceName = UtilityModel.GetResourceNameForUtility(utilityDB.NAME) };
			var meterType = new MeterType() { Id = meterTypeDB.ID, Name = meterTypeDB.NAME, Utility = utility };

			var index = context.INDEXes.Where(i => i.ID == addressDB.INDEX_ID).Select(Index.Get).FirstOrDefault();
			var town = context.TOWNs.Where(t => t.ID == addressDB.TOWN_ID).Select(Town.Get).FirstOrDefault();
			var street = context.STREETs.Where(s => s.ID == addressDB.STREET_ID).Select(Street.Get).FirstOrDefault();
			var building = context.BUILDINGs.Where(b => b.ID == addressDB.BUILDING_ID).Select(Building.Get).FirstOrDefault();

			FlatPart flatPart = null;

			if (flatPartDB != null)
				flatPart = FlatPart.Get(flatPartDB);


			var address = new Address() { Id = addressDB.ID, Index = index, Town = town, Street = street, Building = building, FlatPart = flatPart };

			Meter = new Meter() { Id = meterDB.ID, SerialNumber = meterDB.SERIAL_NUMBER, ReleaseDate = meterDB.RELEASE_DATE, VarificationDate = meterDB.VARIFICATION_DATE, MeterType = meterType, Address = address };
		}

		public void GetMeterDataForChart(int meterId)
		{
			using (var context = new TownUtilityBillSystemV2Entities())
			{
				List<MeterItem> meterItems = new List<MeterItem>();

				DateTime presDate = DateTime.Today;
				DateTime startDate = new DateTime();
				DateTime finishDate = new DateTime();

				int startDay = 1;
				int presYear = presDate.Year;
				int presMonth = presDate.Month;
				int prevYear = presYear - 1;
				int prevMonth = presMonth - 1;
				int nextMonth = presMonth + 1;
				float valueDifference;
				string monthName;

				startDate = new DateTime(prevYear, nextMonth, startDay);
				finishDate = new DateTime(presYear, presMonth, startDay);

				meterItems = context.METER_ITEMs.Where(mi => mi.METER_ID == meterId).Select(MeterItem.GetMeterItemWithOutMeter).ToList();

				if (meterItems.Count != 0)
				{
					for (; startDate <= finishDate; startDate = startDate.AddMonths(1))
					{
						var startValue = meterItems.Where(m => m.Date == startDate.AddMonths(-1)).FirstOrDefault().Value;
						var finishValue = meterItems.Where(m => m.Date == startDate).FirstOrDefault().Value;

						valueDifference = (float)Math.Round(finishValue - startValue, 2);

						if (startDate.Month != 1)
						{
							Months month = (Months)(startDate.Month - 1);
							monthName = HelperMethod.GetResourceNameForMonth(month.ToString()) + " " + startDate.Year;
						}
						else
						{
							Months month = Months.December;
							monthName = HelperMethod.GetResourceNameForMonth(month.ToString()) + " " + startDate.AddYears(-1).Year;
						}

						ChartData.Add(new ChartData() { MonthName = monthName, Value = valueDifference });
					}
				}
			}
		}

		internal void GetMeterAndDataToEdit(int meterItemId)
		{
			using (var context = new TownUtilityBillSystemV2Entities())
			{
				var meterItemDB = context.METER_ITEMs.Find(meterItemId);
				var meterDB = context.METERs.Where(m => m.ID == meterItemDB.METER_ID).FirstOrDefault();

				CreateMeterModelWithOneMeter(meterDB.ID, context, this);

				MeterItem = MeterItem.GetMeterItemWithOutMeter(meterItemDB);
			}
		}

		private static void CreateMeterModelWithOneMeter(int meterId, TownUtilityBillSystemV2Entities context, MeterItemModel model)
		{
			var meterDB = context.METERs.Where(m => m.ID == meterId).FirstOrDefault();
			var meterTypeDB = context.METER_TYPEs.Where(mt => mt.ID == meterDB.METER_TYPE_ID).FirstOrDefault();
			var utilityDB = context.UTILITYs.Where(u => u.ID == meterTypeDB.UTILITY_ID).FirstOrDefault();
			var addressDB = context.ADDRESSes.Where(a => a.ID == meterDB.ADDRESS_ID).FirstOrDefault();
			var index = context.INDEXes.Where(i => i.ID == addressDB.INDEX_ID).Select(Index.Get).FirstOrDefault();
			var town = context.TOWNs.Where(t => t.ID == addressDB.TOWN_ID).Select(Town.Get).FirstOrDefault();
			var street = context.STREETs.Where(s => s.ID == addressDB.STREET_ID).Select(Street.Get).FirstOrDefault();
			var building = context.BUILDINGs.Where(b => b.ID == addressDB.BUILDING_ID).Select(Building.Get).FirstOrDefault();
			var flatPartDB = context.FLAT_PARTs.Where(fp => fp.ID == addressDB.FLAT_PART_ID).FirstOrDefault();
			var unit = context.UNITs.Where(u => u.ID == utilityDB.UNIT_ID).Select(Unit.Get).FirstOrDefault();

			var utility = new Utility() { Id = utilityDB.ID, Name = utilityDB.NAME, Unit = unit };
			var meterType = new MeterType() { Id = meterTypeDB.ID, Name = meterTypeDB.NAME, Utility = utility };

			FlatPart flatPart = null;

			if (flatPartDB != null)
				flatPart = FlatPart.Get(flatPartDB);

			var address = new Address() { Id = addressDB.ID, Index = index, Town = town, Street = street, Building = building, FlatPart = flatPart };

			model.Meter = new Meter() { Id = meterDB.ID, SerialNumber = meterDB.SERIAL_NUMBER, ReleaseDate = meterDB.RELEASE_DATE, VarificationDate = meterDB.VARIFICATION_DATE, MeterType = meterType, Address = address };
		}

		#endregion
	}
}