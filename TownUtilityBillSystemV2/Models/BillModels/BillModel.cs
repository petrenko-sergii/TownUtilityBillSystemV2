using System;
using System.Collections.Generic;
using System.Linq;
using TownUtilityBillSystemV2.Models.AccountModels;
using TownUtilityBillSystemV2.Models.AddressModels;
using TownUtilityBillSystemV2.Models.Chart;
using TownUtilityBillSystemV2.Models.Currency;
using TownUtilityBillSystemV2.Models.CustomerModels;
using TownUtilityBillSystemV2.Models.HelperMethods;
using TownUtilityBillSystemV2.Models.TemperatureModels;
using TownUtilityBillSystemV2.Models.UtilitySupplier;

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

		internal void GetBillsToShow()
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
				var billDB = context.BILLs.Where(b => b.NUMBER == billNumber).FirstOrDefault();

				if (billDB != null)
				{
					Bill = Bill.Get(billDB);

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

		internal void FindBills(string searchString)
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
