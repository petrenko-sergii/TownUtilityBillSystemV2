using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TownUtilityBillSystemV2.Resources;

namespace TownUtilityBillSystemV2.Models.AddressModels
{
	public class Address
	{
		private readonly string country = Localization.Denmark;

		#region Properties

		public int Id { get; set; }

		public string Country { get { return country; } }

		public Index Index { get; set; }

		public Town Town { get; set; }

		public Street Street { get; set; }

		public Building Building { get; set; }

		public FlatPart FlatPart { get; set; }

		#endregion

		#region Ctor

		public Address()
		{
			Index = new Index();
			Town = new Town();
			Street = new Street();
			Building = new Building();
			FlatPart = new FlatPart();
		}

		#endregion
	}
}