using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TownUtilityBillSystemV2.Resources;

namespace TownUtilityBillSystemV2.Models.BaseModels
{
	public abstract class ObjectWithName
	{
		public int Id { get; set; }

		[Display(Name = "Name", ResourceType = typeof(Localization))]
		public string Name { get; set; }
	}
}