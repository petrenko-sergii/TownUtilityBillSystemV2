using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TownUtilityBillSystemV2.Models.Exceptions;
using TownUtilityBillSystemV2.Models.HelperMethods;
using TownUtilityBillSystemV2.Resources;

namespace TownUtilityBillSystemV2.Models.AddressModels
{
	public class AddressModel
	{

		public Index Index;
		public Town Town;
		public Street Street;
		public Building Building;
		public FlatPart FlatPart;
		public List<Index> Indexes;
		public List<Town> Towns;
		public List<Street> Streets;
		public List<Building> Buildings;
		public List<FlatPart> FlatParts;

		public AddressModel()
		{
			Index = new Index();
			Town = new Town();
			Street = new Street();
			Building = new Building();
			FlatPart = new FlatPart();

			Indexes = new List<Index>();
			Towns = new List<Town>();
			Streets = new List<Street>();
			Buildings = new List<Building>();
			FlatParts = new List<FlatPart>();
		}

		public SelectList GetTowns()
		{
			using (var context = new TownUtilityBillSystemV2Entities())
			{
				var townsDB = context.TOWNs.OrderBy(t=>t.NAME).ToList();

				var townList = townsDB.Select(Town.Get).ToList();

				return new SelectList(townList, "Id", "Name");
			}
		}

		public List<Street> GetStreetList(int townId)
		{
			using (var context = new TownUtilityBillSystemV2Entities())
			{
				context.Configuration.ProxyCreationEnabled = false;

				var streetsDB = context.STREETs.Where(s => s.TOWN_ID == townId).ToList();

				List<Street> streetList = streetsDB.Select(Street.Get).ToList();

				return streetList;
			}
		}

		public string GetTownName(int townId)
		{
			string townName = "";

			using (var context = new TownUtilityBillSystemV2Entities())
			{
				var townDB = context.TOWNs.Where(t => t.ID == townId).FirstOrDefault();

				if (townDB != null)
					townName = townDB.NAME;
				else
					throw new InvalidTownIdException(townId);
			}

			return townName;
		}

		public string GetStreetName(int streetId)
		{
			string streetName = "";

			using (var context = new TownUtilityBillSystemV2Entities())
			{
				var streetDB = context.STREETs.Where(s => s.ID == streetId).FirstOrDefault();

				if (streetDB != null)
					streetName = streetDB.NAME;
				else
					throw new InvalidStreetIdException(streetId);

				return streetName;
			}
		}

		public string GetBuildingNumber(int buildingId)
		{
			string buildingNumber = "";

			using (var context = new TownUtilityBillSystemV2Entities())
			{
				var buildingDB = context.BUILDINGs.Where(b => b.ID == buildingId).FirstOrDefault();

				if (buildingDB != null)
					buildingNumber = buildingDB.NUMBER.ToString();

				return buildingNumber;
			}
		}

		public List<Building> GetBuildingList(int streetId)
		{
			using (var context = new TownUtilityBillSystemV2Entities())
			{
				context.Configuration.ProxyCreationEnabled = false;

				var buildingsDB = context.BUILDINGs.Where(b => b.STREET_ID == streetId).ToList();

				List<Building> buildingList = buildingsDB.Select(Building.Get).ToList();

				return buildingList;
			}
		}

		public List<FlatPart> GetFlatPartList(int buildingId)
		{
			using (var context = new TownUtilityBillSystemV2Entities())
			{
				context.Configuration.ProxyCreationEnabled = false;

				var flatPartsDB = context.FLAT_PARTs.Where(f => f.BUILDING_ID == buildingId).ToList();

				List<FlatPart> flatPartList = flatPartsDB.Select(FlatPart.Get).ToList();

				return flatPartList;
			}
		}

		public string GetBuildingImage(int buildingId)
		{
			using (var context = new TownUtilityBillSystemV2Entities())
			{
				IMAGE_BUILDING imageDB = null;
				string imageName = "";
				string imagePathForHtml = "";
				string imagePathDB = "";
				string folderName = "";

				var buildingDB = context.BUILDINGs.Where(b => b.ID == buildingId).FirstOrDefault();

				imageDB = (buildingDB != null) ? context.IMAGE_BUILDINGs.Where(i => i.ID == buildingDB.IMAGE_ID).FirstOrDefault() : null;

				if (imageDB != null)
				{
					imagePathDB = imageDB.PATH.ToString();
					folderName = Path.GetFileName(Path.GetDirectoryName(imagePathDB));
					imageName = Path.GetFileName(imagePathDB);
					imagePathForHtml = "<img src = '/Content/Images/TownBuildings/" + folderName + "/" + imageName + "'" + "id = 'buildingImage'/> <br /> <br /><strong>" + Localization.BuildingImage + "</strong>";
				}
				else
				{
					switch (HelperMethod.GetCurrentLanguage())
					{
						case "da":
							imagePathForHtml = "<img src = '/Content/Images/EmptyImages/NoImageBuildingDa.jpg' id = 'buildingImage'/>";
							break;
						default:
							imagePathForHtml = "<img src = '/Content/Images/EmptyImages/NoImageBuildingEn.jpg' id = 'buildingImage'/>";
							break;
					}
				}

				return imagePathForHtml;
			}
		}

		public static string GetBuildingImageForCustomerDetailsView(int buildingId)
		{
			using (var context = new TownUtilityBillSystemV2Entities())
			{
				IMAGE_BUILDING imageDB = null;
				string imageName = "";
				string imagePath = "";
				string imagePathDB = "";
				string folderName = "";

				var buildingDB = context.BUILDINGs.Where(b => b.ID == buildingId).FirstOrDefault();

				if (buildingDB != null)
					imageDB = context.IMAGE_BUILDINGs.Where(i => i.ID == buildingDB.IMAGE_ID).FirstOrDefault();

				if (imageDB != null)
				{
					imagePathDB = imageDB.PATH.ToString();
					folderName = Path.GetFileName(Path.GetDirectoryName(imagePathDB));
					imageName = Path.GetFileName(imagePathDB);
					imagePath = "/Content/Images/TownBuildings/" + folderName + "/" + imageName;
				}
				else
					imagePath = "/Content/Images/EmptyImages/NoImageBuilding" + HelperMethod.UppercaseFirstLetter(HelperMethod.GetCurrentLanguage()) + ".jpg";

				return imagePath;
			}
		}
	}
}
