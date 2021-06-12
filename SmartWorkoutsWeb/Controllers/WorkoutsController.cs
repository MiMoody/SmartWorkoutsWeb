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
            int userId = db.Users.Where(p => p.Login == User.Identity.Name).FirstOrDefault().ID_User;
            ValidateSubscription.CheckBuy(userId);
            ValidateSubscription.CheckTime(userId);
            var workouts = db.Workouts.Include(w => w.Premium_Works).Include(w => w.Types_Workout);
            return View(workouts.ToList());
        }

        
        [HttpPost]
        public ActionResult LoadWorkouts(string Type)
        {
            int premwork = Convert.ToInt32(db.Users.Where(p => p.Login == User.Identity.Name).FirstOrDefault().PremiumNumber);

            IQueryable<Workouts> workouts=null;
            switch (Type)
            {
                case "Legs":
                    if (premwork == 7) workouts = db.Workouts.Where(p => p.Type_Workout == 3);
                    else workouts = db.Workouts.Where(p => p.Type_Workout == 3 && (p.Number_Premium_Workout == premwork || p.Number_Premium_Workout == 5));

                    break;

                case "Back":
                    if (premwork == 7) workouts = db.Workouts.Where(p => p.Type_Workout == 2);
                    else workouts = db.Workouts.Where(p => p.Type_Workout == 2 && (p.Number_Premium_Workout == premwork || p.Number_Premium_Workout == 5));
                    break;

                case "Breast":
                    if (premwork == 7) workouts = db.Workouts.Where(p => p.Type_Workout == 1);
                    else workouts = db.Workouts.Where(p => p.Type_Workout == 1 && (p.Number_Premium_Workout == premwork || p.Number_Premium_Workout == 5));
                    break;
                case "Hands":
                    if(premwork == 7) workouts = db.Workouts.Where(p => p.Type_Workout == 4);
                    else workouts = db.Workouts.Where(p => p.Type_Workout == 4 && (p.Number_Premium_Workout == premwork || p.Number_Premium_Workout == 5));
                    break;
                case "Press":
                    if (premwork == 7) workouts = db.Workouts.Where(p => p.Type_Workout == 8);
                    else workouts = db.Workouts.Where(p => p.Type_Workout == 8 && (p.Number_Premium_Workout == premwork || p.Number_Premium_Workout == 5));
                    break;

                case "AllBody":
                    if (premwork == 7) workouts = db.Workouts.Where(p => p.Type_Workout == 7);
                    else workouts = db.Workouts.Where(p => p.Type_Workout == 7 && (p.Number_Premium_Workout == premwork || p.Number_Premium_Workout == 5));   
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
