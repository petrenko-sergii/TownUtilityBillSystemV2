using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Globalization;
using System.Web.Mvc;

namespace TownUtilityBillSystemV2.Models.HelperModels
{
	public class CustomModelBinder : DefaultModelBinder
	{
		public CustomModelBinder()
	  : base()
		{
		}

		public override object BindModel(ControllerContext controllerContext,
	 ModelBindingContext bindingContext)
		{

			object result = null;

			// Don't do this here!
			// It might do bindingContext.ModelState.AddModelError
			// and there is no RemoveModelError!
			// 
			// result = base.BindModel(controllerContext, bindingContext);

			if (bindingContext.ModelType == typeof(Decimal))
			{

				string modelName = bindingContext.ModelName;
				string attemptedValue = bindingContext.ValueProvider.GetValue(modelName).AttemptedValue;

				// Depending on cultureinfo the NumberDecimalSeparator can be "," or "."
				// Both "." and "," should be accepted, but aren't.
				string wantedSeperator = NumberFormatInfo.CurrentInfo.NumberDecimalSeparator;
				string alternateSeperator = (wantedSeperator == "," ? "." : ",");

				if (attemptedValue.IndexOf(wantedSeperator) == -1
				  && attemptedValue.IndexOf(alternateSeperator) != -1)
				{
					attemptedValue = attemptedValue.Replace(alternateSeperator, wantedSeperator);
				}

				try
				{
					result = Decimal.Parse(attemptedValue, NumberStyles.Any);
				}
				catch (FormatException e)
				{
					bindingContext.ModelState.AddModelError(modelName, e);
				}

			}
			else
			{
				result = base.BindModel(controllerContext, bindingContext);
			}

			return result;
		}

	}
}