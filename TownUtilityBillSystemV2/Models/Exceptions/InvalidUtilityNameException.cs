using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TownUtilityBillSystemV2.Resources;

namespace TownUtilityBillSystemV2.Models.Exceptions
{
	public class InvalidUtilityNameException : Exception
	{
		public InvalidUtilityNameException(string utilityName)
			: base(String.Format(string.Format("{0} {1}", Localization.InvalidUtilityNameParam, Localization.AdminWillBeNotified), utilityName))
		{
		}
	}
}