using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace TownUtilityBillSystemV2.Models.UtilityModels
{
	public class EFUtilityRepository : IUtilityRepository
	{
		TownUtilityBillSystemV2Entities context = new TownUtilityBillSystemV2Entities();

		public IQueryable<UTILITY> UTILITYs
		{ get { return context.UTILITYs; } }

		public UTILITY Save(UTILITY utility)
		{
			if (utility.ID == 0)
				context.UTILITYs.Add(utility);
			else
				context.Entry(utility).State = EntityState.Modified;
			context.SaveChanges();

			return utility;
		}

		public void Delete(UTILITY utility)
		{
			context.UTILITYs.Remove(utility);
			context.SaveChanges();
		}
	}
}
