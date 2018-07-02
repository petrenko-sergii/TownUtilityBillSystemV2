using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TownUtilityBillSystemV2.Models.AccountModels;
using TownUtilityBillSystemV2.Models.AddressModels;
using TownUtilityBillSystemV2.Models.Currency;
using TownUtilityBillSystemV2.Models.MeterModels;

namespace TownUtilityBillSystemV2.Models.CustomerModels
{
	public class CustomerModel
	{
		private readonly int customerCountToDisplay = 25;

		public List<Customer> Customers;
		public List<CustomerType> CustomerTypes;
		public List<Meter> Meters;
		public Customer Customer;
		public int TotalCount { get; set; }
		public AddressModel AddressModel;
		public CurrencyMoney Currency;

		public int CustomerCountToDisplay { get { return customerCountToDisplay; } }


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
	}
}