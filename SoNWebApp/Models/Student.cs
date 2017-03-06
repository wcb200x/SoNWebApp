using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        public string Race { get; set; }
        public string Gender { get; set; }
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string CellNumber { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int ZipCode { get; set; }
        public string Country { get; set; }
        public string Standing { get; set; }
        public bool HasGraduated { get; set; }
        //[Range(1, 2, ErrorMessage = "CampusID must be either 1 or 2")]
        public int CampusID { get; set; }
        public virtual Campus Campus { get; set; }
        //[Range(1, 3, ErrorMessage = "ProgramID must be 1,2, or 3")]
        public int ProgramID { get; set; }
        public virtual Program Program { get; set; }
        public decimal GPA { get; set; }
        [DataType(DataType.Date)]
        public DateTime EnrollmentDate { get; set; }
        public bool Petition { get; set; }
        public string Notes { get; set; }

        public virtual ICollection<Enrollment> Enrollments { get; set; }
    }

    
}