using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SmartWorkoutsWeb.Models;

namespace SmartWorkoutsWeb.Controllers
{
    public class User_ProgressController : Controller
    {
        private SmartWorkouts_newEntities db = new SmartWorkouts_newEntities();

        // GET: User_Progress
        public ActionResult Index()
        {
            var user_Progress = db.User_Progress.Include(u => u.Users);
            return View(user_Progress.ToList());
        }

        [HttpPost]
        public ActionResult GetProgress()
        {
            if (User.Identity.IsAuthenticated)
            {
                var id = db.Users.Where(p => p.Login == User.Identity.Name).FirstOrDefault().ID_User;

                var progress = db.User_Progress.Where(p => p.User_ID == id).FirstOrDefault();

                if (progress == null)
                {
                    return Json(new { StartWeight = "0", CurrentWeight = "0", DesiredWeight = "0" });
                }

                return Json(new { StartWeight = progress.StartWeight, CurrentWeight = progress.CurrentWeight, DesiredWeight = progress.DesiredWeight});
            }
            return Json(new { StartWeight = "0", CurrentWeight = "0", DesiredWeight = "0" });

        }

        [HttpPost]
        public void UpdateCreateProgress(string StartWeight, string CurrentWeight, string DesiredWeight)
        {
            if (User.Identity.IsAuthenticated)
            {
                StartWeight = StartWeight.Replace(".", ",");
                CurrentWeight = CurrentWeight.Replace(".", ",");
                DesiredWeight = DesiredWeight.Replace(".", ",");
                var id = db.Users.Where(p => p.Login == User.Identity.Name).FirstOrDefault().ID_User;
                var progress = db.User_Progress.Where(p => p.User_ID == id).FirstOrDefault();
                if (progress!=null)
                {
                    progress.StartWeight = Convert.ToDecimal(StartWeight);
                    progress.CurrentWeight = Convert.ToDecimal(CurrentWeight);
                    progress.DesiredWeight = Convert.ToDecimal(DesiredWeight);
                    db.SaveChanges();
                }
                else
                {
                    User_Progress InfoProgress = new User_Progress();
                    InfoProgress.User_ID = id;
                    InfoProgress.StartWeight = Convert.ToDecimal(StartWeight);
                    InfoProgress.CurrentWeight = Convert.ToDecimal(CurrentWeight);
                    InfoProgress.DesiredWeight = Convert.ToDecimal(DesiredWeight);
                    db.User_Progress.Add(InfoProgress);
                    db.SaveChanges();
                }
            }
        }
        // POST: User_Progress/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
       
        // GET: User_Progress/Edit/5
     
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
