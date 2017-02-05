using DayPilot.Web.Mvc;
using DayPilot.Web.Mvc.Enums;
using DayPilot.Web.Mvc.Events.Calendar;
using SoNWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SoNWebApp.Controllers
{
    public class CalendarController : Controller
    {
        // GET: Calendar
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Backend()
        {
            return new Dpc().CallBack(this);
        }
        class Dpc : DayPilotCalendar
        {
            protected override void OnInit(InitArgs e)
            {
                
                var db = new ApplicationDbContext();
                //Events = from ev in db.events select ev;

                //DataIdField = "id";
                //DataTextField = "text";
                //DataStartField = "eventstart";
                //DataEndField = "eventend";

                //Update();
            }
        }
    }
}