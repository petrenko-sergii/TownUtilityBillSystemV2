using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace TownUtilityBillSystemV2.Models.MeterModels
{
	public class EFMeterRepository : IMeterRepository
	{
		TownUtilityBillSystemV2Entities context = new TownUtilityBillSystemV2Entities();

		public IQueryable<METER> METERs
		{ get { return context.METERs; } }

		public METER Save(METER meter)
		{
			if (meter.ID == 0)
				context.METERs.Add(meter);
			else
				context.Entry(meter).State = EntityState.Modified;
			context.SaveChanges();

			return meter;
		}

		public void Delete(METER meter)
		{
			context.METERs.Remove(meter);
			context.SaveChanges();
		}
	}
}
