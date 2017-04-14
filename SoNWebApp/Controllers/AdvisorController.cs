using SoNWebApp.Models;
using SoNWebApp.Models.ViewModels;
using System;
using System.Collections;
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
                StudentsList = db.Students.ToList(),

                TodosList = db.Todos.Where(t => t.EndDate >= DateTime.Today).Take(5)
            };
            return View(viewModel);
        }
        public ActionResult StudentReport()
        {
            return View(db.Students.ToList());
        }
        public ActionResult ComplianceReport()
        {
            var students = db.Compliances.Where(c => c.IsCompliant == false).ToList();

            return View(students);
        }
        public ActionResult GPAReport(decimal gpaThreshold)
        {
            var Student = db.Students.Where(s => s.GPA >= gpaThreshold && s.HasGraduated == false).ToList();
            return View(Student.ToList());
        }
        public ActionResult AdDefault()
        {
            
            var firstFiveOnGoingAlerts = db.Alerts.Where(a => a.EndDate >= DateTime.Today).OrderBy(a => a.StartDate).Take(5).ToList();

            var alertList = new List<string>();
            foreach (var alert in firstFiveOnGoingAlerts)
            {
                if (alert.Type == "Compliance")
                {
                    var students = db.Students.Select(s => s.ID);
                    var incompliantStudents = db.Compliances.Where(c => c.IsCompliant == false).Select(c => c.StudentID).Distinct().ToList().Count();
                    alertList.Add(incompliantStudents + " student(s) out of compliance.");
                    alert.Message = incompliantStudents.ToString();
                    db.SaveChanges();
                }
                if(alert.Type == "Event")
                {
                    var events = db.Events.Where(s => s.start_date > DateTime.Now).OrderBy(d => d.start_date).FirstOrDefault().text ;
                    var eventtime = db.Events.Where(s => s.start_date > DateTime.Now).OrderBy(d => d.start_date).FirstOrDefault().start_date;
                    alertList.Add(events + " is the next event on the calendar." + " It is on " + eventtime);
                    alert.Message = events;
                    db.SaveChanges();
                }
                if (alert.Type == "Application")
                {
                    var application = db.UDApplications.Where(s => s.Status != "Approved" && s.Status != "Wait Listed" && s.Status != "Denied" && s.Status != "Being Reviewed").ToList().Count();
                    alertList.Add(application + " Upper Division application(s) have not yet been seen.");
                    alert.Message = application.ToString();
                    db.SaveChanges();
                }
                if(alert.Type == "GPA")
                {
                    var students = db.Students.ToList();

                    var allgpa = new List<decimal>();
                    foreach (var gpa in students)
                    {
                        var gpas = gpa.GPA;

                        allgpa.Add(gpas);
                        
                    }

                    var sumgpa = allgpa.Sum();
                    var countofgpa = db.Students.Where(s => s.GPA > 0.0M).Distinct().Count();

                    var averagegpa = sumgpa / countofgpa;

                    alertList.Add(averagegpa + " is the current average GPA of all students.");
                    alert.Message = averagegpa.ToString();
                    db.SaveChanges();

                }
            }
        
            var viewModel = new AdvisorDefaultViewModel()
            {
                AlertList = alertList,
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
            var students = db.Students.ToList();
            return PartialView("_StudentsPartial", students);
        }
        public PartialViewResult GetAlertList()
        {
            var firstFiveOnGoingAlerts = db.Alerts.Where(a => a.EndDate >= DateTime.Today).OrderBy(a => a.StartDate).Take(5);

            var alertList = new List<string>();
            foreach (var alert in firstFiveOnGoingAlerts)
            {
                if (alert.Type == "Compliance")
                {
                    var students = db.Students.Select(s => s.ID);
                    var incompliantStudents = db.Compliances.Where(c => c.IsCompliant == false).Select(c => c.ID).Distinct().Count();
                    alertList.Add(incompliantStudents + " students out of compliance.");

                }
            }

            return PartialView("_AlertsPartial", alertList);
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