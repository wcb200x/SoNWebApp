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
    public class EnrollmentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Enrollments
        public ActionResult Index(string searchString)
        {

            var students = from s in db.Students
                           select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                students = students.Where(s => s.LastName.Contains(searchString)
                                              || s.FirstName.Contains(searchString)
                                              || s.StudentNumber.ToString().Contains(searchString));
            }
            var enrollments = db.Enrollments.Include(e => e.Course).Include(e => e.Program);
            return View(enrollments.ToList());
        }

        // GET: Enrollments/Details/5
        public ActionResult Details(int? id)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            Enrollment enrollment = db.Enrollments.Find(id);
            //if (enrollment == null)
            //{
            //    return HttpNotFound();
            //}
            return View(enrollment);
        }

        // GET: Enrollments/Create
        public ActionResult Create()
        {
            ViewBag.StudentID = new SelectList(db.Students, "ID", "StudentNumber");

            ViewBag.CourseID = new SelectList(db.Courses, "Id", "Subject");
            ViewBag.ProgramID = new SelectList(db.Programs, "ID", "Name");
            return View();
        }

        // POST: Enrollments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EnrollmentID,CourseID,StudentNumber,ProgramID,Semester")] Enrollment enrollment)
        {
            if (ModelState.IsValid)
            {
                db.Enrollments.Add(enrollment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.StudentID = new SelectList(db.Students, "ID", "StudentNumber", enrollment.StudentNumber);
            ViewBag.CourseID = new SelectList(db.Courses, "Id", "Subject", enrollment.CourseID);
            ViewBag.ProgramID = new SelectList(db.Programs, "ID", "Name", enrollment.ProgramID);
            return View(enrollment);
        }

        // GET: Enrollments/Edit/5
        public ActionResult Edit(int? id)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            Enrollment enrollment = db.Enrollments.Find(id);
            //if (enrollment == null)
            //{
            //    return HttpNotFound();
            //}
            ViewBag.StudentID = new SelectList(db.Students, "ID", "StudentNumber", enrollment.StudentNumber);

            ViewBag.CourseID = new SelectList(db.Courses, "Id", "Subject", enrollment.CourseID);
            ViewBag.ProgramID = new SelectList(db.Programs, "ID", "Name", enrollment.ProgramID);
            return View(enrollment);
        }

        // POST: Enrollments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EnrollmentID,CourseID,StudentNumber,ProgramID,Semester")] Enrollment enrollment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(enrollment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.StudentID = new SelectList(db.Students, "ID", "StudentNumber", enrollment.StudentNumber);

            ViewBag.CourseID = new SelectList(db.Courses, "Id", "Subject", enrollment.CourseID);
            ViewBag.ProgramID = new SelectList(db.Programs, "ID", "Name", enrollment.ProgramID);
            return View(enrollment);
        }

        // GET: Enrollments/Delete/5
        public ActionResult Delete(int? id)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            Enrollment enrollment = db.Enrollments.Find(id);
            //if (enrollment == null)
            //{
            //    return HttpNotFound();
            //}
            return View(enrollment);
        }

        // POST: Enrollments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Enrollment enrollment = db.Enrollments.Find(id);
            db.Enrollments.Remove(enrollment);
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
