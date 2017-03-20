using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SoNWebApp.Models
{
    public class UDApplication
    {
        public int ID { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Middle Name")]
        public string MiddleName { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        [Display(Name = "Street Address")]
        public string StreetAddress { get; set; }
        [Display(Name = "Street Address 2")]
        public string StreetAddress2 { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        [Display(Name = "Zip Code")]
        public int ZipCode { get; set; }
        [Required]
        [Display(Name = "Home Number")]
        public string HomeNumber { get; set; }
        [Required]
        [Display(Name = "Cell Number")]
        public string CellNumber { get; set; }
        [Required]
        [Display(Name = "Student Number")]
        public int StudentNumber { get; set; }
       
        public string Program1 { get; set; }
        public string Program2 { get; set; }
        public string Program3 { get; set; }
        public string Semester { get; set; }
        [Required]
        [Display(Name = "Current Courses")]
        public string CurrentCourses { get; set; }
        [Required]
        [Display(Name = "Personal Qual Essay")]
        public string PersonalQualEssay { get; set; }
        [Required]
        [Display(Name = "Nurse Experience")]
        public string NurseExperience { get; set; }
        [Required]
        public string Legal1 { get; set; }
        [Required]
        public string Legal2 { get; set; }
        [Required]
        public string Legal3 { get; set; }
        [Required]
        public string Legal4 { get; set; }
        [Required]
        public string Legal5 { get; set; }
        [Required]
        public string Legal6 { get; set; }
        public string ExplainLegal { get; set; }
        [Required]
        [RegularExpression(@"Yes")]
        public string ConfirmLegal { get; set; }


    }

}