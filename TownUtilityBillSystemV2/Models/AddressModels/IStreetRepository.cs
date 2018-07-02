using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TownUtilityBillSystemV2.Models.AddressModels
{
	public interface IStreetRepository
	{
		IQueryable<STREET> STREETs { get; }
		STREET Save(STREET street);
		void Delete(STREET street);
	}
}
