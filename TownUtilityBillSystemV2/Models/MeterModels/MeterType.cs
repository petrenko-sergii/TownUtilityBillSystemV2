using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TownUtilityBillSystemV2.Models.BaseModels;
using TownUtilityBillSystemV2.Models.UtilityModels;
using TownUtilityBillSystemV2.Resources;

namespace TownUtilityBillSystemV2.Models.MeterModels
{
	public class MeterType : ObjectWithName
	{
		[Required(ErrorMessageResourceName = "FieldIsRequired", ErrorMessageResourceType = typeof(Localization))]
		public new int Id { get; set; }

		[Required(ErrorMessageResourceName = "FieldIsRequired", ErrorMessageResourceType = typeof(Localization))]
		[Range(1, int.MaxValue, ErrorMessageResourceName = "ValueMustBeBiggerThan", ErrorMessageResourceType = typeof(Localization))]
		[Display(Name = "VarificationPeriod", ResourceType = typeof(Localization))]
		public int VarificationPeriod { get; set; }

		[Required(ErrorMessageResourceName = "FieldIsRequired", ErrorMessageResourceType = typeof(Localization))]
		public Utility Utility { get; set; }

		public List<Utility> Utilities { get; set; }

		public static MeterType Get(METER_TYPE meterType)
		{
			return new MeterType
			{
				Id = meterType.ID,
				Name = meterType.NAME,
				VarificationPeriod = meterType.VARIFICATION_PERIOD_YEARS
			};
		}
	}
}