using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TownUtilityBillSystemV2.Models.InitialDB;

namespace TownUtilityBillSystemV2.Controllers
{
    public class InitialDBController : Controller
    {
		public InitialDBController()
		{
			InitialDBModel.FillDataDB();
		}
	}
}
