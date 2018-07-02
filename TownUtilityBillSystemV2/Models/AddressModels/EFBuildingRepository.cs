using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace TownUtilityBillSystemV2.Models.AddressModels
{
	public class EFBuildingRepository : IBuildingRepository
	{
		TownUtilityBillSystemV2Entities context = new TownUtilityBillSystemV2Entities();

		public IQueryable<BUILDING> BUILDINGSs
		{ get { return context.BUILDINGs; } }

		public BUILDING Save(BUILDING building)
		{
			if (building.ID == 0)
				context.BUILDINGs.Add(building);
			else
				context.Entry(building).State = EntityState.Modified;
			context.SaveChanges();

			return building;
		}

		public void Delete(BUILDING building)
		{
			context.BUILDINGs.Remove(building);

			context.SaveChanges();
		}
	}
}