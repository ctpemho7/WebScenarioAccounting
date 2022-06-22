using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebScenarioAccounting.Models;
using WebScenarioAccounting;

namespace WebScenarioAccounting.Controllers
{
    public class ScenariosController : Controller
    {
        private WebScenarioAccountingEntities db = new WebScenarioAccountingEntities();

        // GET: Scenarios
        public ActionResult Index()
        {
            var scenarios = db.Scenarios.Include(s => s.SysUser);
            return View(scenarios.ToList());
        }

        // GET: Scenarios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Scenario scenario = db.Scenarios.Find(id);
            if (scenario == null)
            {
                return HttpNotFound();
            }
            ViewBag.ConditionString = "(e1 И (e2 И e3)) ИЛИ (s3 И s2)"; //тут пока брух будет
            // сделать штуку для определения типа условий строка условия через term
            GetDetails(id);

            return View(scenario);
        }
        public void GetDetails(int? id)
        {
            var sensorConditions = db.SensorConditions.Include(s => s.ClassifierSign).Include(s => s.Scenario).Include(s => s.SensorMN).Where(s => s.ScenarioID == id);
            var a = sensorConditions.Join(db.ClassifierSensors,
                                          p => p.SensorMN.TypeID,
                                          c => c.id,
                                          (p, c) => new { RoomNum = p.SensorMN.RoomID, SensorName = c.Name, ClassifierSignValue = p.ClassifierSign.SignValue, Value = p.Value });



            var timeWeatherConditions = db.TimeWeatherConditions.Include(t => t.ClassifierTimeWeather).Include(t => t.Scenario).Where(s => s.ScenarioID == id);
            var timeweather = db.TimeWeatherConditions.Where(s => s.ScenarioID == id)
                .Join(db.ClassifierTimeWeathers, // второй набор
                 p => p.ValueID, // свойство-селектор объекта из первого набора
                 c => c.id, // свойство-селектор объекта из второго набора
                 (p, c) => new { ConditionID = p.id, Value = c.Name, TypeID = c.TypeID}); // результат

            var timeweatherfull = timeweather
                .Join(db.ClassifierValues, // второй набор
                 p => p.TypeID, // свойство-селектор объекта из первого набора
                 c => c.id, // свойство-селектор объекта из второго набора
                 (p, c) => new { ConditionID = p.ConditionID, Name = c.Name, Value = p.Value }); // результат
           
            List<TimeWeatherView> timeWeathers = new List<TimeWeatherView>();
            foreach (var item in timeweatherfull)
                timeWeathers.Add(new TimeWeatherView { ConditionID = item.ConditionID, Name = item.Name, Value = item.Value });


            var acts = db.Commands.Where(s => s.ScenarioID == id)
            .Join(db.Actuators,
                f => f.ThingID,
                s => s.id,
                (f, s) => new { ActuatorID = f.id, ActuatorName = s.Name, Manufacturer = s.Manufacturer, RoomID = s.Room, SubTypeID = s.SubType, CommandTextID = f.CommandID, isElse = f.isElse })
           .Join(db.Rooms,
                f => f.RoomID,
                s => s.id,
                (f, s) => new { ActuatorID = f.ActuatorID, Manufacturer = f.Manufacturer, ActuatorName = f.ActuatorName, RoomName = s.Name, SubTypeID = f.SubTypeID, CommandTextID = f.CommandTextID, isElse = f.isElse })

           .Join(db.ClassifierThingCommands,
                f => f.CommandTextID,
                s => s.id,
                (f, s) => new { ActuatorID = f.ActuatorID, Manufacturer = f.Manufacturer, ActuatorName = f.ActuatorName, RoomName = f.RoomName, CommandText = s.Text, isElse = f.isElse })
;


            List<ActionView> actionsElse = new List<ActionView>();
            List<ActionView> actionsThen = new List<ActionView>();
            foreach (var item in acts)
                if (item.isElse)
                    actionsElse.Add(new ActionView { ActuatorID = item.ActuatorID, Manufacturer = item.Manufacturer, ActuatorName = item.ActuatorName, RoomName = item.RoomName, CommandText = item.CommandText, isElse = item.isElse });
                else
                    actionsThen.Add(new ActionView { ActuatorID = item.ActuatorID, Manufacturer = item.Manufacturer, ActuatorName = item.ActuatorName, RoomName = item.RoomName, CommandText = item.CommandText, isElse = item.isElse });



            ViewBag.TimeWeatherConditions = timeWeathers;
            ViewBag.ActionsElse = actionsElse;
            ViewBag.ActionsThen = actionsThen;
            ViewBag.SensorConditions = sensorConditions.ToList();

        }

        // GET: Scenarios/Create
        public ActionResult Create()
        {
            passIdToList();

            //ViewBag.Author = new SelectList(db.SysUsers, "id", "Passport");
            return View();
        }

        // POST: Scenarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,Name,Author,Description,CreationDate,Condition,TerminationDate")] Scenario scenario)
        {

            if (ModelState.IsValid)
            {
                db.Scenarios.Add(scenario);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            //ViewBag.Author = new SelectList(db.SysUsers, "id", "Passport", scenario.Author);
            passIdToList();
            return View(scenario);
        }

        public void passIdToList()
        {
            var users = db.SysUsers.GroupBy(x => x.Name + " " + x.Surname + " " + x.Patronymic);
            List<SelectListItem> selectListItems = new List<SelectListItem>();
            foreach (var user in users)
            {
                var optionGroup = new SelectListGroup() { Name = user.Key };
                foreach (var item in user)
                    selectListItems.Add(new SelectListItem() { Value = item.id.ToString(), Text = item.Passport.ToString(), Group = optionGroup });
            }

            ViewBag.Author = selectListItems;
        }



        // GET: Scenarios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Scenario scenario = db.Scenarios.Find(id);
            if (scenario == null)
            {
                return HttpNotFound();
            }
            ViewBag.Author = new SelectList(db.SysUsers, "id", "Passport", scenario.Author);
            return View(scenario);
        }

        // POST: Scenarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,Name,Author,Description,CreationDate,Condition,TerminationDate")] Scenario scenario)
        {
            if (ModelState.IsValid)
            {
                db.Entry(scenario).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Author = new SelectList(db.SysUsers, "id", "Passport", scenario.Author);
            return View(scenario);
        }

        // GET: Scenarios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Scenario scenario = db.Scenarios.Find(id);
            if (scenario == null)
            {
                return HttpNotFound();
            }
            return View(scenario);
        }

        // POST: Scenarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Scenario scenario = db.Scenarios.Find(id);
            db.Scenarios.Remove(scenario);
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
