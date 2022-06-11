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
    public class ActuatorsController : Controller
    {
        private WebScenarioAccountingEntities db = new WebScenarioAccountingEntities();

        // GET: Actuators
        public ActionResult Index()
        {
            var actuators = db.Actuators.Include(a => a.ClassifierSubTypeThing).Include(a => a.Room1);
            return View(actuators.ToList());
        }

        // GET: Actuators/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Actuator actuator = db.Actuators.Find(id);
            if (actuator == null)
            {
                return HttpNotFound();
            }
            return View(actuator);
        }

        // GET: Actuators/Create
        public ActionResult Create()
        {
            ViewBag.SubType = new SelectList(db.ClassifierSubTypeThings, "id", "Name");
            ViewBag.Room = new SelectList(db.Rooms, "id", "Name");
            return View();
        }

        // POST: Actuators/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,Manufacturer,Name,Room,SubType,TerminationDate")] Actuator actuator)
        {
            if (ModelState.IsValid)
            {
                db.Actuators.Add(actuator);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SubType = new SelectList(db.ClassifierSubTypeThings, "id", "Name", actuator.SubType);
            ViewBag.Room = new SelectList(db.Rooms, "id", "Name", actuator.Room);
            return View(actuator);
        }

        // GET: Actuators/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Actuator actuator = db.Actuators.Find(id);
            if (actuator == null)
            {
                return HttpNotFound();
            }
            ViewBag.SubType = new SelectList(db.ClassifierSubTypeThings, "id", "Name", actuator.SubType);
            ViewBag.Room = new SelectList(db.Rooms, "id", "Name", actuator.Room);
            return View(actuator);
        }

        // POST: Actuators/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,Manufacturer,Name,Room,SubType,TerminationDate")] Actuator actuator)
        {
            if (ModelState.IsValid)
            {
                db.Entry(actuator).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SubType = new SelectList(db.ClassifierSubTypeThings, "id", "Name", actuator.SubType);
            ViewBag.Room = new SelectList(db.Rooms, "id", "Name", actuator.Room);
            return View(actuator);
        }

        // GET: Actuators/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Actuator actuator = db.Actuators.Find(id);
            if (actuator == null)
            {
                return HttpNotFound();
            }
            return View(actuator);
        }

        // POST: Actuators/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Actuator actuator = db.Actuators.Find(id);
            db.Actuators.Remove(actuator);
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
