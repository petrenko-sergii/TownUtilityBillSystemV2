using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TownUtilityBillSystemV2.Models.Currency
{
	public class CurrencyMoney
	{
		public string Name { get; } = "DKK";

		public override string ToString()
		{
			return Name;
		}

		private static CurrencyMoney currency;

		public static CurrencyMoney _Currency
		{
			get
			{
				if (currency == null)
					currency = new CurrencyMoney();
				return currency;
			}
		}
	}
}