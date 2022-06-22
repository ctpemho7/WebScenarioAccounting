using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebScenarioAccounting.Models;

namespace WebScenarioAccounting.Controllers
{
    public class AccountController : Controller
    {
        private WebScenarioAccountingEntities db = new WebScenarioAccountingEntities();


        // GET: Account/Login
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                // поиск пользователя в бд           
                var user = db.SysUsers.FirstOrDefault(u => u.Login == model.Login && u.Password == model.Password);

                if (user != null)
                {
                    FormsAuthentication.SetAuthCookie(model.Login, true);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Пользователя с таким логином и паролем нет");
                }
            }

            return View(model);
        }


        public ActionResult Logoff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account");
        }

        // GET: Account/Register
        public ActionResult Register()
        {
            ViewBag.Sex = new SelectList(db.ClassifierSexes, "SexID", "SexName");
            ViewBag.UserType = new SelectList(db.ClassifierUserTypes, "TypeID", "TypeName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var user = db.SysUsers.FirstOrDefault(u => u.Login == model.Login);


                if (user == null)
                {
                    // создаем нового пользователя
                   
                    db.SysUsers.Add(new SysUser {
                        id = NewId(),
                        Surname = model.Surname,
                        Name = model.Name,
                        Patronymic = model.Patronymic,
                        Sex = 1,
                        UserType = 1,
                        Login = model.Login,
                        Password = model.Password,
                        TerminationDate = null}); ;
                   
                    db.SaveChanges();
                    //Console.WriteLine(NewId().ToString(), model.Surname, model.Name, model.Patronymic, model.Login, model.Password);
                    user = db.SysUsers.Where(u => u.Login == model.Login && u.Password == model.Password).FirstOrDefault();

                    // если пользователь удачно добавлен в бд
                    if (user != null)
                    {
                        FormsAuthentication.SetAuthCookie(model.Login, true);
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Пользователь с таким логином уже существует");
                }
            }


            return View(model);
        }

        int NewId()
        {
            var a = db.SysUsers.Max(u => u.id);
            return a + 1;
        }


    }
}