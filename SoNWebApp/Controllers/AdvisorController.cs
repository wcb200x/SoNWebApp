using SoNWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SoNWebApp.Controllers
{
    public class AdvisorController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Advisor
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult CRM()
        {
            return View();
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
            return View();
        }
        public ActionResult StudentReport()
        {
            return View(db.Students.ToList());
        }
        //public ActionResult GPAReport(Decimal 3.5)
        //{
        //    var student = db.Students.Where(s => s.GPA >= 3.5M).ToList();
        //    return View("GPAReport", "Student", student);
        //}
    }
}