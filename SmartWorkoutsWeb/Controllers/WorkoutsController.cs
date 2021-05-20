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
    public class WorkoutsController : Controller
    {
        private SmartWorkouts_newEntities db = new SmartWorkouts_newEntities();

        // GET: Workouts
        public ActionResult Index()
        {
            var workouts = db.Workouts.Include(w => w.Premium_Works).Include(w => w.Types_Workout);
            return View(workouts.ToList());
        }

        
        [HttpPost]
        public ActionResult LoadWorkouts(string Type)
        {
            IQueryable<Workouts> workouts=null;
            switch (Type)
            {
                case "Legs":
                     workouts = db.Workouts.Where(p => p.Type_Workout == 3);
                break;

                case "Back":
                     workouts = db.Workouts.Where(p => p.Type_Workout == 2);
                   break;

                case "Breast":
                    workouts = db.Workouts.Where(p => p.Type_Workout == 1);
                    break;
                case "Hands":
                    workouts = db.Workouts.Where(p => p.Type_Workout == 4);
                    break;
                case "Press":
                    workouts = db.Workouts.Where(p => p.Type_Workout == 8);
                    break;

                case "AllBody":
                    workouts = db.Workouts.Where(p => p.Type_Workout == 7);
                    break;
            }

            return PartialView(workouts);
        }

        [HttpPost]
        public ActionResult LoadExercisesInWorkouts(string Id)
        {
            int idWork = Convert.ToInt32(Id);
            var workoutsElements = db.WorkoutElements.Where(w => w.ID_Workout == idWork).Include(w => w.Exercises);
            return PartialView(workoutsElements);
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
