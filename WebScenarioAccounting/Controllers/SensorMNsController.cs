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
    public class SensorMNsController : Controller
    {
        private WebScenarioAccountingEntities db = new WebScenarioAccountingEntities();

        // GET: SensorMNs
        public ActionResult Index()
        {
            var sensorMNs = db.SensorMNs.Include(s => s.ClassifierSensor).Include(s => s.Room);
            return View(sensorMNs.ToList());
        }

        // GET: SensorMNs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SensorMN sensorMN = db.SensorMNs.Find(id);
            if (sensorMN == null)
            {
                return HttpNotFound();
            }
            return View(sensorMN);
        }

        // GET: SensorMNs/Create
        public ActionResult Create()
        {
            ViewBag.TypeID = new SelectList(db.ClassifierSensors, "id", "Name");
            ViewBag.RoomID = new SelectList(db.Rooms, "id", "Name");
            return View();
        }

        // POST: SensorMNs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,TypeID,RoomID,Description")] SensorMN sensorMN)
        {
            if (ModelState.IsValid)
            {
                db.SensorMNs.Add(sensorMN);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TypeID = new SelectList(db.ClassifierSensors, "id", "Name", sensorMN.TypeID);
            ViewBag.RoomID = new SelectList(db.Rooms, "id", "Name", sensorMN.RoomID);
            return View(sensorMN);
        }

        // GET: SensorMNs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SensorMN sensorMN = db.SensorMNs.Find(id);
            if (sensorMN == null)
            {
                return HttpNotFound();
            }
            ViewBag.TypeID = new SelectList(db.ClassifierSensors, "id", "Name", sensorMN.TypeID);
            ViewBag.RoomID = new SelectList(db.Rooms, "id", "Name", sensorMN.RoomID);
            return View(sensorMN);
        }

        // POST: SensorMNs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,TypeID,RoomID,Description")] SensorMN sensorMN)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sensorMN).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TypeID = new SelectList(db.ClassifierSensors, "id", "Name", sensorMN.TypeID);
            ViewBag.RoomID = new SelectList(db.Rooms, "id", "Name", sensorMN.RoomID);
            return View(sensorMN);
        }

        // GET: SensorMNs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SensorMN sensorMN = db.SensorMNs.Find(id);
            if (sensorMN == null)
            {
                return HttpNotFound();
            }
            return View(sensorMN);
        }

        // POST: SensorMNs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SensorMN sensorMN = db.SensorMNs.Find(id);
            db.SensorMNs.Remove(sensorMN);
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
