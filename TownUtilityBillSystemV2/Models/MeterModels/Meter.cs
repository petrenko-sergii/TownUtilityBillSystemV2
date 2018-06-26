using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using TownUtilityBillSystemV2.Resources;
using TownUtilityBillSystemV2.Models.AddressModels;
using TownUtilityBillSystemV2.Models.Chart;

namespace TownUtilityBillSystemV2.Models.MeterModels
{
	public class Meter
	{
		public int Id { get; set; }

		[Required(ErrorMessageResourceName = "FieldIsRequired", ErrorMessageResourceType = typeof(Localization))]
		[StringLength(20, ErrorMessageResourceName = "ValueMustBeAtLeastCharactersLong", ErrorMessageResourceType = typeof(Localization), MinimumLength = 6)]
		[Display(Name = "SerialNumber", ResourceType = typeof(Localization))]
		public string SerialNumber { get; set; }

		[Required(ErrorMessageResourceName = "FieldIsRequired", ErrorMessageResourceType = typeof(Localization))]
		[Display(Name = "ReleaseDate", ResourceType = typeof(Localization))]
		public DateTime ReleaseDate { get; set; }

		[Required(ErrorMessageResourceName = "FieldIsRequired", ErrorMessageResourceType = typeof(Localization))]
		[Display(Name = "VarificationDate", ResourceType = typeof(Localization))]
		public DateTime VarificationDate { get; set; }

		public float ConsumedMonthValue { get; set; }

		public Address Address { get; set; }

		public MeterType MeterType { get; set; }

		public List<MeterType> MeterTypes { get; set; }

		public List<MeterItemModel> MeterItemModels { get; set; }

		public List<ChartData> ChartData { get; set; }
	}
}