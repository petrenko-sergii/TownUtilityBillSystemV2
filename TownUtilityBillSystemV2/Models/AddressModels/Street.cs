using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TownUtilityBillSystemV2.Models.BaseModels;

namespace TownUtilityBillSystemV2.Models.AddressModels
{
	public class Street : ObjectWithName
	{
		public Town Town { get; set; }
		public Index Index { get; set; }

		public static Street Get(STREET street)
		{
			return new Street
			{
				Id = street.ID,
				Name = street.NAME
			};
		}
	}
}
