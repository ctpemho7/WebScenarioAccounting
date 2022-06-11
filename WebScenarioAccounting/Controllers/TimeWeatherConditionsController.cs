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
    public class TimeWeatherConditionsController : Controller
    {
        private WebScenarioAccountingEntities db = new WebScenarioAccountingEntities();

        // GET: TimeWeatherConditions
        public ActionResult Index()
        {
            var timeWeatherConditions = db.TimeWeatherConditions.Include(t => t.ClassifierTimeWeather).Include(t => t.Scenario);
            return View(timeWeatherConditions.ToList());
        }

        // GET: TimeWeatherConditions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TimeWeatherCondition timeWeatherCondition = db.TimeWeatherConditions.Find(id);
            if (timeWeatherCondition == null)
            {
                return HttpNotFound();
            }
            return View(timeWeatherCondition);
        }

        // GET: TimeWeatherConditions/Create
        public ActionResult Create()
        {
            ViewBag.ValueID = new SelectList(db.ClassifierTimeWeathers, "id", "Name");
            ViewBag.ScenarioID = new SelectList(db.Scenarios, "id", "Name");
            return View();
        }

        // POST: TimeWeatherConditions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,ScenarioID,ValueID")] TimeWeatherCondition timeWeatherCondition)
        {
            if (ModelState.IsValid)
            {
                db.TimeWeatherConditions.Add(timeWeatherCondition);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ValueID = new SelectList(db.ClassifierTimeWeathers, "id", "Name", timeWeatherCondition.ValueID);
            ViewBag.ScenarioID = new SelectList(db.Scenarios, "id", "Name", timeWeatherCondition.ScenarioID);
            return View(timeWeatherCondition);
        }

        // GET: TimeWeatherConditions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TimeWeatherCondition timeWeatherCondition = db.TimeWeatherConditions.Find(id);
            if (timeWeatherCondition == null)
            {
                return HttpNotFound();
            }
            ViewBag.ValueID = new SelectList(db.ClassifierTimeWeathers, "id", "Name", timeWeatherCondition.ValueID);
            ViewBag.ScenarioID = new SelectList(db.Scenarios, "id", "Name", timeWeatherCondition.ScenarioID);
            return View(timeWeatherCondition);
        }

        // POST: TimeWeatherConditions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,ScenarioID,ValueID")] TimeWeatherCondition timeWeatherCondition)
        {
            if (ModelState.IsValid)
            {
                db.Entry(timeWeatherCondition).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ValueID = new SelectList(db.ClassifierTimeWeathers, "id", "Name", timeWeatherCondition.ValueID);
            ViewBag.ScenarioID = new SelectList(db.Scenarios, "id", "Name", timeWeatherCondition.ScenarioID);
            return View(timeWeatherCondition);
        }

        // GET: TimeWeatherConditions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TimeWeatherCondition timeWeatherCondition = db.TimeWeatherConditions.Find(id);
            if (timeWeatherCondition == null)
            {
                return HttpNotFound();
            }
            return View(timeWeatherCondition);
        }

        // POST: TimeWeatherConditions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TimeWeatherCondition timeWeatherCondition = db.TimeWeatherConditions.Find(id);
            db.TimeWeatherConditions.Remove(timeWeatherCondition);
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
