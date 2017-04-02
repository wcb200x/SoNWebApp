using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SoNWebApp.Models
{
    public class Event
    {
        public int id { get; set; }
        public string text { get; set; }
        public string Location { get; set; }
        [Display(Name = "Is Recruitment")]
        public bool IsRecruitment { get; set; }
        [Display(Name = "Start Date")]
        public DateTime start_date { get; set; }
        [Display(Name = "End Date")]
        public DateTime end_date { get; set; }
        public string Description { get; set; }

    }
}