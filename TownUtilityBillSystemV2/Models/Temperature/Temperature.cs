using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TownUtilityBillSystemV2.Models
{
	public class Temperature
	{
		public int Id { get; set; }
		public DateTime Date { get; set; }
		public int MinValue { get; set; }
		public int MaxValue { get; set; }
		public int TownId { get; set; }
	}
}