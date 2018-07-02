using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ninject;
using System.Web.Routing;
using TownUtilityBillSystemV2.Models.MeterModels;
using TownUtilityBillSystemV2.Models.AddressModels;

namespace TownUtilityBillSystemV2.App_Start
{
	public class NinjectControllerFactory : DefaultControllerFactory
	{
		private IKernel ninjectKernel;

		public NinjectControllerFactory()
		{
			ninjectKernel = new StandardKernel();
			AddBindings();
		}

		protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
		{
			return controllerType == null ? null : (IController)ninjectKernel.Get(controllerType);
		}

		private void AddBindings()
		{
			ninjectKernel.Bind<IMeterRepository>().To<EFMeterRepository>();
			ninjectKernel.Bind<IStreetRepository>().To<EFStreetRepository>();
			ninjectKernel.Bind<IBuildingRepository>().To<EFBuildingRepository>();

		}
	}
}