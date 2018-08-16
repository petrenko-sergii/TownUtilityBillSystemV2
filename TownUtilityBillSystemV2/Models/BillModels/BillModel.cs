using System;
using System.Collections.Generic;
using System.Linq;
using TownUtilityBillSystemV2.Models.AccountModels;
using TownUtilityBillSystemV2.Models.AddressModels;
using TownUtilityBillSystemV2.Models.Chart;
using TownUtilityBillSystemV2.Models.Currency;
using TownUtilityBillSystemV2.Models.CustomerModels;
using TownUtilityBillSystemV2.Models.HelperMethods;
using TownUtilityBillSystemV2.Models.MeterModels;
using TownUtilityBillSystemV2.Models.TemperatureModels;
using TownUtilityBillSystemV2.Models.UtilityModels;
using TownUtilityBillSystemV2.Models.UtilitySupplier;
using TownUtilityBillSystemV2.Resources;
using static TownUtilityBillSystemV2.Models.InitialDB.InitialDBEnums;

namespace TownUtilityBillSystemV2.Models.BillModels
{
	public class BillModel
	{
		#region Fields

		private readonly int billCountToDisplay = 25;

		#endregion

		#region Properties

		public int BillCountToDisplay { get { return billCountToDisplay; } }

		public Bill Bill;
		public List<Bill> Bills;
		public CustomerModel CustomerModel;
		public CurrencyMoney Currency;
		public UtilitySupplierModel UtilitySupplierModel;
		public List<ChartData> UtilityChargesChartData;
		public List<TemperatureModel> Temperatures;
		public string TempearatureIconPath { get; } = HelperMethod.GetTemperatureIconImage();

		#endregion

		#region Ctor

		public BillModel()
		{
			Bill = new Bill();
			Bills = new List<Bill>();
			CustomerModel = new CustomerModel();
			Currency = new CurrencyMoney();
			UtilitySupplierModel = new UtilitySupplierModel();
			UtilityChargesChartData = new List<ChartData>();
			Temperatures = new List<TemperatureModel>();
		}

		#endregion

		#region Methods

		public void GetBillsToShow()
		{
			using (var context = new TownUtilityBillSystemV2Entities())
			{
				List<BILL> rndBillsDB = GetSomeRandomBillsDB(context);

				foreach (var b in rndBillsDB)
				{
					Customer customer = new Customer();

					var accountDB = context.ACCOUNTs.Where(c => c.ID == b.ACCOUNT_ID).FirstOrDefault();
					var customerDB = context.CUSTOMERs.Where(c => c.ACCOUNT_ID == accountDB.ID).FirstOrDefault();

					CustomerType customerType;
					Address address;

					GetAddressAndCustomerTypeForCustomer(context, customerDB, out customerType, out address);

					customer.Id = customerDB.ID;
					customer.Surname = customerDB.SURNAME;
					customer.Name = customerDB.NAME;
					customer.Address = address;

					Bills.Add(new Bill() { Id = b.ID, Number = b.NUMBER, Period = HelperMethod.GetFullMonthName(b.PERIOD), Date = b.DATE, Sum = b.SUM, Paid = b.PAID, Customer = customer });
				}

			}
		}

		internal void GetBillData(string billNumber)
		{
			using (var context = new TownUtilityBillSystemV2Entities())
			{
				BILL billDB = context.BILLs.Where(b => b.NUMBER == billNumber).FirstOrDefault();
				Account account = context.ACCOUNTs.Where(a => a.ID == billDB.ACCOUNT_ID).Select(Account.Get).FirstOrDefault();

				if (billDB != null)
				{
					Bill = Bill.Get(billDB);
					Bill.Account = account;

					var customerDB = context.CUSTOMERs.Where(c => c.ACCOUNT_ID == billDB.ACCOUNT_ID).FirstOrDefault();

					if (customerDB != null)
					{
						CustomerModel.Customer.Surname = customerDB.SURNAME;
						CustomerModel.Customer.Name = customerDB.NAME;
					}
				}
			}
		}

		internal void GetBillData(int bill_Id)
		{
			using (var context = new TownUtilityBillSystemV2Entities())
			{
				var billDB = context.BILLs.Where(b => b.ID == bill_Id).FirstOrDefault();

				if (billDB != null)
					GetBillData(billDB.NUMBER);
			}
		}

		private List<BILL> GetSomeRandomBillsDB(TownUtilityBillSystemV2Entities context)
		{
			Random rnd = new Random();
			int[] rndBillIds = new int[BillCountToDisplay];
			int billDBCount = context.BILLs.ToList().Count;

			for (int i = 0; i < BillCountToDisplay; i++)
				rndBillIds[i] = rnd.Next(0, billDBCount);

			var rndBillsDB = context.BILLs.Where(b => rndBillIds.Any(i => b.ID == i)).Distinct().ToList();

			return rndBillsDB;
		}

