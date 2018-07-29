using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TownUtilityBillSystemV2.Models.AccountModels;
using TownUtilityBillSystemV2.Models.CustomerModels;
using TownUtilityBillSystemV2.Models.HelperMethods;

namespace TownUtilityBillSystemV2.Models.BillModels
{
	public class Bill
	{
		public int Id { get; set; }
		public string Number { get; set; }
		public DateTime Date { get; set; }
		public string Period { get; set; }
		public decimal Sum { get; set; }
		public bool Paid { get; set; }

		public Customer Customer { get; set; }
		public Account Account { get; set; }

		public static Bill Get(BILL bill)
		{
			return new Bill
			{
				Id = bill.ID,
				Number = bill.NUMBER,
				Date = bill.DATE,
				Period = HelperMethod.GetFullMonthName(bill.PERIOD),
				Sum = bill.SUM,
				Paid = bill.PAID
			};
		}
	}
}