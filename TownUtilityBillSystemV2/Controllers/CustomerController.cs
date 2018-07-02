using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TownUtilityBillSystemV2.Models.AccountModels;
using TownUtilityBillSystemV2.Models.CustomerModels;

namespace TownUtilityBillSystemV2.Controllers
{
    public class CustomerController : Controller
    {
		public ActionResult ShowCustomerMenu() => View();

		public ActionResult ShowPrivateCustomers()
		{
			var model = new CustomerModel();

			model.Customers = model.GetPrivateCutomers();

			return View(model);
		}

		public ActionResult ShowLegalCustomers()
		{
			var model = new CustomerModel();

			model.Customers = model.GetLegalCustomers();

			return View(model);
		}

		public ActionResult FindCustomerBy()
		{
			var model = new CustomerModel();

			model.GetSomeRandomCustomers();

			return View(model);
		}

		public ActionResult ShowAllCustomers()
		{
			var model = new CustomerModel();

			model.Customers = model.GetAllCustomers();

			return View(model);	
		}

		public ActionResult ShowFoundCustomers(string searchString)
		{
			var model = new CustomerModel();

			model.GetFoundCustomers(searchString);

			ViewBag.SearchString = searchString;

			return View(model);
			
		}
	}
}