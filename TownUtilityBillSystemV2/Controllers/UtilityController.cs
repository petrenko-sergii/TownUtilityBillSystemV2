using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TownUtilityBillSystemV2.Models;
using TownUtilityBillSystemV2.Models.Customized;
using TownUtilityBillSystemV2.Models.UtilityModels;
using TownUtilityBillSystemV2.Resources;
using static TownUtilityBillSystemV2.Models.InitialDB.InitialDBEnums;

namespace TownUtilityBillSystemV2.Controllers
{
	[Authorize]
	public class UtilityController : Controller
	{
		private readonly IUtilityRepository utilityRepo;

		public UtilityController(IUtilityRepository utilityRepo)
		{
			this.utilityRepo = utilityRepo;
		}

		public ViewResult ShowUtilities()
		{
			var model = new UtilityModel();

			model.Utilities = utilityRepo.UTILITYs.Select(Utility.GetUtilityWithIdAndNames).ToList();

			return View(model);
		}

		public ActionResult ShowAllUtilitiesPrices()
		{
			var model = new UtilityModel();

			model.GetAllUtilitiesPrices();

			return View(model);
		}

		public ActionResult ShowUtility(string utilityName)
		{
			var model = new UtilityModel();

			model.GetDataForUtility(utilityName);

			return View("~/Views/Utility/ShowUtility.cshtml", model);
		}

		#region Utility CRUD

		public ActionResult CreateUtility()
		{
			//TO DO
			ViewBag.Message = CustomizedMessages.NoAdministratorRightsMessage;

			return View();
		}

		public ActionResult EditUtility(int utilityId)
		{
			var model = new UtilityModel();

			var utilityForEdit = model.GetUtility(utilityId);

			return View("~/Views/Utility/EditUtility.cshtml", utilityForEdit);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult EditUtility(Utility utility)
		{
			if (ModelState.IsValid)
			{
				var model = new UtilityModel();

				model.UpdateUtility(utility);

				return RedirectToAction("ShowUtility", "Utility", new { utilityName = utility.Name });
			}

			return View();
		}

		public ActionResult DeleteUtility(int utilityId)
		{
			//TO DO
			ViewBag.Message = CustomizedMessages.NoAdministratorRightsMessage;

			return View();
		}

		#endregion
	}
}
