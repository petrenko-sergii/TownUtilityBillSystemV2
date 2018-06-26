using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TownUtilityBillSystemV2.Models.InitialDB;
using TownUtilityBillSystemV2.Models.UtilityModels;

namespace TownUtilityBillSystemV2.Tests
{
	[TestClass]
	public class UtilityUnitTest
	{
		[TestMethod]
		public void CheckGetDataForUtilityMethod()
		{
			var utilityModel = new UtilityModel();
			string inputName = "Water";
			string correctUtilityName = InitialDBEnums.Utilities.Water.ToString();

			utilityModel.GetDataForUtility(inputName);

			Assert.AreEqual(correctUtilityName, utilityModel.Utility.ResourceName);
		}

		[TestMethod]
		public void CheckGetAllUtilitiesPricesMethodStandartPriceUsage()
		{
			var utilityModel = new UtilityModel();

			utilityModel.GetAllUtilitiesPrices();

			Assert.AreNotEqual(0, utilityModel.Utilities[0].UsageForStandartPrice);
		}

		[TestMethod]
		public void CheckGetAllUtilitiesPricesMethodNoStandartPriceUsage()
		{
			var utilityModel = new UtilityModel();

			utilityModel.GetAllUtilitiesPrices();

			Assert.AreEqual(0, utilityModel.Utilities[3].UsageForStandartPrice);
		}

		[TestMethod]
		public void CheckGetAllUtilitiesPricesMethodForName()
		{
			var utilityModel = new UtilityModel();
			var comparedUtility = new UtilityModel();

			utilityModel.GetAllUtilitiesPrices();
			comparedUtility.Utility.Name = "Gas";

			Assert.AreEqual(comparedUtility.Utility.Name, utilityModel.Utilities[3].Name);
		}

		[TestMethod]
		public void CheckGetUtilitiesNameMethod()
		{
			var utilityModel = new UtilityModel();

			utilityModel.GetUtilitiesName();

			Assert.IsTrue(utilityModel.Utilities.Count > 0);
		}
	}
}
