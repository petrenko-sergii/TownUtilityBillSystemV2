using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TownUtilityBillSystemV2.Models.MeterModels
{
	public class MeterItemModel
	{
		#region Properties

		public List<MeterItem> MeterItems;
		public Meter Meter;
		public MeterItem MeterItem;

		public string Period { get; set; }
		public float Value { get; set; }

		#endregion

		#region Ctor

		public MeterItemModel()
		{
			MeterItems = new List<MeterItem>();
			Meter = new Meter();
			MeterItem = new MeterItem();
			Period = "";
			Value = 0;
		}

		#endregion
	}
}