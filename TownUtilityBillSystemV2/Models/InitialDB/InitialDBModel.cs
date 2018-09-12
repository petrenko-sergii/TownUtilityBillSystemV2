using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using System.Web.Hosting;
using static TownUtilityBillSystemV2.Models.InitialDB.InitialDBEnums;
using TownUtilityBillSystemV2.Models;
using TownUtilityBillSystemV2.Models.TemperatureModels;
using TownUtilityBillSystemV2.Resources;

namespace TownUtilityBillSystemV2.Models.InitialDB
{
	public class InitialDBModel
	{
		#region Fields & Properties

		#region Fields

		static int accountNumber = 0;
		static int accountId = 1;
		private static Random rnd = new Random();

		#endregion

		#region Properties

		public static int AccountNumber
		{
			get
			{
				if (accountNumber == 0)
					return GetAccountNumber();
				else
					return ++accountNumber;
			}
		}

		private static string appRootPath = HttpContext.Current.Server.MapPath("~/");

		public static string AppRootPath { get => appRootPath; }

		#endregion

		#endregion

		#region FillDB Methods

		public static void FillDataDB()
		{
			//FillUnits();
			//FillBuildingImages(); 
			//FillUtilityIconImages();
			//FillUtilities();
			//FillAddresses();
			//FillCustomerTypes();
			//FillAccounts();
			//FillCustomers();
			//FillTemperatures();
			//FillMeterTypes();
			//FillMeters();
			//FillMeterItems();
			//FillBills();
			//FillNewsImages();
			//FillNews();
			//FillNewsTitles();
			//FillNewsChapters();
			//FillPayments();
		}

		public static void FillUnits()
		{
			using (var context = new TownUtilityBillSystemV2Entities())
			{
				if (!context.UNITs.Any())
				{
					context.UNITs.Add(new UNIT() { NAME = "kWh" });
					context.UNITs.Add(new UNIT() { NAME = "cub.m" });
					context.UNITs.Add(new UNIT() { NAME = "Gcal" });
					context.UNITs.Add(new UNIT() { NAME = "cub.m" });
					context.UNITs.Add(new UNIT() { NAME = "°C" });
					context.UNITs.Add(new UNIT() { NAME = "DKK" });

					context.SaveChanges();
				}
			}
		}

