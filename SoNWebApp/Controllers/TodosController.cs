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
    [Authorize (Roles=("Admin, SuperAdmin"))]
    public class TodosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Todos
        public ActionResult Index()
        {
            return View(db.Todos.ToList());
        }

        // GET: Todos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Todos todos = db.Todos.Find(id);
            if (todos == null)
            {
                return HttpNotFound();
            }
            return View(todos);
        }

        // GET: Todos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Todos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Message,StartDate,EndDate")] Todos todos)
        {
            if (ModelState.IsValid)
            {
                db.Todos.Add(todos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(todos);
        }

        // GET: Todos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Todos todos = db.Todos.Find(id);
            if (todos == null)
            {
                return HttpNotFound();
            }
            return View(todos);
        }

        // POST: Todos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Message,StartDate,EndDate")] Todos todos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(todos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(todos);
        }

        // GET: Todos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Todos todos = db.Todos.Find(id);
            if (todos == null)
            {
                return HttpNotFound();
            }
            return View(todos);
        }

        // POST: Todos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Todos todos = db.Todos.Find(id);
            db.Todos.Remove(todos);
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
