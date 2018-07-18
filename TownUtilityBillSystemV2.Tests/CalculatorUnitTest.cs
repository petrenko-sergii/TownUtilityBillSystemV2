using System;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TownUtilityBillSystemV2.Controllers;
using TownUtilityBillSystemV2.Models.Calculator;

namespace TownUtilityBillSystemV2.Tests
{
	[TestClass]
	public class CalculatorUnitTest
	{
		[TestMethod]
		public void ShowCalculatorOnLineAction_Returns_View()
		{
			var model = new CalculatorItemModel();

			model.AddUtilitiesToCalculatorItemModel();

			Assert.IsTrue(model.Utilities.Count != 0);
		}
	}
}
