﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SoNWebApp.Models
{

    public class Enrollment
    {
        public int EnrollmentID { get; set; }
        public int CourseID { get; set; }
        public int StudentID { get; set; }
        public int ProgramID { get; set; }
        [Required]
        public string Semester { get; set; }
        [Required]
        public string Grade { get; set; }
        public virtual Courses Course { get; set; }
        public virtual Student Student { get; set; }
        public virtual Program Program { get; set; }
        public IEnumerable<SelectListItem> Grades { get; set; }
        public IEnumerable<SelectListItem> Semesters { get; set; }

    }
}