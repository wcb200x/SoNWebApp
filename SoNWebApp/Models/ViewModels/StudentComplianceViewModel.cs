using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoNWebApp.Models.ViewModels
{
    public class StudentComplianceViewModel
    {
        public IEnumerable<Compliance> studentCompliance { get; set; }
        public Document documentUpload { get; set; }
    }
}