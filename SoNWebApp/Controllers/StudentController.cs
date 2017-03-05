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
    public class StudentController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Student
        [Authorize]
        public ActionResult Index()
        {
            return View(db.Students.ToList());
        }

        // GET: Student/Details/5
        [Authorize(Roles = ("Advisor,Admin,SuperAdmin"))]
        public ActionResult Details(int? id)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            Student student = db.Students.FirstOrDefault(s => s.ID == id);
            //if (student == null)
            //{
            //    return HttpNotFound();
            //}
            return View(student);
        }
        [Authorize (Roles =("Advisor,Admin,SuperAdmin"))]
        // GET: Student/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Student/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = ("Advisor,Admin,SuperAdmin"))]
        public ActionResult Create([Bind(Include = "ID,StudentNumber,FirstName,MiddleName,LastName,Race,Gender,DateOfBirth,EmailAddress,PhoneNumber,CellNumber,Address,City,State,ZipCode,Country,Standing,HasGraduated,CampusID,ProgramID,GPA,EnrollmentDate,Petition,Notes")] Student student)
        {
            if (ModelState.IsValid)
            {
                db.Students.Add(student);
                db.SaveChanges();
                return RedirectToAction("CRM","Advisor",false);
            }

            return View(student);
        }

        // GET: Student/Edit/5
        public ActionResult Edit(int? id)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            Student student = db.Students.FirstOrDefault(s => s.ID == id);
            //if (student == null)
            //{
            //    return HttpNotFound();
            //}
            return View(student);
        }

        // POST: Student/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,StudentNumber,FirstName,MiddleName,LastName,Race,Gender,DateOfBirth,EmailAddress,PhoneNumber,CellNumber,Address,City,State,ZipCode,Country,Standing,HasGraduated,CampusID,ProgramID,GPA,EnrollmentDate,Petition,Notes")] Student student)
        {
            if (ModelState.IsValid)
            {
                if (User.IsInRole("Advisor") || (User.IsInRole("Admin")) || (User.IsInRole("SuperAdmin")))
                    {
                    db.Entry(student).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("CRM", "Advisor", false);
                }
                else {

                    db.Entry(student).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Default");
                }
            }
            return View(student);
        }

        // GET: Student/Delete/5
        [Authorize(Roles = ("Advisor,Admin,SuperAdmin"))]
        public ActionResult Delete(int? id)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            Student student = db.Students.FirstOrDefault(s => s.ID == id);
            //if (student == null)
            //{
            //    return HttpNotFound();
            //}
            return View(student);
        }

        // POST: Student/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = ("Advisor,Admin,SuperAdmin"))]
        public ActionResult DeleteConfirmed(int id)
        {
            Student student = db.Students.Find(id);
            db.Students.Remove(student);
            db.SaveChanges();
            return RedirectToAction("CRM","Advisor",false);
        }
        
        public ActionResult StudentDashboard()
        {
            return View();
        }
        public ActionResult Calendar()
        {
            return View();
        }
        public ActionResult ClinicalCompliance()
        {
            return View();
        }
        public ActionResult Advisor()
        {
            return View();
        }
        public ActionResult Alerts()
        {
            var todos = db.Todos.Where(t => t.EndDate >= DateTime.Today).Take(5);

            return View(todos);
        }

        public ActionResult Default()
        {
            var name = HttpContext.User.Identity.Name;

            var viewModel = new StudentDefaultViewModel()
            {
                StudentsList = db.Students.Where(s => s.EmailAddress.ToLower().Contains(name)).FirstOrDefault(),
            TodosList = db.Todos.Where(t => t.EndDate >= DateTime.Today).Take(5)
            };
            return View(viewModel);
        }
    
        public ActionResult ProgramOfStudy()
        {
            return View();
        }
        public PartialViewResult GetStudentsList()
        {
            var name = HttpContext.User.Identity.Name;

            var students = db.Students.Where(s => s.EmailAddress.ToLower().Contains(name)).FirstOrDefault();
            return PartialView("_StudentPartial", students);
        }

        public PartialViewResult GetTodosList()
        {
            var todos = db.Todos.FirstOrDefault();

            return PartialView("_TodosPartial", todos);
        }
        [HttpPost]
        public ActionResult UploadDocument(int studentNumber, HttpPostedFileBase file)
        {

            byte[] uploadedFile = new byte[file.InputStream.Length];
            file.InputStream.Read(uploadedFile, 0, uploadedFile.Length);

            var student = db.Students.FirstOrDefault(s => s.StudentNumber == studentNumber);

            if (student != null)
            {
                var documentModel = new Document
                {
                    StudentID = student.ID,
                    StudentNumber = studentNumber,
                    UploadedBy = HttpContext.User.Identity.Name,
                    ContentLength = file.ContentLength,
                    ContentType = file.ContentType,
                    FileName = file.FileName,
                    FileBytes = uploadedFile
                };
                db.Documents.Add(documentModel);
                db.SaveChanges();
            }

            return View("ClinicalCompliance");
        }
        public ActionResult GetDocument(int studentID)
        {
            var allDocumentsForStudent = db.Documents.Where(d => d.StudentID == studentID);

            var oneDocumentFromStudent = allDocumentsForStudent.FirstOrDefault();

            if (oneDocumentFromStudent != null)
            {
                return File(oneDocumentFromStudent.FileBytes, "application/octet-stream", oneDocumentFromStudent.FileName);
            }
            return RedirectToAction("ClinicalCompliance");
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
