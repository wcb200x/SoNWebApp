using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoNWebApp.Models
{
    public class Enrollment
    {
        public int EnrollmentID { get; set; }
        public int CourseID { get; set; }
        public int StudentID { get; set; }
        public int ProgramID { get; set; }
        public virtual Courses Course { get; set; }
        public virtual Student Student { get; set; }
        public virtual Program Program { get; set; }
    }
}