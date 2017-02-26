using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoNWebApp.Models
{
    public class Student
    {
        public int ID { get; set; }
        public int StudentNumber { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int ZipCode { get; set; }
        public string Standing { get; set; }
        public bool HasGraduated { get; set; }
        public string CampusID { get; set; }
        public decimal GPA { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public bool Petition { get; set; }
        public string Notes { get; set; }

        public virtual ICollection<Enrollment> Enrollments { get; set; }
    }

    
}