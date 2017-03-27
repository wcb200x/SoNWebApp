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
    public class POSController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: POS
        [Authorize (Roles = ("SuperAdmin,Admin,Advisor"))]
        public ActionResult Index()
        {
            var pOS = db.POS.Include(p => p.Student);
            return View(pOS.ToList());
        }

        // GET: POS/Details/5
        [Authorize(Roles = ("SuperAdmin,Admin,Advisor"))]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            POS pOS = db.POS.Find(id);
            if (pOS == null)
            {
                return HttpNotFound();
            }
            return View(pOS);
        }

        // GET: POS/Create
        [Authorize(Roles = ("SuperAdmin,Admin,Advisor"))]
        public ActionResult Create()
        {
            ViewBag.StudentID = new SelectList(db.Students, "ID", "StudentNumber");
            return View();
        }

        // POST: POS/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = ("SuperAdmin,Admin,Advisor"))]
        public ActionResult Create(CreateProgramOfStudyViewModel viewModel)
        {

            if(!ModelState.IsValid)
            {
                ViewBag.StudentID = new SelectList(db.Students, "ID", "StudentNumber", viewModel.StudentID);
                return View(viewModel);
            }

            if (db.POS.Any(p => p.StudentID == viewModel.StudentID))
            {
                return View(viewModel);
            }

            var programOfStudy = new POS
            {
                Course1 = viewModel.Course1,
                Course2 = viewModel.Course2,
                Course3 = viewModel.Course3,
                Course4 = viewModel.Course4,
                Course5 = viewModel.Course5,
                Course6 = viewModel.Course6,
                Course7 = viewModel.Course7,
                Course8 = viewModel.Course8,
                Course9 = viewModel.Course9,
                Course10 = viewModel.Course10,
                Course11 = viewModel.Course11,
                Course12 = viewModel.Course12,
                StudentID = viewModel.StudentID,

            };
            //Create Program of Study Object (Because we should be using viewmodels)
            //pOS.StudentID = studentId;
            //Add Program of study to the DB
            db.POS.Add(programOfStudy);
            //Save Changes
            db.SaveChanges();
            //Return to view
            return RedirectToAction("Index");
            //if (ModelState.IsValid)
            //{
            //    //Get Student/Id
            //    var studentId = db.Students.FirstOrDefault(s => s.StudentNumber == pOS.StudentID).ID.ToString();

            //    //Create Program of Study Object (Because we should be using viewmodels)
            //    pOS.StudentID = studentId;
            //    //Add Program of study to the DB
            //    db.POS.Add(pOS);
            //    //Save Changes
            //    db.SaveChanges();
            //    //Return to view
            //    return RedirectToAction("Index");
            //}

            //ViewBag.StudentID = new SelectList(db.Students, "ID","StudentNumber",pOS.StudentID);
            //return View(pOS);
        }

        // GET: POS/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                var curentUserEmail = HttpContext.User.Identity.Name;
                var student = db.Students.FirstOrDefault(s => s.EmailAddress == curentUserEmail);

                var pos = db.POS.FirstOrDefault(p => p.StudentID == student.ID);

                var programOfStudy = db.POS.FirstOrDefault(p => p.ID == pos.ID);

                var programOfStudyViewModel = new posViewModel()
                {
                    posCourses = programOfStudy,
                    posDocument = db.Students.FirstOrDefault(s => s.ID == programOfStudy.StudentID)

                };

                return View(programOfStudyViewModel);

            }
            
            POS pOS = db.POS.Find(id);

            var viewModel = new posViewModel()
            {
                posCourses = pOS,
                posDocument = db.Students.FirstOrDefault(s => s.ID == pOS.StudentID)
                
            };
            //if (pOS == null)
            //{
            //    return HttpNotFound();
            //}
            return View(viewModel);
        }

        // POST: POS/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Course1,Course2,Course3,Course4,Course5,Course6,Course7,Course8,Course9,Course10,Course11,Course12,StudentID")] POS pOS)
        {
            if (ModelState.IsValid)
            {
                if (User.IsInRole("Advisor") || (User.IsInRole("Admin")) || (User.IsInRole("SuperAdmin")))
                {
                    db.Entry(pOS).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index", "POS", false);
                }
                else
                {
                             
                    db.Entry(pOS).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Default", "Student", false);
                }
            }
            ViewBag.StudentID = new SelectList(db.Students, "ID", "StudentNumber", pOS.StudentID);
            return View(pOS);
        }

        // GET: POS/Delete/5
        [Authorize(Roles = ("SuperAdmin,Admin,Advisor"))]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            POS pOS = db.POS.Find(id);
            if (pOS == null)
            {
                return HttpNotFound();
            }
            return View(pOS);
        }

        // POST: POS/Delete/5
        [Authorize(Roles = ("SuperAdmin,Admin,Advisor"))]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            POS pOS = db.POS.Find(id);
            db.POS.Remove(pOS);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public PartialViewResult GetProjectedCourses()
        {
            var courses = db.POS.ToList();

            return PartialView("_ChangeProjectedSchedule", courses);
        }
        public PartialViewResult GetPOSDocument(int? id)
        {

            POS pOS = db.POS.Find(id);
            var document = db.Students.Where(s => s.ID == id);

            return PartialView("_ProgramOfStudyDocuments", document);
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
