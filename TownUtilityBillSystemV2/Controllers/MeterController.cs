using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TownUtilityBillSystemV2.Models.AddressModels;
using TownUtilityBillSystemV2.Models.HelperMethods;
using TownUtilityBillSystemV2.Models.MeterModels;
using TownUtilityBillSystemV2.Models.UtilityModels;
using System.Data;
using System.Data.Entity;
using System.IO;

namespace TownUtilityBillSystemV2.Controllers
{
	//[Authorize]
	public class MeterController : Controller
    {
		public ActionResult ShowAllMeterTypes()
		{
			var model = new MeterTypeModel();

			model.GetAllMeterTypes();

			return View(model);
		}

		public ActionResult ShowSomeMeters()
		{
			var model = new MeterModel();

			model.GetRandomMeters();

			return View(model);
		}

		public ActionResult ShowAllMeters()
		{
			var model = new MeterModel();

			return View(model.GetAllMeters());
		}

		public ActionResult FindMetersByDetails()
		{
			var model = new MeterModel();

			model.GetRandomMeters();

			return View(model);
		}

		public ActionResult ShowFoundMeters(string searchString)
		{
			var model = new MeterModel();

			model.GetFoundMeters(searchString);

			ViewBag.SearchString = searchString;

			return View(model);
		}

		public ActionResult FindMeterByAddress()
		{
			var model = new AddressModel();

			ViewBag.TownList = model.GetTowns();

			return View();
		}

		public JsonResult GetStreetList(int townId)
		{
			var model = new AddressModel();

			var streetList = model.GetStreetList(townId);

			return Json(streetList, JsonRequestBehavior.AllowGet);
		}

		public string GetTownName(int townId)
		{
			var model = new AddressModel();

			return model.GetTownName(townId);
		}

		public string GetStreetName(int streetId)
		{
			var model = new AddressModel();

			return model.GetStreetName(streetId);
		}

		public string GetBuildingNumber(int buildingId)
		{
			var model = new AddressModel();

			return model.GetBuildingNumber(buildingId);
		}

		public JsonResult GetBuildingList(int streetId)
		{
			var model = new AddressModel();

			var buildingList = model.GetBuildingList(streetId);

			return Json(buildingList, JsonRequestBehavior.AllowGet);
		}

		public JsonResult GetFlatPartList(int buildingId)
		{
			var model = new AddressModel();

			List<FlatPart> flatPartList = model.GetFlatPartList(buildingId);

			return Json(flatPartList, JsonRequestBehavior.AllowGet);
		}

		public string GetBuildingImage(int buildingId)
		{
			var model = new AddressModel();

			return model.GetBuildingImage(buildingId);
		}

		public JsonResult GetMetersForBuilding(int buildingId)
		{
			var model = new MeterModel();

			model.GetMetersForBuilding(buildingId);

			return Json(model.Meters, JsonRequestBehavior.AllowGet);
		}

		public JsonResult GetMetersForFlatPart(int flatPartId)
		{
			var model = new MeterModel();

			model.GetMetersForFlatPart(flatPartId);

			return Json(model.Meters, JsonRequestBehavior.AllowGet);
		}
	}
}
