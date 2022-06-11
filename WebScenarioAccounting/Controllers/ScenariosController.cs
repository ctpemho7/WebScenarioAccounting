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
            ViewBag.ConditionString = "efewfaew"; //тут пока брух будет
            // сделать штуку для определения типа условий строка условия через term
            return View(scenario);
        }
        public void GetDetails(int? id)
        {
            //make view for another controller


            List<SelectListItem> a = new List<SelectListItem>();
            ViewBag.SensorConditions = a;

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
            List < SelectListItem > selectListItems = new List<SelectListItem>();
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
