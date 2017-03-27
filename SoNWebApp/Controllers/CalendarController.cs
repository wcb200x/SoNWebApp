using DayPilot.Web.Mvc;
using DayPilot.Web.Mvc.Enums;
using SoNWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DayPilot.Web.Mvc.Events.Month;

namespace SoNWebApp.Controllers
{
    public class CalendarController : Controller
    {
        // GET: Calendar
        public ActionResult Index()
        {
            return View();
        }

    }
}