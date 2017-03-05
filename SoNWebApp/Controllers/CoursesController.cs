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
    public class CoursesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Courses
        public ActionResult Index(string searchString2)
        {
            var course = from c in db.Courses
                           select c;
            if (!String.IsNullOrEmpty(searchString2))
            {
                course = course.Where(c => c.Title.Contains(searchString2)
                                              || c.ProgramID.ToString().Contains(searchString2));
                                         
            }
            return View(course.ToList());
        }

        // GET: Courses/Details/5
        public ActionResult Details(int? id)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            Courses courses = db.Courses.Find(id);
            //if (courses == null)
            //{
            //    return HttpNotFound();
            //}
            return View(courses);
        }

        // GET: Courses/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Courses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Subject,CatalogNumber,Title,Credits,ProgramID")] Courses courses)
        {
            if (ModelState.IsValid)
            {
                db.Courses.Add(courses);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(courses);
        }

        // GET: Courses/Edit/5
        public ActionResult Edit(int? id)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            Courses courses = db.Courses.Find(id);
            //if (courses == null)
            //{
            //    return HttpNotFound();
            //}
            return View(courses);
        }

        // POST: Courses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Subject,CatalogNumber,Title,Credits,ProgramID")] Courses courses)
        {
            if (ModelState.IsValid)
            {
              
                db.Entry(courses).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(courses);
        }

        // GET: Courses/Delete/5
        public ActionResult Delete(int? id)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            Courses courses = db.Courses.Find(id);
            //if (courses == null)
            //{
            //    return HttpNotFound();
            //}
            return View(courses);
        }

        // POST: Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Courses courses = db.Courses.Find(id);
            db.Courses.Remove(courses);
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
