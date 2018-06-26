using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TownUtilityBillSystemV2.Models.BaseModels;

namespace TownUtilityBillSystemV2.Models.AddressModels
{
	public class Town : ObjectWithName
	{
		public static Town Get(TOWN town)
		{
			return new Town
			{
				Id = town.ID,
				Name = town.NAME
			};
		}
	}
}