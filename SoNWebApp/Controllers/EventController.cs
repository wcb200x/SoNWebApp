using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Xml.Linq;
using SoNWebApp.Models;
using DHTMLX.Common;
using DHTMLX.Scheduler;
using DHTMLX.Scheduler.Data;

namespace SoNWebApp.Controllers
{
    public class EventController : Controller
    {
        //Refer to this link in order to set up the Calendar.
        //http://scheduler-net.com/docs/simple-.net-mvc-application-with-scheduler.html#step_2_add_the_scheduler_reference

        public ApplicationDbContext _db = new ApplicationDbContext();

        public ActionResult Index()
        {
            //try { 
            //var scheduler = new DHXScheduler(this); //initializes dhtmlxScheduler
            //scheduler.LoadData = true;// allows loading data
            //scheduler.EnableDataprocessor = true;// enables DataProcessor in order to enable implementation CRUD operations

            //    return View(scheduler);
            //}
            //catch (Exception ex)
            //{
            //    if (ex != null)
            //    {
            //        return RedirectToAction("Index", "Home");
            //    }                
            //}

            //return RedirectToAction("Index", "Home");
            return View();
        }

        public JsonResult Data()
        {
            //Using Dxhtml JavaScript Edition (open source)
            var events = _db.Events;

            var formatedEvents = new List<object>();
            foreach (var ev in events)
            {
                var formattingEvent = new
                {
                    id = ev.id,
                    start_date = ev.start_date.ToString(),
                    end_date = ev.end_date.ToString(),
                    //start_date = ev.start_date.Date.ToString("yyyy-MM-dd"),
                    //end_date = ev.end_date.Date.ToString("yyyy-MM-dd"),
                    text = ev.text
                };
                formatedEvents.Add(formattingEvent);

        
            }



            return Json(formatedEvents, JsonRequestBehavior.AllowGet);

            //Using Dxhtml MVC Scheduler Edition (free trial)
            //events for loading to scheduler
            //return new SchedulerAjaxData(_db.Events);
        }

        public ActionResult Save(string id, string text, string start_date, string end_date)
        {

            var existingEvent = _db.Events.FirstOrDefault(e => e.id.ToString() == id);
            var newStartDate = Convert.ToDateTime(start_date);
            var newEndDate = Convert.ToDateTime(end_date);


            if (existingEvent != null)
            {
                existingEvent.start_date = newStartDate;
                existingEvent.end_date = newEndDate;
                existingEvent.text = text;
            }
            else
            {

                var newEvent = new Event()
                {
                    start_date = newStartDate,
                    end_date = newEndDate,
                    text = text
                };
                _db.Events.Add(newEvent);
            }

            _db.SaveChanges();



            return View("Index");
        }

        //public ActionResult Save(Event updatedEvent, FormCollection formData)
        //{
        //    var action = new DataAction(formData);

        //    try
        //    {
        //        switch (action.Type)
        //        {
        //            case DataActionTypes.Insert: // your Insert logic
        //                _db.Events.Add(updatedEvent);
        //                break;
        //            case DataActionTypes.Delete: // your Delete logic
        //                updatedEvent = _db.Events.SingleOrDefault(ev => ev.id == updatedEvent.id);
        //                _db.Events.Remove(updatedEvent);
        //                break;
        //            default:// "update" // your Update logic
        //                updatedEvent = _db.Events.SingleOrDefault(
        //                ev => ev.id == updatedEvent.id);
        //                UpdateModel(updatedEvent);
        //                break;
        //        }
        //        _db.SaveChanges();
        //        action.TargetId = updatedEvent.id;
        //    }
        //    catch (Exception e)
        //    {
        //        action.Type = DataActionTypes.Error;
        //    }
        //    return (new AjaxSaveResponse(action));
        //}
    }
}