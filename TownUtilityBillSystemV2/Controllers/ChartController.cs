using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TownUtilityBillSystemV2.Models.AddressModels;
using TownUtilityBillSystemV2.Models.HelperMethods;
using TownUtilityBillSystemV2.Models.TemperatureModels;
using static TownUtilityBillSystemV2.Models.InitialDB.InitialDBEnums;

namespace TownUtilityBillSystemV2.Controllers
{
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
			using (var context = new TownUtilityBillSystemV2Entities())
			{
				List<TemperatureModel> temperaturesModel = new List<TemperatureModel>();
				List<Temperature> temperaturesDB = new List<Temperature>();
				DateTime presDate = DateTime.Today;
				DateTime startDate = new DateTime();
				DateTime finishDate = new DateTime();

				int startDay = 1;
				int presYear = presDate.Year;
				int presMonth = presDate.Month;
				int prevYear = presYear - 1;
				int prevMonth = presMonth - 1;
				float valueSum;
				float averageValue;
				int daysInMonth;
				string defaultChartTown = "Copenhagen";
				var data = new List<TEMPERATURE>();

				startDate = new DateTime(prevYear, presMonth, startDay);
				finishDate = new DateTime(presYear, prevMonth, System.DateTime.DaysInMonth(presYear, prevMonth));

				if (townId != null)
				{
					data = context.TEMPERATUREs.Where(d => d.TOWN_ID == townId).ToList();
				}
				else
				{
					var defaultChartTownDB = (from t in context.TOWNs
											  where t.NAME.Contains(defaultChartTown)
											  select t).FirstOrDefault();
					data = context.TEMPERATUREs.Where(d => d.TOWN_ID == defaultChartTownDB.ID).ToList();
				}

				foreach (var d in data)
					temperaturesDB.Add(new Temperature() { Id = d.ID, Date = d.DATE, MinValue = d.MINVALUE, MaxValue = d.MAXVALUE, TownId = d.TOWN_ID });

				for (; startDate < finishDate;)
				{
					daysInMonth = System.DateTime.DaysInMonth(presYear, startDate.Month);
					valueSum = 0;

					for (int j = 0; j < daysInMonth; j++, startDate = startDate.AddDays(1))
						valueSum += (float)(temperaturesDB.Where(t => t.Date == startDate).FirstOrDefault().MinValue + temperaturesDB.Where(t => t.Date == startDate).FirstOrDefault().MaxValue) / 2;

					averageValue = (float)Math.Round(valueSum / daysInMonth, 1);

					if (startDate.Month != 1)
					{
						Months month = (Months)(startDate.Month - 1);
						string monthName = month.ToString() + " " + startDate.Year;

						temperaturesModel.Add(new TemperatureModel() { Date = new DateTime(startDate.Year, (startDate.Month - 1), startDay), AverageValue = averageValue, MonthName = monthName });
					}
					else
					{
						Months month = Months.December;
						string monthName = month.ToString() + " " + startDate.AddYears(-1).Year;

						temperaturesModel.Add(new TemperatureModel() { Date = new DateTime(startDate.Year, (int)Months.December, startDay), AverageValue = averageValue, MonthName = monthName });
					}
				}

				return Json(temperaturesModel, JsonRequestBehavior.AllowGet);
			}
		}
	}
}