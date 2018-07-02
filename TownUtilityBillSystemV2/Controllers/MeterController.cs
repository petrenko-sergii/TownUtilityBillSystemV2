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
		TownUtilityBillSystemV2Entities context = new TownUtilityBillSystemV2Entities();

		private IMeterRepository meterRepo;
		private IStreetRepository streetRepo;
		private IBuildingRepository buildingRepo;

		public MeterController(IMeterRepository meterRepo, IStreetRepository streetRepo, IBuildingRepository buildingRepo)
		{
			this.meterRepo = meterRepo;
			this.streetRepo = streetRepo;
			this.buildingRepo = buildingRepo;
		}

		public ViewResult ShowAllMeters()
		{
			return View(meterRepo.METERs.Include(m => m.ADDRESS).Include(m => m.METER_TYPE).ToList());
		}

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
			List<STREET> streetsDB = streetRepo.STREETs.Where(s => s.TOWN_ID == townId).OrderBy(s=>s.NAME).ToList();

			var streetsForView = from s in streetsDB
					 select new
					 {
						 Id = s.ID,
						 Name = s.NAME
					 };

			return Json(streetsForView.ToList(), JsonRequestBehavior.AllowGet);
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
			List<BUILDING> buildingsDB = buildingRepo.BUILDINGSs.Where(b => b.STREET_ID == streetId).ToList();

			var buildingsForView = from b in buildingsDB
					 select new
					 {
						 Id = b.ID,
						 Number = b.NUMBER
					 };

			return Json(buildingsForView.ToList(), JsonRequestBehavior.AllowGet);
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

		public JsonResult GetLocalizatedWordsForSelectBoxes()
		{
			return Json(HelperMethod.GetLocalizatedWordsForSelectBoxesFindByAddress(), JsonRequestBehavior.AllowGet);
		}

		public JsonResult GetLocalizatedWordsForMeterTable()
		{
			return Json(HelperMethod.GetLocalizatedWordsForMeterTableFindByAddress(), JsonRequestBehavior.AllowGet);
		}

		public ActionResult ShowMeterTypesForUtility(string utilityName)
		{
			var model = new MeterTypeModel();

			model.GetMeterTypesForUtility(utilityName);

			return View(model);

			//using (var context = new TownUtilityBillSystemV2Entities())
			//{
			//	var model = new MeterTypeModel();
			//	var utilityDB = context.UTILITYs.Where(u => u.NAME == utilityName).FirstOrDefault();
			//	var meterTypesDB = context.METER_TYPEs.Where(mt => mt.UTILITY_ID == utilityDB.ID).ToList();

			//	model.Utility.Id = utilityDB.ID;
			//	model.Utility.Name = utilityDB.NAME;

			//	foreach (var mt in meterTypesDB)
			//		model.MeterTypes.Add(new MeterType() { Id = mt.ID, Name = mt.NAME, VarificationPeriod = mt.VARIFICATION_PERIOD_YEARS });

			//	var view = View("~/Views/Meter/ShowMeterTypesForUtility.cshtml", model);

			//	return view;
			//}
		}
	}
}
