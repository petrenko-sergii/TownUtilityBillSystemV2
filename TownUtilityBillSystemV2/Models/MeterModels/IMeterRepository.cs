using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TownUtilityBillSystemV2.Models.MeterModels
{
	public interface IMeterRepository
	{
		IQueryable<METER> METERs { get; }
		METER Save(METER meter);
		void Delete(METER meter);
	}
}
