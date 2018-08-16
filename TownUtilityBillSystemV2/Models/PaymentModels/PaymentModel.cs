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
		internal void MakePaymentTransaction(int bill_Id, decimal payingSum)
		{
			using (var context = new TownUtilityBillSystemV2Entities())
			{
				using (DbContextTransaction dbTran = context.Database.BeginTransaction())
				{
					try
					{
						#region BILL update

						var billDB = context.BILLs.Find(bill_Id);
						ACCOUNT accountDB = null;

						if (billDB != null)
						{
							accountDB = context.ACCOUNTs.Where(a => a.ID == billDB.ACCOUNT_ID).FirstOrDefault();

							if (accountDB != null && (payingSum >= billDB.SUM || payingSum >= (billDB.SUM + accountDB.BALANCE)))
								billDB.PAID = true;
						}

						context.SaveChanges();

						#endregion

						#region PAYMENT creation

						var newPayment = new PAYMENT()
						{
							ID = Guid.NewGuid(),
							SUM = payingSum,
							DATE = DateTime.Now,
							ACCOUNT_ID = billDB.ACCOUNT_ID,
							NOTE = String.Format("{0} {1} {2}{3}", Localization.Payment, Localization.For, Localization.BillNum, billDB.NUMBER)
						};

						context.PAYMENTs.Add(newPayment);

						context.SaveChanges();

						#endregion

						#region Account update

						if (accountDB != null)
						{
							if (payingSum >= billDB.SUM)
								accountDB.BALANCE += billDB.SUM - payingSum;
							else if ((payingSum - accountDB.BALANCE) >= billDB.SUM && billDB.PAID)
								accountDB.BALANCE = billDB.SUM + accountDB.BALANCE - payingSum;
							else
								accountDB.BALANCE -= payingSum;
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