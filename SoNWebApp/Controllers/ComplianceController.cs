using System;
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
    public class ComplianceController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Compliance
        public ActionResult Index()
        {
            var compliances = db.Compliances.Include(c => c.Document).Include(c => c.Student);
            return View(compliances.ToList());
        }

        // GET: Compliance/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Compliance compliance = db.Compliances.Find(id);
            if (compliance == null)
            {
                return HttpNotFound();
            }
            return View(compliance);
        }

        // GET: Compliance/Create
        public ActionResult Create()
        {
            var compliance = new Compliance();
            var name = ComplianceName();
            compliance.Names = GetComplianceListItems(name);
            ViewBag.StudentID = new SelectList(db.Students, "ID", "StudentNumber");
            return View(compliance);
        }

        // POST: Compliance/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,StudentID,ExpirationDate,IsExpired")] Compliance compliance)
        {
            if (ModelState.IsValid)
            {
                db.Compliances.Add(compliance);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            var name = ComplianceName();
            compliance.Names = GetComplianceListItems(name);
            ViewBag.StudentID = new SelectList(db.Students, "ID", "StudentNumber", compliance.StudentID);
            return View(compliance);
        }

        // GET: Compliance/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Compliance compliance = db.Compliances.Find(id);
            if (compliance == null)
            {
                return HttpNotFound();
            }
            var name = ComplianceName();
            compliance.Names = GetComplianceListItems(name);
            ViewBag.StudentID = new SelectList(db.Students, "ID", "StudentNumber", compliance.StudentID);
            return View(compliance);
        }

        // POST: Compliance/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,StudentID,DocumentID,ExpirationDate,IsExpired")] Compliance compliance)
        {
            if (ModelState.IsValid)
            {
                db.Entry(compliance).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            var name = ComplianceName();
            compliance.Names = GetComplianceListItems(name);
            ViewBag.StudentID = new SelectList(db.Students, "ID", "StudentNumber", compliance.StudentID);
            return View(compliance);
        }

        // GET: Compliance/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Compliance compliance = db.Compliances.Find(id);
            if (compliance == null)
            {
                return HttpNotFound();
            }
            return View(compliance);
        }

        // POST: Compliance/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Compliance compliance = db.Compliances.Find(id);
            db.Compliances.Remove(compliance);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public IEnumerable<string> ComplianceName()
        {
            return new List<string>
            {
                "CPR",
                "HIPPA",
                "Bloodbourne Path",
                "Liability Insurance",
                "Immunizations",
                "Drug Screening",
                "CNA"

            };

        }
        public IEnumerable<SelectListItem> GetComplianceListItems(IEnumerable<string> items)
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
