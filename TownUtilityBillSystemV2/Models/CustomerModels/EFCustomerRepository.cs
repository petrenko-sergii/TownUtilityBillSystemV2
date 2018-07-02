using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TownUtilityBillSystemV2.Models.CustomerModels
{
	public class EFCustomerRepository : ICustomerRepository
	{
		public IQueryable<Customer> Customers => throw new NotImplementedException();

		public void Delete(Customer customer)
		{
			throw new NotImplementedException();
		}

		public Customer Save(Customer customer)
		{
			throw new NotImplementedException();
		}
	}
}