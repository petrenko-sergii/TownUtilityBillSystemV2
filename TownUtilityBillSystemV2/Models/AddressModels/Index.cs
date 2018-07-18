using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TownUtilityBillSystemV2.Models.AddressModels
{
	public class Index
	{
		public int Id { get; set; }
		public int Value { get; set; }

		public static Index Get (INDEX index)
		{
			return new Index
			{
				Id = index.ID,
				Value = index.VALUE
			};
		}
	}
}