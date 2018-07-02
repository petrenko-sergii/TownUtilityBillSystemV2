using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TownUtilityBillSystemV2.Models.CustomerModels
{
	public interface ICustomerRepository
	{
		IQueryable<Customer> Customers { get; }
		Customer Save(Customer customer);
		void Delete(Customer customer);
	}
}