		private void GetAddressAndCustomerTypeForCustomer(TownUtilityBillSystemV2Entities context, CUSTOMER customerDB, out CustomerType customerType, out Address address)
		{
			customerType = context.CUSTOMER_TYPEs.Where(mt => mt.ID == customerDB.CUSTOMER_TYPE_ID).Select(CustomerType.Get).FirstOrDefault();
			var addressDB = context.ADDRESSes.Where(a => a.ID == customerDB.ADDRESS_ID).FirstOrDefault();
			var index = context.INDEXes.Where(i => i.ID == addressDB.INDEX_ID).Select(Index.Get).FirstOrDefault();
			var town = context.TOWNs.Where(t => t.ID == addressDB.TOWN_ID).Select(Town.Get).FirstOrDefault();
			var street = context.STREETs.Where(s => s.ID == addressDB.STREET_ID).Select(Street.Get).FirstOrDefault();
			var building = context.BUILDINGs.Where(b => b.ID == addressDB.BUILDING_ID).Select(Building.GetWithSquareAndImagePath).FirstOrDefault();
			var flatPartDB = context.FLAT_PARTs.Where(fp => fp.ID == addressDB.FLAT_PART_ID).FirstOrDefault();

			FlatPart flatPart = null;

			if (flatPartDB != null)
				flatPart = FlatPart.Get(flatPartDB);

			address = new Address() { Id = addressDB.ID, Index = index, Town = town, Street = street, Building = building, FlatPart = flatPart };
		}

		internal void GetBillDataWithAllUtilities(int bill_Id)
		{
			using (var context = new TownUtilityBillSystemV2Entities())
			{
				CUSTOMER customerDB = null;
				float consumedMonthValue = 0;
				float utilityCharges = 0;
				List<MeterItem> meterItemList = new List<MeterItem>();

				var billDB = context.BILLs.Find(bill_Id);

				if (billDB != null)
				{
					Bill = Bill.Get(billDB);

					var accountDB = context.ACCOUNTs.Where(a => a.ID == billDB.ACCOUNT_ID).FirstOrDefault();

					customerDB = context.CUSTOMERs.Where(c => c.ACCOUNT_ID == accountDB.ID).FirstOrDefault();
				}

				if (customerDB != null)
					GetCustomerDataForView(context, customerDB);
				else
					CustomerModel.Customer = null;

				#region Initialization dates and variables for charts

				float valueDifference;
				string monthName;
				string verificationNote;

				DateTime localDate = DateTime.Now;
				DateTime startDate = new DateTime();
				DateTime finishDate = new DateTime();
				DateTime startDateForChart = new DateTime();
				DateTime finishDateForChart = new DateTime();

				startDate = Convert.ToDateTime(Bill.Period + "-01");
				finishDate = startDate.AddMonths(1);

				int monthDifferenceBillPeriodAndStartUsageChart = 1;
				int presChartYear = startDate.Year;
				int startChartDay = 1;
				int prevChartYear = presChartYear - 1;
				int presChartMonth = startDate.Month;

				#endregion

				var metersDB = context.METERs.Where(m => m.ADDRESS_ID == CustomerModel.Customer.Address.Id).ToList();

				foreach (var m in metersDB)
				{
					#region Filling meterItems for Utility Usage Charts

					List<ChartData> chartData = new List<ChartData>();
					meterItemList.Clear();

					startDateForChart = new DateTime(prevChartYear, presChartMonth + monthDifferenceBillPeriodAndStartUsageChart, startChartDay);
					finishDateForChart = startDate.AddMonths(1);

					var meterTypeDB = context.METER_TYPEs.Where(mt => mt.ID == m.METER_TYPE_ID).FirstOrDefault();
					var utilityDB = context.UTILITYs.Where(u => u.ID == meterTypeDB.UTILITY_ID).FirstOrDefault();
					Utility utility;

					if (utilityDB.USAGEFORSTANDARTPRICE != null && utilityDB.BIGUSAGEPRICE != null)
						utility = Utility.GetUtilityWithBigUsagePrice(utilityDB);
					else
						utility = Utility.GetUtilityWithOutBigUsagePrice(utilityDB);

					if (utility.Id == (int)Utilities.Heating)
						GetTemperatureHistory(this, m.ADDRESS.TOWN_ID);

					meterItemList = context.METER_ITEMs.Where(mi => mi.METER_ID == m.ID).Select(MeterItem.GetMeterItemWithOutMeter).ToList();

					#region Getting chartData for every customer's meter 

					if (meterItemList.Count != 0)
					{
						for (; startDateForChart <= finishDateForChart; startDateForChart = startDateForChart.AddMonths(1))
						{
							var startElValue = meterItemList.FirstOrDefault(ml => ml.Date == startDateForChart.AddMonths(-1)).Value;
							var finishElValue = meterItemList.FirstOrDefault(ml => ml.Date == startDateForChart).Value;
							valueDifference = (float)Math.Round(finishElValue - startElValue, 2);

							monthName = HelperMethod.GetMonthNameAndYear(startDateForChart);

							chartData.Add(new ChartData() { MonthName = monthName, Value = valueDifference });
						}
					}

					#endregion

					#endregion

					var meterType = new MeterType() { Id = meterTypeDB.ID, Name = meterTypeDB.NAME, VarificationPeriod = meterTypeDB.VARIFICATION_PERIOD_YEARS, Utility = utility };

					consumedMonthValue = GetConsumedMonthValue(startDate, finishDate, m);

					if (m.METER_TYPE.UTILITY_ID == (int)Utilities.Electricity && consumedMonthValue > (float)m.METER_TYPE.UTILITY.USAGEFORSTANDARTPRICE)
						utilityCharges = (float)m.METER_TYPE.UTILITY.USAGEFORSTANDARTPRICE * (float)m.METER_TYPE.UTILITY.PRICE + (consumedMonthValue - (float)m.METER_TYPE.UTILITY.USAGEFORSTANDARTPRICE) * (float)m.METER_TYPE.UTILITY.BIGUSAGEPRICE;
					else
						utilityCharges = consumedMonthValue * (float)m.METER_TYPE.UTILITY.PRICE;

					UtilityChargesChartData.Add(new ChartData() { UtilityCharges = (float)Math.Round(utilityCharges, 2) });

					verificationNote = GetVerificationNoteForMeter(m, meterType);

					CustomerModel.Meters.Add(new Meter()
					{
						Id = m.ID,
						SerialNumber = m.SERIAL_NUMBER,
						ReleaseDate = m.RELEASE_DATE,
						VarificationDate = m.VARIFICATION_DATE,
						MeterType = meterType,
						ConsumedMonthValue = consumedMonthValue,
						ChartData = chartData,
						VerificationNote = verificationNote
					});
				}
			}
		}

