using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TownUtilityBillSystemV2.Models.CustomerModels;

namespace TownUtilityBillSystemV2.Tests
{
	[TestClass]
	public class CustomerUnitTest
	{
		[TestMethod]
		public void CheckGetSomeRandomCustomers()
		{
			var cusomerModel = new CustomerModel();

			cusomerModel.GetSomeRandomCustomers();

			Assert.IsInstanceOfType(cusomerModel.Customers, typeof(List<Customer>));
		}

		[TestMethod]
		public void CheckGetSomeRandomCustomersReturnsNotEmptyList()
		{
			var cusomerModel = new CustomerModel();

			cusomerModel.GetSomeRandomCustomers();

			Assert.IsTrue(cusomerModel.Customers.Count != 0);
		}

		[TestMethod]
		public void CheckGetFoundCustomers_returns_FoundItems()
		{
			var cusomerModel = new CustomerModel();
			string searchString = "Kolding";

			cusomerModel.GetFoundCustomers(searchString);

			Assert.IsTrue(cusomerModel.Customers.Count != 0);
		}

		[TestMethod]
		public void CheckGetFoundCustomers_returns_NoFoundItems()
		{
			var cusomerModel = new CustomerModel();
			string searchString = "WrongTown";

			cusomerModel.GetFoundCustomers(searchString);

			Assert.IsTrue(cusomerModel.Customers.Count == 0);
		}


	}
}
