using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TownUtilityBillSystemV2.Models.UtilityModels
{
	public interface IUtilityRepository
	{
		IQueryable<UTILITY> UTILITYs { get; }
		UTILITY Save(UTILITY utility);
		void Delete(UTILITY utility);
	}
}
