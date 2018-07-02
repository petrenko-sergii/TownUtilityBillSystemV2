using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TownUtilityBillSystemV2.Models.BaseModels;
using TownUtilityBillSystemV2.Resources;

namespace TownUtilityBillSystemV2.Models.CustomerModels
{
	public class CustomerType : ObjectWithName
	{
		[Required(ErrorMessageResourceName = "FieldIsRequired", ErrorMessageResourceType = typeof(Localization))]
		public new int Id { get; set; }

		public string ResourceName { get; set; }

		public static CustomerType Get(CUSTOMER_TYPE customerType)
		{
			return new CustomerType
			{
				Id = customerType.ID,
				Name = customerType.NAME,
				ResourceName = GetResourceName(customerType.NAME)
			};
		}

		private static string GetResourceName(string name)
		{
			string resourceName;

			switch (name)
			{
				case "Apartment":
					resourceName = Localization.Apartment;
					break;
				case "House":
					resourceName = Localization.House;
					break;
				case "Shop":
					resourceName = Localization.Shop;
					break;
				case "Hotel":
					resourceName = Localization.Hotel;
					break;
				case "Restaurant":
					resourceName = Localization.Restaurant;
					break;
				case "Cafe":
					resourceName = Localization.Cafe;
					break;
				case "Hospital":
					resourceName = Localization.Hospital;
					break;
				case "School":
					resourceName = Localization.School;
					break;
				case "WorkShop":
					resourceName = Localization.WorkShop;
					break;
				case "Company":
					resourceName = Localization.Company;
					break;
				case "Church":
					resourceName = Localization.Church;
					break;
				case "Administrative building":
					resourceName = Localization.AdministrativeBuilding;
					break;
				case "Factory":
					resourceName = Localization.Factory;
					break;
				case "Plant":
					resourceName = Localization.Plant;
					break;
				case "Nursery":
					resourceName = Localization.Nursery;
					break;
				case "Kinder garden":
					resourceName = Localization.KinderGarden;
					break;
				case "Service station":
					resourceName = Localization.ServiceStation;
					break;
				case "Fuel station":
					resourceName = Localization.FuelStation;
					break;
				case "Salon":
					resourceName = Localization.Salon;
					break;
				case "Museum":
					resourceName = Localization.Museum;
					break;
				case "Theatre":
					resourceName = Localization.Theatre;
					break;
				case "Building":
					resourceName = Localization.Building;
					break;
				case "Other":
					resourceName = Localization.Other;
					break;
				default:
					resourceName = name;
					break;
			}

			return resourceName;
		}
	}
}