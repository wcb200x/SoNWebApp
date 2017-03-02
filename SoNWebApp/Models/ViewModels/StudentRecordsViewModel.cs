using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoNWebApp.Models.ViewModels
{
    public class StudentRecordsViewModel
    {
        public IEnumerable<Student> StudentsList { get; set; }
        public IEnumerable<Todos> TodosList { get; set; }
    }
}