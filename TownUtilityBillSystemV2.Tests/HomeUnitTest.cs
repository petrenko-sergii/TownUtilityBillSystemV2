using System;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TownUtilityBillSystemV2.Controllers;

namespace TownUtilityBillSystemV2.Tests
{
	[TestClass]
	public class HomeUnitTest
	{
		[TestMethod]
		public void AboutActionReturnsAboutView()
		{
			HomeController homeController = new HomeController();
			ViewResult result = homeController.About() as ViewResult;

			Assert.AreEqual("About", result.ViewBag.Title);
		}

		[TestMethod]
		public void ContactActionReturnsContactView()
		{
			HomeController homeController = new HomeController();
			ViewResult result = homeController.Contact() as ViewResult;

			Assert.AreEqual("Contact", result.ViewBag.Title);
		}
	}
}
