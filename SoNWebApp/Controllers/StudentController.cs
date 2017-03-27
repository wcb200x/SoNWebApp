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
using System.IO;

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
      [Authorize]
        public ActionResult Details(int? id, string actualgrade, string GradePointAverage)
        {
            //actualgrade = db.Enrollments.FirstOrDefault(e => e.StudentID == id).Grade;

            var enrollments = db.Enrollments.Where(e => e.StudentID == id);
            var actualGradeList = new List<decimal>();
            foreach (var enrollment in enrollments)
            {
                var actualGrade = GetCourseGrade(enrollment.Grade);
                actualGradeList.Add(actualGrade);
            }


            var testGpaSum = actualGradeList.Sum();
            var testGpaGradeCount = actualGradeList.Count() * 4;

            var testGpa = (testGpaSum / testGpaGradeCount) * 4.0M;
            

            //GradePointAverage = db.Students.FirstOrDefault(s => s.ID == id).GPA.ToString();
            //GradePointAverage = actualgrade;            


            Student student = db.Students.FirstOrDefault(s => s.ID == id);
            student.GPA = testGpa;

            db.SaveChanges();
          
            return View(student);
        }
        [Authorize (Roles =("Advisor,Admin,SuperAdmin"))]
        // GET: Student/Create
        public ActionResult Create()
        {
            var gender = Genders();
            var student = new Student();
            var state = States();
            var standing = Standing();
            student.States = GetStatesListItems(state);
            student.Genders = GetGenderListItems(gender);
            student.Standings = GetStandingListItems(standing);

            ViewBag.ProgramID = new SelectList(db.Programs, "ID", "Name");
            ViewBag.CampusID = new SelectList(db.Campuses, "CampusID", "Name");

            return View(student);
        }

        // POST: Student/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = ("Advisor,Admin,SuperAdmin"))]
        public ActionResult Create([Bind(Include = "ID,StudentNumber,FirstName,MiddleName,LastName,Race,Gender,DateOfBirth,EmailAddress,PhoneNumber,CellNumber,Address,City,State,ZipCode,Country,Standing,HasGraduated,CampusID,ProgramID,GPA,EnrollmentDate,Petition,Notes")] Student student)
        {
            var standing = Standing();
            student.Standings = GetStandingListItems(standing);
            var state = States();
            student.States = GetStatesListItems(state);
            var gender = Genders();
            student.Genders = GetGenderListItems(gender);

            if (ModelState.IsValid)
            {
                db.Students.Add(student);
                db.SaveChanges();
                return RedirectToAction("CRM","Advisor",false);
            }

            ViewBag.ProgramID = new SelectList(db.Programs, "ID", "Name", student.ProgramID);
            ViewBag.CampusID = new SelectList(db.Campuses, "CampusID", "Name", student.CampusID);

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
            var gender = Genders();
            student.Genders = GetGenderListItems(gender);
            var state = States();
            student.States = GetStatesListItems(state);
            var standing = Standing();
            student.Standings = GetStandingListItems(standing);

            ViewBag.ProgramID = new SelectList(db.Programs, "ID", "Name", student.ProgramID);
            ViewBag.CampusID = new SelectList(db.Campuses, "CampusID", "Name", student.CampusID);
            return View(student);
        }

        // POST: Student/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,StudentNumber,FirstName,MiddleName,LastName,Race,Gender,DateOfBirth,EmailAddress,PhoneNumber,CellNumber,Address,City,State,ZipCode,Country,Standing,HasGraduated,CampusID,ProgramID,GPA,EnrollmentDate,Petition,Notes")] Student student)
        {
            var gender = Genders();
            student.Genders = GetGenderListItems(gender);
            var state = States();
            student.States = GetStatesListItems(state);
            var standing = Standing();
            student.Standings = GetStandingListItems(standing);

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
            ViewBag.ProgramID = new SelectList(db.Programs, "ID", "Name", student.ProgramID);
            ViewBag.CampusID = new SelectList(db.Campuses, "CampusID", "Name", student.CampusID);
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
        public ActionResult UploadDocument(int studentNumber, DateTime expirationDate, string DocumentType, HttpPostedFileBase file)
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
                    ExpirationDate = expirationDate,
                    DocumentType = DocumentType,
                    UploadedBy = HttpContext.User.Identity.Name,
                    ContentLength = file.ContentLength,
                    ContentType = file.ContentType,
                    FileName = file.FileName,
                    FileBytes = uploadedFile,
                    
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



        public void EmailStudent(int id)
        {
            var studentEmail = db.Students.FirstOrDefault(s => s.ID == id).EmailAddress;

            //Email Student           
        }

        //public ActionResult StudentPos ()
        //{
            
        //   //var student = db.Students.FirstOrDefault(s => s.ID == id);
        //    //var Pos = db.POS.Where(p => p.StudentID == id);

        //    return RedirectToAction("Edit", "POS", pos.ID );
        //}



        public decimal GetCourseGrade(string courseLetterGrade)
        {
            var actualgrade = 0.0M;

            if (courseLetterGrade == "A+")
            {
                actualgrade = 4.0M;
            }
            else if (courseLetterGrade == "A")
            {
                actualgrade = 4.0M;
            }
            else if (courseLetterGrade == "A-")
            {
                actualgrade = 3.7M;
            }
            else if (courseLetterGrade == "B+")
            {
                actualgrade = 3.3M;
            }
            else if (courseLetterGrade == "B")
            {
                actualgrade = 3.0M;
            }
            else if (courseLetterGrade == "B-")
            {
                actualgrade = 2.7M;
            }
            else if (courseLetterGrade == "C+")
            {
                actualgrade = 2.3M;
            }
            else if (courseLetterGrade == "C")
            {
                actualgrade = 2.0M;
            }
            else if (courseLetterGrade == "C-")
            {
                actualgrade = 1.7M;
            }
            else if (courseLetterGrade == "D+")
            {
                actualgrade = 1.3M;
            }
            else if (courseLetterGrade == "D")
            {
                actualgrade = 1.0M;
            }
            else if (courseLetterGrade == "D-")
            {
                actualgrade = 0.7M;
            }
            else if (courseLetterGrade == "F")
            {
                actualgrade = 0.0M;
            }            

            return actualgrade;
        }
        public IEnumerable<string> Genders()
        {
            return new List<string>
            {
                "Female",
                "Male",
                

            };

        }
        public IEnumerable<SelectListItem> GetGenderListItems(IEnumerable<string> items)
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

        public IEnumerable<string> Standing()
        {
            return new List<string>
            {
                "Freshman",
                "Sophomore",
                "Junior",
                "Senior",


            };

        }
        public IEnumerable<SelectListItem> GetStandingListItems(IEnumerable<string> items)
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
