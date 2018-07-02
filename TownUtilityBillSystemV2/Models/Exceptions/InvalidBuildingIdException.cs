using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TownUtilityBillSystemV2.Resources;

namespace TownUtilityBillSystemV2.Models.Exceptions
{
	[Serializable]
	public class InvalidBuildingIdException : Exception
	{
		public InvalidBuildingIdException()
		{
		}

		public InvalidBuildingIdException(int buildingId)
			: base(String.Format(string.Format("{0} {1}", Localization.InvalidBuildingIdParam, Localization.AdminWillBeNotified), buildingId))
		{
		}

	}
}