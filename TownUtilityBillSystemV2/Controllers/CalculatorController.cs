using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TownUtilityBillSystemV2.Models.Calculator;
using TownUtilityBillSystemV2.Models.HelperMethods;
using TownUtilityBillSystemV2.Models.UtilityModels;
using TownUtilityBillSystemV2.Resources;
using static TownUtilityBillSystemV2.Models.InitialDB.InitialDBEnums;

namespace TownUtilityBillSystemV2.Controllers
{
	[Authorize]
	public class CalculatorController : Controller
	{
		public ActionResult ShowCalculatorOnLine()
		{
			var model = new CalculatorItemModel();

			model.AddUtilitiesToCalculatorItemModel();

			return View(model);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult CalculateCharges(CalculatorItemModel inputModel)
		{
			var model = new CalculatorItemModel();

			model.AddUtilitiesToCalculatorItemModel();

			if (ModelState.IsValid)
			{
				if (inputModel.ElectricUsage == 0 && inputModel.WaterUsage == 0 && inputModel.HeatUsage == 0 && inputModel.GasUsage == 0)
				{
					ViewBag.ErrorMessage = Localization.YouMustEnterAtLeastOneUtilityUsage;

					return View("~/Views/Calculator/ShowCalculatorOnLine.cshtml", model);
				}
				
				model.CalculateCharges(inputModel);

				return View("~/Views/Calculator/ShowCharges.cshtml", model);
			}

			return View("~/Views/Calculator/ShowCalculatorOnLine.cshtml", model);
		}

		public JsonResult GetLocalizatedWordsForChart()
		{
			return Json(HelperMethod.GetLocalizatedWordsForChart(), JsonRequestBehavior.AllowGet);
		}
	}
}
