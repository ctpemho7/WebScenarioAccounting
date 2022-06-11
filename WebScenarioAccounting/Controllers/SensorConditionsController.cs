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
    public class SensorConditionsController : Controller
    {
        private WebScenarioAccountingEntities db = new WebScenarioAccountingEntities();

        // GET: SensorConditions
        public ActionResult Index()
        {
            var sensorConditions = db.SensorConditions.Include(s => s.ClassifierSign).Include(s => s.Scenario).Include(s => s.SensorMN);
            return View(sensorConditions.ToList());
        }

        // GET: SensorConditions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SensorCondition sensorCondition = db.SensorConditions.Find(id);
            if (sensorCondition == null)
            {
                return HttpNotFound();
            }
            return View(sensorCondition);
        }

        // GET: SensorConditions/Create
        public ActionResult Create()
        {
            ViewBag.SignID = new SelectList(db.ClassifierSigns, "id", "SignValue");
            ViewBag.ScenarioID = new SelectList(db.Scenarios, "id", "Name");
            ViewBag.SensorID = new SelectList(db.SensorMNs, "id", "Description");
            return View();
        }

        // POST: SensorConditions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,ScenarioID,SensorID,SignID,Value")] SensorCondition sensorCondition)
        {
            if (ModelState.IsValid)
            {
                db.SensorConditions.Add(sensorCondition);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SignID = new SelectList(db.ClassifierSigns, "id", "SignValue", sensorCondition.SignID);
            ViewBag.ScenarioID = new SelectList(db.Scenarios, "id", "Name", sensorCondition.ScenarioID);
            ViewBag.SensorID = new SelectList(db.SensorMNs, "id", "Description", sensorCondition.SensorID);
            return View(sensorCondition);
        }

        // GET: SensorConditions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SensorCondition sensorCondition = db.SensorConditions.Find(id);
            if (sensorCondition == null)
            {
                return HttpNotFound();
            }
            ViewBag.SignID = new SelectList(db.ClassifierSigns, "id", "SignValue", sensorCondition.SignID);
            ViewBag.ScenarioID = new SelectList(db.Scenarios, "id", "Name", sensorCondition.ScenarioID);
            ViewBag.SensorID = new SelectList(db.SensorMNs, "id", "Description", sensorCondition.SensorID);
            return View(sensorCondition);
        }

        // POST: SensorConditions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,ScenarioID,SensorID,SignID,Value")] SensorCondition sensorCondition)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sensorCondition).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SignID = new SelectList(db.ClassifierSigns, "id", "SignValue", sensorCondition.SignID);
            ViewBag.ScenarioID = new SelectList(db.Scenarios, "id", "Name", sensorCondition.ScenarioID);
            ViewBag.SensorID = new SelectList(db.SensorMNs, "id", "Description", sensorCondition.SensorID);
            return View(sensorCondition);
        }

        // GET: SensorConditions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SensorCondition sensorCondition = db.SensorConditions.Find(id);
            if (sensorCondition == null)
            {
                return HttpNotFound();
            }
            return View(sensorCondition);
        }

        // POST: SensorConditions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SensorCondition sensorCondition = db.SensorConditions.Find(id);
            db.SensorConditions.Remove(sensorCondition);
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