		private string GetVerificationNoteForMeter(METER meter, MeterType meterType)
		{
			var age = Bill.Date.Year - meter.VARIFICATION_DATE.Year;

			if (meter.VARIFICATION_DATE > Bill.Date.AddYears(-age))
				age--;

			return (age >= meterType.VarificationPeriod) ?
				String.Format(Localization.MeterVerificationNote, 
				meterType.VarificationPeriod, meter.VARIFICATION_DATE.ToString("dd MM yyyy")) :
				String.Empty;
		}

		private static float GetConsumedMonthValue(DateTime startDate, DateTime finishDate, METER meter)
		{
			using (var context = new TownUtilityBillSystemV2Entities())
			{
				float consumedMonthValue;

				var startMeterItem = (from item in context.METER_ITEMs
									  where item.METER_ID == meter.ID && item.DATE == startDate
									  select item).FirstOrDefault();

				var finishMeterItem = (from item in context.METER_ITEMs
									   where item.METER_ID == meter.ID && item.DATE == finishDate
									   select item).FirstOrDefault();

				consumedMonthValue = finishMeterItem.VALUE - startMeterItem.VALUE;

				return consumedMonthValue;
			}
		}

		private static void GetTemperatureHistory(BillModel model, int townId)
		{
			using (var context = new TownUtilityBillSystemV2Entities())
			{
				List<Temperature> temperaturesDB = new List<Temperature>();
				DateTime temperatureStartDate = new DateTime();
				DateTime temperatureFinishDate = new DateTime();
				int temperatureYearsHistory = 2;
				float valueSum = 0;
				float averageValue;
				string fullMonthName = "";

				temperatureStartDate = Convert.ToDateTime(model.Bill.Period + "-01");
				temperatureStartDate = temperatureStartDate.AddYears(-1);
				temperatureFinishDate = temperatureStartDate.AddMonths(1);

				temperaturesDB = context.TEMPERATUREs.Where(t => t.TOWN_ID == townId).Select(Temperature.Get).ToList();

				for (int j = 0; j < temperatureYearsHistory; j++)
				{
					for (; temperatureStartDate < temperatureFinishDate; temperatureStartDate = temperatureStartDate.AddDays(1))
						valueSum += (float)(temperaturesDB.FirstOrDefault(t => t.Date == temperatureStartDate).MinValue +
							temperaturesDB.FirstOrDefault(t => t.Date == temperatureStartDate).MaxValue) / 2;

					temperatureStartDate = temperatureStartDate.AddMonths(-1);
					fullMonthName = HelperMethod.UppercaseFirstLetter(temperatureStartDate.ToString("MMMM yyyy"));

					averageValue = valueSum / System.DateTime.DaysInMonth(temperatureStartDate.Year, temperatureStartDate.Month);

					model.Temperatures.Add(new TemperatureModel() { AverageValue = (float)Math.Round(averageValue, 1), MonthName = fullMonthName });

					temperatureStartDate = temperatureStartDate.AddYears(1);
					temperatureFinishDate = temperatureStartDate.AddMonths(1);

					valueSum = 0;
				}
			}
		}

