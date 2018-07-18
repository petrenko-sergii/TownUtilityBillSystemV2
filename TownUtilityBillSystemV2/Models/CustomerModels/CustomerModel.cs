using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TownUtilityBillSystemV2.Models.AccountModels;
using TownUtilityBillSystemV2.Models.AddressModels;
using TownUtilityBillSystemV2.Models.Chart;
using TownUtilityBillSystemV2.Models.Currency;
using TownUtilityBillSystemV2.Models.HelperMethods;
using TownUtilityBillSystemV2.Models.MeterModels;
using TownUtilityBillSystemV2.Models.UtilityModels;
using TownUtilityBillSystemV2.Resources;
using static TownUtilityBillSystemV2.Models.InitialDB.InitialDBEnums;

namespace TownUtilityBillSystemV2.Models.CustomerModels
{
	public class CustomerModel
	{
		private readonly int customerCountToDisplay = 25;

		#region Properties

		public List<Customer> Customers;
		public List<CustomerType> CustomerTypes;
		public List<Meter> Meters;
		public Customer Customer;
		public int TotalCount { get; set; }
		public AddressModel AddressModel;
		public CurrencyMoney Currency;

		public int CustomerCountToDisplay { get { return customerCountToDisplay; } }

		#endregion

		#region Ctor

		public CustomerModel()
		{
			Customers = new List<Customer>();
			AddressModel = new AddressModel();
			Meters = new List<Meter>();
			Customer = new Customer();
			CustomerTypes = new List<CustomerType>();
			Currency = new CurrencyMoney();
			TotalCount = 0;
		}

		#endregion

		#region Methods

		public List<Customer> GetAllCustomers()
		{
			using (var context = new TownUtilityBillSystemV2Entities())
			{
				var customersDB = context.CUSTOMERs.OrderBy(c => c.SURNAME).ThenBy(c => c.NAME).ToList();

				foreach (var c in customersDB)
				{
					var customerTypeDB = context.CUSTOMER_TYPEs.Where(ct => ct.ID == c.CUSTOMER_TYPE_ID).FirstOrDefault();
					CustomerType customerType = CustomerType.Get(customerTypeDB);

					var accountDB = context.ACCOUNTs.Where(a => a.ID == c.ACCOUNT_ID).FirstOrDefault();
					Account account = Account.Get(accountDB);

					Customers.Add(new Customer() { Id = c.ID, Account = account, Surname = c.SURNAME, Name = c.NAME, Email = c.EMAIL, Phone = c.PHONE, CustomerType = customerType });
				}

				return Customers;
			}
		}

		public void GetFoundCustomers(string searchString)
		{
			using (var context = new TownUtilityBillSystemV2Entities())
			{
				var customersDB = (from c in context.CUSTOMERs
								   where
									   c.ACCOUNT.NUMBER.Contains(searchString) ||
									   c.SURNAME.Contains(searchString) ||
									   (c.SURNAME + " " + c.NAME).Contains(searchString) ||
									   c.NAME.Contains(searchString) ||
									   c.PHONE.Contains(searchString) ||
									   c.EMAIL.Contains(searchString) ||
									   c.CUSTOMER_TYPE.NAME.Contains(searchString) ||
									   c.ADDRESS.STREET.NAME.Contains(searchString) ||
									   c.ADDRESS.TOWN.NAME.Contains(searchString) ||
									   c.ADDRESS.FLAT_PART.NAME.Contains(searchString) ||
									   c.ADDRESS.INDEX.VALUE.ToString().Contains(searchString)
								   select c
									).ToList();

				CreateCustomerModelFromCustomerList(context, ref Customers, customersDB);
			}
		}

		public void GetSomeRandomCustomers()
		{
			Random rnd = new Random();

			using (var context = new TownUtilityBillSystemV2Entities())
			{
				int[] rndCustomerIds = new int[CustomerCountToDisplay];

				TotalCount = context.CUSTOMERs.ToList().Count;

				for (int i = 0; i < CustomerCountToDisplay; i++)
					rndCustomerIds[i] = rnd.Next(0, TotalCount);

				var rndCustomersDB = context.CUSTOMERs.Where(m => rndCustomerIds.Any(i => m.ID == i)).Distinct().ToList();

				CreateCustomerModelFromCustomerList(context, ref Customers, rndCustomersDB);
			}
		}

