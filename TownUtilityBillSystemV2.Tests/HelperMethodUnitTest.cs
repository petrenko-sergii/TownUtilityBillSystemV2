using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TownUtilityBillSystemV2.Models.HelperMethods;

namespace TownUtilityBillSystemV2.Tests
{
	[TestClass]
	public class HelperMethodUnitTest
	{
		[TestMethod]
		public void CheckGetCurrentLanguage()
		{
			string currentCulture = HelperMethod.GetCurrentLanguage();

			Assert.IsFalse(String.IsNullOrEmpty(currentCulture));
		}

		[TestMethod]
		public void CheckGetLocalizatedWordsForSelectBoxesFindByAddress()
		{
			string[] result = HelperMethod.GetLocalizatedWordsForSelectBoxesFindByAddress();

			Assert.IsTrue(result != null && result.Length != 0);
		}

		[TestMethod]
		public void GetLocalizatedWordsTemperatureChartForFirstLoad()
		{
			string[] result = HelperMethod.GetLocalizatedWordsForSelectBoxesFindByAddress();

			Assert.IsInstanceOfType(result, typeof(string[]));
		}
	}
}
