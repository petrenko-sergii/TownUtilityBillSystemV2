using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TownUtilityBillSystemV2.Resources;

namespace TownUtilityBillSystemV2.Models.Exceptions
{
	[Serializable]
	public class InvalidTownIdException : Exception
	{
		public InvalidTownIdException(int townId)
			: base(String.Format(string.Format("{0} {1}", Localization.InvalidTownIdParam, Localization.AdminWillBeNotified), townId))
		{
		}
	}
}