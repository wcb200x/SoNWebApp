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
    public class AlertsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Alerts
        [Authorize(Roles = ("SuperAdmin,Admin"))]
        public ActionResult Index()
        {
            return View(db.Alerts.ToList());
        }

        // GET: Alerts/Details/5
        [Authorize(Roles = ("SuperAdmin,Admin"))]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Alerts alerts = db.Alerts.Find(id);
            if (alerts == null)
            {
                return HttpNotFound();
            }
            return View(alerts);
        }

        // GET: Alerts/Create
        [Authorize(Roles = ("SuperAdmin,Admin"))]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Alerts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = ("SuperAdmin,Admin"))]
        public ActionResult Create([Bind(Include = "ID,Type,StartDate,EndDate")] Alerts alerts)
        {
            if (ModelState.IsValid)
            {
                db.Alerts.Add(alerts);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(alerts);
        }

        // GET: Alerts/Edit/5
        [Authorize(Roles = ("SuperAdmin,Admin"))]
        public ActionResult Edit(int? id)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            Alerts alerts = db.Alerts.Find(id);
            //if (alerts == null)
            //{
            //    return HttpNotFound();
            //}
            return View(alerts);
        }

        // POST: Alerts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = ("SuperAdmin,Admin"))]
        public ActionResult Edit([Bind(Include = "ID,Type,StartDate,EndDate")] Alerts alerts)
        {
            if (ModelState.IsValid)
            {
                db.Entry(alerts).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(alerts);
        }

        // GET: Alerts/Delete/5
        [Authorize(Roles = ("SuperAdmin,Admin"))]
        public ActionResult Delete(int? id)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            Alerts alerts = db.Alerts.Find(id);
            //if (alerts == null)
            //{
            //    return HttpNotFound();
            //}
            return View(alerts);
        }

        // POST: Alerts/Delete/5
        [Authorize(Roles = ("SuperAdmin,Admin"))]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Alerts alerts = db.Alerts.Find(id);
            db.Alerts.Remove(alerts);
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
