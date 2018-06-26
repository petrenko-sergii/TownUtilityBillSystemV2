using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TownUtilityBillSystemV2.Models.BaseModels;

namespace TownUtilityBillSystemV2.Models.AddressModels
{
	public class Building : ObjectWithImage
	{
		public string Number { get; set; }
		public float Square { get; set; }
		public Street Street { get; set; }

		public static Building Get(BUILDING building)
		{
			return new Building
			{
				Id = building.ID,
				Number = building.NUMBER
			};
		}
	}
}