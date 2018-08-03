using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using TownUtilityBillSystemV2.App_Start;
using TownUtilityBillSystemV2.Controllers;
using TownUtilityBillSystemV2.Models.HelperModels;

namespace TownUtilityBillSystemV2
{
    public class MvcApplication : System.Web.HttpApplication
    {
		private InitialDBController initialDbCtrl;

		protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
			ControllerBuilder.Current.SetControllerFactory(new NinjectControllerFactory());
			ModelBinders.Binders.Add(typeof(decimal), new DecimalModelBinder());
			ModelBinders.Binders.Add(typeof(decimal?), new DecimalModelBinder());
			ClientDataTypeModelValidatorProvider.ResourceClassKey = "ErrorMessages";
			DefaultModelBinder.ResourceClassKey = "ErrorMessages";

			/// <summary>
			/// Initialize DB if it is empty
			/// </summary>
			//initialDbCtrl = new InitialDBController();
		}

		protected void Application_BeginRequest(object sender, EventArgs e)
		{
			HttpCookie cookie = HttpContext.Current.Request.Cookies["Language"];
			if (cookie != null && cookie.Value != null)
			{
				System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(cookie.Value);
				System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(cookie.Value);
			}
			else
			{
				System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en");
				System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en");
			}
		}
	}
}
