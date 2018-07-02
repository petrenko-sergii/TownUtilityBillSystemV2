using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TownUtilityBillSystemV2.Controllers
{
    public class StatisticController : Controller
    {
		// GET: Statistic
		public ActionResult ShowStatisticMenu()
		{
			return View();
		}
	}
}