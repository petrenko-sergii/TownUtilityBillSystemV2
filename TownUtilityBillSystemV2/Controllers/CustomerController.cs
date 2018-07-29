using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TownUtilityBillSystemV2.Models.AccountModels;
using TownUtilityBillSystemV2.Models.AddressModels;
using TownUtilityBillSystemV2.Models.CustomerModels;
using TownUtilityBillSystemV2.Models.MeterModels;
using TownUtilityBillSystemV2.Models.UtilityModels;
using TownUtilityBillSystemV2.Models.Customized;

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

		public ActionResult EditCustomer(int customerId)
		{
			var model = new CustomerModel();

			Customer customerToEdit = model.GetCustomerForEdit(customerId);

			return View(customerToEdit);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult EditCustomer(Customer customer)
		{
			var model = new CustomerModel();

			if (ModelState.IsValid)
			{
				model.UpdateCustomer(customer);

				string methodToInvoke = model.IsCustomerAPrivateType(customer.CustomerType.Id) ? "ShowPrivateCustomers" : "ShowLegalCustomers";

				return RedirectToAction(methodToInvoke, "Customer");
			}

			customer.CustomerTypes = model.GetCustomerTypes();

			return View();
		}

		public ActionResult ShowDetailsForCustomer(int customerId)
		{
			var model = new CustomerModel();

			model.GetMeterDetailsForCustomer(customerId);
			ViewBag.currentAddressForJS = model.Customer.Address;

			return View(model);
		}

		public ActionResult CreateCustomer()
		{
			//TO DO
			ViewBag.Message = CustomizedMessages.NoAdministratorRightsMessage;

			return View();
		}

		public ActionResult DeleteCustomer(int customerId)
		{
			//TO DO
			ViewBag.Message = CustomizedMessages.NoAdministratorRightsMessage;

			return View();
		}
	}
}
