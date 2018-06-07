using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TownUtilityBillSystemV2.Models.UtilitySupplier
{
	public class UtilitySupplierModel
	{
		private UtilitySupplier utilitySupplier;

		public UtilitySupplier UtilitySupplier
		{
			get
			{
				return utilitySupplier;
			}
		}


		public UtilitySupplierModel()
		{
			utilitySupplier = UtilitySupplier.Instance;
		}
	}
}
