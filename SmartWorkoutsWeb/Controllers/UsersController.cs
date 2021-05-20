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
    public class UsersController : Controller
    {
        private SmartWorkouts_newEntities db = new SmartWorkouts_newEntities();

      
        public ActionResult Create()
        {
            return View();
        }
        
        [HttpPost]
        public void Create(string UserName, string UserSurname,string UserEmail, string UserDateBirth, string UserLogin, string UserPassword)
        {
                Users users = new Users();
                users.Name = UserName;
                users.Surname = UserSurname;
                users.DateBirth = Convert.ToDateTime(UserDateBirth);
                users.Email = UserEmail;
                users.Login = UserLogin;
                users.Password = UserPassword;
                users.PremiumNumber = 5;
                users.RoleID = "U";
                db.Users.Add(users);
                db.SaveChanges();   
          
        }

        [HttpPost]
        public ActionResult CheckRepeatLogin( string UserLogin)
        {
                var user = db.Users.FirstOrDefault(p => p.Login == UserLogin);
            if(user!=null)
            {
                return Json(new { msg = true });   
            }
            else
            {
                return Json(new { msg = false });
            }
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
