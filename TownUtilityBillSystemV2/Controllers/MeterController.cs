using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TownUtilityBillSystemV2.Models.AddressModels;
using TownUtilityBillSystemV2.Models.HelperMethods;
using TownUtilityBillSystemV2.Models.MeterModels;
using System.Data;
using System.Data.Entity;
using System.IO;
using TownUtilityBillSystemV2.Models.CustomerModels;
using TownUtilityBillSystemV2.Models.UtilityModels;
using TownUtilityBillSystemV2.Models.AccountModels;
using TownUtilityBillSystemV2.Models.Chart;
using static TownUtilityBillSystemV2.Models.InitialDB.InitialDBEnums;
using TownUtilityBillSystemV2.Models.Exceptions;
using TownUtilityBillSystemV2.Models.Customized;

namespace TownUtilityBillSystemV2.Controllers
{
	[Authorize]
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
		}

		public ActionResult ShowMeterData(int meterId)
		{
			var model = new MeterItemModel();

			model.GetMeterData(meterId);

			return View(model);
		}

		public ActionResult GetMeterDataForChart(int meterId)
		{
			var model = new MeterItemModel();

			model.GetMeterDataForChart(meterId);

			if(model.ChartData.Count != 0)
				return Json(model.ChartData, JsonRequestBehavior.AllowGet);

			return Json(null, JsonRequestBehavior.AllowGet);
		}

		public ActionResult TryToShowAllUtilityCharts(int meterId)
		{
			using (var context = new TownUtilityBillSystemV2Entities())
			{
				var meterDB = context.METERs.Find(meterId);

				if (meterDB != null)
				{
					int addressId = meterDB.ADDRESS_ID;
					int customerIdDB = context.CUSTOMERs.Where(c => c.ADDRESS_ID == addressId).FirstOrDefault().ID;

					return RedirectToAction("ShowAllUtilityCharts", "Meter", new { customerId = customerIdDB });
				}
				else
					throw new InvalidMeterIdException(meterId);
			}
		}

		public ActionResult ShowAllUtilityCharts(int customerId)
		{
			var model = new CustomerModel();

			model.GetAllUtilityDataForCustomer(customerId);

			ViewBag.currentAddressForJS = model.Customer.Address;

			return View("~/Views/Chart/ShowAllUtilityCharts.cshtml", model);
		}

		public ActionResult GetAllUtilitiesDataForChart(int addressId)
		{
			var model = new CustomerModel();

			var dataForChart = model.GetAllUtilitiesDataForChart(addressId);

			return Json(dataForChart, JsonRequestBehavior.AllowGet);
		}

		#region Meter CRUD

		public ActionResult CreateMeter()
		{
			//TO DO
			ViewBag.Message = CustomizedMessages.NoAdministratorRightsMessage;

			return View();
		}

		public ActionResult EditMeter(int meterId)
		{
			var model = new Meter();

			model.GetMeterForEdit(meterId);

			return View(model);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult EditMeter(Meter meter)
		{
			var model = new Meter();

			if (ModelState.IsValid)
			{
				model.UpdateMeter(meter);

				return RedirectToAction("ShowFoundMeters", "Meter", new { searchString = meter.SerialNumber });
			}

			meter.MeterTypes = context.METER_TYPEs.Select(MeterType.Get).ToList();

			return View(meter);
		}

		public ActionResult DeleteMeter(int meterId)
		{
			//TO DO
			ViewBag.Message = CustomizedMessages.NoAdministratorRightsMessage;

			return View();
		}

		#endregion

		#region MeterType CRUD

		public ActionResult CreateMeterType()
		{
			//TO DO
			ViewBag.Message = CustomizedMessages.NoAdministratorRightsMessage;

			return View();
		}

		public ActionResult EditMeterType(int meterTypeId)
		{
			var model = new MeterType();

			var meterTypeForEdit = model.GetMeterType(meterTypeId);

			return View(meterTypeForEdit);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult EditMeterType(MeterType meterType)
		{
			if (ModelState.IsValid)
			{
				var model = new MeterType();

				model.UpdateMeterType(meterType);

				return RedirectToAction("ShowAllMeterTypes", "Meter");
			}

			meterType.Utilities = context.UTILITYs.Select(Utility.GetUtilityWithIdAndResourceName).ToList();

			return View();
		}

		public ActionResult DeleteMeterType(int meterTypeId)
		{
			//TO DO
			ViewBag.Message = CustomizedMessages.NoAdministratorRightsMessage;

			return View();
		}

		#endregion

		#region MeterItem CRUD

		public ActionResult CreateMeterItem()
		{
			//TO DO
			ViewBag.Message = CustomizedMessages.NoAdministratorRightsMessage;

			return View();
		}

		public ActionResult EditMeterData(int meterItemId)
		{
			var model = new MeterItemModel();

			model.GetMeterAndDataToEdit(meterItemId);

			ViewBag.CurrentCulture = HelperMethod.GetCurrentLanguage();

			return View(model);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult EditMeterData(MeterItem meterItem)
		{
			var model = new MeterItem();

			if (ModelState.IsValid)
			{
				model.UpdateMeterData(meterItem);

				int meterId = model.GetMeterId(meterItem.Id);

				return RedirectToAction("ShowMeterData", "Meter", new { meterId });
			}
			return View();
		}

		public ActionResult DeleteMeterItem(int meterItemId)
		{
			//TO DO
			ViewBag.Message = CustomizedMessages.NoAdministratorRightsMessage;

			return View();
		}

		#endregion
	}
}
