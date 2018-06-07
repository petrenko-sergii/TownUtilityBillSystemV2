using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TownUtilityBillSystemV2.Models.Customized;
using TownUtilityBillSystemV2.Models.Utility;
using TownUtilityBillSystemV2.Resources;
using static TownUtilityBillSystemV2.Models.InitialDB.InitialDBEnums;

namespace TownUtilityBillSystemV2.Controllers
{
    public class UtilityController : Controller
    {
		public ActionResult ShowUtilities()
		{
			var model = new UtilityModel();

			model.GetUtilitiesName();

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
	}
}
