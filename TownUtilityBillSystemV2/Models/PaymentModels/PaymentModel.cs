using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;

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

						string guidString = Guid.NewGuid().ToString();
						//ID = Guid.NewGuid()

						var newPayment = new PAYMENT() {NUMBER = billDB.NUMBER, SUM = billDB.SUM, DATE = billDB.DATE, ACCOUNT_ID = billDB.ACCOUNT_ID };

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