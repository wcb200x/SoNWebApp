﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SoNWebApp.Models;

namespace SoNWebApp.Controllers
{
    public class UDApplicationsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: UDApplications
        [Authorize(Roles = ("Admin,Advisor,SuperAdmin"))]
        public ActionResult Index()
        {
            return View(db.UDApplications.ToList());
        }

        [Authorize(Roles = ("Admin,Advisor,SuperAdmin"))]
        // GET: UDApplications/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UDApplication uDApplication = db.UDApplications.FirstOrDefault(s => s.ID == id);
            if (uDApplication == null)
            {
                return HttpNotFound();
            }
            return View(uDApplication);
        }

        // GET: UDApplications/Create
        public ActionResult Create()
        {
            var uDApplication = new UDApplication();
            var state = States();
            uDApplication.States = GetStatesListItems(state);
            return View(uDApplication);
        }

        // POST: UDApplications/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,FirstName,MiddleName,LastName,Email,StreetAddress,StreetAddress2,City,State,ZipCode,HomeNumber,CellNumber,StudentNumber,Program1,Semester,CurrentCourses,PersonalQualEssay,NurseExperience,Legal1,Legal2,Legal3,Legal4,Legal5,Legal6,ExplainLegal,ConfirmLegal,Status")] UDApplication uDApplication)
        {
       
            if (ModelState.IsValid)
            {
                if (User.IsInRole("Advisor") || (User.IsInRole("Admin")) || (User.IsInRole("SuperAdmin")))
                {
                    db.UDApplications.Add(uDApplication);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {

                    db.UDApplications.Add(uDApplication);
                    db.SaveChanges();
                    return RedirectToAction("Default", "Student", false);
                }
            }
            var state = States();
            uDApplication.States = GetStatesListItems(state);
            return View(uDApplication);
        }

        // GET: UDApplications/Edit/5
        [Authorize(Roles = ("Admin,Advisor,SuperAdmin"))]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UDApplication uDApplication = db.UDApplications.FirstOrDefault(s => s.ID == id);
            if (uDApplication == null)
            {
                return HttpNotFound();
            }
            var status = Status();
            uDApplication.Statuses = GetStatusListItems(status);
            var state = States();
            uDApplication.States = GetStatesListItems(state);
            return View(uDApplication);
        }

        // POST: UDApplications/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = ("Admin,Advisor,SuperAdmin"))]
        public ActionResult Edit([Bind(Include = "ID,FirstName,MiddleName,LastName,Email,StreetAddress,StreetAddress2,City,State,ZipCode,HomeNumber,CellNumber,StudentNumber,Program1,Semester,CurrentCourses,PersonalQualEssay,NurseExperience,Legal1,Legal2,Legal3,Legal4,Legal5,Legal6,ExplainLegal,ConfirmLegal,Status")] UDApplication uDApplication)
        {
            if (ModelState.IsValid)
            {
                db.Entry(uDApplication).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            var status = Status();
            uDApplication.Statuses = GetStatusListItems(status);
            var state = States();
            uDApplication.States = GetStatesListItems(state);
            return View(uDApplication);
        }

        // GET: UDApplications/Delete/5
        [Authorize(Roles = ("Admin,Advisor,SuperAdmin"))]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UDApplication uDApplication = db.UDApplications.FirstOrDefault(s => s.ID == id);
            if (uDApplication == null)
            {
                return HttpNotFound();
            }
            return View(uDApplication);
        }

        // POST: UDApplications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = ("Admin,Advisor,SuperAdmin"))]
        public ActionResult DeleteConfirmed(int id)
        {
            UDApplication uDApplication = db.UDApplications.Find(id);
            db.UDApplications.Remove(uDApplication);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult StudentStatus(int? id)
        {
            if (id == null)
            {
                var curentUserEmail = HttpContext.User.Identity.Name;
                var student = db.Students.FirstOrDefault(s => s.EmailAddress == curentUserEmail);

                var application = db.UDApplications.FirstOrDefault(p => p.StudentNumber == student.StudentNumber);

                return View(application);
            }

                return View();
        }
        public IEnumerable<string> Status()
        {
            return new List<string>
            {
                "Approved",
                "Wait Listed",
                "Declined",
                "Being Reviewed"

            };

        }
        public IEnumerable<SelectListItem> GetStatusListItems(IEnumerable<string> items)
        {
            var selectList = new List<SelectListItem>();
            foreach (var element in items)
            {
                selectList.Add(new SelectListItem
                {
                    Value = element,
                    Text = element
                });
            }
            return selectList;
        }
        public IEnumerable<string> States()
        {
            return new List<string>
            {
               "Alabama",
                "Alaska",
                "Arizona",
                "Arkansas",
                "California",
                "Colorado",
                "Connecticut",
                "Delaware",
                "Florida",
                "Georgia",
                "Hawaii",
                "Idaho",
                "Illinois",
                "Indiana",
                "Iowa",
                "Kansas",
                "Kentucky",
                "Louisiana",
                "Maine",
                "Maryland",
                "Massachusetts",
                "Michigan",
                "Minnesota",
                "Mississippi",
                "Missouri",
                "Montana",
                "Nebraska",
                "Nevada",
                "New Hampshire",
                "New Jersey",
                "New Mexico",
                "New York",
                "North Carolina",
                "North Dakota",
                "Ohio",
                "Oklahoma",
                "Oregon",
                "Pennsylvania",
                "Rhode Island",
                "South Carolina",
                "South Dakota",
                "Tennessee",
                "Texas",
                "Utah",
                "Vermont",
                "Virginia",
                "Washington",
                "West Virginia",
                "Wisconsin",
                "Wyoming",
                "District of Columbia",
                "Puerto Rico",
                "Guam",
                "U.S. Virgin Islands",

            };

        }
        public IEnumerable<SelectListItem> GetStatesListItems(IEnumerable<string> items)
        {
            var selectList = new List<SelectListItem>();
            foreach (var element in items)
            {
                selectList.Add(new SelectListItem
                {
                    Value = element,
                    Text = element
                });
            }
            return selectList;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
