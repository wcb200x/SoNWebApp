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

    }

}