		public static void FillBuildingImages()
		{
			string[] arrayKoldingBuildings = Directory.GetFiles(AppRootPath + @"Content\Images\TownBuildings\Kolding\");
			string[] arrayOdenseBuildings = Directory.GetFiles(AppRootPath + @"Content\Images\TownBuildings\Odense\");

			ArraySortByNameAndNumber(arrayKoldingBuildings);
			ArraySortByNameAndNumber(arrayOdenseBuildings);

			using (var context = new TownUtilityBillSystemV2Entities())
			{
				if (!context.IMAGE_BUILDINGs.Any())
				{
					foreach (string building in arrayKoldingBuildings)
						context.IMAGE_BUILDINGs.Add(new IMAGE_BUILDING() { PATH = building });

					foreach (string building in arrayOdenseBuildings)
						context.IMAGE_BUILDINGs.Add(new IMAGE_BUILDING() { PATH = building });

					context.SaveChanges();
				}
			}
		}

		public static void FillUtilityIconImages()
		{
			using (var context = new TownUtilityBillSystemV2Entities())
			{
				if (!context.IMAGE_UTILITYs.Any())
				{
					context.IMAGE_UTILITYs.Add(new IMAGE_UTILITY() { PATH = AppRootPath + @"Content\Images\UtilityIcons\ElectricityIcon.jpg" });
					context.IMAGE_UTILITYs.Add(new IMAGE_UTILITY() { PATH = AppRootPath + @"Content\Images\UtilityIcons\WaterIcon.jpg" });
					context.IMAGE_UTILITYs.Add(new IMAGE_UTILITY() { PATH = AppRootPath + @"Content\Images\UtilityIcons\HeatingIcon.jpg" });
					context.IMAGE_UTILITYs.Add(new IMAGE_UTILITY() { PATH = AppRootPath + @"Content\Images\UtilityIcons\GasIcon.jpg" });

					context.SaveChanges();
				}

			}
		}

		public static void FillUtilities()
		{
			using (var context = new TownUtilityBillSystemV2Entities())
			{
				if (!context.UTILITYs.Any())
				{
					context.UTILITYs.Add(new UTILITY() { NAME = "Electricity", PRICE = 2.24m, UNIT_ID = 1, USAGEFORSTANDARTPRICE = 200, BIGUSAGEPRICE = 2.8m, IMAGE_ID = 1 });
					context.UTILITYs.Add(new UTILITY() { NAME = "Water", PRICE = 15.98m, UNIT_ID = 2, IMAGE_ID = 2 });
					context.UTILITYs.Add(new UTILITY() { NAME = "Heating", PRICE = 482.68m, UNIT_ID = 3, IMAGE_ID = 3 });
					context.UTILITYs.Add(new UTILITY() { NAME = "Gas", PRICE = 12.48m, UNIT_ID = 4, IMAGE_ID = 4 });

					context.SaveChanges();
				}
			}
		}

		public static void FillAddresses()
		{
			//FillTowns();
			//FillIndexes();
			//FillStreets();
			//FillBuildings(); 
			//FillFlatsParts();
			//FillAddressTable();
		}

		public static void FillTowns()
		{
			using (var context = new TownUtilityBillSystemV2Entities())
			{
				if (!context.TOWNs.Any())
				{
					context.TOWNs.Add(new TOWN() { NAME = "Copenhagen" });
					context.TOWNs.Add(new TOWN() { NAME = "Odense" });
					context.TOWNs.Add(new TOWN() { NAME = "Kolding" });
					context.TOWNs.Add(new TOWN() { NAME = "Aarhus" });
					context.TOWNs.Add(new TOWN() { NAME = "Viborg" });
					context.TOWNs.Add(new TOWN() { NAME = "Esbjerg" });
					context.TOWNs.Add(new TOWN() { NAME = "Vejle" });
					context.TOWNs.Add(new TOWN() { NAME = "Randers" });
					context.TOWNs.Add(new TOWN() { NAME = "Thisted" });

					context.SaveChanges();
				}
			}
		}

		public static void FillIndexes()
		{
			using (var context = new TownUtilityBillSystemV2Entities())
			{
				if (!context.INDEXes.Any())
				{
					context.INDEXes.Add(new INDEX() { VALUE = 1107 });
					context.INDEXes.Add(new INDEX() { VALUE = 1864 });
					context.INDEXes.Add(new INDEX() { VALUE = 2100 });
					context.INDEXes.Add(new INDEX() { VALUE = 2200 });
					context.INDEXes.Add(new INDEX() { VALUE = 2300 });
					context.INDEXes.Add(new INDEX() { VALUE = 2400 });
					context.INDEXes.Add(new INDEX() { VALUE = 2500 });
					context.INDEXes.Add(new INDEX() { VALUE = 5000 });
					context.INDEXes.Add(new INDEX() { VALUE = 5200 });
					context.INDEXes.Add(new INDEX() { VALUE = 5250 });
					context.INDEXes.Add(new INDEX() { VALUE = 5270 });
					context.INDEXes.Add(new INDEX() { VALUE = 6000 });
					context.INDEXes.Add(new INDEX() { VALUE = 8000 });
					context.INDEXes.Add(new INDEX() { VALUE = 8200 });
					context.INDEXes.Add(new INDEX() { VALUE = 8210 });
					context.INDEXes.Add(new INDEX() { VALUE = 8230 });
					context.INDEXes.Add(new INDEX() { VALUE = 8260 });
					context.INDEXes.Add(new INDEX() { VALUE = 8800 });
					context.INDEXes.Add(new INDEX() { VALUE = 8840 });
					context.INDEXes.Add(new INDEX() { VALUE = 7100 });
					context.INDEXes.Add(new INDEX() { VALUE = 8900 });
					context.INDEXes.Add(new INDEX() { VALUE = 8920 });
					context.INDEXes.Add(new INDEX() { VALUE = 8930 });
					context.INDEXes.Add(new INDEX() { VALUE = 8960 });
					context.INDEXes.Add(new INDEX() { VALUE = 7700 });

					context.SaveChanges();
				}
			}
		}

		public static void FillStreets()
		{
			using (var context = new TownUtilityBillSystemV2Entities())
			{
				if (!context.STREETs.Any())
				{
					#region KoldingStreets

					context.STREETs.Add(new STREET() { NAME = "Haderslevvej", TOWN_ID = 3, INDEX_ID = 12 });
					context.STREETs.Add(new STREET() { NAME = "Ndr Ringvej", TOWN_ID = 3, INDEX_ID = 12 });
					context.STREETs.Add(new STREET() { NAME = "Katrinegade", TOWN_ID = 3, INDEX_ID = 12 });
					context.STREETs.Add(new STREET() { NAME = "Sdr Ringvej", TOWN_ID = 3, INDEX_ID = 12 });
					context.STREETs.Add(new STREET() { NAME = "Istedvej", TOWN_ID = 3, INDEX_ID = 12 });
					context.STREETs.Add(new STREET() { NAME = "Hertug Abels", TOWN_ID = 3, INDEX_ID = 12 });
					context.STREETs.Add(new STREET() { NAME = "Agtrupvej", TOWN_ID = 3, INDEX_ID = 12 });
					context.STREETs.Add(new STREET() { NAME = "Jernbanegade ", TOWN_ID = 3, INDEX_ID = 12 });
					context.STREETs.Add(new STREET() { NAME = "Brostraede", TOWN_ID = 3, INDEX_ID = 12 });
					context.STREETs.Add(new STREET() { NAME = "Sydbanegade", TOWN_ID = 3, INDEX_ID = 12 });
					context.STREETs.Add(new STREET() { NAME = "Bredgade", TOWN_ID = 3, INDEX_ID = 12 });
					context.STREETs.Add(new STREET() { NAME = "Kastaniealle", TOWN_ID = 3, INDEX_ID = 12 });
					context.STREETs.Add(new STREET() { NAME = "Galgebjergvej", TOWN_ID = 3, INDEX_ID = 12 });
					context.STREETs.Add(new STREET() { NAME = "Drosselvej", TOWN_ID = 3, INDEX_ID = 12 });

					#endregion

					#region OdenseStreets

					context.STREETs.Add(new STREET() { NAME = "Rugaardsvej", TOWN_ID = 2, INDEX_ID = 8 });
					context.STREETs.Add(new STREET() { NAME = "Thomas Overskous Vej", TOWN_ID = 2, INDEX_ID = 8 });
					context.STREETs.Add(new STREET() { NAME = "Middelfartvej", TOWN_ID = 2, INDEX_ID = 9 });
					context.STREETs.Add(new STREET() { NAME = "Roesskovsvej", TOWN_ID = 2, INDEX_ID = 9 });
					context.STREETs.Add(new STREET() { NAME = "Frederiksgade", TOWN_ID = 2, INDEX_ID = 8 });
					context.STREETs.Add(new STREET() { NAME = "Nyborgvej ", TOWN_ID = 2, INDEX_ID = 8 });
					context.STREETs.Add(new STREET() { NAME = "Kochsgade", TOWN_ID = 2, INDEX_ID = 8 });
					context.STREETs.Add(new STREET() { NAME = "Windelsvej", TOWN_ID = 2, INDEX_ID = 8 });
					context.STREETs.Add(new STREET() { NAME = "Gammel Hoejmevej ", TOWN_ID = 2, INDEX_ID = 10 });
					context.STREETs.Add(new STREET() { NAME = "Assensvej", TOWN_ID = 2, INDEX_ID = 10 });
					context.STREETs.Add(new STREET() { NAME = "Sanderumvej", TOWN_ID = 2, INDEX_ID = 10 });
					context.STREETs.Add(new STREET() { NAME = "Bladstrupvej", TOWN_ID = 2, INDEX_ID = 11 });

					#endregion

					context.SaveChanges();
				}
			}
		}

		public static void FillBuildings()
		{
			using (var context = new TownUtilityBillSystemV2Entities())
			{
				if (!context.BUILDINGs.Any())
				{
					int buildingCount;

					#region KoldingBuildings
					buildingCount = 189;
					for (int i = 1; i <= buildingCount; i++)
						context.BUILDINGs.Add(new BUILDING() { NUMBER = i.ToString(), STREET_ID = 1 });

					buildingCount = 87;
					for (int i = 1; i <= buildingCount; i++)
						context.BUILDINGs.Add(new BUILDING() { NUMBER = i.ToString(), STREET_ID = 2 });

					buildingCount = 37;
					for (int i = 1; i <= buildingCount; i++)
						context.BUILDINGs.Add(new BUILDING() { NUMBER = i.ToString(), STREET_ID = 3 });

					buildingCount = 30;
					for (int i = 1; i <= buildingCount; i++)
						context.BUILDINGs.Add(new BUILDING() { NUMBER = i.ToString(), STREET_ID = 10 });

					buildingCount = 32;
					for (int i = 1; i <= buildingCount; i++)
						context.BUILDINGs.Add(new BUILDING() { NUMBER = i.ToString(), STREET_ID = 11 });
					#endregion

					#region OdenseBuildings

					buildingCount = 251;
					for (int i = 1; i <= buildingCount; i++)
						context.BUILDINGs.Add(new BUILDING() { NUMBER = i.ToString(), STREET_ID = 15 });

					buildingCount = 226;
					for (int i = 1; i <= buildingCount; i++)
						context.BUILDINGs.Add(new BUILDING() { NUMBER = i.ToString(), STREET_ID = 17 });

					buildingCount = 150;
					for (int i = 1; i <= buildingCount; i++)
						context.BUILDINGs.Add(new BUILDING() { NUMBER = i.ToString(), STREET_ID = 18 });
					#endregion

					context.SaveChanges();
				}
			}
			FillSquaresAndImages();
		}

		public static void FillSquaresAndImages()
		{
			using (var context = new TownUtilityBillSystemV2Entities())
			{
				#region AddSquaresToBuildingsWithImages
				context.BUILDINGs.Find(10).SQUARE = 340F;
				context.BUILDINGs.Find(12).SQUARE = 140F;
				context.BUILDINGs.Find(22).SQUARE = 322F;
				context.BUILDINGs.Find(24).SQUARE = 284;
				context.BUILDINGs.Find(37).SQUARE = 671.3F;
				context.BUILDINGs.Find(76).SQUARE = 532.4F;
				context.BUILDINGs.Find(79).SQUARE = 112.2F;
				context.BUILDINGs.Find(89).SQUARE = 103.8F;
				context.BUILDINGs.Find(100).SQUARE = 638;
				context.BUILDINGs.Find(102).SQUARE = 0;
				context.BUILDINGs.Find(104).SQUARE = 654.2F;
				context.BUILDINGs.Find(117).SQUARE = 281.9F;
				context.BUILDINGs.Find(120).SQUARE = 164.3F;
				context.BUILDINGs.Find(124).SQUARE = 598.5F;
				context.BUILDINGs.Find(190).SQUARE = 190.4F;
				context.BUILDINGs.Find(191).SQUARE = 1084F;
				context.BUILDINGs.Find(192).SQUARE = 94;
				context.BUILDINGs.Find(193).SQUARE = 838;
				context.BUILDINGs.Find(194).SQUARE = 103.2F;
				context.BUILDINGs.Find(196).SQUARE = 88.3F;
				context.BUILDINGs.Find(239).SQUARE = 193.8F;
				context.BUILDINGs.Find(676).SQUARE = 312;
				context.BUILDINGs.Find(679).SQUARE = 181.2F;
				context.BUILDINGs.Find(735).SQUARE = 96;
				context.BUILDINGs.Find(737).SQUARE = 93;
				context.BUILDINGs.Find(741).SQUARE = 112;
				context.BUILDINGs.Find(745).SQUARE = 1612;
				context.BUILDINGs.Find(781).SQUARE = 244;
				#endregion

				#region AddImages

				context.BUILDINGs.Find(10).IMAGE_ID = 1;
				context.BUILDINGs.Find(12).IMAGE_ID = 2;
				context.BUILDINGs.Find(22).IMAGE_ID = 3;
				context.BUILDINGs.Find(24).IMAGE_ID = 4;
				context.BUILDINGs.Find(37).IMAGE_ID = 5;
				context.BUILDINGs.Find(76).IMAGE_ID = 6;
				context.BUILDINGs.Find(79).IMAGE_ID = 7;
				context.BUILDINGs.Find(89).IMAGE_ID = 8;
				context.BUILDINGs.Find(100).IMAGE_ID = 9;
				context.BUILDINGs.Find(102).IMAGE_ID = 10;
				context.BUILDINGs.Find(104).IMAGE_ID = 11;
				context.BUILDINGs.Find(117).IMAGE_ID = 12;
				context.BUILDINGs.Find(120).IMAGE_ID = 13;
				context.BUILDINGs.Find(124).IMAGE_ID = 14;
				context.BUILDINGs.Find(190).IMAGE_ID = 15;
				context.BUILDINGs.Find(191).IMAGE_ID = 16;
				context.BUILDINGs.Find(192).IMAGE_ID = 17;
				context.BUILDINGs.Find(193).IMAGE_ID = 18;
				context.BUILDINGs.Find(194).IMAGE_ID = 19;
				context.BUILDINGs.Find(195).IMAGE_ID = 20;
				context.BUILDINGs.Find(196).IMAGE_ID = 21;
				context.BUILDINGs.Find(202).IMAGE_ID = 22;
				context.BUILDINGs.Find(204).IMAGE_ID = 23;
				context.BUILDINGs.Find(206).IMAGE_ID = 24;
				context.BUILDINGs.Find(239).IMAGE_ID = 25;
				context.BUILDINGs.Find(676).IMAGE_ID = 26;
				context.BUILDINGs.Find(679).IMAGE_ID = 27;
				context.BUILDINGs.Find(691).IMAGE_ID = 28;
				context.BUILDINGs.Find(693).IMAGE_ID = 29;
				context.BUILDINGs.Find(735).IMAGE_ID = 30;
				context.BUILDINGs.Find(737).IMAGE_ID = 31;
				context.BUILDINGs.Find(741).IMAGE_ID = 32;
				context.BUILDINGs.Find(745).IMAGE_ID = 33;
				context.BUILDINGs.Find(781).IMAGE_ID = 34;
				context.BUILDINGs.Find(791).IMAGE_ID = 35;
				#endregion

				context.SaveChanges();

				#region AddSquaresToBuildingsWithOutImages

				Random rnd = new Random();
				int minSq = 86;
				int maxSq = 470;

				foreach (var building in context.BUILDINGs.ToList())
				{
					if (building.SQUARE == null)
						building.SQUARE = rnd.Next(minSq, maxSq);
				}

				context.SaveChanges();

				#endregion
			}
		}

		public static void FillFlatsParts()
		{
			Random rnd = new Random();
			int minSq = 69;
			int maxSq = 115;

			using (var context = new TownUtilityBillSystemV2Entities())
			{
				if (!context.FLAT_PARTs.Any())
				{
					int flatCount;

					#region KoldingFlats

					flatCount = 4;
					for (int i = 1; i <= flatCount; i++)
						context.FLAT_PARTs.Add(new FLAT_PART() { NUMBER = i, BUILDING_ID = 22, SQUARE = rnd.Next(minSq, maxSq) });
					context.FLAT_PARTs.Add(new FLAT_PART() { NAME = "Idea Scum Studio", BUILDING_ID = 22, SQUARE = rnd.Next(minSq * 2, maxSq * 2) });

					flatCount = 4;
					for (int i = 1; i <= flatCount; i++)
						context.FLAT_PARTs.Add(new FLAT_PART() { NUMBER = i, BUILDING_ID = 24, SQUARE = rnd.Next(minSq, maxSq) });

					flatCount = 6;
					for (int i = 1; i <= flatCount; i++)
						context.FLAT_PARTs.Add(new FLAT_PART() { NUMBER = i, BUILDING_ID = 37, SQUARE = rnd.Next(minSq, maxSq) });
					context.FLAT_PARTs.Add(new FLAT_PART() { NAME = "Star Nails", BUILDING_ID = 37, SQUARE = rnd.Next(minSq * 2, maxSq * 2) });
					context.FLAT_PARTs.Add(new FLAT_PART() { NAME = "J BY J HairCutter", BUILDING_ID = 37, SQUARE = rnd.Next(minSq * 2, maxSq * 2) });

					flatCount = 2;
					for (int i = 1; i <= flatCount; i++)
						context.FLAT_PARTs.Add(new FLAT_PART() { NUMBER = i, BUILDING_ID = 79, SQUARE = rnd.Next(minSq, maxSq) });

					flatCount = 6;
					for (int i = 1; i <= flatCount; i++)
						context.FLAT_PARTs.Add(new FLAT_PART() { NUMBER = i, BUILDING_ID = 195, SQUARE = rnd.Next(minSq, maxSq) });

					flatCount = 4;
					for (int i = 1; i <= flatCount; i++)
						context.FLAT_PARTs.Add(new FLAT_PART() { NUMBER = i, BUILDING_ID = 202, SQUARE = rnd.Next(minSq, maxSq) });

					flatCount = 4;
					for (int i = 1; i <= flatCount; i++)
						context.FLAT_PARTs.Add(new FLAT_PART() { NUMBER = i, BUILDING_ID = 204, SQUARE = rnd.Next(minSq, maxSq) });

					flatCount = 4;
					for (int i = 1; i <= flatCount; i++)
						context.FLAT_PARTs.Add(new FLAT_PART() { NUMBER = i, BUILDING_ID = 206, SQUARE = rnd.Next(minSq, maxSq) });

					flatCount = 8;
					for (int i = 1; i <= flatCount; i++)
						context.FLAT_PARTs.Add(new FLAT_PART() { NUMBER = i, BUILDING_ID = 691, SQUARE = rnd.Next(minSq, maxSq) });

					flatCount = 8;
					for (int i = 1; i <= flatCount; i++)
						context.FLAT_PARTs.Add(new FLAT_PART() { NUMBER = i, BUILDING_ID = 693, SQUARE = rnd.Next(minSq, maxSq) });

					flatCount = 18;
					for (int i = 1; i <= flatCount; i++)
						context.FLAT_PARTs.Add(new FLAT_PART() { NUMBER = i, BUILDING_ID = 791, SQUARE = rnd.Next(minSq, maxSq) });
					#endregion
					context.SaveChanges();
				}
			}
			UpdateBuildingSquares();
		}

		public static void UpdateBuildingSquares()
		{
			float factor = 1.07F;

			using (var context = new TownUtilityBillSystemV2Entities())
			{
				HashSet<int> arrayBuildingsWithFlats = new HashSet<int>();

				foreach (var flat in context.FLAT_PARTs.ToList())
				{
					arrayBuildingsWithFlats.Add(flat.BUILDING_ID);
				}

				float buildingSquare;

				foreach (var building in arrayBuildingsWithFlats)
				{
					buildingSquare = 0;

					foreach (var flat in context.FLAT_PARTs.Where(f => f.BUILDING_ID == building).ToList())
					{
						buildingSquare += (float)flat.SQUARE;
					}
					buildingSquare *= factor;
					context.BUILDINGs.Find(building).SQUARE = buildingSquare;
				}
				context.SaveChanges();
			}
		}

		public static void FillAddressTable()
		{
			int i, j;

			using (var context = new TownUtilityBillSystemV2Entities())
			{
				if (!context.ADDRESSes.Any())
				{
					#region Kolding Town

					#region KoldingHaderslevvej

					for (i = 1, j = 22; i < j; i++)
						context.ADDRESSes.Add(new ADDRESS() { INDEX_ID = 12, TOWN_ID = 3, STREET_ID = 1, BUILDING_ID = i });

					context.ADDRESSes.Add(new ADDRESS() { INDEX_ID = 12, TOWN_ID = 3, STREET_ID = 1, BUILDING_ID = 22, FLAT_PART_ID = 1 });
					context.ADDRESSes.Add(new ADDRESS() { INDEX_ID = 12, TOWN_ID = 3, STREET_ID = 1, BUILDING_ID = 22, FLAT_PART_ID = 2 });
					context.ADDRESSes.Add(new ADDRESS() { INDEX_ID = 12, TOWN_ID = 3, STREET_ID = 1, BUILDING_ID = 22, FLAT_PART_ID = 3 });
					context.ADDRESSes.Add(new ADDRESS() { INDEX_ID = 12, TOWN_ID = 3, STREET_ID = 1, BUILDING_ID = 22, FLAT_PART_ID = 4 });
					context.ADDRESSes.Add(new ADDRESS() { INDEX_ID = 12, TOWN_ID = 3, STREET_ID = 1, BUILDING_ID = 22, FLAT_PART_ID = 5 });
					context.ADDRESSes.Add(new ADDRESS() { INDEX_ID = 12, TOWN_ID = 3, STREET_ID = 1, BUILDING_ID = 23 });
					context.ADDRESSes.Add(new ADDRESS() { INDEX_ID = 12, TOWN_ID = 3, STREET_ID = 1, BUILDING_ID = 24, FLAT_PART_ID = 6 });
					context.ADDRESSes.Add(new ADDRESS() { INDEX_ID = 12, TOWN_ID = 3, STREET_ID = 1, BUILDING_ID = 24, FLAT_PART_ID = 7 });
					context.ADDRESSes.Add(new ADDRESS() { INDEX_ID = 12, TOWN_ID = 3, STREET_ID = 1, BUILDING_ID = 24, FLAT_PART_ID = 8 });
					context.ADDRESSes.Add(new ADDRESS() { INDEX_ID = 12, TOWN_ID = 3, STREET_ID = 1, BUILDING_ID = 24, FLAT_PART_ID = 9 });

					for (i = 25, j = 37; i < j; i++)
						context.ADDRESSes.Add(new ADDRESS() { INDEX_ID = 12, TOWN_ID = 3, STREET_ID = 1, BUILDING_ID = i });

					context.ADDRESSes.Add(new ADDRESS() { INDEX_ID = 12, TOWN_ID = 3, STREET_ID = 1, BUILDING_ID = 37, FLAT_PART_ID = 10 });
					context.ADDRESSes.Add(new ADDRESS() { INDEX_ID = 12, TOWN_ID = 3, STREET_ID = 1, BUILDING_ID = 37, FLAT_PART_ID = 11 });
					context.ADDRESSes.Add(new ADDRESS() { INDEX_ID = 12, TOWN_ID = 3, STREET_ID = 1, BUILDING_ID = 37, FLAT_PART_ID = 12 });
					context.ADDRESSes.Add(new ADDRESS() { INDEX_ID = 12, TOWN_ID = 3, STREET_ID = 1, BUILDING_ID = 37, FLAT_PART_ID = 13 });
					context.ADDRESSes.Add(new ADDRESS() { INDEX_ID = 12, TOWN_ID = 3, STREET_ID = 1, BUILDING_ID = 37, FLAT_PART_ID = 14 });
					context.ADDRESSes.Add(new ADDRESS() { INDEX_ID = 12, TOWN_ID = 3, STREET_ID = 1, BUILDING_ID = 37, FLAT_PART_ID = 15 });
					context.ADDRESSes.Add(new ADDRESS() { INDEX_ID = 12, TOWN_ID = 3, STREET_ID = 1, BUILDING_ID = 37, FLAT_PART_ID = 16 });
					context.ADDRESSes.Add(new ADDRESS() { INDEX_ID = 12, TOWN_ID = 3, STREET_ID = 1, BUILDING_ID = 37, FLAT_PART_ID = 17 });

					for (i = 38, j = 79; i < j; i++)
						context.ADDRESSes.Add(new ADDRESS() { INDEX_ID = 12, TOWN_ID = 3, STREET_ID = 1, BUILDING_ID = i });

					context.ADDRESSes.Add(new ADDRESS() { INDEX_ID = 12, TOWN_ID = 3, STREET_ID = 1, BUILDING_ID = 79, FLAT_PART_ID = 18 });
					context.ADDRESSes.Add(new ADDRESS() { INDEX_ID = 12, TOWN_ID = 3, STREET_ID = 1, BUILDING_ID = 79, FLAT_PART_ID = 19 });

					for (i = 80, j = 190; i < j; i++)
						context.ADDRESSes.Add(new ADDRESS() { INDEX_ID = 12, TOWN_ID = 3, STREET_ID = 1, BUILDING_ID = i });

					#endregion

					#region KoldingNdr_Ringvej

					for (i = 190, j = 195; i < j; i++)
						context.ADDRESSes.Add(new ADDRESS() { INDEX_ID = 12, TOWN_ID = 3, STREET_ID = 2, BUILDING_ID = i });

					context.ADDRESSes.Add(new ADDRESS() { INDEX_ID = 12, TOWN_ID = 3, STREET_ID = 2, BUILDING_ID = 195, FLAT_PART_ID = 20 });
					context.ADDRESSes.Add(new ADDRESS() { INDEX_ID = 12, TOWN_ID = 3, STREET_ID = 2, BUILDING_ID = 195, FLAT_PART_ID = 21 });
					context.ADDRESSes.Add(new ADDRESS() { INDEX_ID = 12, TOWN_ID = 3, STREET_ID = 2, BUILDING_ID = 195, FLAT_PART_ID = 22 });
					context.ADDRESSes.Add(new ADDRESS() { INDEX_ID = 12, TOWN_ID = 3, STREET_ID = 2, BUILDING_ID = 195, FLAT_PART_ID = 23 });
					context.ADDRESSes.Add(new ADDRESS() { INDEX_ID = 12, TOWN_ID = 3, STREET_ID = 2, BUILDING_ID = 195, FLAT_PART_ID = 24 });
					context.ADDRESSes.Add(new ADDRESS() { INDEX_ID = 12, TOWN_ID = 3, STREET_ID = 2, BUILDING_ID = 195, FLAT_PART_ID = 25 });

					for (i = 196, j = 202; i < j; i++)
						context.ADDRESSes.Add(new ADDRESS() { INDEX_ID = 12, TOWN_ID = 3, STREET_ID = 2, BUILDING_ID = i });

					context.ADDRESSes.Add(new ADDRESS() { INDEX_ID = 12, TOWN_ID = 3, STREET_ID = 2, BUILDING_ID = 202, FLAT_PART_ID = 26 });
					context.ADDRESSes.Add(new ADDRESS() { INDEX_ID = 12, TOWN_ID = 3, STREET_ID = 2, BUILDING_ID = 202, FLAT_PART_ID = 27 });
					context.ADDRESSes.Add(new ADDRESS() { INDEX_ID = 12, TOWN_ID = 3, STREET_ID = 2, BUILDING_ID = 202, FLAT_PART_ID = 28 });
					context.ADDRESSes.Add(new ADDRESS() { INDEX_ID = 12, TOWN_ID = 3, STREET_ID = 2, BUILDING_ID = 202, FLAT_PART_ID = 29 });
					context.ADDRESSes.Add(new ADDRESS() { INDEX_ID = 12, TOWN_ID = 3, STREET_ID = 2, BUILDING_ID = 203 });
					context.ADDRESSes.Add(new ADDRESS() { INDEX_ID = 12, TOWN_ID = 3, STREET_ID = 2, BUILDING_ID = 204, FLAT_PART_ID = 30 });
					context.ADDRESSes.Add(new ADDRESS() { INDEX_ID = 12, TOWN_ID = 3, STREET_ID = 2, BUILDING_ID = 204, FLAT_PART_ID = 31 });
					context.ADDRESSes.Add(new ADDRESS() { INDEX_ID = 12, TOWN_ID = 3, STREET_ID = 2, BUILDING_ID = 204, FLAT_PART_ID = 32 });
					context.ADDRESSes.Add(new ADDRESS() { INDEX_ID = 12, TOWN_ID = 3, STREET_ID = 2, BUILDING_ID = 204, FLAT_PART_ID = 33 });
					context.ADDRESSes.Add(new ADDRESS() { INDEX_ID = 12, TOWN_ID = 3, STREET_ID = 2, BUILDING_ID = 205 });
					context.ADDRESSes.Add(new ADDRESS() { INDEX_ID = 12, TOWN_ID = 3, STREET_ID = 2, BUILDING_ID = 206, FLAT_PART_ID = 34 });
					context.ADDRESSes.Add(new ADDRESS() { INDEX_ID = 12, TOWN_ID = 3, STREET_ID = 2, BUILDING_ID = 206, FLAT_PART_ID = 35 });
					context.ADDRESSes.Add(new ADDRESS() { INDEX_ID = 12, TOWN_ID = 3, STREET_ID = 2, BUILDING_ID = 206, FLAT_PART_ID = 36 });
					context.ADDRESSes.Add(new ADDRESS() { INDEX_ID = 12, TOWN_ID = 3, STREET_ID = 2, BUILDING_ID = 206, FLAT_PART_ID = 37 });

					for (i = 207, j = 239; i < j; i++)
						context.ADDRESSes.Add(new ADDRESS() { INDEX_ID = 12, TOWN_ID = 3, STREET_ID = 2, BUILDING_ID = i });

					context.ADDRESSes.Add(new ADDRESS() { INDEX_ID = 12, TOWN_ID = 3, STREET_ID = 2, BUILDING_ID = 239 });

					for (i = 240, j = 277; i < j; i++)
						context.ADDRESSes.Add(new ADDRESS() { INDEX_ID = 12, TOWN_ID = 3, STREET_ID = 2, BUILDING_ID = i });

					#endregion

					#region KoldingOtherStreets

					for (i = 277, j = 314; i < j; i++)
						context.ADDRESSes.Add(new ADDRESS() { INDEX_ID = 12, TOWN_ID = 3, STREET_ID = 3, BUILDING_ID = i });

					for (i = 314, j = 344; i < j; i++)
						context.ADDRESSes.Add(new ADDRESS() { INDEX_ID = 12, TOWN_ID = 3, STREET_ID = 10, BUILDING_ID = i });

					for (i = 344, j = 376; i < j; i++)
						context.ADDRESSes.Add(new ADDRESS() { INDEX_ID = 12, TOWN_ID = 3, STREET_ID = 11, BUILDING_ID = i });

					#endregion

					#endregion

					#region Odense Town

					#region OdenseStreets

					for (i = 376, j = 627; i < j; i++)
						context.ADDRESSes.Add(new ADDRESS() { INDEX_ID = 9, TOWN_ID = 2, STREET_ID = 15, BUILDING_ID = i });

					for (i = 627, j = 691; i < j; i++)
						context.ADDRESSes.Add(new ADDRESS() { INDEX_ID = 9, TOWN_ID = 2, STREET_ID = 17, BUILDING_ID = i });

					context.ADDRESSes.Add(new ADDRESS() { INDEX_ID = 9, TOWN_ID = 2, STREET_ID = 17, BUILDING_ID = 691, FLAT_PART_ID = 38 });
					context.ADDRESSes.Add(new ADDRESS() { INDEX_ID = 9, TOWN_ID = 2, STREET_ID = 17, BUILDING_ID = 691, FLAT_PART_ID = 39 });
					context.ADDRESSes.Add(new ADDRESS() { INDEX_ID = 9, TOWN_ID = 2, STREET_ID = 17, BUILDING_ID = 691, FLAT_PART_ID = 40 });
					context.ADDRESSes.Add(new ADDRESS() { INDEX_ID = 9, TOWN_ID = 2, STREET_ID = 17, BUILDING_ID = 691, FLAT_PART_ID = 41 });
					context.ADDRESSes.Add(new ADDRESS() { INDEX_ID = 9, TOWN_ID = 2, STREET_ID = 17, BUILDING_ID = 691, FLAT_PART_ID = 42 });
					context.ADDRESSes.Add(new ADDRESS() { INDEX_ID = 9, TOWN_ID = 2, STREET_ID = 17, BUILDING_ID = 691, FLAT_PART_ID = 43 });
					context.ADDRESSes.Add(new ADDRESS() { INDEX_ID = 9, TOWN_ID = 2, STREET_ID = 17, BUILDING_ID = 691, FLAT_PART_ID = 44 });
					context.ADDRESSes.Add(new ADDRESS() { INDEX_ID = 9, TOWN_ID = 2, STREET_ID = 17, BUILDING_ID = 691, FLAT_PART_ID = 45 });
					context.ADDRESSes.Add(new ADDRESS() { INDEX_ID = 9, TOWN_ID = 2, STREET_ID = 17, BUILDING_ID = 692 });
					context.ADDRESSes.Add(new ADDRESS() { INDEX_ID = 9, TOWN_ID = 2, STREET_ID = 17, BUILDING_ID = 693, FLAT_PART_ID = 46 });
					context.ADDRESSes.Add(new ADDRESS() { INDEX_ID = 9, TOWN_ID = 2, STREET_ID = 17, BUILDING_ID = 693, FLAT_PART_ID = 47 });
					context.ADDRESSes.Add(new ADDRESS() { INDEX_ID = 9, TOWN_ID = 2, STREET_ID = 17, BUILDING_ID = 693, FLAT_PART_ID = 48 });
					context.ADDRESSes.Add(new ADDRESS() { INDEX_ID = 9, TOWN_ID = 2, STREET_ID = 17, BUILDING_ID = 693, FLAT_PART_ID = 49 });
					context.ADDRESSes.Add(new ADDRESS() { INDEX_ID = 9, TOWN_ID = 2, STREET_ID = 17, BUILDING_ID = 693, FLAT_PART_ID = 50 });
					context.ADDRESSes.Add(new ADDRESS() { INDEX_ID = 9, TOWN_ID = 2, STREET_ID = 17, BUILDING_ID = 693, FLAT_PART_ID = 51 });
					context.ADDRESSes.Add(new ADDRESS() { INDEX_ID = 9, TOWN_ID = 2, STREET_ID = 17, BUILDING_ID = 693, FLAT_PART_ID = 52 });
					context.ADDRESSes.Add(new ADDRESS() { INDEX_ID = 9, TOWN_ID = 2, STREET_ID = 17, BUILDING_ID = 693, FLAT_PART_ID = 53 });

					for (i = 694, j = 791; i < j; i++)
						context.ADDRESSes.Add(new ADDRESS() { INDEX_ID = 9, TOWN_ID = 2, STREET_ID = 17, BUILDING_ID = i });

					for (int n = 54; n < 72; n++)
						context.ADDRESSes.Add(new ADDRESS() { INDEX_ID = 9, TOWN_ID = 2, STREET_ID = 17, BUILDING_ID = 791, FLAT_PART_ID = n });

					for (i = 792, j = 854; i < j; i++)
						context.ADDRESSes.Add(new ADDRESS() { INDEX_ID = 9, TOWN_ID = 2, STREET_ID = 17, BUILDING_ID = i });

					for (i = 854, j = 1003; i < j; i++)
						context.ADDRESSes.Add(new ADDRESS() { INDEX_ID = 9, TOWN_ID = 2, STREET_ID = 18, BUILDING_ID = i });

					#endregion

					#endregion

					context.SaveChanges();
				}
			}
		}

		public static void FillCustomerTypes()
		{
			using (var context = new TownUtilityBillSystemV2Entities())
			{
				if (!context.CUSTOMER_TYPEs.Any())
				{
					context.CUSTOMER_TYPEs.Add(new CUSTOMER_TYPE() { NAME = "Apartment" });
					context.CUSTOMER_TYPEs.Add(new CUSTOMER_TYPE() { NAME = "House" });
					context.CUSTOMER_TYPEs.Add(new CUSTOMER_TYPE() { NAME = "Shop" });
					context.CUSTOMER_TYPEs.Add(new CUSTOMER_TYPE() { NAME = "Hotel" });
					context.CUSTOMER_TYPEs.Add(new CUSTOMER_TYPE() { NAME = "Restaurant" });
					context.CUSTOMER_TYPEs.Add(new CUSTOMER_TYPE() { NAME = "Cafe" });
					context.CUSTOMER_TYPEs.Add(new CUSTOMER_TYPE() { NAME = "Hospital" });
					context.CUSTOMER_TYPEs.Add(new CUSTOMER_TYPE() { NAME = "School" });
					context.CUSTOMER_TYPEs.Add(new CUSTOMER_TYPE() { NAME = "WorkShop" });
					context.CUSTOMER_TYPEs.Add(new CUSTOMER_TYPE() { NAME = "Company" });
					context.CUSTOMER_TYPEs.Add(new CUSTOMER_TYPE() { NAME = "Church" });
					context.CUSTOMER_TYPEs.Add(new CUSTOMER_TYPE() { NAME = "Administrative building" });
					context.CUSTOMER_TYPEs.Add(new CUSTOMER_TYPE() { NAME = "Factory" });
					context.CUSTOMER_TYPEs.Add(new CUSTOMER_TYPE() { NAME = "Plant" });
					context.CUSTOMER_TYPEs.Add(new CUSTOMER_TYPE() { NAME = "Nursery" });
					context.CUSTOMER_TYPEs.Add(new CUSTOMER_TYPE() { NAME = "Kinder garden" });
					context.CUSTOMER_TYPEs.Add(new CUSTOMER_TYPE() { NAME = "Service station" });
					context.CUSTOMER_TYPEs.Add(new CUSTOMER_TYPE() { NAME = "Fuel station" });
					context.CUSTOMER_TYPEs.Add(new CUSTOMER_TYPE() { NAME = "Salon" });
					context.CUSTOMER_TYPEs.Add(new CUSTOMER_TYPE() { NAME = "Museum" });
					context.CUSTOMER_TYPEs.Add(new CUSTOMER_TYPE() { NAME = "Theatre" });
					context.CUSTOMER_TYPEs.Add(new CUSTOMER_TYPE() { NAME = "Building" });
					context.CUSTOMER_TYPEs.Add(new CUSTOMER_TYPE() { NAME = "Other" });

					context.SaveChanges();
				}
			}
		}

		public static void FillAccounts()
		{
			// accountQty equals to Customer quantity
			int accountQty = 1062;

			using (var context = new TownUtilityBillSystemV2Entities())
			{
				if (!context.ACCOUNTs.Any())
				{
					for (int i = 0; i < accountQty; i++)
						context.ACCOUNTs.Add(new ACCOUNT() { NUMBER = AccountNumber.ToString(), BALANCE = GetRandomAccountBalance() });

					context.SaveChanges();
				}
			}
		}

		public static void FillCustomers()
		{
			FillCustomersWithImages();
			FillCustomersWithOutImages();
		}

		public static void FillCustomersWithImages()
		{
			string surname;
			string name;

			using (var context = new TownUtilityBillSystemV2Entities())
			{
				if (!context.CUSTOMERs.Any())
				{
					context.CUSTOMERs.Add(new CUSTOMER() { ACCOUNT_ID = accountId++, NAME = "Marios Pizza & Pasta", EMAIL = "mariopp@com.dk", PHONE = CreatePhoneNumber(), ADDRESS_ID = 10, CUSTOMER_TYPE_ID = 5 });
					context.CUSTOMERs.Add(new CUSTOMER() { ACCOUNT_ID = accountId++, NAME = "Sonja Rosing Massage", EMAIL = "sonjarosing@rose.dk", PHONE = CreatePhoneNumber(), ADDRESS_ID = 12, CUSTOMER_TYPE_ID = 19 });

					for (int i = 22; i < 26; i++)
					{
						surname = CreateSurname();
						name = CreateName();
						context.CUSTOMERs.Add(new CUSTOMER()
						{
							ACCOUNT_ID = accountId++,
							SURNAME = surname,
							NAME = name,
							EMAIL = CreateEmail(surname, name),
							PHONE = CreatePhoneNumber(),
							ADDRESS_ID = i,
							CUSTOMER_TYPE_ID = 1
						});
					}
					context.CUSTOMERs.Add(new CUSTOMER() { ACCOUNT_ID = accountId++, NAME = "Idea Scum Studio", EMAIL = "ideascum@com.dk", PHONE = CreatePhoneNumber(), ADDRESS_ID = 26, CUSTOMER_TYPE_ID = 3 });

					for (int i = 28; i < 32; i++)
					{
						surname = CreateSurname();
						name = CreateName();
						context.CUSTOMERs.Add(new CUSTOMER()
						{
							ACCOUNT_ID = accountId++,
							SURNAME = surname,
							NAME = name,
							EMAIL = CreateEmail(surname, name),
							PHONE = CreatePhoneNumber(),
							ADDRESS_ID = i,
							CUSTOMER_TYPE_ID = 1
						});
					}
					for (int i = 44; i < 50; i++)
					{
						surname = CreateSurname();
						name = CreateName();
						context.CUSTOMERs.Add(new CUSTOMER()
						{
							ACCOUNT_ID = accountId++,
							SURNAME = surname,
							NAME = name,
							EMAIL = CreateEmail(surname, name),
							PHONE = CreatePhoneNumber(),
							ADDRESS_ID = i,
							CUSTOMER_TYPE_ID = 1
						});
					}
					context.CUSTOMERs.Add(new CUSTOMER() { ACCOUNT_ID = accountId++, NAME = "Star Nails", EMAIL = "starnails@com.dk", PHONE = CreatePhoneNumber(), ADDRESS_ID = 50, CUSTOMER_TYPE_ID = 19 });
					context.CUSTOMERs.Add(new CUSTOMER() { ACCOUNT_ID = accountId++, NAME = "J BY J HairCutter", EMAIL = "jbyj@mail.com", PHONE = CreatePhoneNumber(), ADDRESS_ID = 51, CUSTOMER_TYPE_ID = 19 });

					context.CUSTOMERs.Add(new CUSTOMER() { ACCOUNT_ID = accountId++, NAME = "Sanistaal S Company", EMAIL = "office@sscom.dk", PHONE = CreatePhoneNumber(), ADDRESS_ID = 90, CUSTOMER_TYPE_ID = 10 });
					for (int i = 93; i < 95; i++)
					{
						surname = CreateSurname();
						name = CreateName();
						context.CUSTOMERs.Add(new CUSTOMER()
						{
							ACCOUNT_ID = accountId++,
							SURNAME = surname,
							NAME = name,
							EMAIL = CreateEmail(surname, name),
							PHONE = CreatePhoneNumber(),
							ADDRESS_ID = i,
							CUSTOMER_TYPE_ID = 1
						});
					}

					context.CUSTOMERs.Add(new CUSTOMER() { ACCOUNT_ID = accountId++, NAME = "Premier", EMAIL = "kolding@premier.dk", PHONE = CreatePhoneNumber(), ADDRESS_ID = 104, CUSTOMER_TYPE_ID = 19 });
					context.CUSTOMERs.Add(new CUSTOMER() { ACCOUNT_ID = accountId++, NAME = "Aldi", EMAIL = "kolding@aldi.dk", PHONE = CreatePhoneNumber(), ADDRESS_ID = 115, CUSTOMER_TYPE_ID = 3 });
					context.CUSTOMERs.Add(new CUSTOMER() { ACCOUNT_ID = accountId++, NAME = "Shell", EMAIL = "kolding@shell.dk", PHONE = CreatePhoneNumber(), ADDRESS_ID = 117, CUSTOMER_TYPE_ID = 18 });
					context.CUSTOMERs.Add(new CUSTOMER() { ACCOUNT_ID = accountId++, NAME = "Fakta", EMAIL = "kolding@fakta.dk", PHONE = CreatePhoneNumber(), ADDRESS_ID = 119, CUSTOMER_TYPE_ID = 3 });
					context.CUSTOMERs.Add(new CUSTOMER() { ACCOUNT_ID = accountId++, NAME = "Longso Biler", EMAIL = "longso@biler.dk", PHONE = CreatePhoneNumber(), ADDRESS_ID = 132, CUSTOMER_TYPE_ID = 10 });
					context.CUSTOMERs.Add(new CUSTOMER() { ACCOUNT_ID = accountId++, NAME = "Q8", EMAIL = "q8kolding@petrol.dk", PHONE = CreatePhoneNumber(), ADDRESS_ID = 135, CUSTOMER_TYPE_ID = 18 });
					context.CUSTOMERs.Add(new CUSTOMER() { ACCOUNT_ID = accountId++, NAME = "Netto", EMAIL = "kolding@netto.dk", PHONE = CreatePhoneNumber(), ADDRESS_ID = 139, CUSTOMER_TYPE_ID = 3 });

					surname = CreateSurname();
					name = CreateName();
					context.CUSTOMERs.Add(new CUSTOMER()
					{
						ACCOUNT_ID = accountId++,
						SURNAME = surname,
						NAME = name,
						EMAIL = CreateEmail(surname, name),
						PHONE = CreatePhoneNumber(),
						ADDRESS_ID = 205,
						CUSTOMER_TYPE_ID = 9
					});

					context.CUSTOMERs.Add(new CUSTOMER() { ACCOUNT_ID = accountId++, NAME = "Individuals With Asperger Syndrome", EMAIL = "hospitalasperger@org.dk", PHONE = CreatePhoneNumber(), ADDRESS_ID = 206, CUSTOMER_TYPE_ID = 7 });

					surname = CreateSurname();
					name = CreateName();
					context.CUSTOMERs.Add(new CUSTOMER()
					{
						ACCOUNT_ID = accountId++,
						SURNAME = surname,
						NAME = name,
						EMAIL = CreateEmail(surname, name),
						PHONE = CreatePhoneNumber(),
						ADDRESS_ID = 207,
						CUSTOMER_TYPE_ID = 2
					});

					context.CUSTOMERs.Add(new CUSTOMER() { ACCOUNT_ID = accountId++, NAME = "Hostel For Individuals With Asperger Syndrome", EMAIL = "hostelasperger@org.dk", PHONE = CreatePhoneNumber(), ADDRESS_ID = 208, CUSTOMER_TYPE_ID = 22 });

					surname = CreateSurname();
					name = CreateName();
					context.CUSTOMERs.Add(new CUSTOMER()
					{
						ACCOUNT_ID = accountId++,
						SURNAME = surname,
						NAME = name,
						EMAIL = CreateEmail(surname, name),
						PHONE = CreatePhoneNumber(),
						ADDRESS_ID = 209,
						CUSTOMER_TYPE_ID = 2
					});

					for (int i = 210; i < 215; i++)
					{
						surname = CreateSurname();
						name = CreateName();
						context.CUSTOMERs.Add(new CUSTOMER()
						{
							ACCOUNT_ID = accountId++,
							SURNAME = surname,
							NAME = name,
							EMAIL = CreateEmail(surname, name),
							PHONE = CreatePhoneNumber(),
							ADDRESS_ID = i,
							CUSTOMER_TYPE_ID = 1
						});
					}

					surname = CreateSurname();
					name = CreateName();
					context.CUSTOMERs.Add(new CUSTOMER()
					{
						ACCOUNT_ID = accountId++,
						SURNAME = surname,
						NAME = name,
						EMAIL = CreateEmail(surname, name),
						PHONE = CreatePhoneNumber(),
						ADDRESS_ID = 216,
						CUSTOMER_TYPE_ID = 2
					});

					for (int i = 222; i < 226; i++)
					{
						surname = CreateSurname();
						name = CreateName();
						context.CUSTOMERs.Add(new CUSTOMER()
						{
							ACCOUNT_ID = accountId++,
							SURNAME = surname,
							NAME = name,
							EMAIL = CreateEmail(surname, name),
							PHONE = CreatePhoneNumber(),
							ADDRESS_ID = i,
							CUSTOMER_TYPE_ID = 1
						});
					}


					for (int i = 227; i < 231; i++)
					{
						surname = CreateSurname();
						name = CreateName();
						context.CUSTOMERs.Add(new CUSTOMER()
						{
							ACCOUNT_ID = accountId++,
							SURNAME = surname,
							NAME = name,
							EMAIL = CreateEmail(surname, name),
							PHONE = CreatePhoneNumber(),
							ADDRESS_ID = i,
							CUSTOMER_TYPE_ID = 1
						});
					}

					for (int i = 232; i < 236; i++)
					{
						surname = CreateSurname();
						name = CreateName();
						context.CUSTOMERs.Add(new CUSTOMER()
						{
							ACCOUNT_ID = accountId++,
							SURNAME = surname,
							NAME = name,
							EMAIL = CreateEmail(surname, name),
							PHONE = CreatePhoneNumber(),
							ADDRESS_ID = i,
							CUSTOMER_TYPE_ID = 1
						});
					}

					context.CUSTOMERs.Add(new CUSTOMER() { ACCOUNT_ID = accountId++, NAME = "Jens Jacobsen AutoVaerksted", EMAIL = "jensjacobsenr@yahoo.dk", PHONE = CreatePhoneNumber(), ADDRESS_ID = 267, CUSTOMER_TYPE_ID = 17 });
					context.CUSTOMERs.Add(new CUSTOMER() { ACCOUNT_ID = accountId++, NAME = "Audi Centre", EMAIL = "odense@audi.dk", PHONE = CreatePhoneNumber(), ADDRESS_ID = 705, CUSTOMER_TYPE_ID = 3 });
					context.CUSTOMERs.Add(new CUSTOMER() { ACCOUNT_ID = accountId++, NAME = "Statoil", EMAIL = "odensemdfvej@statoil.dk", PHONE = CreatePhoneNumber(), ADDRESS_ID = 708, CUSTOMER_TYPE_ID = 3 });

					for (int i = 720; i < 728; i++)
					{
						surname = CreateSurname();
						name = CreateName();
						context.CUSTOMERs.Add(new CUSTOMER()
						{
							ACCOUNT_ID = accountId++,
							SURNAME = surname,
							NAME = name,
							EMAIL = CreateEmail(surname, name),
							PHONE = CreatePhoneNumber(),
							ADDRESS_ID = i,
							CUSTOMER_TYPE_ID = 1
						});
					}

					for (int i = 729; i < 737; i++)
					{
						surname = CreateSurname();
						name = CreateName();
						context.CUSTOMERs.Add(new CUSTOMER()
						{
							ACCOUNT_ID = accountId++,
							SURNAME = surname,
							NAME = name,
							EMAIL = CreateEmail(surname, name),
							PHONE = CreatePhoneNumber(),
							ADDRESS_ID = i,
							CUSTOMER_TYPE_ID = 1
						});
					}

					surname = CreateSurname();
					name = CreateName();
					context.CUSTOMERs.Add(new CUSTOMER()
					{
						ACCOUNT_ID = accountId++,
						SURNAME = surname,
						NAME = name,
						EMAIL = CreateEmail(surname, name),
						PHONE = CreatePhoneNumber(),
						ADDRESS_ID = 778,
						CUSTOMER_TYPE_ID = 2
					});

					context.CUSTOMERs.Add(new CUSTOMER() { ACCOUNT_ID = accountId++, NAME = "Unik Haarmode", EMAIL = "unikhaarmoder@yahoo.dk", PHONE = CreatePhoneNumber(), ADDRESS_ID = 780, CUSTOMER_TYPE_ID = 19 });
					context.CUSTOMERs.Add(new CUSTOMER() { ACCOUNT_ID = accountId++, NAME = "Dame Herre", EMAIL = "dameherre@yahoo.dk", PHONE = CreatePhoneNumber(), ADDRESS_ID = 784, CUSTOMER_TYPE_ID = 19 });
					context.CUSTOMERs.Add(new CUSTOMER() { ACCOUNT_ID = accountId++, NAME = "Fotex", EMAIL = "odense@fotex.dk", PHONE = CreatePhoneNumber(), ADDRESS_ID = 788, CUSTOMER_TYPE_ID = 3 });
					context.CUSTOMERs.Add(new CUSTOMER() { ACCOUNT_ID = accountId++, NAME = "Lund`b", EMAIL = "lundsodense@mail.dk", PHONE = CreatePhoneNumber(), ADDRESS_ID = 824, CUSTOMER_TYPE_ID = 6 });

					for (int i = 834; i < 852; i++)
					{
						surname = CreateSurname();
						name = CreateName();
						context.CUSTOMERs.Add(new CUSTOMER()
						{
							ACCOUNT_ID = accountId++,
							SURNAME = surname,
							NAME = name,
							EMAIL = CreateEmail(surname, name),
							PHONE = CreatePhoneNumber(),
							ADDRESS_ID = i,
							CUSTOMER_TYPE_ID = 1
						});
					}
					context.SaveChanges();
				}
			}
		}

		public static void FillCustomersWithOutImages()
		{
			using (var context = new TownUtilityBillSystemV2Entities())
			{
				string surname;
				string name;
				List<int> linkedAddressIds = new List<int>();
				int customerTypeCount = context.CUSTOMER_TYPEs.Count();

				var customersWithImagesDB = context.CUSTOMERs.ToList();

				foreach (var c in customersWithImagesDB)
					linkedAddressIds.Add(c.ADDRESS_ID);

				var emptyAddressesDB = context.ADDRESSes.ToList();

				foreach (var a in emptyAddressesDB)
				{
					if (!linkedAddressIds.Contains(a.ID))
					{
						surname = CreateSurname();
						name = CreateName();
						context.CUSTOMERs.Add(new CUSTOMER()
						{
							ACCOUNT_ID = accountId++,
							SURNAME = surname,
							NAME = name,
							EMAIL = CreateEmail(surname, name),
							PHONE = CreatePhoneNumber(),
							ADDRESS_ID = a.ID,
							CUSTOMER_TYPE_ID = rnd.Next(1, customerTypeCount)
						});
					}
				}
				context.SaveChanges();
			}
		}

		public static void FillTemperatures()
		{
			using (var context = new TownUtilityBillSystemV2Entities())
			{
				DateTime date = new DateTime();
				DateTime presentDate = DateTime.Today;
				int startYear = 2016;
				int startMonth = 11;
				int startDay = 1;
				int rndMin = -3;
				int rndMax = 3;
				int minTemp = 1;
				int maxTemp = 2;
				int month;

				int[,] averageTemperaturesPerMonth = new int[12, 3] {
																		{ (int)Months.January, -1 , 4 },
																		{ (int)Months.February, -1 , 4 },
																		{ (int)Months.March, 0 ,  7},
																		{ (int)Months.April,  3, 11},
																		{(int)Months.May ,  7,  16},
																		{ (int)Months.June,  11,  19},
																		{ (int)Months.July,  13,  22},
																		{ (int)Months.August,  13,  22},
																		{ (int)Months.September,  10,  17},
																		{ (int)Months.October,  6,  12},
																		{ (int)Months.November,  3, 7 },
																		{ (int)Months.December,  0, 5 },
																	};
				int rowLength = averageTemperaturesPerMonth.GetLength(0);
				int colLength = averageTemperaturesPerMonth.GetLength(1);

				var townsDB = context.TOWNs.ToList();

				if (!context.TEMPERATUREs.Any())
				{
					foreach (var t in townsDB)
					{
						date = new DateTime(startYear, startMonth, startDay);

						for (; date < presentDate; date = date.AddDays(1))
						{
							month = date.Month - 1;
							context.TEMPERATUREs.Add(new TEMPERATURE()
							{
								DATE = date,
								TOWN_ID = t.ID,
								MINVALUE = averageTemperaturesPerMonth[month, minTemp] + rnd.Next(rndMin, rndMax),
								MAXVALUE = averageTemperaturesPerMonth[month, maxTemp] + rnd.Next(rndMin, rndMax)
							});
						}

						context.SaveChanges();
					}
				}
			}
		}

		public static void FillMeterTypes()
		{
			using (var context = new TownUtilityBillSystemV2Entities())
			{
				if (!context.METER_TYPEs.Any())
				{
					context.METER_TYPEs.Add(new METER_TYPE() { NAME = "Itron CENTRON", VARIFICATION_PERIOD_YEARS = 4, UTILITY_ID = 1 });
					context.METER_TYPEs.Add(new METER_TYPE() { NAME = "LANDIS & GYR (L&G), type ALF", VARIFICATION_PERIOD_YEARS = 3, UTILITY_ID = 1 });
					context.METER_TYPEs.Add(new METER_TYPE() { NAME = "General Electric I210+", VARIFICATION_PERIOD_YEARS = 5, UTILITY_ID = 1 });
					context.METER_TYPEs.Add(new METER_TYPE() { NAME = "Schneider Electric PM1200", VARIFICATION_PERIOD_YEARS = 5, UTILITY_ID = 1 });
					context.METER_TYPEs.Add(new METER_TYPE() { NAME = "NEPTUNE E-Coder", VARIFICATION_PERIOD_YEARS = 3, UTILITY_ID = 2 });
					context.METER_TYPEs.Add(new METER_TYPE() { NAME = "ULTRASONIC STUF-280W", VARIFICATION_PERIOD_YEARS = 3, UTILITY_ID = 2 });
					context.METER_TYPEs.Add(new METER_TYPE() { NAME = "Elster Ultrasonic F96", VARIFICATION_PERIOD_YEARS = 4, UTILITY_ID = 3 });
					context.METER_TYPEs.Add(new METER_TYPE() { NAME = "Ista C03P", VARIFICATION_PERIOD_YEARS = 4, UTILITY_ID = 3 });
					context.METER_TYPEs.Add(new METER_TYPE() { NAME = "Multical 302", VARIFICATION_PERIOD_YEARS = 3, UTILITY_ID = 3 });
					context.METER_TYPEs.Add(new METER_TYPE() { NAME = "TaleXus ACE9000 KBD", VARIFICATION_PERIOD_YEARS = 4, UTILITY_ID = 4 });
					context.METER_TYPEs.Add(new METER_TYPE() { NAME = "Kita FDC-4000", VARIFICATION_PERIOD_YEARS = 3, UTILITY_ID = 4 });

					context.SaveChanges();
				}
			}
		}

		public static void FillMeters()
		{
			Dictionary<int, int> varifPeriods = new Dictionary<int, int>();
			DateTime releaseDate = new DateTime();
			DateTime varifDate = new DateTime();
			int meterTypeId;
			int meterElQty = 0;
			int meterWaterQty = 0;
			int meterHeatQty = 0;
			int meterGasQty = 0;

			using (var context = new TownUtilityBillSystemV2Entities())
			{
				if (!context.METERs.Any())
				{
					List<int> idsBuildingDB = new List<int>();
					List<int> idsAddressesWithFourMeters = new List<int>();
					List<int> idsAddressesWithNotAllMeters = new List<int>();
					var meterTypesDB = context.METER_TYPEs.ToList();
					var buildingsWithImagesDB = context.BUILDINGs.Where(b => b.IMAGE_ID.HasValue).ToList();
					var addressesDB = context.ADDRESSes.ToList();
					int minMeterQty = 2;
					int maxMeterQty = 4;
					int meterQty;

					foreach (var type in meterTypesDB)
					{
						varifPeriods.Add(type.ID, type.VARIFICATION_PERIOD_YEARS);

						if (type.UTILITY_ID == (int)Utilities.Electricity)
							meterElQty++;
						else if (type.UTILITY_ID == (int)Utilities.Water)
							meterWaterQty++;
						else if (type.UTILITY_ID == (int)Utilities.Heating)
							meterHeatQty++;
						else
							meterGasQty++;
					}

					int utilIdOneIndexes = meterElQty;
					int utilIdTwoIndexes = utilIdOneIndexes + meterWaterQty;
					int utilIdThreeIndexes = utilIdTwoIndexes + meterHeatQty;
					int utilIdFourIndexes = utilIdThreeIndexes + meterGasQty;

					foreach (var b in buildingsWithImagesDB)
						idsBuildingDB.Add(b.ID);

					foreach (var a in addressesDB)
					{
						if (idsBuildingDB.Contains(a.BUILDING_ID))
							idsAddressesWithFourMeters.Add(a.ID);
						else
							idsAddressesWithNotAllMeters.Add(a.ID);
					}

					maxMeterQty += 1;

					#region AddressesWithFourMeters
					foreach (var a in idsAddressesWithFourMeters)
					{
						for (int j = 1; j < maxMeterQty; j++)
						{
							meterTypeId = GetRandomMeterTypeId(j, utilIdOneIndexes, utilIdTwoIndexes, utilIdThreeIndexes, utilIdFourIndexes);
							releaseDate = GetRandomReleaseDate();
							varifDate = GetVarificationDate(releaseDate, varifPeriods, meterTypeId);

							context.METERs.Add(new METER()
							{
								SERIAL_NUMBER = CreateMeterNumber(),
								ADDRESS_ID = a,
								METER_TYPE_ID = meterTypeId,
								RELEASE_DATE = releaseDate,
								VARIFICATION_DATE = varifDate
							});
						}
					}
					#endregion

					#region AddressesWithNotAllMeters
					foreach (var a in idsAddressesWithNotAllMeters)
					{
						meterQty = rnd.Next(minMeterQty, maxMeterQty);
						meterQty++;

						for (int j = 1; j < meterQty; j++)
						{
							meterTypeId = GetRandomMeterTypeId(j, utilIdOneIndexes, utilIdTwoIndexes, utilIdThreeIndexes, utilIdFourIndexes);
							releaseDate = GetRandomReleaseDate();
							varifDate = GetVarificationDate(releaseDate, varifPeriods, meterTypeId);

							context.METERs.Add(new METER()
							{
								SERIAL_NUMBER = CreateMeterNumber(),
								ADDRESS_ID = a,
								METER_TYPE_ID = meterTypeId,
								RELEASE_DATE = releaseDate,
								VARIFICATION_DATE = varifDate
							});
						}
					}
					#endregion

					context.SaveChanges();
				}
			}
		}

		public static void FillMeterItems()
		{
			DateTime date = new DateTime();
			DateTime presentDate = DateTime.Today;
			int startYear = 2017;
			int startMonth = 1;
			int startDay = 1;
			int metersWithBuildingImagesDisplayCount = 380;
			int metersWithBuildingWithOutImagesDisplayCount = 20;

			float utilityUsagePerSqMeter;

			using (var context = new TownUtilityBillSystemV2Entities())
			{
				List<int> idsBuildingDB = new List<int>();
				List<int> idsAddressesWithFourMeters = new List<int>();
				List<int> idsAddressesWithNotAllMeters = new List<int>();

				float minUsageFactor = 0.77F;
				float maxUsageFactor = 1.18F;
				int rndMinStartMeterValue = 2200;
				int rndMaxStartMeterValue = 8900;
				float usageFactor;
				float presMeterValue;
				float prevMeterValue;
				float heatingMonthFactor;

				var meterTypesDB = context.METER_TYPEs.ToList();
				var buildingsWithImagesDB = context.BUILDINGs.Where(b => b.IMAGE_ID.HasValue).ToList();
				var addressesDB = context.ADDRESSes.ToList();
				var metersDB = context.METERs.ToList();
				var utilitiesDB = context.UTILITYs.ToList();

				foreach (var b in buildingsWithImagesDB)
					idsBuildingDB.Add(b.ID);

				foreach (var a in addressesDB)
				{
					if (idsBuildingDB.Contains(a.BUILDING_ID))
						idsAddressesWithFourMeters.Add(a.ID);
					else
						idsAddressesWithNotAllMeters.Add(a.ID);
				}

				var metersWithBuildingImagesDB = metersDB.Where(m => idsAddressesWithFourMeters.Contains(m.ADDRESS_ID)).ToList();

				var metersWithBuildingWithOutImagesDB = metersDB.Where(m => idsAddressesWithNotAllMeters.Contains(m.ADDRESS_ID)).ToList();

				//need to fill all meters
				metersWithBuildingImagesDB.RemoveRange(metersWithBuildingImagesDisplayCount, metersWithBuildingImagesDB.Count - metersWithBuildingImagesDisplayCount); // need to fill all meters data
				metersWithBuildingWithOutImagesDB.RemoveRange(metersWithBuildingWithOutImagesDisplayCount, metersWithBuildingWithOutImagesDB.Count - metersWithBuildingWithOutImagesDisplayCount);

				var metersToFill = metersWithBuildingImagesDB;
				metersToFill = metersToFill.Concat(metersWithBuildingWithOutImagesDB).ToList();

				foreach (var meter in metersToFill)
				{
					var addressDB = context.ADDRESSes.Find(meter.ADDRESS_ID);
					var buildingDB = context.BUILDINGs.Find(addressDB.BUILDING_ID);
					var meterTypeDB = context.METER_TYPEs.Find(meter.METER_TYPE_ID);
					var utilityDB = context.UTILITYs.Find(meterTypeDB.UTILITY_ID);
					float square = 0;

					if (buildingDB.SQUARE.HasValue)
						square = (float)buildingDB.SQUARE;

					utilityUsagePerSqMeter = GetUtilityUsagePerSqMeter(utilityDB.ID);
					prevMeterValue = rnd.Next(rndMinStartMeterValue, rndMaxStartMeterValue);
					date = new DateTime(startYear, startMonth, startDay);

					for (; date <= presentDate; date = date.AddMonths(1))
					{
						usageFactor = GetRandomDoubleNumber(minUsageFactor, maxUsageFactor);

						if (utilityDB.ID == (int)Utilities.Heating)
						{
							heatingMonthFactor = GetHeatingMonthFactor(date, meter.ADDRESS.TOWN_ID);
							presMeterValue = (float)Convert.ToDouble(square * utilityUsagePerSqMeter * heatingMonthFactor);
						}
						else
							presMeterValue = (float)Convert.ToDouble(square * utilityUsagePerSqMeter * usageFactor);

						prevMeterValue += presMeterValue;

						context.METER_ITEMs.Add(new METER_ITEM()
						{
							METER_ID = meter.ID,
							DATE = date,
							VALUE = prevMeterValue
						});
					}
				}

				context.SaveChanges();
			}
		}

		public static void FillBills()
		{
			DateTime date = new DateTime();
			DateTime presentDate = DateTime.Today;
			DateTime billDate = new DateTime();
			DateTime paidMonth = new DateTime();
			DateTime monthBeforePresent = new DateTime();
			int days = presentDate.Day;
			int billsForCalculationCount = 200;
			float prevValue = 0;
			float presValue = 0;
			int startYear = 2018;
			int startMonth = 01;
			int startDay = 1;
			int minAddDay = 0;
			int maxAddDay = 2;
			string period;
			decimal sum = 0;
			bool paidFlag;
			string number;
			decimal deltaValue;

			date = new DateTime(startYear, startMonth, startDay);
			monthBeforePresent = presentDate.AddMonths(-1).AddDays(-(--days));

			using (var context = new TownUtilityBillSystemV2Entities())
			{
				var customersDB = context.CUSTOMERs.ToList();
				customersDB.RemoveRange(billsForCalculationCount, customersDB.Count - billsForCalculationCount); // delete some customers ---> need to fill all customers data

				var utilityWithTwoPrices = context.UTILITYs.Find((int)Utilities.Electricity);
				decimal minUsageForStandartPrice = (decimal)utilityWithTwoPrices.USAGEFORSTANDARTPRICE;
				decimal bigUsagePrice = (decimal)utilityWithTwoPrices.BIGUSAGEPRICE;

				for (; date < presentDate; date = date.AddMonths(1))
				{
					billDate = date.AddDays(rnd.Next(minAddDay, maxAddDay));
					paidMonth = date.AddMonths(-1);

					period = paidMonth.ToString("yyyy-MM");

					foreach (var c in customersDB)
					{
						ADDRESS address = new ADDRESS();
						ACCOUNT account = new ACCOUNT();
						METER_TYPE meterType = new METER_TYPE();
						UTILITY utility = new UTILITY();
						var metersDB = context.METERs.ToList();
						var meterItemsDB = context.METER_ITEMs.ToList();
						var meterTypesDB = context.METER_TYPEs.ToList();

						sum = 0;

						address = context.ADDRESSes.Find(c.ADDRESS_ID);
						account = context.ACCOUNTs.Find(c.ACCOUNT_ID);
						var metersForAddress = metersDB.Where(meter => meter.ADDRESS_ID == address.ID).ToList();

						foreach (var m in metersForAddress)
						{
							meterType = context.METER_TYPEs.Find(m.METER_TYPE_ID);
							utility = context.UTILITYs.Find(meterType.UTILITY_ID);
							prevValue = 0;
							presValue = 0;

							var meterItemsForExactMeter = meterItemsDB.Where(item => item.METER_ID == m.ID).ToList();

							foreach (var item in meterItemsForExactMeter)
							{
								if (item.DATE == paidMonth)
									prevValue = item.VALUE;
								else if (item.DATE == date)
									presValue = item.VALUE;
							}
							deltaValue = (decimal)(presValue - prevValue);

							if (utility.NAME == Utilities.Electricity.ToString())
							{
								if (deltaValue > minUsageForStandartPrice)
									sum += (decimal)utility.PRICE * minUsageForStandartPrice + bigUsagePrice * (deltaValue - minUsageForStandartPrice);
							}
							else
								sum += (decimal)utility.PRICE * deltaValue;
						}

						if (paidMonth == monthBeforePresent)
							paidFlag = false;
						else
							paidFlag = true;

						number = account.NUMBER + "-" + period.Replace("-", "");

						if (sum != 0)
							context.BILLs.Add(new BILL() { NUMBER = number, ACCOUNT_ID = c.ACCOUNT_ID, DATE = billDate, PERIOD = period, SUM = sum, PAID = paidFlag });
					}
				}
				context.SaveChanges();
			}
		}

		public static void FillPayments()
		{
			using (var context = new TownUtilityBillSystemV2Entities())
			{
				var billsDB = context.BILLs.Where(b => b.PAID).ToList();

				foreach (BILL b in billsDB)
				{
					context.PAYMENTs.Add(new PAYMENT()
					{
						ID = Guid.NewGuid(),
						SUM = b.SUM,
						DATE = b.DATE.AddDays(rnd.Next(1, 4)),
						NOTE = String.Format("{0} {1} {2}{3}", Localization.Payment, Localization.For, Localization.BillNum, b.NUMBER),
						ACCOUNT_ID = b.ACCOUNT_ID
					});
				}

				context.SaveChanges();
			}
		}

		public static void FillNewsImages()
		{
			using (var context = new TownUtilityBillSystemV2Entities())
			{
				if (!context.IMAGE_NEWS.Any())
				{
					context.IMAGE_NEWS.Add(new IMAGE_NEWS() { PATH = AppRootPath + @"Content\Images\News\PlantNight.jpg" });
					context.IMAGE_NEWS.Add(new IMAGE_NEWS() { PATH = AppRootPath + @"Content\Images\News\Investment.jpg" });
					context.IMAGE_NEWS.Add(new IMAGE_NEWS() { PATH = AppRootPath + @"Content\Images\News\NewBoardMember.jpg" });
					context.IMAGE_NEWS.Add(new IMAGE_NEWS() { PATH = AppRootPath + @"Content\Images\News\WindMills.jpg" });
					context.IMAGE_NEWS.Add(new IMAGE_NEWS() { PATH = AppRootPath + @"Content\Images\News\Plant.jpg" });

					context.SaveChanges();
				}
			}
		}

		public static void FillNews()
		{
			using (var context = new TownUtilityBillSystemV2Entities())
			{
				if (!context.NEWS.Any())
				{
					context.NEWS.Add(new NEWS()
					{
						NAME = "SaleOf10509527ExistingSharesInDONGEnergy",
						DATE = new DateTime(2018, 05, 21, 12, 10, 00),
						IMAGE_ID = 1
					});
					context.NEWS.Add(new NEWS()
					{
						NAME = "MajorShareholderNotificationNewEnergyInvestment",
						DATE = new DateTime(2018, 04, 16, 10, 11, 00),
						IMAGE_ID = 2
					});
					context.NEWS.Add(new NEWS()
					{
						NAME = "TheNominationCommitteeOfDONGEnergyRecommendsNewMemberForTheBoardOfDirectors",
						DATE = new DateTime(2018, 03, 15, 17, 05, 00),
						IMAGE_ID = 3
					});
					context.NEWS.Add(new NEWS()
					{
						NAME = "DONGEnergyAwardedThreeGermanOffshoreWindProjects",
						DATE = new DateTime(2018, 03, 02, 10, 32, 00),
						IMAGE_ID = 4
					});
					context.NEWS.Add(new NEWS()
					{
						NAME = "ResolutionsFromTheAnnualGeneralMeetingofDONGEnergy",
						DATE = new DateTime(2018, 02, 05, 09, 45, 00),
						IMAGE_ID = 5
					});

					context.SaveChanges();
				}
			}
		}

		public static void FillNewsTitles()
		{
			using (var context = new TownUtilityBillSystemV2Entities())
			{
				if (!context.NEWS_TITLEs.Any())
				{
					context.NEWS_TITLEs.Add(new NEWS_TITLE()
					{
						TITLE = "Sale of 10,509,527 existing shares in DONG Energy A/S by New Energy Investment",
						LANGUAGE = "en",
						NEWS_ID = 1
					});

					context.NEWS_TITLEs.Add(new NEWS_TITLE()
					{
						TITLE = "Salg af 10.509.527 eksisterende aktier i DONG Energy A / S af New Energy Investment",
						LANGUAGE = "da",
						NEWS_ID = 1
					});

					context.NEWS_TITLEs.Add(new NEWS_TITLE()
					{
						TITLE = "Verkauf von 10.509.527 bestehenden Aktien an DONG Energy A / S durch New Energy Investment",
						LANGUAGE = "de",
						NEWS_ID = 1
					});

					context.NEWS_TITLEs.Add(new NEWS_TITLE()
					{
						TITLE = "Major Shareholder notification – New Energy Investment",
						LANGUAGE = "en",
						NEWS_ID = 2
					});

					context.NEWS_TITLEs.Add(new NEWS_TITLE()
					{
						TITLE = "Major Shareholder Notification - Ny Energi Investering",
						LANGUAGE = "da",
						NEWS_ID = 2
					});

					context.NEWS_TITLEs.Add(new NEWS_TITLE()
					{
						TITLE = "Mitteilung wichtiger Aktionäre - Neue Energieinvestitionen",
						LANGUAGE = "de",
						NEWS_ID = 2
					});

					context.NEWS_TITLEs.Add(new NEWS_TITLE()
					{
						TITLE = "The Nomination Committee of DONG Energy A/S recommends new member for the Board of Directors",
						LANGUAGE = "en",
						NEWS_ID = 3
					});

					context.NEWS_TITLEs.Add(new NEWS_TITLE()
					{
						TITLE = "Valgkomiteen for DONG Energy A / S anbefaler nyt medlem for bestyrelsen",
						LANGUAGE = "da",
						NEWS_ID = 3
					});

					context.NEWS_TITLEs.Add(new NEWS_TITLE()
					{
						TITLE = "Der Nominierungsausschuss von DONG Energy A / S empfiehlt ein neues Mitglied für den Verwaltungsrat",
						LANGUAGE = "de",
						NEWS_ID = 3
					});

					context.NEWS_TITLEs.Add(new NEWS_TITLE()
					{
						TITLE = "DONG Energy awarded three German offshore wind projects",
						LANGUAGE = "en",
						NEWS_ID = 4
					});

					context.NEWS_TITLEs.Add(new NEWS_TITLE()
					{
						TITLE = "DONG Energy tildelte tre tyske offshore vindprojekter",
						LANGUAGE = "da",
						NEWS_ID = 4
					});

					context.NEWS_TITLEs.Add(new NEWS_TITLE()
					{
						TITLE = "DONG Energy hat drei deutsche Offshore-Windprojekte ausgezeichnet",
						LANGUAGE = "de",
						NEWS_ID = 4
					});

					context.NEWS_TITLEs.Add(new NEWS_TITLE()
					{
						TITLE = "Resolutions from the Annual General Meeting of DONG Energy A/S",
						LANGUAGE = "en",
						NEWS_ID = 5
					});

					context.NEWS_TITLEs.Add(new NEWS_TITLE()
					{
						TITLE = "Beslutninger fra DONG Energy A/S 'ordinære generalforsamling",
						LANGUAGE = "da",
						NEWS_ID = 5
					});

					context.NEWS_TITLEs.Add(new NEWS_TITLE()
					{
						TITLE = "Beschlüsse der Hauptversammlung der DONG Energy A/S",
						LANGUAGE = "de",
						NEWS_ID = 5
					});

					context.SaveChanges();
				}
			}
		}

		public static void FillNewsChapters()
		{
			using (var context = new TownUtilityBillSystemV2Entities())
			{
				if (!context.NEWS_CHAPTERs.Any())
				{
					#region NewsChaptersInEnglish

					#region News#1

					context.NEWS_CHAPTERs.Add(new NEWS_CHAPTER()
					{
						NEWS_ID = 1,
						TEXT = "THE SECURITIES DESCRIBED HEREIN HAVE NOT BEEN AND WILL NOT BE REGISTERED UNDER THE U.S. SECURITIES ACT OF 1933, AS AMENDED, AND MAY NOT BE OFFERED OR SOLD IN THE UNITED STATES ABSENT REGISTRATION OR AN APPLICABLE EXEMPTION FROM REGISTRATION REQUIREMENTS.",
						LANGUAGE = "en"
					});

					context.NEWS_CHAPTERs.Add(new NEWS_CHAPTER()
					{
						NEWS_ID = 1,
						TEXT = "With reference to the announcement made on 3 April, 2018, DONG Energy A/S (‘DONG Energy’) (NASDAQ OMX: DENERG), has received the following information from New Energy Investment S.à.r.l. (‘NEI’):",
						LANGUAGE = "en"
					});

					context.NEWS_CHAPTERs.Add(new NEWS_CHAPTER()
					{
						NEWS_ID = 1,
						TEXT = "NEI has agreed to sell 10,509,527 existing shares in DONG Energy, equivalent to 2.5% of the existing shares in DONG Energy to institutional investors at a price of DKK 270 per share, pursuant to an accelerated bookbuild offering (the ‘Transaction’). NEI is a Luxembourg company indirectly owned by entities under the control of the Merchant Banking Division of The Goldman Sachs Group, Inc.",
						LANGUAGE = "en"
					});

					context.NEWS_CHAPTERs.Add(new NEWS_CHAPTER()
					{
						NEWS_ID = 1,
						TEXT = "Following settlement of the Transaction, NEI will hold 18,935,215 shares in DONG Energy corresponding to 4.5% of the existing shares in DONG Energy.",
						LANGUAGE = "en"
					});

					context.NEWS_CHAPTERs.Add(new NEWS_CHAPTER()
					{
						NEWS_ID = 1,
						TEXT = "NEI has agreed to a 90-day lock-up period from yesterday, subject to waiver with the prior written consent of a certain manager conducting the bookbuilding process and to certain customary exceptions.",
						LANGUAGE = "en"
					});

					context.NEWS_CHAPTERs.Add(new NEWS_CHAPTER()
					{
						NEWS_ID = 1,
						TEXT = "DONG Energy will not receive any proceeds from the Transaction.",
						LANGUAGE = "en"
					});

					#endregion

					#region News#2
					context.NEWS_CHAPTERs.Add(new NEWS_CHAPTER()
					{
						NEWS_ID = 2,
						TEXT = "Pursuant to section 29 of the Danish Securities Trading Act, New Energy Investment S.à r.l. (‘NEI’) has notified DONG Energy A/S (‘DONG Energy’) that today, following an accelerated bookbuild offering carried out on 3 February 2018, NEI has agreed to dispose of 26,500,000 shares in DONG Energy to certain institutional investors, each with a nominal value of DKK 10.",
						LANGUAGE = "en"
					});

					context.NEWS_CHAPTERs.Add(new NEWS_CHAPTER()
					{
						NEWS_ID = 2,
						TEXT = "Following settlement of the bookbuild offering, NEI will hold 29,444,742 shares and voting rights in DONG Energy, corresponding to 7.0% of the issued shares and voting rights of DONG Energy.",
						LANGUAGE = "en"
					});

					context.NEWS_CHAPTERs.Add(new NEWS_CHAPTER()
					{
						NEWS_ID = 2,
						TEXT = "NEI is a limited liability company organised under the laws of Luxembourg under reg. no. B181487. The address of NEI is 2 Rue de Fossé, L-1536 Luxembourg, Grand Duchy of Luxembourg.",
						LANGUAGE = "en"
					});

					context.NEWS_CHAPTERs.Add(new NEWS_CHAPTER()
					{
						NEWS_ID = 2,
						TEXT = "As at the date hereof, NEI is controlled by New Energy I S.à r.l. (‘NE I’) and New Energy II S.à r.l. (‘NE II’). NE I, which possesses the majority of voting rights in NEI, and NE II are limited liability companies organised under the laws of Luxembourg and are controlled by entities which are under the control of the Merchant Banking Division of The Goldman Sachs Group, Inc.. These entities include Danish Energy Investors B, L.P., a Cayman Islands limited partnership, which possesses the majority of voting rights in NE I.",
						LANGUAGE = "en"
					});

					context.NEWS_CHAPTERs.Add(new NEWS_CHAPTER()
					{
						NEWS_ID = 2,
						TEXT = "The information provided in this announcement does not change DONG Energy’s previous financial guidance for the 2018 financial year.",
						LANGUAGE = "en"
					});

					#endregion

					#region News#3

					context.NEWS_CHAPTERs.Add(new NEWS_CHAPTER()
					{
						NEWS_ID = 3,
						TEXT = "Today, the Nomination Committee of DONG Energy A/S has decided to recommend that Dieter Wemmer be elected new member of the Board of Directors.",
						LANGUAGE = "en"
					});

					context.NEWS_CHAPTERs.Add(new NEWS_CHAPTER()
					{
						NEWS_ID = 3,
						TEXT = "Dieter Wemmer has worked in leading finance positions for more than two decades, most recently as Chief Financial Officer of Allianz SE and as board member of the UBS Group AG. He is highly experienced within capital markets, investments and risk management and combines a sharp financial insight with a strategic and operational mind-set.",
						LANGUAGE = "en"
					});

					context.NEWS_CHAPTERs.Add(new NEWS_CHAPTER()
					{
						NEWS_ID = 3,
						TEXT = "Thomas Thune Andersen, Chairman of the Board of Directors and the Nomination Committee of DONG Energy A/S, said:",
						LANGUAGE = "en"
					});

					context.NEWS_CHAPTERs.Add(new NEWS_CHAPTER()
					{
						NEWS_ID = 3,
						TEXT = "“I’m pleased that the Nomination Committee has recommended Dieter Wemmer as a new member of the Board of Directors. He holds very strong financial capabilities and adds strong experience within capital markets, investments and risk management to the board. I’m confident that he’ll be an asset for the Board.”",
						LANGUAGE = "en"
					});

					context.NEWS_CHAPTERs.Add(new NEWS_CHAPTER()
					{
						NEWS_ID = 3,
						TEXT = "Dieter Wemmer is expected to be elected for the Board of Directors at the annual general meeting in Q1 2018.",
						LANGUAGE = "en"
					});

					context.NEWS_CHAPTERs.Add(new NEWS_CHAPTER()
					{
						NEWS_ID = 3,
						TEXT = "The Board of Directors of DONG Energy A/S currently consists of six members elected by the general meeting. As previously announced, the long-term objective is to have eight members of the Board of Directors elected by the general meeting.",
						LANGUAGE = "en"
					});

					context.NEWS_CHAPTERs.Add(new NEWS_CHAPTER()
					{
						NEWS_ID = 3,
						TEXT = "Further information about Dieter Wemmer is enclosed below.",
						LANGUAGE = "en"
					});

					#endregion

					#region News#4

					context.NEWS_CHAPTERs.Add(new NEWS_CHAPTER()
					{
						NEWS_ID = 4,
						TEXT = "In the first of two German auction rounds, the Bundesnetzagentur has today awarded DONG Energy the right to build three offshore wind projects in the German North Sea. DONG Energy submitted six projects in the bid and won with the following three projects which have a total capacity of 590MW: OWP West (240MW), Borkum Riffgrund West 2 (240MW),  Gode Wind 3 (110MW).",
						LANGUAGE = "en"
					});

					context.NEWS_CHAPTERs.Add(new NEWS_CHAPTER()
					{
						NEWS_ID = 4,
						TEXT = "The three projects are planned to be commissioned in 2024, subject to Final Investment Decision by DONG Energy in 2021.",
						LANGUAGE = "en"
					});

					context.NEWS_CHAPTERs.Add(new NEWS_CHAPTER()
					{
						NEWS_ID = 4,
						TEXT = "Samuel Leupold, Executive Vice President and CEO of Wind Power at DONG Energy, says: “We’re pleased with being awarded three projects in the first of two German auction rounds, and we have good opportunities to add further capacity to our winning projects in next year’s German auction. Today’s results contribute to our ambition of driving profitable growth by adding approximately 5GW of additional capacity by 2025.”",
						LANGUAGE = "en"
					});

					context.NEWS_CHAPTERs.Add(new NEWS_CHAPTER()
					{
						NEWS_ID = 4,
						TEXT = "For two of the projects – OWP West and Borkum Riffgrund West 2 – DONG Energy made bids at zero EUR per MWh, i.e. these projects will not receive a subsidy on top of the wholesale electricity price. The Gode Wind 3 project was awarded based on a bid price of EUR 60 per MWh.",
						LANGUAGE = "en"
					});

					context.NEWS_CHAPTERs.Add(new NEWS_CHAPTER()
					{
						NEWS_ID = 4,
						TEXT = "Samuel Leupold says:“The zero subsidy bid is a breakthrough for the cost competitiveness of offshore wind, and it demonstrates the technology's massive global growth potential as a cornerstone in the economically viable shift to green energy systems. Cheaper clean energy will benefit governments and consumers – and not least help meet the Paris COP21 targets to fight climate change. Still it’s important to note that the zero bid is enabled by a number of circumstances in this auction. Most notably, the realization window is extended to 2024. This allows developers to apply the next generation turbine technology, which will support a major step down in costs. Also, the bid reflects the fact that grid connection is not included.”",
						LANGUAGE = "en"
					});

					context.NEWS_CHAPTERs.Add(new NEWS_CHAPTER()
					{
						NEWS_ID = 4,
						TEXT = "Samuel Leupold continues: “Financial discipline is key to us. We are of course reflecting the project’s exposure to market risk in the cost of capital applied. We see a solid value creation potential in this German project portfolio and will now begin to further mature the projects towards a Final Investment Decision (FID) in 2021.”",
						LANGUAGE = "en"
					});

					context.NEWS_CHAPTERs.Add(new NEWS_CHAPTER()
					{
						NEWS_ID = 4,
						TEXT = "Volker Malmen, country manager in DONG Energy Germany, says: “Making green energy cheaper than black has for years been part of DONG Energy's strategic ambition. Offshore wind is fully capable of replacing retiring power plants and to become the backbone of Germany’s energy transition, and I hope that today’s encouraging results will inspire an accelerated and higher volume build-out of offshore wind in Germany and motivate the electrification of transportation and heating.”",
						LANGUAGE = "en"
					});

					context.NEWS_CHAPTERs.Add(new NEWS_CHAPTER()
					{
						NEWS_ID = 4,
						TEXT = "DONG Energy will be responsible for the turbines, array cables and offshore substation, while grid operator TenneT will be responsible for construction, operation and ownership of the onshore substation and the export cable.",
						LANGUAGE = "en"
					});

					context.NEWS_CHAPTERs.Add(new NEWS_CHAPTER()
					{
						NEWS_ID = 4,
						TEXT = "DONG Energy currently has 902MW of offshore wind in operation in German waters with Gode Wind 1&2 and Borkum Riffgrund 1 and another 450MW under construction at Borkum Riffgrund 2, which is expected to be commissioned in 2019. In total, DONG Energy operates 3,600MW offshore wind capacity across Germany, UK and Denmark and has a further 3,800GW under construction.",
						LANGUAGE = "en"
					});

					context.NEWS_CHAPTERs.Add(new NEWS_CHAPTER()
					{
						NEWS_ID = 4,
						TEXT = "Platform change: Significantly bigger turbines – probably 13-15MW – will be on the market by 2024. With bigger turbines, the developer can increase electricity production while at the same time reduce the number of turbine positions. This contributes significantly to cost reductions during construction (fewer towers and array cables, and lower costs for installation vessels and manpower) as well as during a lifetime of operations and maintenance.",
						LANGUAGE = "en"
					});

					context.NEWS_CHAPTERs.Add(new NEWS_CHAPTER()
					{
						NEWS_ID = 4,
						TEXT = "Scale: OWP West and Borkum Riffgrund West 2 will be combined into one large-scale project with the option of adding additional volume in next year’s auction to further increase the total size of the project.",
						LANGUAGE = "en"
					});

					context.NEWS_CHAPTERs.Add(new NEWS_CHAPTER()
					{
						NEWS_ID = 4,
						TEXT = "Location: The projects benefit from average wind speeds of more than 10 m/s, which is among the highest wind speeds measured across DONG Energy’s portfolio of wind farms. Also, the projects are located next to DONG Energy’s Borkum Riffgrund 1&2 which means that operations and maintenance can be done from DONG Energy’s existing O&M hub in Norddeich.",
						LANGUAGE = "en"
					});

					context.NEWS_CHAPTERs.Add(new NEWS_CHAPTER()
					{
						NEWS_ID = 4,
						TEXT = "Extended lifetime: The German authorities have approved the possibility to extend the operational lifetime of the asset from 25 to 30 years.",
						LANGUAGE = "en"
					});

					context.NEWS_CHAPTERs.Add(new NEWS_CHAPTER()
					{
						NEWS_ID = 4,
						TEXT = "Not full scope: Developers were not bidding for the grid connection in the German auction, which means that grid connection is not included in the bid price.",
						LANGUAGE = "en"
					});

					context.NEWS_CHAPTERs.Add(new NEWS_CHAPTER()
					{
						NEWS_ID = 4,
						TEXT = "The above drivers deliver a cost-of-electricity below our forecasted wholesale power price and will allow us to create value and meet our return requirements at the expected market prices without subsidies. Compared to German power price forecasts available from leading research firms, we consider our price forecast to be relatively conservative. We have applied a higher cost-of-capital than in previous projects to reflect the potential increase in market price exposure. ",
						LANGUAGE = "en"
					});

					context.NEWS_CHAPTERs.Add(new NEWS_CHAPTER()
					{
						NEWS_ID = 4,
						TEXT = "The cost reductions required for a German project without subsidies are fully feasible, both technically and commercially. Towards a final investment decision in 2021, DONG Energy will monitor the key factors which will determine long-term power prices in Germany. These factors include the impact of EU actions to reinvigorate the European carbon trading scheme; the phase-out of conventional and nuclear capacity; the future role of coal in Europe; and the build-out of onshore transmission grids.",
						LANGUAGE = "en"
					});

					context.NEWS_CHAPTERs.Add(new NEWS_CHAPTER()
					{
						NEWS_ID = 4,
						TEXT = "The information provided in this announcement does not change DONG Energy’s previous financial guidance for the financial year of 2018 or the announced expected investment level for 2018.",
						LANGUAGE = "en"
					});

					#endregion

					#region News#5

					context.NEWS_CHAPTERs.Add(new NEWS_CHAPTER()
					{
						NEWS_ID = 5,
						TEXT = "Today, DONG Energy A/S held its Annual General Meeting where the following was adopted:",
						LANGUAGE = "en"
					});

					context.NEWS_CHAPTERs.Add(new NEWS_CHAPTER()
					{
						NEWS_ID = 5,
						TEXT = "DONG Energy’s audited annual report 2017 was approved.",
						LANGUAGE = "en"
					});


					context.NEWS_CHAPTERs.Add(new NEWS_CHAPTER()
					{
						NEWS_ID = 5,
						TEXT = "The distribution of profit according to the adopted annual report 2017 was approved. The dividend will amount to DKK 6 per share.",
						LANGUAGE = "en"
					});


					context.NEWS_CHAPTERs.Add(new NEWS_CHAPTER()
					{
						NEWS_ID = 5,
						TEXT = "Thomas Thune Andersen was re-elected as Chairman of the Board of Directors, Lene Skole was re-elected as Deputy Chairman of the Board of Directors, and Lynda Armstrong, Pia Gjellerup and Benny D. Loft were re-elected as members of the Board of Directors.",
						LANGUAGE = "en"
					});


					context.NEWS_CHAPTERs.Add(new NEWS_CHAPTER()
					{
						NEWS_ID = 5,
						TEXT = "Peter Korsholm was elected as new member of the Board of Directors.",
						LANGUAGE = "en"
					});


					context.NEWS_CHAPTERs.Add(new NEWS_CHAPTER()
					{
						NEWS_ID = 5,
						TEXT = "PricewaterhouseCoopers was re-appointed as auditor.",
						LANGUAGE = "en"
					});


					context.NEWS_CHAPTERs.Add(new NEWS_CHAPTER()
					{
						NEWS_ID = 5,
						TEXT = "The board remuneration for 2018 was approved.",
						LANGUAGE = "en"
					});

					context.NEWS_CHAPTERs.Add(new NEWS_CHAPTER()
					{
						NEWS_ID = 5,
						TEXT = "The Board of Directors and the Executive Board were discharged from their obligations.",
						LANGUAGE = "en"
					});

					#endregion

					#endregion

					#region NewsChaptersInDanish

					#region News#1

					context.NEWS_CHAPTERs.Add(new NEWS_CHAPTER()
					{
						NEWS_ID = 1,
						TEXT = "DE VÆRDIPAPIRER, DER ER BESKRIVET HER, ER IKKE OG VIL IKKE ER REGISTRERET UNDER VEDTAGEN AF USA'S VÆRDIPAPIR AF 1933, SOM ÆNDRET, OG MÅ IKKE TILBUDES ELLER SALGES I DEN FORENEDE STATES ABSENT REGISTRERING ELLER EN GÆLDENDE FRITAG FRA REGISTRATIONSKRAV.",
						LANGUAGE = "da"
					});

					context.NEWS_CHAPTERs.Add(new NEWS_CHAPTER()
					{
						NEWS_ID = 1,
						TEXT = "Med henvisning til meddelelsen af 3. april 2018 har DONG Energy A / S ('DONG Energy') (NASDAQ OMX: DENERG) modtaget følgende oplysninger fra New Energy Investment S.à.r.l. (NEI '):",
						LANGUAGE = "da"
					});

					context.NEWS_CHAPTERs.Add(new NEWS_CHAPTER()
					{
						NEWS_ID = 1,
						TEXT = "NEI har aftalt at sælge 10.509.527 eksisterende aktier i DONG Energy svarende til 2,5% af de eksisterende aktier i DONG Energy til institutionelle investorer til en pris på DKK 270 pr. Aktie i henhold til et accelereret bookbuild-tilbud (”Transaction”). NEI er et luxembourgsk selskab, der indirekte ejes af enheder under ledelse af Merchant Banking Division i The Goldman Sachs Group, Inc.",
						LANGUAGE = "da"
					});

					context.NEWS_CHAPTERs.Add(new NEWS_CHAPTER()
					{
						NEWS_ID = 1,
						TEXT = "Efter afvikling af transaktionen vil NEI besidde 18.935.215 aktier i DONG Energy svarende til 4,5% af de eksisterende aktier i DONG Energy.",
						LANGUAGE = "da"
					});

					context.NEWS_CHAPTERs.Add(new NEWS_CHAPTER()
					{
						NEWS_ID = 1,
						TEXT = "NEI har aftalt en 90-dages låseperiode fra i går, med forbehold af frafald med forudgående skriftligt samtykke fra en bestemt leder, der gennemfører bookbuildingprocessen og visse sædvanlige undtagelser.",
						LANGUAGE = "da"
					});

					context.NEWS_CHAPTERs.Add(new NEWS_CHAPTER()
					{
						NEWS_ID = 1,
						TEXT = "DONG Energy modtager ikke noget provenu fra Transaktionen.",
						LANGUAGE = "da"
					});

					#endregion

					#region News#2

					context.NEWS_CHAPTERs.Add(new NEWS_CHAPTER()
					{
						NEWS_ID = 2,
						TEXT = "I henhold til værdipapirhandelslovens § 29, New Energy Investment S.à r.l. ('NEI') har meddelt DONG Energy A / S ('DONG Energy'), at NEI efter et accelereret bookbuild-tilbud udført den 3. februar 2018 har aftalt at afhænde 26.500.000 aktier i DONG Energy til visse institutionelle investorer, hver med en pålydende værdi på 10 kr.",
						LANGUAGE = "da"
					});

					context.NEWS_CHAPTERs.Add(new NEWS_CHAPTER()
					{
						NEWS_ID = 2,
						TEXT = "Efter afvikling af bookbuild tilbyderne vil NEI besidde 29.444.742 aktier og stemmeret i DONG Energy svarende til 7,0% af DONG Energys udstedte aktier og stemmerettigheder.",
						LANGUAGE = "da"
					});

					context.NEWS_CHAPTERs.Add(new NEWS_CHAPTER()
					{
						NEWS_ID = 2,
						TEXT = "NEI er et aktieselskab, der er organiseret i henhold til luxembourgsk lovgivning under reg. ingen. B181487. NEI's adresse er 2 Rue de Fossé, L-1536 Luxembourg, Storhertugdømmet Luxembourg.",
						LANGUAGE = "da"
					});

					context.NEWS_CHAPTERs.Add(new NEWS_CHAPTER()
					{
						NEWS_ID = 2,
						TEXT = "På tidspunktet herfor er NEI kontrolleret af New Energy I S.à r.l. ('NE I') og New Energy II S.à.l. ('NE II'). NE I, som besidder størstedelen af stemmerettighederne i NEI og NE II, er aktieselskaber, der er organiseret i henhold til luxembourgsk lovgivning, og kontrolleres af enheder, der er under ledelse af Merchant Banking Division of The Goldman Sachs Group, Inc .. Disse enheder omfatter Danish Energy Investors B, LP, et Cayman Islands begrænset partnerskab, der besidder størstedelen af stemmerettighederne i NE I.",
						LANGUAGE = "da"
					});

					context.NEWS_CHAPTERs.Add(new NEWS_CHAPTER()
					{
						NEWS_ID = 2,
						TEXT = "Oplysningerne i denne meddelelse ændrer ikke DONG Energys tidligere finansielle vejledning for regnskabsåret 2018.",
						LANGUAGE = "da"
					});

					#endregion

					#region News#3

					context.NEWS_CHAPTERs.Add(new NEWS_CHAPTER()
					{
						NEWS_ID = 3,
						TEXT = "DONG Energy A / S har i dag valgt at anbefale, at Dieter Wemmer vælges som nyt medlem af bestyrelsen.",
						LANGUAGE = "da"
					});

					context.NEWS_CHAPTERs.Add(new NEWS_CHAPTER()
					{
						NEWS_ID = 3,
						TEXT = "Dieter Wemmer har arbejdet i ledende økonomistillinger i mere end to årtier, senest som finansdirektør for Allianz SE og som bestyrelsesmedlem i UBS Group AG. Han har stor erfaring inden for kapitalmarkeder, investeringer og risikostyring og kombinerer et skarpt økonomisk indsigt med et strategisk og operationelt sindssæt.",
						LANGUAGE = "da"
					});

					context.NEWS_CHAPTERs.Add(new NEWS_CHAPTER()
					{
						NEWS_ID = 3,
						TEXT = "Thomas Thune Andersen, bestyrelsesformand og valgkomiteen for DONG Energy A / S, sagde:",
						LANGUAGE = "da"
					});

					context.NEWS_CHAPTERs.Add(new NEWS_CHAPTER()
					{
						NEWS_ID = 3,
						TEXT = "“Det glæder mig, at valgkomiteen har anbefalet Dieter Wemmer som nyt medlem af bestyrelsen. Han har meget stærke finansielle kapaciteter og tilføjer stærk erfaring inden for kapitalmarkeder, investeringer og risikostyring til bestyrelsen. Jeg er overbevist om, at han vil være et aktiv for bestyrelsen.”",
						LANGUAGE = "da"
					});

					context.NEWS_CHAPTERs.Add(new NEWS_CHAPTER()
					{
						NEWS_ID = 3,
						TEXT = "Dieter Wemmer forventes at blive valgt til bestyrelsen på generalforsamlingen i 1. kvartal 2018.",
						LANGUAGE = "da"
					});

					context.NEWS_CHAPTERs.Add(new NEWS_CHAPTER()
					{
						NEWS_ID = 3,
						TEXT = "Bestyrelsen for DONG Energy A / S består i øjeblikket af seks medlemmer valgt af generalforsamlingen. Som tidligere meddelt er det langsigtede mål at have otte medlemmer af bestyrelsen valgt af generalforsamlingen.",
						LANGUAGE = "da"
					});

					context.NEWS_CHAPTERs.Add(new NEWS_CHAPTER()
					{
						NEWS_ID = 3,
						TEXT = "Yderligere information om Dieter Wemmer er vedlagt nedenfor.",
						LANGUAGE = "da"
					});

					#endregion

					#region News#4

					context.NEWS_CHAPTERs.Add(new NEWS_CHAPTER()
					{
						NEWS_ID = 4,
						TEXT = "I den første af to tyske auktionsrunder har Bundesnetzagentur i dag tildelt DONG Energy ret til at bygge tre offshore-vindprojekter i det tyske Nordsø. DONG Energy indsendte seks projekter i buddet og vandt med følgende tre projekter, som har en samlet kapacitet på 590 MW: OWP West (240 MW), Borkum Riffgrund West 2 (240 MW), Gode Wind 3 (110 MW).",
						LANGUAGE = "da"
					});

					context.NEWS_CHAPTERs.Add(new NEWS_CHAPTER()
					{
						NEWS_ID = 4,
						TEXT = "De tre projekter planlægges at blive bestilt i 2024, med forbehold af DONG Energy's endelige investeringsbeslutning i 2021.",
						LANGUAGE = "da"
					});

					context.NEWS_CHAPTERs.Add(new NEWS_CHAPTER()
					{
						NEWS_ID = 4,
						TEXT = "Samuel Leupold, Executive Vice President og CEO for Wind Power i DONG Energy, siger: ”Vi er tilfredse med at blive tildelt tre projekter i den første af to tyske auktionsrunder,og vi har gode muligheder for at tilføje yderligere kapacitet til vores vindende projekter i næste års tyske auktion.Dagens resultater bidrager til vores ambition om at drive rentabel vækst ved at tilføje ca. 5 GW ekstra kapacitet inden 2025.”",
						LANGUAGE = "da"
					});

					context.NEWS_CHAPTERs.Add(new NEWS_CHAPTER()
					{
						NEWS_ID = 4,
						TEXT = "For to af projekterne - OWP West og Borkum Riffgrund West 2 - DONG Energy lavede bud til nul EUR per MWh, dvs. disse projekter vil ikke modtage et tilskud på toppen af engros-elprisen. Gode Wind 3-projektet blev tildelt baseret på en budpris på 60 EUR pr. MWh.",
						LANGUAGE = "da"
					});

					context.NEWS_CHAPTERs.Add(new NEWS_CHAPTER()
					{
						NEWS_ID = 4,
						TEXT = "Samuel Leupold siger: ”Nulstilskuddet er et gennembrud for offshorevindens konkurrencedygtighed, og det demonstrerer teknologiens massive globale vækstpotentiale som en hjørnesten i det økonomisk bæredygtige skift til grønne energisystemer.Billigere ren energi vil gavne regeringer og forbrugere - og ikke mindst hjælpe med at opfylde Paris COP21 - målene til bekæmpelse af klimaændringer.Det er dog vigtigt at bemærke, at nulbudet er aktiveret af en række omstændigheder i denne auktion.Færdiggørelsesvinduet forlænges især til 2024.Dette giver udviklere mulighed for at anvende næste generationens turbinteknologi, som vil understøtte et stort nedskæringer i omkostningerne.Buddet afspejler også det faktum, at netforbindelse ikke er medtaget.”",
						LANGUAGE = "da"
					});

					context.NEWS_CHAPTERs.Add(new NEWS_CHAPTER()
					{
						NEWS_ID = 4,
						TEXT = "Samuel Leupold fortsætter: ”Finansiel disciplin er nøglen til os.Vi afspejler selvfølgelig projektets eksponering for markedsrisiko i omkostningerne ved anvendt kapital.Vi ser et solidt værdiskabende potentiale i denne tyske projektportefølje og vil nu begynde at modne projekterne mod en endelig investeringsbeslutning(FID) i 2021. ”",
						LANGUAGE = "da"
					});

					context.NEWS_CHAPTERs.Add(new NEWS_CHAPTER()
					{
						NEWS_ID = 4,
						TEXT = "Volker Malmen, landschef i DONG Energy Germany, siger: ”At gøre grøn energi billigere end sort har i årevis været en del af DONG Energys strategiske ambition. Offshore vind er fuldt ud i stand til at erstatte pensionerende kraftværker og blive rygraden i Tysklands energitransmission. Jeg håber, at dagens opmuntrende resultater vil inspirere til en accelereret og højere volumenudbygning af havvind i Tyskland og motivere elektrificering af transport og opvarmning .”",
						LANGUAGE = "da"
					});

					context.NEWS_CHAPTERs.Add(new NEWS_CHAPTER()
					{
						NEWS_ID = 4,
						TEXT = "DONG Energy vil være ansvarlig for turbinerne, arraykablerne og offshore-undergrundsstationen, mens netoperatøren TenneT vil være ansvarlig for opførelse, drift og ejerskab af onshore-undergrundsstationen og eksportkablet.",
						LANGUAGE = "da"
					});

					context.NEWS_CHAPTERs.Add(new NEWS_CHAPTER()
					{
						NEWS_ID = 4,
						TEXT = "DONG Energy har i øjeblikket 902 MW offshore vind i drift i tyske farvande med Gode Wind 1 & 2 og Borkum Riffgrund 1 og en anden 450 MW under opførelse på Borkum Riffgrund 2, som forventes iværksat i 2019. DONG Energy driver i alt 3.600 MW offshore vind kapacitet i Tyskland, Storbritannien og Danmark og har yderligere 3.800GW under opførelse.",
						LANGUAGE = "da"
					});

					context.NEWS_CHAPTERs.Add(new NEWS_CHAPTER()
					{
						NEWS_ID = 4,
						TEXT = "Platformændring: Markant større turbiner - sandsynligvis 13-15MW - vil være på markedet inden 2024. Med større turbiner kan bygherren øge elproduktionen, samtidig med at antallet af turbinepositioner reduceres. Dette bidrager væsentligt til omkostningsreduktioner under opførelsen (færre tårne- og arraykabler og lavere omkostninger til installationsfartøjer og arbejdskraft) samt i løbet af en levetid på drift og vedligeholdelse.",
						LANGUAGE = "da"
					});

					context.NEWS_CHAPTERs.Add(new NEWS_CHAPTER()
					{
						NEWS_ID = 4,
						TEXT = "Skala: OWP West og Borkum Riffgrund West 2 vil blive kombineret i et stort projekt med mulighed for at tilføje yderligere volumen i næste års auktion for yderligere at øge projektets samlede størrelse.",
						LANGUAGE = "da"
					});

					context.NEWS_CHAPTERs.Add(new NEWS_CHAPTER()
					{
						NEWS_ID = 4,
						TEXT = "Lokalitet: Projekterne har fordel af gennemsnitlige vindhastigheder på mere end 10 m / s, hvilket er blandt de højeste vindhastigheder målt på DONG Energys portefølje af vindmølleparker. Projekterne er også placeret ved siden af DONG Energy's Borkum Riffgrund 1 & 2, hvilket betyder, at drift og vedligeholdelse kan ske fra DONG Energys eksisterende O & M-hub i Norddeich.",
						LANGUAGE = "da"
					});

					context.NEWS_CHAPTERs.Add(new NEWS_CHAPTER()
					{
						NEWS_ID = 4,
						TEXT = "Udvidet levetid: De tyske myndigheder har godkendt muligheden for at udvide aktivets levetid fra 25 til 30 år.",
						LANGUAGE = "da"
					});

					context.NEWS_CHAPTERs.Add(new NEWS_CHAPTER()
					{
						NEWS_ID = 4,
						TEXT = "Ikke fuldt omfang: Udbydere bød ikke på netforbindelsen i den tyske auktion, hvilket betyder, at netforbindelse ikke er inkluderet i budprisen.",
						LANGUAGE = "da"
					});

					context.NEWS_CHAPTERs.Add(new NEWS_CHAPTER()
					{
						NEWS_ID = 4,
						TEXT = "Ovennævnte drivere leverer strømforbrug under vores forventede grossistpris og giver os mulighed for at skabe værdi og opfylde vores afkastkrav til de forventede markedspriser uden tilskud. Sammenlignet med tyske energiprisprognoser fra ledende forskningsfirmaer vurderer vi, at vores prisforventning er forholdsvis konservativ. Vi har anvendt en højere kapitalomkostning end i tidligere projekter for at afspejle den potentielle stigning i markedspriseksponering.",
						LANGUAGE = "da"
					});

					context.NEWS_CHAPTERs.Add(new NEWS_CHAPTER()
					{
						NEWS_ID = 4,
						TEXT = "De omkostningsreduktioner, der kræves til et tysk projekt uden tilskud, er fuldt ud gennemførlige, både teknisk og kommercielt. I forlængelse af en endelig investeringsbeslutning i 2021 vil DONG Energy overvåge de nøglefaktorer, som vil fastlægge langsigtede kraftpriser i Tyskland. Disse faktorer omfatter virkningen af EU-aktioner for at genoplive den europæiske kulstofhandelsordning; udfasningen af konventionel og nuklear kapacitet kulens fremtidige rolle i Europa og udbygningen af onshore transmission grids.",
						LANGUAGE = "da"
					});

					context.NEWS_CHAPTERs.Add(new NEWS_CHAPTER()
					{
						NEWS_ID = 4,
						TEXT = "Oplysningerne i denne meddelelse ændrer ikke DONG Energys tidligere økonomiske vejledning for regnskabsåret 2018 eller det annoncerede forventede investeringsniveau for 2018.",
						LANGUAGE = "da"
					});

					#endregion

					#region News#5

					context.NEWS_CHAPTERs.Add(new NEWS_CHAPTER()
					{
						NEWS_ID = 5,
						TEXT = "I dag afholdt DONG Energy A / S sin ordinære generalforsamling, hvor følgende blev vedtaget:",
						LANGUAGE = "da"
					});

					context.NEWS_CHAPTERs.Add(new NEWS_CHAPTER()
					{
						NEWS_ID = 5,
						TEXT = "DONG Energys reviderede årsrapport 2017 blev godkendt.",
						LANGUAGE = "da"
					});


					context.NEWS_CHAPTERs.Add(new NEWS_CHAPTER()
					{
						NEWS_ID = 5,
						TEXT = "Fordelingen af overskud i henhold til den vedtagne årsrapport 2017 blev godkendt. Udbyttet vil udgøre 6 DKK pr. Aktie.",
						LANGUAGE = "da"
					});


					context.NEWS_CHAPTERs.Add(new NEWS_CHAPTER()
					{
						NEWS_ID = 5,
						TEXT = "Thomas Thune Andersen blev genvalgt som bestyrelsesformand, Lene Skole blev genvalgt som næstformand for bestyrelsen, og Lynda Armstrong, Pia Gjellerup og Benny D. Loft blev genvalgt som bestyrelsesmedlemmer af bestyrelsen.",
						LANGUAGE = "da"
					});


					context.NEWS_CHAPTERs.Add(new NEWS_CHAPTER()
					{
						NEWS_ID = 5,
						TEXT = "Peter Korsholm blev valgt som nyt medlem af bestyrelsen.",
						LANGUAGE = "da"
					});


					context.NEWS_CHAPTERs.Add(new NEWS_CHAPTER()
					{
						NEWS_ID = 5,
						TEXT = "PricewaterhouseCoopers blev genudnævnt som revisor.",
						LANGUAGE = "da"
					});


					context.NEWS_CHAPTERs.Add(new NEWS_CHAPTER()
					{
						NEWS_ID = 5,
						TEXT = "Bestyrelsens vederlag for 2018 blev godkendt.",
						LANGUAGE = "da"
					});

					context.NEWS_CHAPTERs.Add(new NEWS_CHAPTER()
					{
						NEWS_ID = 5,
						TEXT = "Bestyrelsen og direktionen blev afgivet fra deres forpligtelser.",
						LANGUAGE = "da"
					});

					#endregion

					#endregion

					#region NewsChaptersInGerman

					#region News#1

					context.NEWS_CHAPTERs.Add(new NEWS_CHAPTER()
					{
						NEWS_ID = 1,
						TEXT = "DIE HIERIN BESCHRIEBENEN WERTPAPIERE WURDEN UND WERDEN NICHT NACH DEM U.S. SECURITIES ACT VON 1933, WIE GEÄNDERT, REGISTRIERT UND KÖNNEN NICHT IN DER ABSENZEN REGISTRIERUNG ODER EINER ANWENDBAREN BEFREIUNG VON REGISTRIERUNGSANFORDERUNGEN ANGEBOTEN ODER VERKAUFT WERDEN.",
						LANGUAGE = "de"
					});

					context.NEWS_CHAPTERs.Add(new NEWS_CHAPTER()
					{
						NEWS_ID = 1,
						TEXT = "In Bezug auf die Ankündigung vom 3. April 2018 hat DONG Energy A / S ('DONG Energy') (NASDAQ OMX: DENERG) folgende Informationen von New Energy Investment S.à.r.l erhalten. (NEI):",
						LANGUAGE = "de"
					});

					context.NEWS_CHAPTERs.Add(new NEWS_CHAPTER()
					{
						NEWS_ID = 1,
						TEXT = "NEI hat zugestimmt, 10.509.527 bestehende Aktien von DONG Energy, entsprechend 2,5% der bestehenden Aktien von DONG Energy, institutionellen Investoren zu einem Preis von DKK 270 pro Aktie im Rahmen eines Accelerated Bookbuilding-Angebots (die 'Transaktion') zu verkaufen. NEI ist ein luxemburgisches Unternehmen, das sich indirekt im Besitz von Unternehmen befindet, die unter der Kontrolle der Merchant Banking Division der Goldman Sachs Group, Inc. stehen.",
						LANGUAGE = "de"
					});

					context.NEWS_CHAPTERs.Add(new NEWS_CHAPTER()
					{
						NEWS_ID = 1,
						TEXT = "Nach Abschluss der Transaktion wird NEI 18.935.215 Aktien an DONG Energy halten, was 4,5% der bestehenden Anteile an DONG Energy entspricht.",
						LANGUAGE = "de"
					});

					context.NEWS_CHAPTERs.Add(new NEWS_CHAPTER()
					{
						NEWS_ID = 1,
						TEXT = "NEI hat einer gestrigen Sperrfrist von 90 Tagen zugestimmt, vorbehaltlich des Verzichts auf die vorherige schriftliche Zustimmung eines bestimmten Managers, der das Bookbuilding-Verfahren durchführt, und auf bestimmte übliche Ausnahmen.",
						LANGUAGE = "de"
					});

					context.NEWS_CHAPTERs.Add(new NEWS_CHAPTER()
					{
						NEWS_ID = 1,
						TEXT = "DONG Energy erhält keine Erlöse aus der Transaktion.",
						LANGUAGE = "de"
					});

					#endregion

					#region News#2
					context.NEWS_CHAPTERs.Add(new NEWS_CHAPTER()
					{
						NEWS_ID = 2,
						TEXT = "Gemäß § 29 des dänischen Wertpapierhandelsgesetzes ist New Energy Investment S.à r.l. (NEI) hat DONG Energy A / S (im Folgenden: DONG Energy) mitgeteilt, dass NEI nach einem beschleunigten Bookbuilding-Angebot vom 3. Februar 2018 zugestimmt hat, 26.500.000 Aktien von DONG Energy an bestimmte institutionelle Anleger zu veräußern mit einem Nennwert von 10 DKK.",
						LANGUAGE = "de"
					});

					context.NEWS_CHAPTERs.Add(new NEWS_CHAPTER()
					{
						NEWS_ID = 2,
						TEXT = "Nach Abwicklung des Bookbuild-Angebots wird NEI 29.444.742 Aktien und Stimmrechte an DONG Energy halten, was 7,0% der ausgegebenen Aktien und Stimmrechte von DONG Energy entspricht.",
						LANGUAGE = "de"
					});

					context.NEWS_CHAPTERs.Add(new NEWS_CHAPTER()
					{
						NEWS_ID = 2,
						TEXT = "NEI ist eine Gesellschaft mit beschränkter Haftung, die nach luxemburgischem Recht unter Reg. Nein. B181487. Die Adresse von NEI ist 2 Rue de Fossé, L-1536 Luxemburg, Großherzogtum Luxemburg.",
						LANGUAGE = "de"
					});

					context.NEWS_CHAPTERs.Add(new NEWS_CHAPTER()
					{
						NEWS_ID = 2,
						TEXT = "Zum jetzigen Zeitpunkt wird NEI von New Energy I S.à r.l. ('NE I') und New Energy II S.à r.l. ('NE II'). NE I, die die Mehrheit der Stimmrechte an NEI besitzt, und NE II sind Gesellschaften mit beschränkter Haftung, die nach luxemburgischem Recht organisiert sind und von Unternehmen kontrolliert werden, die unter der Kontrolle der Merchant Banking Division der Goldman Sachs Group, Inc. stehen. Zu diesen Unternehmen gehört Danish Energy Investors B, LP, eine Cayman Islands Limited Partnership, die die Mehrheit der Stimmrechte in NE I besitzt.",
						LANGUAGE = "de"
					});

					context.NEWS_CHAPTERs.Add(new NEWS_CHAPTER()
					{
						NEWS_ID = 2,
						TEXT = "Die in dieser Ankündigung enthaltenen Informationen ändern nicht die bisherige Finanzplanung von DONG Energy für das Geschäftsjahr 2018.",
						LANGUAGE = "de"
					});

					#endregion

					#region News#3

					context.NEWS_CHAPTERs.Add(new NEWS_CHAPTER()
					{
						NEWS_ID = 3,
						TEXT = "Der Nominierungsausschuss der DONG Energy A / S hat heute beschlossen, die Wahl von Dieter Wemmer als neues Mitglied des Verwaltungsrates zu empfehlen.",
						LANGUAGE = "de"
					});

					context.NEWS_CHAPTERs.Add(new NEWS_CHAPTER()
					{
						NEWS_ID = 3,
						TEXT = "Dieter Wemmer war mehr als zwei Jahrzehnte in führenden Finanzpositionen tätig, zuletzt als Chief Financial Officer der Allianz SE und als Verwaltungsrat der UBS Group AG. Er verfügt über große Erfahrung in den Bereichen Kapitalmärkte, Investitionen und Risikomanagement und verbindet einen klaren finanziellen Einblick mit einer strategischen und operativen Denkweise.",
						LANGUAGE = "de"
					});

					context.NEWS_CHAPTERs.Add(new NEWS_CHAPTER()
					{
						NEWS_ID = 3,
						TEXT = "Thomas Thune Andersen, Präsident des Verwaltungsrats und des Nominierungsausschusses von DONG Energy A / S, sagte:",
						LANGUAGE = "de"
					});

					context.NEWS_CHAPTERs.Add(new NEWS_CHAPTER()
					{
						NEWS_ID = 3,
						TEXT = "“Ich freue mich, dass der Nominierungsausschuss Dieter Wemmer als neues Mitglied des Verwaltungsrates empfohlen hat. Er verfügt über sehr gute finanzielle Fähigkeiten und verfügt über umfangreiche Erfahrung in den Bereichen Kapitalmärkte, Investitionen und Risikomanagement. Ich bin zuversichtlich, dass er ein Gewinn für den Vorstand sein wird. ",
						LANGUAGE = "de"
					});

					context.NEWS_CHAPTERs.Add(new NEWS_CHAPTER()
					{
						NEWS_ID = 3,
						TEXT = "Dieter Wemmer wird voraussichtlich im ersten Quartal 2018 an der Generalversammlung in den Verwaltungsrat gewählt.",
						LANGUAGE = "de"
					});

					context.NEWS_CHAPTERs.Add(new NEWS_CHAPTER()
					{
						NEWS_ID = 3,
						TEXT = "Der Verwaltungsrat von DONG Energy A / S besteht derzeit aus sechs von der Hauptversammlung gewählten Mitgliedern. Wie bereits angekündigt, besteht das langfristige Ziel darin, acht Mitglieder des Verwaltungsrates durch die Generalversammlung wählen zu lassen.",
						LANGUAGE = "de"
					});

					context.NEWS_CHAPTERs.Add(new NEWS_CHAPTER()
					{
						NEWS_ID = 3,
						TEXT = "Weitere Informationen zu Dieter Wemmer finden Sie unten.",
						LANGUAGE = "de"
					});

					#endregion

					#region News#4

					context.NEWS_CHAPTERs.Add(new NEWS_CHAPTER()
					{
						NEWS_ID = 4,
						TEXT = "In der ersten von zwei deutschen Auktionsrunden hat die Bundesnetzagentur heute DONG Energy das Recht eingeräumt, drei Offshore-Windprojekte in der deutschen Nordsee zu bauen. DONG Energy reichte sechs Projekte in das Angebot ein und gewann mit den folgenden drei Projekten, die eine Gesamtkapazität von 590 MW haben: OWP West (240 MW), Borkum Riffgrund West 2 (240 MW), Gode Wind 3 (110 MW).",
						LANGUAGE = "de"
					});

					context.NEWS_CHAPTERs.Add(new NEWS_CHAPTER()
					{
						NEWS_ID = 4,
						TEXT = "Die drei Projekte sollen 2024 in Auftrag gegeben werden, vorbehaltlich der abschließenden Investitionsentscheidung von DONG Energy im Jahr 2021.",
						LANGUAGE = "de"
					});

					context.NEWS_CHAPTERs.Add(new NEWS_CHAPTER()
					{
						NEWS_ID = 4,
						TEXT = "Samuel Leupold, Executive Vice President und CEO von Wind Power bei DONG Energy, sagt: 'Wir freuen uns über drei Projekte in der ersten von zwei deutschen Auktionsrunden, und wir haben gute Möglichkeiten, unsere ausgezeichneten Projekte in nächstes Jahr Deutsche Auktion.Die heutigen Ergebnisse tragen zu unserem ehrgeizigen Ziel bei, bis 2025 rund 5 GW zusätzliche Kapazität für profitables Wachstum zu schaffen. '",
						LANGUAGE = "de"
					});

					context.NEWS_CHAPTERs.Add(new NEWS_CHAPTER()
					{
						NEWS_ID = 4,
						TEXT = "Für zwei der Projekte - OWP West und Borkum Riffgrund West 2 - hat DONG Energy Gebote von null Euro pro MWh abgegeben, d. H. Diese Projekte erhalten keine Subvention zusätzlich zum Stromgroßhandelspreis. Das Projekt Gode Wind 3 wurde mit einem Angebotspreis von 60 EUR pro MWh vergeben.",
						LANGUAGE = "de"
					});

					context.NEWS_CHAPTERs.Add(new NEWS_CHAPTER()
					{
						NEWS_ID = 4,
						TEXT = "Samuel Leupold sagt: 'Das Nullsubventionsgebot ist ein Durchbruch für die Kostenwettbewerbsfähigkeit von Offshore - Wind und es demonstriert das enorme globale Wachstumspotenzial der Technologie als einen Eckpfeiler in der wirtschaftlich lebensfähigen Umstellung auf grüne Energiesysteme.Billige saubere Energie wird Regierungen und Verbrauchern zugute kommen - und nicht zuletzt dazu beitragen,	die COP21 - Ziele von Paris im Kampf gegen den Klimawandel zu erreichen.Dennoch ist es wichtig zu beachten,	dass das Nullgebot durch eine Reihe von Umständen in dieser Auktion ermöglicht wird.Vor allem wird das Realisierungsfenster auf 2024 erweitert.Dies ermöglicht Entwicklern,	die Turbinentechnologie der nächsten Generation anzuwenden,	was zu einer erheblichen Kostensenkung führt.Außerdem spiegelt das Angebot die Tatsache wider, dass die Netzverbindung nicht enthalten ist.'",
						LANGUAGE = "de"
					});

					context.NEWS_CHAPTERs.Add(new NEWS_CHAPTER()
					{
						NEWS_ID = 4,
						TEXT = "Samuel Leupold fährt fort: 'Finanzielle Disziplin ist der Schlüssel zu uns.Wir spiegeln natürlich das Marktrisiko - Risiko des Projekts in den angewandten Kapitalkosten wider.Wir sehen in diesem deutschen Projektportfolio ein solides Wertschöpfungspotenzial und werden nun die Projekte im Jahr 2021 zu einer Final Investment Decision(FID) weiterentwickeln. '",
						LANGUAGE = "de"
					});

					context.NEWS_CHAPTERs.Add(new NEWS_CHAPTER()
					{
						NEWS_ID = 4,
						TEXT = @"Volker Malmen, Country Manager bei DONG Energy Deutschland, sagt: 'Grüne Energie zu einem billigeren Preis als Schwarz zu machen,
						gehört seit Jahren zu den strategischen Zielen von DONG Energy.Der Offshore - Wind kann ausgeschiedene Kraftwerke vollständig ersetzen und zum Rückgrat der deutschen Energiewende werden.Ich hoffe,
						dass die ermutigenden Ergebnisse von heute zu einem beschleunigten und volumenstärkeren Offshore - Windausbau in Deutschland führen und die Elektrifizierung von Transport und Heizung motivieren werden.'",
						LANGUAGE = "de"
					});

					context.NEWS_CHAPTERs.Add(new NEWS_CHAPTER()
					{
						NEWS_ID = 4,
						TEXT = "DONG Energy wird für die Turbinen, Array-Kabel und das Offshore-Umspannwerk verantwortlich sein, während der Netzbetreiber TenneT für den Bau, den Betrieb und den Besitz des Onshore-Umspannwerks und des Exportkabels verantwortlich sein wird.",
						LANGUAGE = "de"
					});

					context.NEWS_CHAPTERs.Add(new NEWS_CHAPTER()
					{
						NEWS_ID = 4,
						TEXT = "DONG Energy hat derzeit 902 MW Offshore-Wind in deutschen Gewässern mit Gode Wind 1 & 2 und Borkum Riffgrund 1 und weitere 450 MW in Borkum Riffgrund 2 im Bau, die 2019 in Betrieb gehen sollen. Insgesamt betreibt DONG Energy 3.600 MW Offshore-Wind Kapazität in Deutschland, Großbritannien und Dänemark und hat weitere 3.800GW im Bau.",
						LANGUAGE = "de"
					});

					context.NEWS_CHAPTERs.Add(new NEWS_CHAPTER()
					{
						NEWS_ID = 4,
						TEXT = "Plattformwechsel: Bis 2024 werden deutlich größere Anlagen - voraussichtlich 13-15 MW - auf dem Markt sein. Mit größeren Turbinen kann der Entwickler die Stromerzeugung steigern und gleichzeitig die Anzahl der Turbinenpositionen reduzieren. Dies trägt erheblich zur Kostenreduzierung während des Baus (weniger Türme und Array-Kabel und geringere Kosten für Installationsschiffe und Arbeitskräfte) sowie während einer Lebensdauer von Betrieb und Wartung bei.",
						LANGUAGE = "de"
					});

					context.NEWS_CHAPTERs.Add(new NEWS_CHAPTER()
					{
						NEWS_ID = 4,
						TEXT = "Scale: OWP West und Borkum Riffgrund West 2 werden zu einem Großprojekt zusammengelegt, mit der Option, im nächsten Jahr zusätzliches Volumen hinzuzufügen, um die Gesamtgröße des Projekts weiter zu erhöhen.",
						LANGUAGE = "de"
					});

					context.NEWS_CHAPTERs.Add(new NEWS_CHAPTER()
					{
						NEWS_ID = 4,
						TEXT = "Standort: Die Projekte profitieren von durchschnittlichen Windgeschwindigkeiten von mehr als 10 m / s. Dies ist eine der höchsten Windstärken, die im Windpark-Portfolio von DONG Energy gemessen werden. Außerdem befinden sich die Projekte neben Borkum Riffgrund 1 & 2 von DONG Energy, was bedeutet, dass Betrieb und Wartung von DONG Energy's bestehendem O & M-Hub in Norddeich durchgeführt werden können.",
						LANGUAGE = "de"
					});

					context.NEWS_CHAPTERs.Add(new NEWS_CHAPTER()
					{
						NEWS_ID = 4,
						TEXT = "Verlängerte Lebensdauer: Die deutschen Behörden haben der Möglichkeit zugestimmt, die Betriebsdauer des Vermögenswerts von 25 auf 30 Jahre zu verlängern.",
						LANGUAGE = "de"
					});

					context.NEWS_CHAPTERs.Add(new NEWS_CHAPTER()
					{
						NEWS_ID = 4,
						TEXT = "Nicht in vollem Umfang: Entwickler haben in der deutschen Auktion nicht für die Netzanbindung geboten, so dass die Netzanbindung nicht im Angebotspreis enthalten ist.",
						LANGUAGE = "de"
					});

					context.NEWS_CHAPTERs.Add(new NEWS_CHAPTER()
					{
						NEWS_ID = 4,
						TEXT = "Die oben genannten Faktoren liefern Stromkosten, die unter unseren prognostizierten Großhandelsstrompreisen liegen, und ermöglichen es uns, einen Mehrwert zu schaffen und unsere Renditeanforderungen zu den erwarteten Marktpreisen ohne Subventionen zu erfüllen. Im Vergleich zu den deutschen Strompreisprognosen führender Forschungsunternehmen halten wir unsere Preisprognose für relativ konservativ. Wir haben höhere Kapitalkosten als bei früheren Projekten angewendet, um dem potenziellen Anstieg des Marktpreisrisikos Rechnung zu tragen.",
						LANGUAGE = "de"
					});

					context.NEWS_CHAPTERs.Add(new NEWS_CHAPTER()
					{
						NEWS_ID = 4,
						TEXT = "Die für ein deutsches Projekt ohne Subventionen erforderlichen Kostensenkungen sind sowohl technisch als auch kommerziell durchführbar. Im Hinblick auf eine endgültige Investitionsentscheidung im Jahr 2021 wird DONG Energy die Schlüsselfaktoren überwachen, die die langfristigen Strompreise in Deutschland bestimmen werden. Zu diesen Faktoren gehören die Auswirkungen von EU-Maßnahmen zur Neubelebung des europäischen Emissionshandelssystems; der Ausstieg aus konventionellen und nuklearen Kapazitäten; die zukünftige Rolle der Kohle in Europa; und der Ausbau von Onshore-Übertragungsnetzen.",
						LANGUAGE = "de"
					});

					context.NEWS_CHAPTERs.Add(new NEWS_CHAPTER()
					{
						NEWS_ID = 4,
						TEXT = "Die in dieser Mitteilung enthaltenen Informationen ändern nicht die bisherige Finanzplanung von DONG Energy für das Geschäftsjahr 2018 oder das für 2018 erwartete Investitionsvolumen.",
						LANGUAGE = "de"
					});

					#endregion

					#region News#5

					context.NEWS_CHAPTERs.Add(new NEWS_CHAPTER()
					{
						NEWS_ID = 5,
						TEXT = "Heute hielt DONG Energy A / S seine Jahreshauptversammlung ab, auf der Folgendes angenommen wurde:",
						LANGUAGE = "de"
					});

					context.NEWS_CHAPTERs.Add(new NEWS_CHAPTER()
					{
						NEWS_ID = 5,
						TEXT = "Der geprüfte Jahresbericht 2017 von DONG Energy wurde genehmigt.",
						LANGUAGE = "de"
					});


					context.NEWS_CHAPTERs.Add(new NEWS_CHAPTER()
					{
						NEWS_ID = 5,
						TEXT = "Die Gewinnverteilung nach dem angenommenen Jahresbericht 2017 wurde genehmigt. Die Dividende beträgt 6 DKK pro Aktie.",
						LANGUAGE = "de"
					});


					context.NEWS_CHAPTERs.Add(new NEWS_CHAPTER()
					{
						NEWS_ID = 5,
						TEXT = "Thomas Thune Andersen wurde als Präsident des Verwaltungsrats wiedergewählt, Lene Skole wurde als stellvertretender Vorsitzender des Verwaltungsrates wiedergewählt und Lynda Armstrong, Pia Gjellerup und Benny D. Loft wurden als Mitglieder des Verwaltungsrats wiedergewählt von Direktoren.",
						LANGUAGE = "de"
					});


					context.NEWS_CHAPTERs.Add(new NEWS_CHAPTER()
					{
						NEWS_ID = 5,
						TEXT = "Peter Korsholm wurde als neues Mitglied in den Verwaltungsrat gewählt.",
						LANGUAGE = "de"
					});


					context.NEWS_CHAPTERs.Add(new NEWS_CHAPTER()
					{
						NEWS_ID = 5,
						TEXT = "PricewaterhouseCoopers wurde erneut zum Abschlussprüfer bestellt.",
						LANGUAGE = "de"
					});


					context.NEWS_CHAPTERs.Add(new NEWS_CHAPTER()
					{
						NEWS_ID = 5,
						TEXT = "Die Vorstandsvergütung für 2018 wurde genehmigt.",
						LANGUAGE = "de"
					});

					context.NEWS_CHAPTERs.Add(new NEWS_CHAPTER()
					{
						NEWS_ID = 5,
						TEXT = "Der Verwaltungsrat und die Geschäftsleitung wurden von ihren Verpflichtungen entbunden.",
						LANGUAGE = "de"
					});

					#endregion

					#endregion

					context.SaveChanges();
				}
			}
		}

		#endregion

		#region Help Methods

		public static string CreateSurname()
		{
			int surnamesQty = 90;
			return Enum.GetName(typeof(Surnames), rnd.Next(surnamesQty));
		}

		public static string CreateName()
		{
			int namesQty = 70;
			return Enum.GetName(typeof(Names), rnd.Next(namesQty));
		}

		public static string CreateEmail(string surname, string name)
		{
			return String.Concat(surname.ToLower(), name.ToLower(), "@", Enum.GetName(typeof(Email), rnd.Next(3)), ".", Enum.GetName(typeof(Domain), rnd.Next(2)));
		}

		public static string CreatePhoneNumber()
		{
			string phone = "";
			string codePhone = "+45";
			int maxPhoneNumber = 99999999;

			phone = String.Concat(codePhone, rnd.Next(maxPhoneNumber));
			for (; phone.Length < 11;)
				phone += rnd.Next(9).ToString();

			return phone;
		}

		public static string[] ArraySortByNameAndNumber(string[] ar)
		{
			Regex rgx = new Regex("([^0-9]*)([0-9]+)");
			Array.Sort(ar, (a, b) =>
			{
				var ma = rgx.Matches(a);
				var mb = rgx.Matches(b);
				for (int i = 0; i < ma.Count; ++i)
				{
					int ret = ma[i].Groups[1].Value.CompareTo(mb[i].Groups[1].Value);
					if (ret != 0)
						return ret;

					ret = int.Parse(ma[i].Groups[2].Value) - int.Parse(mb[i].Groups[2].Value);
					if (ret != 0)
						return ret;
				}
				return 0;
			});

			return ar;
		}

		public static int GetRandomMeterTypeId(int j, int utilIdOneIndexes, int utilIdTwoIndexes, int utilIdThreeIndexes, int utilIdFourIndexes)
		{
			int meterTypeId;

			if (j == (int)Utilities.Electricity)
				meterTypeId = rnd.Next(1, ++utilIdOneIndexes);
			else if (j == (int)Utilities.Water)
				meterTypeId = rnd.Next(++utilIdOneIndexes, ++utilIdTwoIndexes);
			else if (j == (int)Utilities.Heating)
				meterTypeId = rnd.Next(++utilIdTwoIndexes, ++utilIdThreeIndexes);
			else
				meterTypeId = rnd.Next(++utilIdThreeIndexes, ++utilIdFourIndexes);
			return meterTypeId;
		}

		public static DateTime GetRandomReleaseDate()
		{
			DateTime releaseDate = new DateTime();
			Dictionary<int, int> daysInMonth = new Dictionary<int, int>();
			int year, month, day;
			int minYear = 2012;
			int maxYear = 2014;
			int minMonth = 1;
			int maxMonth = 12;

			daysInMonth.Add(1, 31);
			daysInMonth.Add(2, 28);
			daysInMonth.Add(3, 31);
			daysInMonth.Add(4, 30);
			daysInMonth.Add(5, 31);
			daysInMonth.Add(6, 30);
			daysInMonth.Add(7, 31);
			daysInMonth.Add(8, 31);
			daysInMonth.Add(9, 30);
			daysInMonth.Add(10, 31);
			daysInMonth.Add(11, 30);
			daysInMonth.Add(12, 31);

			year = rnd.Next(minYear, maxYear);
			month = rnd.Next(minMonth, maxMonth);
			day = rnd.Next(minMonth, daysInMonth[month]);

			releaseDate = new DateTime(year, month, day);

			return releaseDate;
		}

		public static DateTime GetVarificationDate(DateTime releaseDate, Dictionary<int, int> varifPeriods, int meterTypeId)
		{
			DateTime varifDate = new DateTime();
			int minDay = 1;
			int rndMaxDay = 8;

			varifDate = releaseDate.AddYears(varifPeriods[meterTypeId]).AddDays(rnd.Next(minDay, rndMaxDay));

			if (varifDate > DateTime.Now)
				varifDate = releaseDate;

			return varifDate;
		}

		public static string CreateMeterNumber()
		{
			int seriesQty = Enum.GetNames(typeof(MeterSeries)).Length;
			int minNumber = 100000;
			int maxNumber = 999999;

			return Enum.GetName(typeof(MeterSeries), rnd.Next(seriesQty)) + rnd.Next(minNumber, maxNumber).ToString();
		}

		public static float GetUtilityUsagePerSqMeter(int utility)
		{
			float utilityUsagePerSqMeter;

			if (utility == (int)Utilities.Electricity)
				utilityUsagePerSqMeter = 2.72F;
			else if (utility == (int)Utilities.Water)
				utilityUsagePerSqMeter = 0.11F;
			else if (utility == (int)Utilities.Heating)
				utilityUsagePerSqMeter = 0.013F;
			else
				utilityUsagePerSqMeter = 0.075F;

			return utilityUsagePerSqMeter;
		}

		public static float GetRandomDoubleNumber(float minimum, float maximum)
		{
			return (float)rnd.NextDouble() * (maximum - minimum) + minimum;
		}

		public static decimal GetRandomAccountBalance()
		{
			int[] array = { 0, 0, 1 };
			int rndArrayIndex = rnd.Next(0, array.Length);
			int rndMin = -1000;
			int rndMax = 4000;

			if (array[rndArrayIndex] == 1)
				return rnd.Next(rndMin, rndMax);

			return 0;
		}

		public static int GetAccountNumber()
		{
			int rndMin = 50000000;
			int rndMax = 80000000;
			accountNumber = rnd.Next(rndMin, rndMax);

			return accountNumber;
		}

		public static float GetHeatingMonthFactor(DateTime date, int townId)
		{
			int helpFactor1 = 9;
			int helpFactor2 = 10;
			using (var context = new TownUtilityBillSystemV2Entities())
			{
				List<Temperature> temperaturesDB = new List<Temperature>();
				DateTime temperatureStartDate = new DateTime();
				DateTime temperatureFinishDate = new DateTime();
				temperatureFinishDate = date;
				temperatureStartDate = date.AddMonths(-1);

				float heatingMonthFactor;
				int month;

				if (date.Month != (int)Months.January)
					month = date.Month - 1;
				else
					month = (int)Months.December;


				float valueSum = 0;
				float averageValue;
				int daysInMonth = System.DateTime.DaysInMonth(temperatureStartDate.Year, temperatureStartDate.Month);

				var temperatureItemsDB = context.TEMPERATUREs.Where(t => t.TOWN_ID == townId).ToList();

				foreach (var d in temperatureItemsDB)
					temperaturesDB.Add(new Temperature() { Id = d.ID, Date = d.DATE, MinValue = d.MINVALUE, MaxValue = d.MAXVALUE });

				for (; temperatureStartDate < temperatureFinishDate; temperatureStartDate = temperatureStartDate.AddDays(1))
					valueSum += (float)(temperaturesDB.Where(t => t.Date == temperatureStartDate).FirstOrDefault().MinValue + temperaturesDB.Where(t => t.Date == temperatureStartDate).FirstOrDefault().MaxValue) / 2;

				averageValue = valueSum / daysInMonth;

				if (month >= (int)Months.May && month <= (int)Months.September)
					heatingMonthFactor = 0;
				else
					heatingMonthFactor = (helpFactor2 - averageValue) / helpFactor1;

				return heatingMonthFactor;
			}
		}

		#endregion
	}
}