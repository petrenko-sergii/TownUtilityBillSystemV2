using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TownUtilityBillSystemV2.Models.Exceptions;
using TownUtilityBillSystemV2.Models.MeterModels;

namespace TownUtilityBillSystemV2.Tests
{
	[TestClass]
	public class MeterUnitTest
	{
		[TestMethod]
		[ExpectedException(typeof(InvalidBuildingIdException))]
		public void CheckGetMetersForBuildingWithWrongBuildingId()
		{
			int wrongBuildingid = -1;
			var meterModel = new MeterModel();

			meterModel.GetMetersForBuilding(wrongBuildingid);
		}

		[TestMethod]
		public void CheckGetMetersForBuildingWithCorrectBuildingId_typeTest()
		{
			int correctBuildingid = 100;
			var meterModel = new MeterModel();

			meterModel.GetMetersForBuilding(correctBuildingid);

			Assert.IsInstanceOfType(meterModel.Meters, typeof(List<Meter>));
		}

		[TestMethod]
		public void CheckGetMetersForBuildingWithCorrectBuildingId_nullTest()
		{
			int correctBuildingid = 50;
			var meterModel = new MeterModel();

			meterModel.GetMetersForBuilding(correctBuildingid);

			Assert.IsNotNull(meterModel.Meters);
		}

		
	}
}
