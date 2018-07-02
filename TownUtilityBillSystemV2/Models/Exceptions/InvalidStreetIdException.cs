using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TownUtilityBillSystemV2.Resources;


namespace TownUtilityBillSystemV2.Models.Exceptions
{
	[Serializable]
	public class InvalidStreetIdException : Exception
	{
		public InvalidStreetIdException()
		{
		}

		public InvalidStreetIdException(int streetId)
			: base(String.Format(string.Format("{0} {1}", Localization.InvalidStreetIdParam, Localization.AdminWillBeNotified), streetId))
		{
		}
	}
}