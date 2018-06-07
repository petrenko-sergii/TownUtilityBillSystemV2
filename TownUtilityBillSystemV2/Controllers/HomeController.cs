using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TownUtilityBillSystemV2.Models.News;
using TownUtilityBillSystemV2.Models.UtilitySupplier;

namespace TownUtilityBillSystemV2.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			var model = new NewsModel();

			model.GetNewsTitlesForSlideShow();

			return View(model);
		}

		public ActionResult About()
		{
			return View();
		}

		public ActionResult Contact()
		{
			UtilitySupplierModel utilitySupplierModel = new UtilitySupplierModel();

			return View(utilitySupplierModel.UtilitySupplier);
		}

		public ActionResult News()
		{
			var model = new NewsModel();

			model.GetNewsTitlesForSlideShow();

			return View("~/Views/News/News.cshtml", model);
		}

		public ActionResult ShowSingleNews(int newsId)
		{
			var model = new NewsModel();

			model.GetSingleNews(newsId);

			return View("~/Views/News/ShowSingleNews.cshtml", model);
		}
	}
}
