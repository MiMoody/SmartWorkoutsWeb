using SmartWorkoutsWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace SmartWorkoutsWeb.Controllers
{
    public class HomeController : Controller
    {
        private SmartWorkouts_newEntities db = new SmartWorkouts_newEntities();

        private static string GetMD5Hash(string text)
        {
            using (var hashAlg = MD5.Create()) // Создаем экземпляр класса реализующего алгоритм MD5
            {
                byte[] hash = hashAlg.ComputeHash(Encoding.UTF8.GetBytes(text)); // Хешируем байты строки text
                var builder = new StringBuilder(hash.Length * 2); // Создаем экземпляр StringBuilder. Этот класс предназначен для эффективного конструирования строк
                for (int i = 0; i < hash.Length; i++)
                {
                    builder.Append(hash[i].ToString("X2")); // Добавляем к строке очередной байт в виде строки в 16-й системе счисления
                }
                return builder.ToString(); // Возвращаем значение хеша
            }
        }


        public ActionResult Index()
        {
            var list = db.Posts.OrderByDescending(model=>model.ID_Post).Take(5);
            var Infouser = User.Identity.Name;
            var r = 123;
            return View(list);
        }

        [HttpPost]
        async public Task SendMessageEmail(string Name, string Email, string Comment)
        {
               
                    MailAddress from = new MailAddress("svev2369@gmail.com", Name);
                    MailAddress to = new MailAddress("Shevchenk201@yandex.ru");
                    MailMessage m = new MailMessage(from, to);
                    m.Subject = $"Комментарий от пользователя - {Name} ";
                    m.Body = $"Email отправителя: {Email} \n" + Comment;
                    SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                    smtp.Credentials = new NetworkCredential("svev2369@gmail.com", "artemchik34");
                    smtp.EnableSsl = true;
                   await smtp.SendMailAsync(m);
                
        }


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        

        [Authorize]
        public ActionResult Logout()
        {
            Session.Clear();
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        public ActionResult AccessError()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult Login()
        {
            if (Request.IsAuthenticated)
                return RedirectToAction("AccessError");
            else
                return View();
        }

        [HttpPost]
        public ActionResult Login( string Login, string Password)
        {
            var result = db.Users.FirstOrDefault(u => u.Login == Login && u.Password == Password);
            if (result == null)
            {
                var login = db.Users.FirstOrDefault(u => u.Login == Login);
                bool LoginCheck = true, PasswordCheck = true;
                if (login != null)
                {
                    LoginCheck = true;
                    PasswordCheck = false;
                }
                else 
                {
                    PasswordCheck = true;
                    LoginCheck = false;
                }
                return Json(new { LoginCheck, PasswordCheck, AllResult = false });
            }
            else
            {
                FormsAuthentication.SetAuthCookie(Login, false);
                Session["Menu"] = null;
                return Json(new { LoginCheck = "", PasswordCheck = "", AllResult = true });
            }

        }
    }
}