		public void FindBills(string searchString)
		{
			using (var context = new TownUtilityBillSystemV2Entities())
			{
				DateTime billPeriodDate;
				string billPeriod = null;

				if (DateTime.TryParse(searchString, out billPeriodDate))
					billPeriod = billPeriodDate.ToString("yyyy-MM");


				List<int> accountIds = (from c in context.CUSTOMERs
										where
										   c.SURNAME.Contains(searchString) ||
										   c.NAME.Contains(searchString) ||
										   (c.SURNAME + " " + c.NAME).Contains(searchString) ||
										   c.ADDRESS.TOWN.NAME.Contains(searchString) ||
										   c.ADDRESS.STREET.NAME.Contains(searchString) ||
										   c.ADDRESS.BUILDING.NUMBER.Contains(searchString) ||
										   c.ADDRESS.FLAT_PART.NAME.Contains(searchString) ||
										   c.CUSTOMER_TYPE.NAME.Contains(searchString) ||
										   c.ADDRESS.INDEX.VALUE.ToString().Contains(searchString)
										select c)
								   .Select(c => c.ACCOUNT_ID).ToList();

				var billsDBFoundByCustomerData = context.BILLs.Where(b => accountIds.Any(i => b.ACCOUNT_ID == i)).Distinct().ToList();


				var billsDBFoundByBillData = (from b in context.BILLs
											  where
												  b.NUMBER.Contains(searchString) ||
												  b.PERIOD.Contains(billPeriod)
											  select b)
							   .ToList();

				var allBills = billsDBFoundByCustomerData.Concat(billsDBFoundByBillData).OrderBy(b => b.NUMBER).ToList();

				foreach (var b in allBills)
				{
					Customer customer = new Customer();

					var accountDB = context.ACCOUNTs.Where(c => c.ID == b.ACCOUNT_ID).FirstOrDefault();
					var customerDB = context.CUSTOMERs.Where(c => c.ACCOUNT_ID == accountDB.ID).FirstOrDefault();

					CustomerType customerType;
					Address address;

					GetAddressAndCustomerTypeForCustomer(context, customerDB, out customerType, out address);

					customer.Id = customerDB.ID;
					customer.Surname = customerDB.SURNAME;
					customer.Name = customerDB.NAME;
					customer.Address = address;

					Bills.Add(new Bill() { Id = b.ID, Number = b.NUMBER, Period = HelperMethod.GetFullMonthName(b.PERIOD), Date = b.DATE, Sum = b.SUM, Paid = b.PAID, Customer = customer });
				}
			}
		}

		internal void GetAllBillsForCustomer(int customerId)
		{
			using (var context = new TownUtilityBillSystemV2Entities())
			{
				var customerDB = context.CUSTOMERs.Find(customerId);
				var accountDB = context.ACCOUNTs.Where(a => a.ID == customerDB.ACCOUNT_ID).FirstOrDefault();
				Bills = context.BILLs.Where(b => b.ACCOUNT_ID == accountDB.ID).Select(Bill.Get).ToList();

				if (customerDB != null)
					GetCustomerDataForView(context, customerDB);
				else
					CustomerModel.Customer = null;
			}
		}
		
		private void GetCustomerDataForView(TownUtilityBillSystemV2Entities context, CUSTOMER customerDB)
		{
			CustomerType customerType;
			Address address;
			GetAddressAndCustomerTypeForCustomer(context, customerDB, out customerType, out address);

			var account = context.ACCOUNTs.Where(a => a.ID == customerDB.ACCOUNT_ID).Select(Account.Get).FirstOrDefault();

			CustomerModel.Customer = new Customer() { Id = customerDB.ID, Account = account, Surname = customerDB.SURNAME, Name = customerDB.NAME, Email = customerDB.EMAIL, Phone = customerDB.PHONE, CustomerType = customerType, Address = address };
		}

		#endregion
	}
}
