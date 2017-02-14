using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoNWebApp.Models
{
    public class Courses
    {
        public int Id { get; set; }
        public string Subject { get; set; }
        public string Title { get; set; }
        public int CatalogNumber { get; set; }
        public int Credits { get; set; }
        public int ProgramID { get; set; }
        public int CampusID { get; set; }
    }
}