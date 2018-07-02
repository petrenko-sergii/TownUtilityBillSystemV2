using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TownUtilityBillSystemV2.Models.AddressModels
{
	public interface IBuildingRepository
	{
		IQueryable<BUILDING> BUILDINGSs { get; }
		BUILDING Save(BUILDING building);
		void Delete(BUILDING building);
	}
}
