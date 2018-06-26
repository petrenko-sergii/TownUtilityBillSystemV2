using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using TownUtilityBillSystemV2.Resources;

namespace TownUtilityBillSystemV2.Models.MeterModels
{
	public class MeterItem
	{
		public int Id { get; set; }
		public Meter Meter { get; set; }
		public DateTime Date { get; set; }

		[Required(ErrorMessageResourceName = "FieldIsRequired", ErrorMessageResourceType = typeof(Localization))]
		[Range(0.1, int.MaxValue, ErrorMessageResourceName = "ValueMustBeBiggerThanZero", ErrorMessageResourceType = typeof(Localization))]
		[Display(Name = "NewValue", ResourceType = typeof(Localization))]
		public float Value { get; set; }
	}
}