using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using TownUtilityBillSystemV2.Resources;

namespace TownUtilityBillSystemV2.Models.PaymentModels
{
	public class PaymentModel
	{
		internal void MakePaymentTransaction(int bill_Id)
		{
			using (var context = new TownUtilityBillSystemV2Entities())
			{
				using (DbContextTransaction dbTran = context.Database.BeginTransaction())
				{
					try
					{
						#region BILL update

						var billDB = context.BILLs.Find(bill_Id);

						if (billDB != null)
							billDB.PAID = true;

						context.SaveChanges();

						#endregion

						#region PAYMENT creation

						//TO DO
						var newPayment = new PAYMENT() {ID = Guid.NewGuid(), SUM = billDB.SUM,
							DATE = billDB.DATE, ACCOUNT_ID = billDB.ACCOUNT_ID,
							NOTE = String.Format("{0} {1} {2}{3}", Localization.Payment,Localization.For, Localization.BillNum, billDB.NUMBER)
						};

						context.PAYMENTs.Add(newPayment);

						context.SaveChanges();

						#endregion

						#region Account update

						var accountDB = context.ACCOUNTs.Where(a => a.ID == billDB.ACCOUNT_ID).FirstOrDefault();

						if (accountDB != null)
						{
							//TO DO
							//accountDB.BALANCE -= newPayment.SUM;
							//accountDB.BALANCE = 
						}

						context.SaveChanges();

						#endregion

						dbTran.Commit();
					}
					catch (DbEntityValidationException ex)
					{
						dbTran.Rollback();
						throw;
					}
				}
			}
		}
	}
}