using SoNWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SoNWebApp.Controllers
{
    [Authorize (Roles= ("Admin, SuperAdmin"))]
    public class AdminController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Home()
        {
            return View();
        }
        public ActionResult Calendar()
        {
            return View();
        }
        public ActionResult DatabaseAdmin()
        {
            return View();
        }
        public ActionResult ManageAlerts()
        {
            return View();
        }
        public ActionResult ManageUsers()
        {
            return View();
        }
        public ActionResult Reports()
        {
            return View();
        }
        public ActionResult StudentReport()
        {
            return View(db.Students.ToList());
        }
        public ActionResult GPAReport(decimal gpaThreshold)
        {
            var Student = db.Students.Where(s => s.GPA >= gpaThreshold).ToList();
            return View(Student.ToList());
        }
        public ActionResult CRM(string sortOrder, string searchString)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.GPASortParm = sortOrder == "GPA" ? "GPA_desc" : "GPA";

            var students = from s in db.Students
                           select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                students = students.Where(s => s.LastName.Contains(searchString)
                                              || s.FirstName.Contains(searchString)
                                              || s.StudentNumber.ToString().Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    students = students.OrderByDescending(s => s.LastName);
                    break;
                case "GPA":
                    students = students.OrderBy(s => s.GPA);
                    break;
                case "GPA_desc":
                    students = students.OrderByDescending(s => s.GPA);
                    break;
                default:
                    students = students.OrderBy(s => s.LastName);
                    break;
            }

            return View(students.ToList());
        }
        public ActionResult Todos()
        {
            return View(); 
        }
        public ActionResult ProgramCoursesReport(int programNum)
        {
            var classes = db.Courses.Where(p => p.ProgramID == programNum).ToList();
            return View(classes.ToList());
        }
        public ActionResult StudentsInPrograms(int program)
        {
            var students = db.Students.Where(s => s.ProgramID == program && s.HasGraduated == false).ToList();
            return View(students.ToList());
        }
    }

}