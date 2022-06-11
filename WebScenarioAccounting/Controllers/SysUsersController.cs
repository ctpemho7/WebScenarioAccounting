using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebScenarioAccounting;

namespace WebScenarioAccounting.Controllers
{
    public class SysUsersController : Controller
    {
        private WebScenarioAccountingEntities db = new WebScenarioAccountingEntities();

        // GET: SysUsers
        public ActionResult Index()
        {
            var sysUsers = db.SysUsers.Include(s => s.ClassifierSex).Include(s => s.ClassifierUserType);
            return View(sysUsers.ToList());
        }

        // GET: SysUsers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SysUser sysUser = db.SysUsers.Find(id);
            if (sysUser == null)
            {
                return HttpNotFound();
            }
            return View(sysUser);
        }

        // GET: SysUsers/Create
        public ActionResult Create()
        {
            ViewBag.Sex = new SelectList(db.ClassifierSexes, "SexID", "SexName");
            ViewBag.UserType = new SelectList(db.ClassifierUserTypes, "TypeID", "TypeName");
            return View();
        }

        // POST: SysUsers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,Passport,Surname,Name,Patronymic,Birthdate,Sex,UserType,Login,Password,TerminationDate")] SysUser sysUser)
        {
            if (ModelState.IsValid)
            {
                db.SysUsers.Add(sysUser);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Sex = new SelectList(db.ClassifierSexes, "SexID", "SexName", sysUser.Sex);
            ViewBag.UserType = new SelectList(db.ClassifierUserTypes, "TypeID", "TypeName", sysUser.UserType);
            return View(sysUser);
        }

        // GET: SysUsers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SysUser sysUser = db.SysUsers.Find(id);
            if (sysUser == null)
            {
                return HttpNotFound();
            }
            ViewBag.Sex = new SelectList(db.ClassifierSexes, "SexID", "SexName", sysUser.Sex);
            ViewBag.UserType = new SelectList(db.ClassifierUserTypes, "TypeID", "TypeName", sysUser.UserType);
            return View(sysUser);
        }

        // POST: SysUsers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,Passport,Surname,Name,Patronymic,Birthdate,Sex,UserType,Login,Password,TerminationDate")] SysUser sysUser)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sysUser).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Sex = new SelectList(db.ClassifierSexes, "SexID", "SexName", sysUser.Sex);
            ViewBag.UserType = new SelectList(db.ClassifierUserTypes, "TypeID", "TypeName", sysUser.UserType);
            return View(sysUser);
        }

        // GET: SysUsers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SysUser sysUser = db.SysUsers.Find(id);
            if (sysUser == null)
            {
                return HttpNotFound();
            }
            return View(sysUser);
        }

        // POST: SysUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SysUser sysUser = db.SysUsers.Find(id);
            db.SysUsers.Remove(sysUser);
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
