using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SoNWebApp.Controllers
{
    public class AdminController : Controller
    {
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
    }
    
}