﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SoNWebApp.Models;
using System.Collections;
using System.Web.Helpers;

namespace SoNWebApp.Controllers
{

    [Authorize(Roles = ("Advisor,Admin,SuperAdmin"))]
    public class EnrollmentsController : Controller
    {
 

        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Enrollments
        public ActionResult Index(string searchString)
        {

            var studentsenrolled = from e in db.Enrollments
                           select e;
            if (!String.IsNullOrEmpty(searchString))
            {
                studentsenrolled = studentsenrolled.Where(e => e.Student.LastName.Contains(searchString)
                                              || e.Student.FirstName.Contains(searchString)
                                              || e.Student.StudentNumber.ToString().Contains(searchString));
                                          
            }

            

            var enrollments = db.Enrollments.Include(e => e.Course).Include(e => e.Program);
            return View(studentsenrolled.ToList());
        }

        // GET: Enrollments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Enrollment enrollment = db.Enrollments.Find(id);
            if (enrollment == null)
            {
                return HttpNotFound();
            }
            return View(enrollment);
        }

        // GET: Enrollments/Create
        public ActionResult Create()
        {
            var semester = Semesters();
            var grades = LetterGrades();
            var enrollment = new Enrollment();
            enrollment.Grades = GetSelectListItems(grades);
            enrollment.Semesters = GetSemesterListItems(semester);

            ViewBag.StudentID = new SelectList(db.Students, "ID", "StudentNumber");
            ViewBag.CourseID = new SelectList(db.Courses, "Id", "Subject");
            ViewBag.ProgramID = new SelectList(db.Programs, "ID", "Name");
            return View(enrollment);
        }

        // POST: Enrollments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EnrollmentID,CourseID,StudentID,ProgramID,Semester,Grade")] Enrollment enrollment)
        {
            var semester = Semesters();
            var grades = LetterGrades();
            enrollment.Grades = GetSelectListItems(grades);
            enrollment.Semesters = GetSemesterListItems(semester);

            if (ModelState.IsValid)
            {
                db.Enrollments.Add(enrollment);
                db.SaveChanges();
                var enrollments = db.Enrollments.Where(e => e.StudentID == enrollment.StudentID);
                var actualGradeList = new List<decimal>();
                foreach (var enrollmentGrade in enrollments)
                {
                    var actualGrade = GetCourseGrade(enrollmentGrade.Grade);
                    actualGradeList.Add(actualGrade);
                }


                var testGpaSum = actualGradeList.Sum();
                var testGpaGradeCount = actualGradeList.Count() * 4;

                var testGpa = (testGpaSum / testGpaGradeCount) * 4.0M;


                //GradePointAverage = db.Students.FirstOrDefault(s => s.ID == id).GPA.ToString();
                //GradePointAverage = actualgrade;            


                Student student = db.Students.FirstOrDefault(s => s.ID == enrollment.StudentID);
                student.GPA = testGpa;

        
               
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.StudentID = new SelectList(db.Students, "ID", "StudentNumber", enrollment.StudentID);
            ViewBag.CourseID = new SelectList(db.Courses, "Id", "Subject", enrollment.CourseID);
            ViewBag.ProgramID = new SelectList(db.Programs, "ID", "Name", enrollment.ProgramID);

            return View(enrollment);
        }
 
        // GET: Enrollments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Enrollment enrollment = db.Enrollments.Find(id);
            if (enrollment == null)
            {
                return HttpNotFound();
            }
            var semester = Semesters();
            var grades = LetterGrades();
            enrollment.Grades = GetSelectListItems(grades);
            enrollment.Semesters = GetSemesterListItems(semester);
            ViewBag.StudentID = new SelectList(db.Students, "ID", "StudentNumber", enrollment.StudentID);
            ViewBag.CourseID = new SelectList(db.Courses, "Id", "Subject", enrollment.CourseID);
            ViewBag.ProgramID = new SelectList(db.Programs, "ID", "Name", enrollment.ProgramID);
            return View(enrollment);
        }

        // POST: Enrollments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EnrollmentID,CourseID,StudentID,ProgramID,Semester,Grade")] Enrollment enrollment)
        {
            var semester = Semesters();
            var grades = LetterGrades();
            enrollment.Grades = GetSelectListItems(grades);
            enrollment.Semesters = GetSemesterListItems(semester);
            if (ModelState.IsValid)
            {
                db.Entry(enrollment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.StudentID = new SelectList(db.Students, "ID", "StudentNumber", enrollment.StudentID);
            ViewBag.CourseID = new SelectList(db.Courses, "Id", "Subject", enrollment.CourseID);
            ViewBag.ProgramID = new SelectList(db.Programs, "ID", "Name", enrollment.ProgramID);
            return View(enrollment);
        }

        // GET: Enrollments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Enrollment enrollment = db.Enrollments.Find(id);
            if (enrollment == null)
            {
                return HttpNotFound();
            }
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
        public decimal GetCourseGrade(string courseLetterGrade)
        {
            var actualgrade = 0.0M;

            if (courseLetterGrade == "A+")
            {
                actualgrade = 4.0M;
            }
            else if (courseLetterGrade == "A")
            {
                actualgrade = 4.0M;
            }
            else if (courseLetterGrade == "A-")
            {
                actualgrade = 3.7M;
            }
            else if (courseLetterGrade == "B+")
            {
                actualgrade = 3.3M;
            }
            else if (courseLetterGrade == "B")
            {
                actualgrade = 3.0M;
            }
            else if (courseLetterGrade == "B-")
            {
                actualgrade = 2.7M;
            }
            else if (courseLetterGrade == "C+")
            {
                actualgrade = 2.3M;
            }
            else if (courseLetterGrade == "C")
            {
                actualgrade = 2.0M;
            }
            else if (courseLetterGrade == "C-")
            {
                actualgrade = 1.7M;
            }
            else if (courseLetterGrade == "D+")
            {
                actualgrade = 1.3M;
            }
            else if (courseLetterGrade == "D")
            {
                actualgrade = 1.0M;
            }
            else if (courseLetterGrade == "D-")
            {
                actualgrade = 0.7M;
            }
            else if (courseLetterGrade == "F")
            {
                actualgrade = 0.0M;
            }

            return actualgrade;
        }
    public IEnumerable<string> LetterGrades()
        {
            return new List<string>
            {
                "A+",
                "A",
                "A-",
                "B+",
                "B",
                "B-",
                "C+",
                "C",
                "C-",
                "D+",
                "D",
                "D-",
                "F",
            };

        }
        public IEnumerable<SelectListItem> GetSelectListItems (IEnumerable<string> elements)
        {
            var selectList = new List<SelectListItem>();
            foreach (var element in elements)
            {
                selectList.Add(new SelectListItem
                {
                    Value = element,
                    Text = element
                });
            }
                return selectList;
        }

        public IEnumerable<string> Semesters()
        {
            return new List<string>
            {
                "Summer 2017",
                "Fall 2017",
                "Spring 2018",
                "Summer 2018",
                "Fall 2018",
                "Spring 2019",
              
            };

        }
        public IEnumerable<SelectListItem> GetSemesterListItems(IEnumerable<string> items)
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
        public JsonResult GetCourses(int ProgramID)
        {
            var coursesList = new List<SelectListItem>();

            var courses = db.Courses.Where(c => c.ProgramID == ProgramID).Select(course => new SelectListItem {Text = course.Subject, Value = course.Id.ToString()}).ToList();

            return Json(courses);
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
