using System;
using System.Linq;
using System.Web.Mvc;
using Moq;
using NUnit.Framework;
using TownUtilityBillSystemV2.Controllers;
using TownUtilityBillSystemV2.Models.MeterModels;
using TownUtilityBillSystemV2.Models.AddressModels;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace TownUtilityBillSystemV2.Tests
{
	[TestFixture]
	public class MeterControllerUnitTest
	{
		[Test]
		public void Meter_ShowAllMeters_View_Contains_ListOfMETERs_Model()
		{
			Mock<IMeterRepository> mock = new Mock<IMeterRepository>();
			Mock<IStreetRepository> streetMock = new Mock<IStreetRepository>();
			Mock<IBuildingRepository> buildingMock = new Mock<IBuildingRepository>();

			mock.Setup(m => m.METERs).Returns(new METER[]
			{
				new METER { ID = 1, SERIAL_NUMBER = "YT000001", RELEASE_DATE = Convert.ToDateTime("2012-03-14"), VARIFICATION_DATE = Convert.ToDateTime("2016-03-19")},
				new METER { ID = 2, SERIAL_NUMBER = "YT000002", RELEASE_DATE = Convert.ToDateTime("2012-03-15"), VARIFICATION_DATE = Convert.ToDateTime("2016-03-20")},
				new METER { ID = 3, SERIAL_NUMBER = "YT000003", RELEASE_DATE = Convert.ToDateTime("2012-03-16"), VARIFICATION_DATE = Convert.ToDateTime("2016-03-21")}

			}.AsQueryable());

			MeterController controller = new MeterController(mock.Object, streetMock.Object, buildingMock.Object);

			var actual = (List<METER>)controller.ShowAllMeters().Model;

			Assert.IsInstanceOf<List<METER>>(actual);
		}

		[Test]
		public void Meter_ShowAllMeters_View_Contains_4_METRERs()
		{
			Mock<IMeterRepository> mock = new Mock<IMeterRepository>();
			Mock<IStreetRepository> streetMock = new Mock<IStreetRepository>();
			Mock<IBuildingRepository> buildingMock = new Mock<IBuildingRepository>();

			mock.Setup(m => m.METERs).Returns(new METER[]
			{
				new METER { ID = 1, SERIAL_NUMBER = "YT000001", RELEASE_DATE = Convert.ToDateTime("2012-03-14"), VARIFICATION_DATE = Convert.ToDateTime("2016-03-19")},
				new METER { ID = 2, SERIAL_NUMBER = "YT000002", RELEASE_DATE = Convert.ToDateTime("2012-03-15"), VARIFICATION_DATE = Convert.ToDateTime("2016-03-20")},
				new METER { ID = 3, SERIAL_NUMBER = "YT000003", RELEASE_DATE = Convert.ToDateTime("2012-03-16"), VARIFICATION_DATE = Convert.ToDateTime("2016-03-21")},
				new METER { ID = 4, SERIAL_NUMBER = "YT000004", RELEASE_DATE = Convert.ToDateTime("2012-03-17"), VARIFICATION_DATE = Convert.ToDateTime("2016-03-22")}
			}.AsQueryable());

			MeterController controller = new MeterController(mock.Object, streetMock.Object, buildingMock.Object);

			var actual = (List<METER>)controller.ShowAllMeters().Model;

			Assert.AreEqual(4,actual.Count);
		}


		[Test]
		public void GetStreetList_Returns_ProperValues()
		{
			Mock<IMeterRepository> mock = new Mock<IMeterRepository>();
			Mock<IStreetRepository> streetMock = new Mock<IStreetRepository>();
			Mock<IBuildingRepository> buildingMock = new Mock<IBuildingRepository>();

			streetMock.Setup(s => s.STREETs).Returns( new STREET[]
			{
				new STREET{ID = 1, NAME = "LedburyVej", TOWN_ID = 2, INDEX_ID = 8},
				new STREET{ID = 2, NAME = "AastorpVej", TOWN_ID = 3, INDEX_ID = 12},
				new STREET{ID = 3, NAME = "VejleVej", TOWN_ID = 3, INDEX_ID = 12},
				new STREET{ID = 4, NAME = "BoveVej", TOWN_ID = 2, INDEX_ID = 9}
			}.AsQueryable());

			MeterController controller = new MeterController(mock.Object, streetMock.Object, buildingMock.Object);

			JsonResult result = controller.GetStreetList(3) as JsonResult;
			JavaScriptSerializer serializer = new JavaScriptSerializer();

			string actual = serializer.Serialize(result.Data);

			Assert.AreEqual(@"[{""Id"":2,""Name"":""AastorpVej""},{""Id"":3,""Name"":""VejleVej""}]", actual);
		}

		[Test]
		public void GetBuildingList_Returns_ProperValues()
		{
			Mock<IMeterRepository> mock = new Mock<IMeterRepository>();
			Mock<IStreetRepository> streetMock = new Mock<IStreetRepository>();
			Mock<IBuildingRepository> buildingMock = new Mock<IBuildingRepository>();

			buildingMock.Setup(b => b.BUILDINGSs).Returns(new BUILDING[]
			{
				new BUILDING{ID = 1, NUMBER = "1", STREET_ID = 3},
				new BUILDING{ID = 2, NUMBER = "2", STREET_ID = 5},
				new BUILDING{ID = 3, NUMBER = "3", STREET_ID = 3},
				new BUILDING{ID = 4, NUMBER = "4", STREET_ID = 3},
				new BUILDING{ID = 5, NUMBER = "5", STREET_ID = 5}
			}.AsQueryable());

			MeterController controller = new MeterController(mock.Object, streetMock.Object, buildingMock.Object);

			JsonResult result = controller.GetBuildingList(3) as JsonResult;
			JavaScriptSerializer serializer = new JavaScriptSerializer();

			string actual = serializer.Serialize(result.Data);

			Assert.AreEqual(@"[{""Id"":1,""Number"":""1""},{""Id"":3,""Number"":""3""},{""Id"":4,""Number"":""4""}]", actual);
		}
	}
}
