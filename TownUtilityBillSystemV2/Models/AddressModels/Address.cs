using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TownUtilityBillSystemV2.Models.AddressModels
{
	public class Address
	{
		#region Properties

		public int Id { get; set; }

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