using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TownUtilityBillSystemV2.Models.PaymentCardModels;

namespace TownUtilityBillSystemV2.Tests
{
	[TestClass]
	public class PaymentCardUnitTest
	{
		[TestMethod]
		public void Check_PaymentCardModel_For_PaymentCardTypes()
		{
			PaymentCardModel model = new PaymentCardModel();

			Assert.AreNotEqual(0, model.PaymentCardTypes.Count);
		}

		[TestMethod]
		public void Check_PaymentCardModel_For_CurrencyInstance()
		{
			PaymentCardModel model = new PaymentCardModel();

			Assert.IsNotNull(model.Currency);
		}

	}
}
