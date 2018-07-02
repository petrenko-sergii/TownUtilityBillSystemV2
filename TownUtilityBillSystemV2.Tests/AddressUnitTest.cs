using System;
using TownUtilityBillSystemV2.Models.AddressModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;
using TownUtilityBillSystemV2.Models.Exceptions;

namespace TownUtilityBillSystemV2.Tests
{
	[TestClass]
	public class AddressUnitTest
	{
		[TestMethod]
		public void CheckGetTownName()
		{
			int koldingTownId = 3;
			var addressModel = new AddressModel();

			Assert.AreEqual("Kolding", addressModel.GetTownName(koldingTownId));
		}

		[TestMethod]
		public void CheckGetBuildingNoImage()
		{
			int wrongBuildingId = -1;
			var addressModel = new AddressModel();
			string actualResult = addressModel.GetBuildingImage(wrongBuildingId);

			Assert.IsTrue(actualResult.Contains("NoImageBuilding"));
		}

		[TestMethod]
		public void CheckGetBuildingImage()
		{
			int correctBuildingId = 100;
			var addressModel = new AddressModel();
			string actualResult = addressModel.GetBuildingImage(correctBuildingId);

			Assert.IsFalse(actualResult.Contains("NoImageBuilding"));
		}

		[TestMethod]
		[ExpectedException(typeof(InvalidStreetIdException))]
		public void CheckGetStreetNameWithWrongId()
		{
			int wrongStreetId = -1;
			var addressModel = new AddressModel();

			addressModel.GetStreetName(wrongStreetId);
		}

		[TestMethod]
		public void CheckGetStreetNameWithCorrectId()
		{
			int correctStreetId = 5;
			var addressModel = new AddressModel();

			string streetName = addressModel.GetStreetName(correctStreetId);

			Assert.IsNotNull(streetName);
			Assert.AreNotEqual("", streetName);
		}

		[TestMethod]
		[ExpectedException(typeof(InvalidTownIdException))]
		public void CheckGetTownNameWithWrongTownId()
		{
			int wrongTownId = -1;
			var addressModel = new AddressModel();
			addressModel.GetTownName(wrongTownId);
		}
	}
}
