using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SoNWebApp.Models;

namespace SoNWebApp.Controllers
{
    public class CampusController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Campus
        [Authorize(Roles = ("Advisor,Admin,SuperAdmin"))]
        public ActionResult Index()
        {
            return View(db.Campuses.ToList());
        }

        // GET: Campus/Details/5
        [Authorize(Roles = ("Advisor,Admin,SuperAdmin"))]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Campus campus = db.Campuses.Find(id);
            if (campus == null)
            {
                return HttpNotFound();
            }
            return View(campus);
        }

        // GET: Campus/Create
        [Authorize(Roles = ("Advisor,Admin,SuperAdmin"))]
        public ActionResult Create()
        {
            var campus = new Campus();
            var state = States();
            campus.States = GetStatesListItems(state);
            return View(campus);
        }

        // POST: Campus/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = ("Advisor,Admin,SuperAdmin"))]
        public ActionResult Create([Bind(Include = "CampusID,Name,City,State,Address,ZipCode")] Campus campus)
        {
            var state = States();
            campus.States = GetStatesListItems(state);
            if (ModelState.IsValid)
            {
                db.Campuses.Add(campus);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(campus);
        }

        // GET: Campus/Edit/5
        [Authorize(Roles = ("Advisor,Admin,SuperAdmin"))]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Campus campus = db.Campuses.Find(id);
            if (campus == null)
            {
                return HttpNotFound();
            }
            var state = States();
            campus.States = GetStatesListItems(state);
            return View(campus);
        }

        // POST: Campus/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = ("Advisor,Admin,SuperAdmin"))]
        public ActionResult Edit([Bind(Include = "CampusID,Name,City,State,Address,ZipCode")] Campus campus)
        {
            var state = States();
            campus.States = GetStatesListItems(state);
            if (ModelState.IsValid)
            {
                db.Entry(campus).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(campus);
        }

        // GET: Campus/Delete/5
        [Authorize(Roles = ("Advisor,Admin,SuperAdmin"))]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Campus campus = db.Campuses.Find(id);
            if (campus == null)
            {
                return HttpNotFound();
            }
            return View(campus);
        }

        // POST: Campus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = ("Advisor,Admin,SuperAdmin"))]
        public ActionResult DeleteConfirmed(int id)
        {
            Campus campus = db.Campuses.Find(id);
            db.Campuses.Remove(campus);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public IEnumerable<string> States()
        {
            return new List<string>
            {
               "Kentucky",
               

            };

        }
        public IEnumerable<SelectListItem> GetStatesListItems(IEnumerable<string> items)
        {
            var selectList = new List<SelectListItem>();
            foreach (var element in items)
            {
                selectList.Add(new SelectListItem
                {
                    Value = element,
                    Text = element
                });
            }
            return selectList;
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
