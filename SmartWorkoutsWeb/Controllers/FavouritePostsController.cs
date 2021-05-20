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
    public class FavouritePostsController : Controller
    {
        private SmartWorkouts_newEntities db = new SmartWorkouts_newEntities();

        // GET: FavouritePosts
        public ActionResult Index()
        {
            var favouritePosts = db.FavouritePosts.Include(f => f.Posts).Include(f => f.Users);
            return View(favouritePosts.ToList());
        }

        // GET: FavouritePosts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FavouritePosts favouritePosts = db.FavouritePosts.Find(id);
            if (favouritePosts == null)
            {
                return HttpNotFound();
            }
            return View(favouritePosts);
        }

        // GET: FavouritePosts/Create
        public ActionResult Create()
        {
            ViewBag.ID_Post = new SelectList(db.Posts, "ID_Post", "NamePost");
            ViewBag.ID_User = new SelectList(db.Users, "ID_User", "Name");
            return View();
        }

        // POST: FavouritePosts/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdFavPost,ID_Post,ID_User")] FavouritePosts favouritePosts)
        {
            if (ModelState.IsValid)
            {
                db.FavouritePosts.Add(favouritePosts);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_Post = new SelectList(db.Posts, "ID_Post", "NamePost", favouritePosts.ID_Post);
            ViewBag.ID_User = new SelectList(db.Users, "ID_User", "Name", favouritePosts.ID_User);
            return View(favouritePosts);
        }

        // GET: FavouritePosts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FavouritePosts favouritePosts = db.FavouritePosts.Find(id);
            if (favouritePosts == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_Post = new SelectList(db.Posts, "ID_Post", "NamePost", favouritePosts.ID_Post);
            ViewBag.ID_User = new SelectList(db.Users, "ID_User", "Name", favouritePosts.ID_User);
            return View(favouritePosts);
        }

        // POST: FavouritePosts/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdFavPost,ID_Post,ID_User")] FavouritePosts favouritePosts)
        {
            if (ModelState.IsValid)
            {
                db.Entry(favouritePosts).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_Post = new SelectList(db.Posts, "ID_Post", "NamePost", favouritePosts.ID_Post);
            ViewBag.ID_User = new SelectList(db.Users, "ID_User", "Name", favouritePosts.ID_User);
            return View(favouritePosts);
        }

        // GET: FavouritePosts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FavouritePosts favouritePosts = db.FavouritePosts.Find(id);
            if (favouritePosts == null)
            {
                return HttpNotFound();
            }
            return View(favouritePosts);
        }

        // POST: FavouritePosts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FavouritePosts favouritePosts = db.FavouritePosts.Find(id);
            db.FavouritePosts.Remove(favouritePosts);
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
