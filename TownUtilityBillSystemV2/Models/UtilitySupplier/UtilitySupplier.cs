using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TownUtilityBillSystemV2.Models.HelperMethods;

namespace TownUtilityBillSystemV2.Models.UtilitySupplier
{
	public class UtilitySupplier
	{
		private static UtilitySupplier instance;

		private string name = "DONG Energy A/S";
		private string phone = "+45 99 55 11 11 ";
		private string fax = "+45 99 55 00 11";
		private string email = "info@dongenergy.com";
		private string address = "Kraftvaerksvej 53, Skaerbaek, DK - 7000 Fredericia";
		private string countryEn = "Denmark";
		private string countryDa = "Danmark";
		private string site = "www.dongenergy.com";
		private string bank = "Danske Bank";
		private string accountDetails = "1569 9795 3619 52";
		private string imageIconPath = "/Content/Images/DongEnergy/DONGenergy.jpg";

		public string Name { get { return name; } }
		public string Phone { get { return phone; } }
		public string Fax { get { return fax; } }
		public string Email { get { return email; } }
		public string Site { get { return site; } }
		public string Bank { get { return bank; } }
		public string AccountDetails { get { return accountDetails; } }
		public string ImageIconPath { get { return imageIconPath; } }
		public string Address
		{
			get
			{
				if (HelperMethod.GetCurrentLanguage() == "en")
					return String.Format("{0}, {1}", address, countryEn);

				return String.Format("{0}, {1}", address, countryDa); ;
			}
		}

		private UtilitySupplier() { }

		public static UtilitySupplier Instance
		{
			get
			{
				if (instance == null)
					instance = new UtilitySupplier();
				return instance;
			}
		}
	}
}