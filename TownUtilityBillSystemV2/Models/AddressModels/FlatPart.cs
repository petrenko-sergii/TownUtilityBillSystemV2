using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TownUtilityBillSystemV2.Models.BaseModels;

namespace TownUtilityBillSystemV2.Models.AddressModels
{
	public class FlatPart : ObjectWithName
	{
		public int Number { get; set; }
		public float Square { get; set; }
		public Building Building { get; set; }

		public static FlatPart Get(FLAT_PART flatPart)
		{
			FlatPart viewFlatPart = new FlatPart();

			if (!String.IsNullOrEmpty(flatPart.NAME) && !flatPart.NUMBER.HasValue)
			{
				viewFlatPart.Id = flatPart.ID;
				viewFlatPart.Name = flatPart.NAME;
			}
			else if (String.IsNullOrEmpty(flatPart.NAME) && flatPart.NUMBER.HasValue)
			{
				viewFlatPart.Id = flatPart.ID;
				viewFlatPart.Number = (int)flatPart.NUMBER;
			}
			else if (!String.IsNullOrEmpty(flatPart.NAME) && flatPart.NUMBER.HasValue) {
				viewFlatPart.Id = flatPart.ID;
				viewFlatPart.Number = (int)flatPart.NUMBER;
				viewFlatPart.Name = flatPart.NAME;
			}

			return viewFlatPart;
		}
	}
}