		public List<Customer> GetPrivateCutomers()
		{
			using (var context = new TownUtilityBillSystemV2Entities())
			{
				List<Customer> privateCustomers = new List<Customer>();
				List<int> privateTypeIds = GetPrivateCustomerTypeIds(context);

				var privateCustomersDB = context.CUSTOMERs.Where(c => privateTypeIds.Any(i => c.CUSTOMER_TYPE_ID == i)).ToList();

				foreach (var c in privateCustomersDB)
				{
					var customerTypeDB = context.CUSTOMER_TYPEs.Where(ct => ct.ID == c.CUSTOMER_TYPE_ID).FirstOrDefault();
					CustomerType customerType = CustomerType.Get(customerTypeDB);

					var accountDB = context.ACCOUNTs.Where(a => a.ID == c.ACCOUNT_ID).FirstOrDefault();
					Account account = Account.Get(accountDB);

					privateCustomers.Add(new Customer() { Id = c.ID, Account = account, Surname = c.SURNAME, Name = c.NAME, Email = c.EMAIL, Phone = c.PHONE, CustomerType = customerType });
				}

				return privateCustomers;
			}
		}

		public List<Customer> GetLegalCustomers()
		{
			using (var context = new TownUtilityBillSystemV2Entities())
			{
				List<Customer> legalCustomers = new List<Customer>();
				List<int> privateTypeIds = GetPrivateCustomerTypeIds(context);
				var legalCustomersDB = context.CUSTOMERs.Where(c => !privateTypeIds.Any(i => c.CUSTOMER_TYPE_ID == i)).ToList();

				foreach (var c in legalCustomersDB)
				{
					var customerTypeDB = context.CUSTOMER_TYPEs.Where(ct => ct.ID == c.CUSTOMER_TYPE_ID).FirstOrDefault();
					CustomerType customerType = CustomerType.Get(customerTypeDB);
					var accountDB = context.ACCOUNTs.Where(a => a.ID == c.ACCOUNT_ID).FirstOrDefault();
					Account account = Account.Get(accountDB);

					legalCustomers.Add(new Customer() { Id = c.ID, Account = account, Surname = c.SURNAME, Name = c.NAME, Email = c.EMAIL, Phone = c.PHONE, CustomerType = customerType });
				}

				return legalCustomers.OrderBy(c => c.Surname).ThenBy(c => c.Name).ToList();
			}
		}

		private static List<int> GetPrivateCustomerTypeIds(TownUtilityBillSystemV2Entities context)
		{
			List<int> privateTypeIds = new List<int>();

			var privateTypesDB = (from ct in context.CUSTOMER_TYPEs
								  where
									  ct.NAME.Contains("Apartment") ||
									  ct.NAME.Contains("House")
								  select ct).ToList();

			foreach (var t in privateTypesDB)
				privateTypeIds.Add(t.ID);

			return privateTypeIds;
		}

		private static void CreateCustomerModelFromCustomerList(TownUtilityBillSystemV2Entities context, ref List<Customer> customers, List<CUSTOMER> customersDB)
		{
			foreach (var c in customersDB)
			{
				var customerTypeDB = context.CUSTOMER_TYPEs.Where(ct => ct.ID == c.CUSTOMER_TYPE_ID).FirstOrDefault();

				var addressDB = context.ADDRESSes.Where(a => a.ID == c.ADDRESS_ID).FirstOrDefault();
				var indexDB = context.INDEXes.Where(i => i.ID == addressDB.INDEX_ID).FirstOrDefault();
				var townDB = context.TOWNs.Where(t => t.ID == addressDB.TOWN_ID).FirstOrDefault();
				var streetDB = context.STREETs.Where(s => s.ID == addressDB.STREET_ID).FirstOrDefault();
				var buildingDB = context.BUILDINGs.Where(b => b.ID == addressDB.BUILDING_ID).FirstOrDefault();
				var flatPartDB = context.FLAT_PARTs.Where(fp => fp.ID == addressDB.FLAT_PART_ID).FirstOrDefault();

				CustomerType customerType = CustomerType.Get(customerTypeDB);

				var index = new Index() { Id = indexDB.ID, Value = indexDB.VALUE };
				var town = new Town() { Id = townDB.ID, Name = townDB.NAME };
				var street = new Street() { Id = streetDB.ID, Name = streetDB.NAME };
				var building = new Building() { Id = buildingDB.ID, Number = buildingDB.NUMBER };

				FlatPart flatPart = null;

				if (flatPartDB != null)
				{
					if (!String.IsNullOrEmpty(flatPartDB.NAME) && !flatPartDB.NUMBER.HasValue)
						flatPart = new FlatPart() { Id = flatPartDB.ID, Name = flatPartDB.NAME };
					else if (String.IsNullOrEmpty(flatPartDB.NAME) && flatPartDB.NUMBER.HasValue)
						flatPart = new FlatPart() { Id = flatPartDB.ID, Number = (int)flatPartDB.NUMBER };
					else if (!String.IsNullOrEmpty(flatPartDB.NAME) && flatPartDB.NUMBER.HasValue)
						flatPart = new FlatPart() { Id = flatPartDB.ID, Number = (int)flatPartDB.NUMBER, Name = flatPartDB.NAME };
				}

				var address = new Address() { Id = addressDB.ID, Index = index, Town = town, Street = street, Building = building, FlatPart = flatPart };

				var accountDB = context.ACCOUNTs.Where(a => a.ID == c.ACCOUNT_ID).FirstOrDefault();
				Account account = Account.Get(accountDB);

				customers.Add(new Customer() { Id = c.ID, Account = account, Surname = c.SURNAME, Name = c.NAME, Email = c.EMAIL, Phone = c.PHONE, CustomerType = customerType, Address = address });
			}
		}

