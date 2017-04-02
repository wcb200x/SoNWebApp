using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SoNWebApp.Models;
using SoNWebApp.Models.ViewModels;

namespace SoNWebApp.Controllers
{
    [Authorize]
    public class ComplianceController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Compliance
        [Authorize(Roles = ("Advisor,Admin,SuperAdmin"))]
        public ActionResult Index(string searchString)
        {
            //ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            //ViewBag.IsExpiredSortParm = sortOrder == "IsExpired" ? "IsExpired_desc" : "IsExpired";
            //ViewBag.IsCompliantSortParm = sortOrder == "IsCompliant" ? "IsCompliant_desc" : "IsCompliant";


 
 
            //switch (sortOrder)
            //{
            //    case "name_desc":
            //        students = students.OrderByDescending(c => c.Student.LastName);
            //        break;
            //    case "IsExpired":
            //        students = students.OrderBy(c => c.IsExpired);
            //        break;
            //    case "IsExpired_desc":
            //        students = students.OrderByDescending(c => c.IsExpired);
            //        break;
            //    case "IsCompliant":
            //        students = students.OrderBy(c => c.IsCompliant);
            //        break;
            //    case "IsCompliant_desc":
            //        students = students.OrderByDescending(c => c.IsCompliant);
            //        break;
            //    default:
            //        students = students.OrderBy(c => c.Student.LastName);
            //        break;
            //}



            var compliances = db.Compliances.Include(c => c.DocumentID).Include(c => c.Student);


            var clinicalCompliances = db.Compliances.ToList();
            var ccdocidList = clinicalCompliances.Select(d => d.DocumentID).ToList();
            var documents = db.Documents.Where(d => ccdocidList.Contains(d.Id)).ToList();

            var viewModel = clinicalCompliances.Select(c => new AdvisorCCIndexViewModel
            {
                ExpirationDate = c.ExpirationDate,
                DocumentID = c.DocumentID,
                IsExpired = c.ExpirationDate < DateTime.Today ? true : false,
                ID = c.ID,
                Name = c.Name,
                StudentNumber = c.Student.StudentNumber,
                FirstName = c.Student.FirstName,
                LastName = c.Student.LastName,
                IsCompliant = c.IsCompliant

            });

            foreach (var doc in documents)
            {
                viewModel.FirstOrDefault(v => v.DocumentID == doc.Id).Document = doc;
                
            }
            if (!String.IsNullOrEmpty(searchString))
            {
                viewModel = viewModel.Where(c => c.LastName.Contains(searchString)
                                              || c.FirstName.Contains(searchString)
                                              || c.Name.Contains(searchString)
                                              || c.StudentNumber.ToString().Contains(searchString));
            }

            return View(viewModel.ToList());

         
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
        [Authorize(Roles = ("Advisor,Admin,SuperAdmin"))]
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
        [Authorize(Roles = ("Advisor,Admin,SuperAdmin"))]
        public ActionResult Create([Bind(Include = "ID,Name,DocumentID,StudentID,ExpirationDate,IsExpired,IsCompliant")] Compliance compliance)
        {
            if (ModelState.IsValid)
            {
                compliance.ExpirationDate = DateTime.Today.AddDays(-1);
                if (compliance.ExpirationDate < DateTime.Today)
                {
                    compliance.IsExpired = true;
                }
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
        [Authorize(Roles = ("Advisor,Admin,SuperAdmin,Student"))]
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
            return View(compliance);
        }

        // POST: Compliance/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = ("Advisor,Admin,SuperAdmin,Student"))]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,StudentID,DocumentID,ExpirationDate,IsExpired,IsCompliant")] Compliance compliance)
        {

            if (ModelState.IsValid)
            {
                if (User.IsInRole("Advisor") || (User.IsInRole("Admin")) || (User.IsInRole("SuperAdmin")))
                {
                    if (compliance.ExpirationDate < DateTime.Today)
                    {
                        compliance.IsExpired = true;
                    }
                    else
                    {
                        compliance.IsExpired = false;
                    }
                    db.Entry(compliance).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    if (compliance.ExpirationDate < DateTime.Today)
                    {
                        compliance.IsExpired = true;
                    }
                    else
                    {
                        compliance.IsExpired = false;
                    }
                    db.Entry(compliance).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("ClinicalCompliance","Student",false);
                }
            }
            var name = ComplianceName();
            compliance.Names = GetComplianceListItems(name);
            return View(compliance);
        }

        // GET: Compliance/Delete/5
        [Authorize(Roles = ("Advisor,Admin,SuperAdmin"))]
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
        [Authorize(Roles = ("Advisor,Admin,SuperAdmin"))]
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
