using SmartWorkoutsWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Qiwi.BillPayments.Client;
using Qiwi.BillPayments.Model;
using Qiwi.BillPayments.Model.In;
using Qiwi.BillPayments.Model.Out;

namespace SmartWorkoutsWeb
{
    public static class ValidateSubscription
    {
        private static  SmartWorkouts_newEntities db = new SmartWorkouts_newEntities();
       
        public static void CheckBuy(int id)
        {
            //  var Build = db.BuildID.Where(p => p.IdUser == id).FirstOrDefault();


            //if (Build != null)
            //{
            //    var BuildId = Build.IDBuild;
            //    var client = BillPaymentsClientFactory.Create(secretKey: "48e7qUxn9T7RyYE1MVZswX1FRSbE6iyCj2gCRwwF3Dnh5XrasNTx3BGPiMsyXQFNKQhvukniQG8RTVhYm3iPpZQHyQ5YrT3phbZG6TNwPP2U7m5gKjuh6VnZHi2m3BJikoJLV4B2ApEazrH7h6XK1YmK9NZj5ZTRBq3faLpArjKkKtbuVSMXJbURipA7i");
            //    BillResponse response = client.GetBillInfo(BuildId);


            //    switch (response.Status.ValueString)
            //    {

            //        case "PAID":

            //            int NumWork = Build.NumberPremWork;
            //            db.BuildID.Remove(Build);
            //            db.SaveChanges();
            //            Users users = db.Users.Where(p => p.ID_User == id).FirstOrDefault();
            //            users.PremiumNumber = NumWork;
            //            db.SaveChanges();
            //            var price = db.Premium_Works.Where(p => p.Number_Premium_Work == NumWork).FirstOrDefault().Price_Premium_Work;
            //            Contracts contracts = new Contracts { IdUser = id, NumberPremiumWork = NumWork, PurchaseDate = DateTime.Now, ExpiryDate = DateTime.Now.AddDays(30), Price = price };
            //            db.Contracts.Add(contracts);
            //            db.SaveChanges();
            //            break;

            //        case "WAITING":

            //            break;

            //        case "REJECTED":
            //            db.BuildID.Remove(Build);
            //            db.SaveChanges();
            //            break;

            //        case "EXPIRED":
            //            db.BuildID.Remove(Build);
            //            db.SaveChanges();
            //            break;
            //    }

            //}

        }

        public static void CheckTime(int id)
        {
            var user = db.Users.Where(p => p.ID_User == id).FirstOrDefault();
            if (user.PremiumNumber != 5)
            {
                var Contract = db.Contracts.Where(p => p.IdUser == id).OrderByDescending(p => p.IdContract).FirstOrDefault();

                if (Contract != null)
                {
                    if (Contract.ExpiryDate < DateTime.Now)
                    {
                        user.PremiumNumber = 5;
                        db.SaveChanges();
                    }
                }
            }
            
          
        }
    }
}