		internal object GetAllUtilitiesDataForChart(int addressId)
		{
			using (var context = new TownUtilityBillSystemV2Entities())
			{
				List<ChartData> chartData = new List<ChartData>();
				List<MeterItem> elMeterItems = new List<MeterItem>();
				List<MeterItem> waterMeterItems = new List<MeterItem>();
				List<MeterItem> heatMeterItems = new List<MeterItem>();
				List<MeterItem> gasMeterItems = new List<MeterItem>();

				List<int> metersListIds = new List<int>();

				DateTime presDate = DateTime.Today;
				DateTime startDate = new DateTime();
				DateTime finishDate = new DateTime();

				int startDay = 1;
				int presYear = presDate.Year;
				int presMonth = presDate.Month;
				int prevYear = presYear - 1;
				int nextMonth = presMonth + 1;
				float valueElDifference;
				float valueWaterDifference;
				float valueHeatDifference;
				float valueGasDifference;
				int elIndex;
				int waterIndex;
				int heatIndex;
				int gasIndex;
				string monthName;

				startDate = new DateTime(prevYear, nextMonth, startDay);
				finishDate = new DateTime(presYear, presMonth, startDay);

				metersListIds = context.METERs.Where(m => m.ADDRESS_ID == addressId).Select(m=>m.ID).ToList();

				elIndex = metersListIds[(int)Utilities.Electricity - 1];
				waterIndex = metersListIds[(int)Utilities.Water - 1];

				elMeterItems = context.METER_ITEMs.Where(mi=>mi.METER_ID == elIndex).Select(MeterItem.GetMeterItemWithOutMeter).ToList();
				waterMeterItems = context.METER_ITEMs.Where(mi => mi.METER_ID == waterIndex).Select(MeterItem.GetMeterItemWithOutMeter).ToList();

				if (metersListIds.Count > 2)
				{
					heatIndex = metersListIds[(int)Utilities.Heating - 1];
					heatMeterItems = context.METER_ITEMs.Where(mi => mi.METER_ID == heatIndex).Select(MeterItem.GetMeterItemWithOutMeter).ToList();
				}

				if (metersListIds.Count > 3)
				{
					gasIndex = metersListIds[(int)Utilities.Gas - 1];
					gasMeterItems = context.METER_ITEMs.Where(mi => mi.METER_ID == gasIndex).Select(MeterItem.GetMeterItemWithOutMeter).ToList();
				}

				if (elMeterItems.Count != 0 && waterMeterItems.Count != 0 && heatMeterItems.Count != 0 && gasMeterItems.Count != 0)
				{
					for (; startDate <= finishDate; startDate = startDate.AddMonths(1))
					{
						var startElValue = elMeterItems.FirstOrDefault(m => m.Date == startDate.AddMonths(-1)).Value;
						var finishElValue = elMeterItems.FirstOrDefault(m => m.Date == startDate).Value;
						valueElDifference = (float)Math.Round(finishElValue - startElValue, 2);


						var startWaterValue = waterMeterItems.Where(m => m.Date == startDate.AddMonths(-1)).FirstOrDefault().Value;
						var finishWaterValue = waterMeterItems.Where(m => m.Date == startDate).FirstOrDefault().Value;
						valueWaterDifference = (float)Math.Round(finishWaterValue - startWaterValue, 2);

						var startHeatValue = heatMeterItems.Where(m => m.Date == startDate.AddMonths(-1)).FirstOrDefault().Value;
						var finishHeatValue = heatMeterItems.Where(m => m.Date == startDate).FirstOrDefault().Value;
						valueHeatDifference = (float)Math.Round(finishHeatValue - startHeatValue, 2);

						var startGasValue = gasMeterItems.Where(m => m.Date == startDate.AddMonths(-1)).FirstOrDefault().Value;
						var finishGasValue = gasMeterItems.Where(m => m.Date == startDate).FirstOrDefault().Value;
						valueGasDifference = (float)Math.Round(finishGasValue - startGasValue, 2);

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

						chartData.Add(new ChartData() { MonthName = monthName, ElectricValue = valueElDifference, WaterValue = valueWaterDifference, HeatValue = valueHeatDifference, GasValue = valueGasDifference });
					}

					return chartData.Select(x => new { name = x.MonthName, elValue = x.ElectricValue, waterValue = x.WaterValue, heatValue = x.HeatValue, gasValue = x.GasValue });
				}
				return null;
			}
		}

