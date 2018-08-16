using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TownUtilityBillSystemV2.Models.BillModels;

namespace TownUtilityBillSystemV2.Tests
{
	[TestClass]
	public class BillUnitTest
	{
		[TestMethod]
		public void CheckShowBills_Returns_ListOfBills()
		{
			var model = new BillModel();

			model.GetBillsToShow();

			Assert.IsInstanceOfType(model.Bills, typeof(List<Bill>));
		}

		[TestMethod]
		public void CheckShowFoundBills_WithCorrectCity()
		{
			var model = new BillModel();

			string searchString = "Kolding";

			model.FindBills(searchString);

			Assert.AreNotEqual(0,model.Bills.Count);
		}

		[TestMethod]
		public void CheckShowFoundBills_WithWrongCity()
		{
			var model = new BillModel();

			string searchString = "New York";

			model.FindBills(searchString);

			Assert.AreEqual(0, model.Bills.Count);
		}

		[TestMethod]
		public void CheckShowBills_Returns_SomeInstances()
		{
			var model = new BillModel();

			model.GetBillsToShow();

			Assert.AreNotEqual(0, model.Bills.Count);
		}

	}
}
