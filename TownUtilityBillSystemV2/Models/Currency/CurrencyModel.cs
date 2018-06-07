using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TownUtilityBillSystemV2.Models.Currency
{
	public class CurrencyModel
	{
		private CurrencyMoney currency;

		public CurrencyMoney Currency
		{
			get
			{
				return currency;
			}
		}

		public CurrencyModel()
		{
			currency = CurrencyMoney._Currency;
		}
	}
}