		internal void GetAllUtilityDataForCustomer(int customerId)
		{
			using (var context = new TownUtilityBillSystemV2Entities())
			{
				var customerDB = context.CUSTOMERs.Find(customerId);

				if (customerDB != null)
				{
					var customerTypeDB = context.CUSTOMER_TYPEs.Where(mt => mt.ID == customerDB.CUSTOMER_TYPE_ID).FirstOrDefault();
					var addressDB = context.ADDRESSes.Where(a => a.ID == customerDB.ADDRESS_ID).FirstOrDefault();
					var indexDB = context.INDEXes.Where(i => i.ID == addressDB.INDEX_ID).FirstOrDefault();
					var townDB = context.TOWNs.Where(t => t.ID == addressDB.TOWN_ID).FirstOrDefault();
					var streetDB = context.STREETs.Where(s => s.ID == addressDB.STREET_ID).FirstOrDefault();
					var buildingDB = context.BUILDINGs.Where(b => b.ID == addressDB.BUILDING_ID).FirstOrDefault();
					var flatPartDB = context.FLAT_PARTs.Where(fp => fp.ID == addressDB.FLAT_PART_ID).FirstOrDefault();
					var metersDB = context.METERs.Where(m => m.ADDRESS_ID == addressDB.ID).ToList();

					foreach (var m in metersDB)
					{
						var meterTypeDB = context.METER_TYPEs.Where(mt => mt.ID == m.METER_TYPE_ID).FirstOrDefault();

						var utilityDB = context.UTILITYs.Where(u => u.ID == meterTypeDB.UTILITY_ID).FirstOrDefault();

						var utility = new Utility() { Id = utilityDB.ID, Name = utilityDB.NAME };

						var meterType = new MeterType() { Id = meterTypeDB.ID, Name = meterTypeDB.NAME, Utility = utility };

						Meters.Add(new Meter() { Id = m.ID, SerialNumber = m.SERIAL_NUMBER, ReleaseDate = m.RELEASE_DATE, VarificationDate = m.VARIFICATION_DATE, MeterType = meterType });
					}

					var customerType = new CustomerType() { Id = customerTypeDB.ID, ResourceName = CustomerType.GetResourceName(customerTypeDB.NAME)};
					var index = new Index() { Id = indexDB.ID, Value = indexDB.VALUE };
					var town = new Town() { Id = townDB.ID, Name = townDB.NAME };
					var street = new Street() { Id = streetDB.ID, Name = streetDB.NAME };
					var building = new Building() { Id = buildingDB.ID, Number = buildingDB.NUMBER, Square = (float)buildingDB.SQUARE };

					FlatPart flatPart = null;

					if (flatPartDB != null)
					{
						if (!String.IsNullOrEmpty(flatPartDB.NAME) && !flatPartDB.NUMBER.HasValue)
							flatPart = new FlatPart() { Id = flatPartDB.ID, Name = flatPartDB.NAME };

						else if (String.IsNullOrEmpty(flatPartDB.NAME) && flatPartDB.NUMBER.HasValue)
							flatPart = new FlatPart() { Id = flatPartDB.ID, Number = (int)flatPartDB.NUMBER };

						else if (!String.IsNullOrEmpty(flatPartDB.NAME) && flatPartDB.NUMBER.HasValue)
							flatPart = new FlatPart() { Id = flatPartDB.ID, Number = (int)flatPartDB.NUMBER, Name = flatPartDB.NAME };
					}

					var address = new Address() { Id = addressDB.ID, Index = index, Town = town, Street = street, Building = building, FlatPart = flatPart };

					var accountDB = context.ACCOUNTs.Where(a => a.ID == customerDB.ACCOUNT_ID).FirstOrDefault();
					Account account = Account.Get(accountDB);

					Customer = new Customer() { Id = customerDB.ID, Account = account, Surname = customerDB.SURNAME, Name = customerDB.NAME, Email = customerDB.EMAIL, Phone = customerDB.PHONE, CustomerType = customerType, Address = address };

				}
				else
					Customer = null;
			}

			#endregion
		}
	}
}