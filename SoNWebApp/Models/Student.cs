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
        [Display(Name = "Student Number")]
        public int StudentNumber { get; set; }
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Middle Name")]
        public string MiddleName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        public string Race { get; set; }
        public string Gender { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date of Birth")]
        public DateTime DateOfBirth { get; set; }
        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; }
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
        [Display(Name = "Cell Number")]
        public string CellNumber { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        [Display(Name = "Zip Code")]
        public int ZipCode { get; set; }
        public string Country { get; set; }
        public string Standing { get; set; }
        [Display(Name = "Has Graduated")]
        public bool HasGraduated { get; set; }
        [Range(1, 2, ErrorMessage = "CampusID must be either 1 or 2")]
        public int CampusID { get; set; }
        public virtual Campus Campus { get; set; }
        [Range(1, 3, ErrorMessage = "ProgramID must be 1,2, or 3")]
        public int ProgramID { get; set; }
        public virtual Program Program { get; set; }
        public decimal GPA { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Enrollment Date")]
        public DateTime EnrollmentDate { get; set; }
        public bool Petition { get; set; }
        public string Notes { get; set; }

        public virtual ICollection<Enrollment> Enrollments { get; set; }
    }

    
}