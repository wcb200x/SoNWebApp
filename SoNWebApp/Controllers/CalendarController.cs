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

        public ActionResult Backend()
        {
            return new Dpm().CallBack(this);
        }
        class Dpm : DayPilotMonth
        {
            protected override void OnInit(InitArgs e)
            {
                
                var db = new ApplicationDbContext();
                Events = from ev in db.Events select ev;

                DataIdField = "id";
                DataTextField = "text";
                DataStartField = "eventstart";
                DataEndField = "eventend";

                Update();
            }
            protected override void OnCommand(CommandArgs e)
            {
                switch (e.Command)
                {
                    case "previous":
                        StartDate = StartDate.AddMonths(-1);
                        Update(CallBackUpdateType.Full);
                        break;

                    case "next":
                        StartDate = StartDate.AddMonths(1);
                        Update(CallBackUpdateType.Full);
                        break;
                }
            }

     
        }
    }
}