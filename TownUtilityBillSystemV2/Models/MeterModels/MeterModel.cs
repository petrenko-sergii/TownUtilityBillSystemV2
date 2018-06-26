using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TownUtilityBillSystemV2.Models.AddressModels;
using TownUtilityBillSystemV2.Models.HelperMethods;
using TownUtilityBillSystemV2.Models.UtilityModels;
using System.Data.Entity;
using System.Web.Mvc;

namespace TownUtilityBillSystemV2.Models.MeterModels
{
	public class MeterModel
	{
		private int meterCountToDisplay = 25;

		#region Properties

		public Meter Meter;

		public List<Meter> Meters;

		public int TotalCount { get; set; }

		public int MeterCountToDisplay { get { return meterCountToDisplay; } }

		#endregion

		#region  Ctor


		public MeterModel()
		{
			Meter = new Meter();
			Meters = new List<Meter>();
			TotalCount = 0;
		}

		#endregion
		
		#region Methods

		public void GetRandomMeters()
		{
			Random rnd = new Random();

			using (var context = new TownUtilityBillSystemV2Entities())
			{
				int[] rndMeterIds = new int[MeterCountToDisplay];

				TotalCount = context.METERs.ToList().Count;

				for (int i = 0; i < MeterCountToDisplay; i++)
					rndMeterIds[i] = rnd.Next(0, TotalCount);

				var rndMetersDB = context.METERs.Where(m => rndMeterIds.Any(i => m.ID == i)).Distinct().ToList();

				CreateMeterModelFromMeterList(context, this, rndMetersDB);
			}
		}

		public IEnumerable<METER> GetAllMeters()
		{
			using (var context = new TownUtilityBillSystemV2Entities())
			{
				var metersDB = context.METERs.Include(m => m.ADDRESS).Include(m => m.METER_TYPE).ToList();

				return metersDB;
			}
		}

		public void GetFoundMeters(string searchString)
		{
			using (var context = new TownUtilityBillSystemV2Entities())
			{
				var metersDB = (from m in context.METERs
								where
									m.SERIAL_NUMBER.Contains(searchString) ||
									m.METER_TYPE.NAME.Contains(searchString) ||
									m.METER_TYPE.UTILITY.NAME.Contains(searchString) ||
									m.ADDRESS.STREET.NAME.Contains(searchString) ||
									m.ADDRESS.TOWN.NAME.Contains(searchString) ||
									m.ADDRESS.FLAT_PART.NAME.Contains(searchString) ||
									m.ADDRESS.INDEX.VALUE.ToString().Contains(searchString)
								select m
									).ToList();

				CreateMeterModelFromMeterList(context, this, metersDB);
			}
		}

		public void GetMetersForBuilding(int buildingId)
		{
			using (var context = new TownUtilityBillSystemV2Entities())
			{
				context.Configuration.ProxyCreationEnabled = false;

				var addressDB = context.ADDRESSes.Where(a => a.BUILDING_ID == buildingId).FirstOrDefault();

				if (addressDB.FLAT_PART_ID == null)
					FillMetersForModel(context, addressDB);
			}
		}

		public void GetMetersForFlatPart(int flatPartId)
		{
			using (var context = new TownUtilityBillSystemV2Entities())
			{
				context.Configuration.ProxyCreationEnabled = false;

				var addressDB = context.ADDRESSes.Where(a => a.FLAT_PART_ID == flatPartId).FirstOrDefault();

				if (addressDB != null)
					FillMetersForModel(context, addressDB);
			}
		}

		private void FillMetersForModel(TownUtilityBillSystemV2Entities context, ADDRESS addressDB)
		{
			var metersDB = context.METERs.Where(m => m.ADDRESS_ID == addressDB.ID).ToList();

			foreach (var m in metersDB)
			{
				var meterTypeDB = context.METER_TYPEs.Where(mt => mt.ID == m.METER_TYPE_ID).FirstOrDefault();

				var utilityDB = context.UTILITYs.Where(u => u.ID == meterTypeDB.UTILITY_ID).FirstOrDefault();

				var utility = new Utility() { Id = utilityDB.ID, Name = utilityDB.NAME };

				var meterType = new MeterType() { Id = meterTypeDB.ID, Name = meterTypeDB.NAME, Utility = utility };

				Meters.Add(new Meter() { Id = m.ID, SerialNumber = m.SERIAL_NUMBER, ReleaseDate = m.RELEASE_DATE, VarificationDate = m.VARIFICATION_DATE, MeterType = meterType });
			}
		}

		private static void CreateMeterModelFromMeterList(TownUtilityBillSystemV2Entities context, MeterModel model, List<METER> rndMetersDB)
		{
			foreach (var m in rndMetersDB)
			{
				var meterTypeDB = context.METER_TYPEs.Where(mt => mt.ID == m.METER_TYPE_ID).FirstOrDefault();
				var utilityDB = context.UTILITYs.Where(u => u.ID == meterTypeDB.UTILITY_ID).FirstOrDefault();
				var addressDB = context.ADDRESSes.Where(a => a.ID == m.ADDRESS_ID).FirstOrDefault();
				var indexDB = context.INDEXes.Where(i => i.ID == addressDB.INDEX_ID).FirstOrDefault();
				var townDB = context.TOWNs.Where(t => t.ID == addressDB.TOWN_ID).FirstOrDefault();
				var streetDB = context.STREETs.Where(s => s.ID == addressDB.STREET_ID).FirstOrDefault();
				var buildingDB = context.BUILDINGs.Where(b => b.ID == addressDB.BUILDING_ID).FirstOrDefault();
				var flatPartDB = context.FLAT_PARTs.Where(fp => fp.ID == addressDB.FLAT_PART_ID).FirstOrDefault();

				var utility = new Utility() { Id = utilityDB.ID, Name = utilityDB.NAME, ResourceName = UtilityModel.GetResourceNameForUtility(utilityDB.NAME) };

				var meterType = new MeterType() { Id = meterTypeDB.ID, Name = meterTypeDB.NAME, Utility = utility };

				var index = new Index() { Id = indexDB.ID, Value = indexDB.VALUE };
				var town = new Town() { Id = townDB.ID, Name = townDB.NAME };
				var street = new Street() { Id = streetDB.ID, Name = streetDB.NAME };
				var building = new Building() { Id = buildingDB.ID, Number = buildingDB.NUMBER };

				FlatPart flatPart = null;

				if (flatPartDB != null)
				{
					if (!String.IsNullOrEmpty(flatPartDB.NAME) && !flatPartDB.NUMBER.HasValue)
						flatPart = new FlatPart() { Id = flatPartDB.ID, Name = flatPartDB.NAME };
					else if (String.IsNullOrEmpty(flatPartDB.NAME) && flatPartDB.NUMBER.HasValue)
						flatPart = new FlatPart() { Id = flatPartDB.ID, Number = (int)flatPartDB.NUMBER };
					else if (!String.IsNullOrEmpty(flatPartDB.NAME) && flatPartDB.NUMBER.HasValue)
						flatPart = new FlatPart() { Id = flatPartDB.ID, Number = (int)flatPartDB.NUMBER, Name = flatPartDB.NAME };
				}

				var address = new Address() { Id = addressDB.ID, Index = index, Town = town, Street = street, Building = building, FlatPart = flatPart };

				model.Meters.Add(new Meter() { Id = m.ID, SerialNumber = m.SERIAL_NUMBER, ReleaseDate = m.RELEASE_DATE, VarificationDate = m.VARIFICATION_DATE, MeterType = meterType, Address = address });
			}
		}

		#endregion
	}
}