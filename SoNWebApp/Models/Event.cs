using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SoNWebApp.Models
{
    public class Event
    {
        public int ID { get; set; }
        public string Text { get; set; }
        public string Location { get; set; }
        [Display(Name = "Is Recruitment")]
        public bool IsRecruitment { get; set; }
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }
        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }
        public string Name { get; set; }

    }
}