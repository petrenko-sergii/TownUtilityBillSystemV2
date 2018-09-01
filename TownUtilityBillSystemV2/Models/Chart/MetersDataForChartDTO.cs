using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TownUtilityBillSystemV2.Models.Chart
{
	public class MetersDataForChartDTO
	{
		public List<List<ChartData>> MetersChartData;
		public List<string> UtilityResourceNames;
		public List<string> UnitNames;
		public List<float> UtilityCharges;

		public MetersDataForChartDTO()
		{
			MetersChartData = new List<List<ChartData>>();
			UtilityResourceNames = new List<string>();
			UnitNames = new List<string>();
			UtilityCharges = new List<float>();
		}
	}
}
