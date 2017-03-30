using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoNWebApp.Models.ViewModels
{
    public class StudentCCIndexViewModel
    {
        public int ID { get; set; }
        public int DocumentID { get; set; }
        public Document Document { get; set; }
        public DateTime ExpirationDate { get; set; }
        public bool IsExpired { get; set; }
        public string Name { get; set; }
        public int StudentNumber { get; set; }
    }
}