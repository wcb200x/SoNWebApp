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
        public ActionResult Search()
        {
            return View();
        }
        public ActionResult StudentRecords()
        {
            return View(db.Students.ToList());
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
        public ActionResult AdDefault()
        {
            var viewModel = new AdvisorDefaultViewModel()
            {
                TodosList = db.Todos.Where(t => t.EndDate >= DateTime.Today).Take(5)
            };
            return View(viewModel);
        }

        public PartialViewResult GetTodosList()
        {
            var todos = db.Todos.FirstOrDefault();

            return PartialView("_TodosPartial", todos);
        }


    }
}