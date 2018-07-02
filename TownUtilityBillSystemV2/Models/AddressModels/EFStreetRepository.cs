using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace TownUtilityBillSystemV2.Models.AddressModels
{
	public class EFStreetRepository : IStreetRepository
	{
		TownUtilityBillSystemV2Entities context = new TownUtilityBillSystemV2Entities();

		public IQueryable<STREET> STREETs
		{ get { return context.STREETs; } }

		public STREET Save(STREET street)
		{
			if (street.ID == 0)
				context.STREETs.Add(street);
			else
				context.Entry(street).State = EntityState.Modified;
			context.SaveChanges();

			return street;
		}

		public void Delete(STREET street)
		{
			context.STREETs.Remove(street);

			context.SaveChanges();
		}
	}
}
