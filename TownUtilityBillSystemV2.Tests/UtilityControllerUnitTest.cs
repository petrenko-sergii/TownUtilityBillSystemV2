using System;
using System.Linq;
using System.Web.Mvc;
using Moq;
using NUnit.Framework;
using TownUtilityBillSystemV2.Controllers;
using TownUtilityBillSystemV2.Models.UtilityModels;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace TownUtilityBillSystemV2.Tests
{
	[TestFixture]
	public class UtilityControllerUnitTest
	{
		[Test]
		public void Utility_ShowUtilities_View_Contains_3_Utilities()
		{
			Mock<IUtilityRepository> mock = new Mock<IUtilityRepository>();

			mock.Setup(m => m.UTILITYs).Returns(new UTILITY[]
			{
				new UTILITY { ID = 1, NAME = "Electricity"},
				new UTILITY { ID = 2, NAME = "Water"},
				new UTILITY { ID = 3, NAME = "Gas"}

			}.AsQueryable());

			UtilityController controller = new UtilityController(mock.Object);

			UtilityModel actual = (UtilityModel)controller.ShowUtilities().Model;

			Assert.AreEqual(3, actual.Utilities.Count);
		}

		[Test]
		public void ShowAllUtilitiesPrices_Returns_ActionResult()
		{
			Mock<IUtilityRepository> mock = new Mock<IUtilityRepository>();

			UtilityController controller = new UtilityController(mock.Object);

			var actual = controller.ShowAllUtilitiesPrices();

			Assert.IsInstanceOf<ActionResult>(actual);
		}

		[Test]
		public void Utility_ShowUtilities_View_Contains_UtilityModel()
		{
			Mock<IUtilityRepository> mock = new Mock<IUtilityRepository>();

			UtilityController controller = new UtilityController(mock.Object);

			var actual = (UtilityModel)controller.ShowUtilities().Model;

			Assert.IsInstanceOf<UtilityModel>(actual);
		}

		[Test]
		public void Utility_ShowUtilities_View_Does_Not_Contain_4_Utilities()
		{
			Mock<IUtilityRepository> mock = new Mock<IUtilityRepository>();

			mock.Setup(m => m.UTILITYs).Returns(new UTILITY[]
			{
				new UTILITY { ID = 1, NAME = "Electricity"},
				new UTILITY { ID = 2, NAME = "Water"}

			}.AsQueryable());

			UtilityController controller = new UtilityController(mock.Object);

			UtilityModel actual = (UtilityModel)controller.ShowUtilities().Model;

			Assert.AreNotEqual(4, actual.Utilities.Count);
		}
	}
}
