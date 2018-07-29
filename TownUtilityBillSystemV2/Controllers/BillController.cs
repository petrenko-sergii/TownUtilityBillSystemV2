﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using TownUtilityBillSystemV2.Models.AddressModels;
using TownUtilityBillSystemV2.Models.BillModels;
using TownUtilityBillSystemV2.Models.CustomerModels;
using TownUtilityBillSystemV2.Models.Customized;
using TownUtilityBillSystemV2.Models.HelperMethods;
using TownUtilityBillSystemV2.Models.PaymentCardModels;
using TownUtilityBillSystemV2.Models.PaymentModels;
using TownUtilityBillSystemV2.Resources;

namespace TownUtilityBillSystemV2.Controllers
{
    public class BillController : Controller
    {
		TownUtilityBillSystemV2Entities contextDB = new TownUtilityBillSystemV2Entities();

		public ActionResult ShowBills()
		{
			var model = new BillModel();

			model.GetBillsToShow();

			return View(model);
		}

		public ActionResult ShowFoundBills(string searchString)
		{
			var model = new BillModel();

			model.FindBills(searchString);

			ViewBag.SearchString = searchString;

			return View(model);
		}

		public ActionResult ShowAllBillsForCustomer(int customerId)
		{
			var model = new BillModel();

			model.GetAllBillsForCustomer(customerId);

			return View(model);

			//using (var context = new TownUtilityBillSystemV2Entities())
			//{
			//	var model = new BillModel();
			//	//var billsDB = context.BILLs.Where(b => b.CUSTOMER_ID == customerId).ToList();
			//	var customerDB = context.CUSTOMERs.Find(customerId);
			//	var accountDB = context.ACCOUNTs.Where(a => a.ID == customerDB.ACCOUNT_ID).FirstOrDefault();
			//	var billsDB = context.BILLs.Where(b => b.ACCOUNT_ID == accountDB.ID).ToList();

			//	if (customerDB != null)
			//		GetCustomerDataForView(context, model, customerDB);
			//	else
			//		model.CustomerModel.Customer = null;

			//	foreach (var b in billsDB)
			//		model.Bills.Add(new Bill() { Id = b.ID, Date = b.DATE, Number = b.NUMBER, Period = HelperMethod.GetFullMonthName(b.PERIOD), Sum = b.SUM, Paid = b.PAID });

			//	return View(model);
			//}
		}

		public ActionResult PayOnLineBill() => View();

		public JsonResult GetBillData(string billNumber)
		{
			var model = new BillModel();

			model.GetBillData(billNumber);
			
			return Json(model, JsonRequestBehavior.AllowGet);
		}

		[HttpPost]
		public ActionResult CallPaymentCardForm(int bill_Id)
		{
			return Json(Url.Action("PaymentCardForm", "Bill", bill_Id));
		}

		public ActionResult PaymentCardForm(int bill_Id)
		{
			using (var context = new TownUtilityBillSystemV2Entities())
			{
				var model = new PaymentCardModel();
				var billDB = context.BILLs.Find(bill_Id);

				if (billDB != null)
					model.Bill = Bill.Get(billDB);

				return View(model);
			}
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult PaymentCardForm(PaymentCardModel payment)
		{
			ValidatePaymentFirstCheck(payment);

			if (ModelState.IsValid)
			{
				var model = new PaymentModel();

				model.MakePaymentTransaction(payment.Bill.Id);

				return RedirectToAction("ShowBillPaidInfo", "Bill", new { bill_Id = payment.Bill.Id });
			}

			return View(payment);
		}

		private void ValidatePaymentFirstCheck(PaymentCardModel payment)
		{
			Regex regexCardNumber = new Regex("^[0-9]{1,16}$");
			Regex regexCardCVV = new Regex("^[0-9]{1,3}$");
			DateTime presentDate = DateTime.Today;

			if (payment.PaymentCard.Number != null)
			{
				payment.PaymentCard.Number = payment.PaymentCard.Number.Replace(" ", "");

				if (!regexCardNumber.IsMatch(payment.PaymentCard.Number))
					ViewBag.WrongCardNumberError = Localization.CreditCardNumberMustHave16Digits;
			}

			if (payment.PaymentCard.CVV != null && !regexCardCVV.IsMatch(payment.PaymentCard.CVV))
				ViewBag.WrongCVVError = Localization.CVVnumberMustHave3Digits;

			if (payment.PaymentCard.ExpireDate.ToString() != "1/1/0001 12:00:00 AM" && payment.PaymentCard.ExpireDate < presentDate)
				ViewBag.WrongExpireDateError = Localization.CardIExpired;
		}

		public ActionResult ShowBillPaidInfo(int bill_Id)
		{
			var model = new BillModel();

			model.GetBillData(bill_Id);

			return View(model);
		}

		#region Bill CRUD

		public ActionResult CreateBill()
		{
			//TO DO
			ViewBag.Message = CustomizedMessages.NoAdministratorRightsMessage;

			return View();
		}

		public ActionResult DeleteBill(int bill_Id)
		{
			//TO DO
			ViewBag.Message = CustomizedMessages.NoAdministratorRightsMessage;

			return View();
		}

		#endregion
	}
}
