using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace SoNWebApp.Models
{
    public class Courses
    {
        public int Id { get; set; }
        public string Subject { get; set; }
        public string Title { get; set; }
        public int Credits { get; set; }
        public int ProgramID { get; set; }
        public virtual Program Program { get; set; }
        public ICollection<Enrollment> Enrollments { get; set; }
    }
}