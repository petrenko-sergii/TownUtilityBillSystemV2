using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TownUtilityBillSystemV2.Models.PaymentModels
{
	public class Payment
	{
		public int Id { get; set; }
		public string Number { get; set; }
		public decimal Sum { get; set; }
		public DateTime Date { get; set; }
		public string Note { get; set; }
		public int AccountId { get; set; }
	}
}