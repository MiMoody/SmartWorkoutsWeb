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

        // GET: User_Progress/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User_Progress user_Progress = db.User_Progress.Find(id);
            if (user_Progress == null)
            {
                return HttpNotFound();
            }
            return View(user_Progress);
        }

        // GET: User_Progress/Create
        public ActionResult Create()
        {
            ViewBag.User_ID = new SelectList(db.Users, "ID_User", "Name");
            return View();
        }

        // POST: User_Progress/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_Progress,User_Wieght,User_Waist,User_Breast,Data_Measurement,User_ID")] User_Progress user_Progress)
        {
            if (ModelState.IsValid)
            {
                db.User_Progress.Add(user_Progress);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.User_ID = new SelectList(db.Users, "ID_User", "Name", user_Progress.User_ID);
            return View(user_Progress);
        }

        // GET: User_Progress/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User_Progress user_Progress = db.User_Progress.Find(id);
            if (user_Progress == null)
            {
                return HttpNotFound();
            }
            ViewBag.User_ID = new SelectList(db.Users, "ID_User", "Name", user_Progress.User_ID);
            return View(user_Progress);
        }

        // POST: User_Progress/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_Progress,User_Wieght,User_Waist,User_Breast,Data_Measurement,User_ID")] User_Progress user_Progress)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user_Progress).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.User_ID = new SelectList(db.Users, "ID_User", "Name", user_Progress.User_ID);
            return View(user_Progress);
        }

        // GET: User_Progress/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User_Progress user_Progress = db.User_Progress.Find(id);
            if (user_Progress == null)
            {
                return HttpNotFound();
            }
            return View(user_Progress);
        }

        // POST: User_Progress/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User_Progress user_Progress = db.User_Progress.Find(id);
            db.User_Progress.Remove(user_Progress);
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
