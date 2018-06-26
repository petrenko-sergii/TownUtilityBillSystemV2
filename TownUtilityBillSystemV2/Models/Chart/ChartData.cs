using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TownUtilityBillSystemV2.Models.Chart
{
	public class ChartData
	{
		public int Id { get; set; }
		public float Value { get; set; }
		public string MonthName { get; set; }
		public float UtilityCharges { get; set; }

		public float ElectricValue { get; set; }
		public float WaterValue { get; set; }
		public float HeatValue { get; set; }
		public float GasValue { get; set; }
	}
}
