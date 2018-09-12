using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TownUtilityBillSystemV2.Models.AddressModels;
using TownUtilityBillSystemV2.Models.Chart;
using TownUtilityBillSystemV2.Models.HelperMethods;
using TownUtilityBillSystemV2.Models.TemperatureModels;
using static TownUtilityBillSystemV2.Models.InitialDB.InitialDBEnums;

namespace TownUtilityBillSystemV2.Controllers
{
	[Authorize]
	public class ChartController : Controller
    {
		public ActionResult ShowTemperatureChart()
		{
			var model = new AddressModel();

			ViewBag.TownList = model.GetTowns();

			ViewBag.ImageIconPath = HelperMethod.GetTemperatureIconImage();

			return View();
		}

		public ActionResult GetTemperatureChartData(int? townId)
		{
			var model = new ChartModel();

			model.TemperaturesModel = model.GetTemperatureChartData(townId);

			return Json(model.TemperaturesModel, JsonRequestBehavior.AllowGet);
		}

		public JsonResult GetLocalizatedWordsTemperatureChartForFirstLoad()
		{
			return Json(HelperMethod.GetLocalizatedWordsTemperatureChartForFirstLoad(), JsonRequestBehavior.AllowGet);
		}
	}
}