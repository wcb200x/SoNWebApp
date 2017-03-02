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

        // GET: UDApplications/Details/5
        [Authorize(Roles = ("Admin,Advisor,SuperAdmin"))]
        public ActionResult Details(int? id)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            UDApplication uDApplication = db.UDApplications.FirstOrDefault(s => s.ID == id);
            //if (uDApplication == null)
            //{
            //    return HttpNotFound();
            //}
            return View(uDApplication);
        }

        // GET: UDApplications/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UDApplications/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,FirstName,MiddleName,LastName,StreetAddress,StreetAddress2,City,State,ZipCode,HomeNumber,CellNumber,StudentNumber,Location,Semester,CurrentCourses,Essay1,Experience,Legal1,Legal2,Legal3,Legal4,Legal5,Legal6,ExplainLegal,Legal7")] UDApplication uDApplication)
        {
            if (ModelState.IsValid)
            {
                db.UDApplications.Add(uDApplication);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(uDApplication);
        }

        // GET: UDApplications/Edit/5
        [Authorize(Roles = ("Admin,Advisor,SuperAdmin"))]
        public ActionResult Edit(int? id)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            UDApplication uDApplication = db.UDApplications.FirstOrDefault(s => s.ID == id);
            //if (uDApplication == null)
            //{
            //    return HttpNotFound();
            //}
            return View(uDApplication);
        }

        // POST: UDApplications/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = ("Admin,Advisor,SuperAdmin"))]
        public ActionResult Edit([Bind(Include = "ID,FirstName,MiddleName,LastName,StreetAddress,StreetAddress2,City,State,ZipCode,HomeNumber,CellNumber,StudentNumber,Location,Semester,CurrentCourses,Essay1,Experience,Legal1,Legal2,Legal3,Legal4,Legal5,Legal6,ExplainLegal,Legal7")] UDApplication uDApplication)
        {
            if (ModelState.IsValid)
            {
                db.Entry(uDApplication).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(uDApplication);
        }

        // GET: UDApplications/Delete/5
        [Authorize(Roles = ("Admin,Advisor,SuperAdmin"))]
        public ActionResult Delete(int? id)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            UDApplication uDApplication = db.UDApplications.FirstOrDefault(s => s.ID == id);
            //if (uDApplication == null)
            //{
            //    return HttpNotFound();
            //}
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