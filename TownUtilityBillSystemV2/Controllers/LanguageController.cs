using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace TownUtilityBillSystemV2.Controllers
{
    public class LanguageController : Controller
    {
        // GET: Language
        public ActionResult Index()
        {
            return View();
        }

		public ActionResult Change(string languageAbbreviation)
		{
			if(languageAbbreviation != null) 
			{
				Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(languageAbbreviation);
				Thread.CurrentThread.CurrentUICulture = new CultureInfo(languageAbbreviation);
			}

			HttpCookie cookie = new HttpCookie("Language");
			cookie.Value = languageAbbreviation;
			Response.Cookies.Add(cookie);

			return View("Index");
		}

		public JsonResult GetCurrentLanguage()
		{
			string currentLanguage = Thread.CurrentThread.CurrentCulture.ToString();

			return Json(currentLanguage, JsonRequestBehavior.AllowGet);
		}
	}
}
