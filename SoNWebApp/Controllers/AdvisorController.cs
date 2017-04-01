using SoNWebApp.Models;
using SoNWebApp.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SoNWebApp.Controllers
{
    [Authorize (Roles = ("Advisor,Admin,SuperAdmin"))]
    public class AdvisorController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Advisor
        public ActionResult Index()
        {
            return View();
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
        public ActionResult Reports()
        {
            return View();
        }
        public ActionResult AdClinicalCompliance()
        {
            return View(db.Students.ToList());
        }
        public ActionResult Search()
        {
            return View();
        }
        public ActionResult StudentRecords()
        {
            var viewModel = new StudentRecordsViewModel()
            {
                StudentsList = db.Students,

                TodosList = db.Todos.Where(t => t.EndDate >= DateTime.Today).Take(5)
            };
            return View(viewModel);
        }
        public ActionResult StudentReport()
        {
            return View(db.Students.ToList());
        }
        public ActionResult GPAReport(decimal gpaThreshold)
        {
            var Student = db.Students.Where(s => s.GPA >= gpaThreshold && s.HasGraduated == false).ToList();
            return View(Student.ToList());
        }
        public ActionResult AdDefault()
        {
            var firstFiveOnGoingAlerts = db.Alerts.Where(a => a.EndDate >= DateTime.Today).OrderBy(a => a.StartDate).Take(5);

            var alertList = new List<string>();
            foreach (var alert in firstFiveOnGoingAlerts)
            {
                if(alert.Type == "Compliance")
                {
                    var students = db.Students.Select(s => s.ID);
                    var incompliantStudents = db.Compliances.Where(c => c.IsCompliant == false).Select(c => c.ID).Distinct().Count();
                    alertList.Add(incompliantStudents + "students out of compliance.");

                }
            }
            var viewModel = new AdvisorDefaultViewModel()
            {
                TodosList = db.Todos.Where(t => t.EndDate >= DateTime.Today).Take(5)
                

            };
            return View(viewModel);
        }
        public ActionResult ViewDocuments()
        {
           
            var documents = db.Documents.ToList();


            return View(documents.ToList());
        }

        public PartialViewResult GetTodosList()
        {
            var todos = db.Todos.FirstOrDefault();

            return PartialView("_TodosPartial", todos);
        }
        public PartialViewResult GetStudentsList()
        {
            var students = db.Students.FirstOrDefault();
            return PartialView("_StudentsPartial", students);
        }
        public PartialViewResult GetAlertList()
        {
            var alerts = db.Alerts.FirstOrDefault();
            return PartialView("_AlertsPartial", alerts);
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