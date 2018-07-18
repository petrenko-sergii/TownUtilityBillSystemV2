using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TownUtilityBillSystemV2.Resources;

namespace TownUtilityBillSystemV2.Models.Exceptions
{
	[Serializable]
	public class InvalidMeterIdException : Exception
	{
		public InvalidMeterIdException()
		{
		}

		public InvalidMeterIdException(int meterId)
			: base(String.Format(string.Format("{0} {1}", Localization.InvalidMeterIdParam, Localization.AdminWillBeNotified), meterId))
		{
		}
	}
}