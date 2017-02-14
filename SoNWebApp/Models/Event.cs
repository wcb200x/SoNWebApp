using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoNWebApp.Models
{
    public class Event
    {
        public int ID { get; set; }
        public string Text { get; set; }
        public string Location { get; set; }
        public bool IsRecruitment { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Name { get; set; }

    }
}