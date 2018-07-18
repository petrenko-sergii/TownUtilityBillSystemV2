using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TownUtilityBillSystemV2.Models.BaseModels;

namespace TownUtilityBillSystemV2.Models.UtilityModels
{
	public class Unit : ObjectWithName
	{
		public static Unit Get(UNIT unit)
		{
			return new Unit
			{
				Id = unit.ID,
				Name = unit.NAME
			};
		}
	}
}