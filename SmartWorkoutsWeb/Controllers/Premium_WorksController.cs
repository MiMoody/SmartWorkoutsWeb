using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Qiwi.BillPayments.Client;
using Qiwi.BillPayments.Model;
using Qiwi.BillPayments.Model.In;
using Qiwi.BillPayments.Model.Out;
using SmartWorkoutsWeb.Models;

namespace SmartWorkoutsWeb.Controllers
{
    public class Premium_WorksController : Controller
    {
        private SmartWorkouts_newEntities db = new SmartWorkouts_newEntities();

        // GET: Premium_Works
        public ActionResult Index()
        {
            if (!User.Identity.IsAuthenticated) ViewBag.IDBuySubscription = 0;
            else {

                int userId = db.Users.Where(p => p.Login == User.Identity.Name).FirstOrDefault().ID_User;
                ValidateSubscription.CheckBuy(userId);
                ValidateSubscription.CheckTime(userId);
                ViewBag.IDBuySubscription = Convert.ToInt32(db.Users.Where(p => p.Login == User.Identity.Name).FirstOrDefault().PremiumNumber);
            }

            return View(db.Premium_Works.ToList());
        }
        
        public ActionResult BuySubscription(string id)
        {
            string IdBuild = Guid.NewGuid().ToString();
            BillPaymentsClient client = BillPaymentsClientFactory.Create( secretKey: "48e7qUxn9T7RyYE1MVZswX1FRSbE6iyCj2gCRwwF3Dnh5XrasNTx3BGPiMsyXQFNKQhvukniQG8RTVhYm3iPpZQHyQ5YrT3phbZG6TNwPP2U7m5gKjuh6VnZHi2m3BJikoJLV4B2ApEazrH7h6XK1YmK9NZj5ZTRBq3faLpArjKkKtbuVSMXJbURipA7i");
            var SuccessUrl = new Uri("https://www.google.ru");

            var URL = client.CreatePaymentForm(
                  paymentInfo: new PaymentInfo
                  {
                      PublicKey = "48e7qUxn9T7RyYE1MVZswX1FRSbE6iyCj2gCRwwF3Dnh5XrasNTx3BGPiMsyXQFNKQhvukniQG8RTVhYm3iPpZQHyQ5YrT3phbZG6TNwPP2U7m5gKjuh6VnZHi2m3BJikoJLV4B2ApEazrH7h6XK1YmK9NZj5ZTRBq3faLpArjKkKtbuVSMXJbURipA7i",
                      Amount = new MoneyAmount
                      {
                          ValueDecimal = 1.0m,
                          CurrencyEnum = CurrencyEnum.Rub
                      },
                      BillId = IdBuild,
                      SuccessUrl = SuccessUrl
                  },
                  customFields: new CustomFields
                  {
                      ThemeCode = "Artem-Shfh2vS73eA"
                  }
              );
            int NumberPremWork = Convert.ToInt32(id);
            int idUser = db.Users.Where(p => p.Login == User.Identity.Name).FirstOrDefault().ID_User;
            Users users = db.Users.Where(p => p.ID_User == idUser).FirstOrDefault();
            users.PremiumNumber = NumberPremWork;
            db.SaveChanges();
            var price = db.Premium_Works.Where(p => p.Number_Premium_Work == NumberPremWork).FirstOrDefault().Price_Premium_Work;
            Contracts contracts = new Contracts { IdUser = idUser, NumberPremiumWork = NumberPremWork, PurchaseDate = DateTime.Now, ExpiryDate = DateTime.Now.AddDays(30), Price = price };
            db.Contracts.Add(contracts);
            db.SaveChanges();
            //var URL = client.CreateBill(
            //   info: new CreateBillInfo
            //   {
            //       BillId = IdBuild,
            //       Amount = new MoneyAmount
            //       {
            //           ValueDecimal = 199.9m,
            //           CurrencyEnum = CurrencyEnum.Rub
            //       },
            //       Comment = "comment",
            //       ExpirationDateTime = DateTime.Now.AddDays(45)
            //   }
            //   );
            //int IdUser = db.Users.Where(p => p.Login == User.Identity.Name).FirstOrDefault().ID_User;
            //BuildID build = new BuildID { IdUser = IdUser, IDBuild = IdBuild, NumberPremWork = NumberPremWork };
            //db.BuildID.Add(build);
            //db.SaveChanges();  
            var StringURL = URL.OriginalString;
            return Json(new { ask= StringURL });
        }

        // GET: Premium_Works/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Premium_Works premium_Works = db.Premium_Works.Find(id);
            if (premium_Works == null)
            {
                return HttpNotFound();
            }
            return View(premium_Works);
        }

        // GET: Premium_Works/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Premium_Works/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Number_Premium_Work,Name_Premium_Work,Description_Premium_Work,Price_Premium_Work")] Premium_Works premium_Works)
        {
            if (ModelState.IsValid)
            {
                db.Premium_Works.Add(premium_Works);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(premium_Works);
        }

        // GET: Premium_Works/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Premium_Works premium_Works = db.Premium_Works.Find(id);
            if (premium_Works == null)
            {
                return HttpNotFound();
            }
            return View(premium_Works);
        }

        // POST: Premium_Works/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Number_Premium_Work,Name_Premium_Work,Description_Premium_Work,Price_Premium_Work")] Premium_Works premium_Works)
        {
            if (ModelState.IsValid)
            {
                db.Entry(premium_Works).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(premium_Works);
        }

        // GET: Premium_Works/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Premium_Works premium_Works = db.Premium_Works.Find(id);
            if (premium_Works == null)
            {
                return HttpNotFound();
            }
            return View(premium_Works);
        }

        // POST: Premium_Works/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Premium_Works premium_Works = db.Premium_Works.Find(id);
            db.Premium_Works.Remove(premium_Works);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
