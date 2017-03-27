using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoNWebApp.Models
{
    public class Compliance
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int StudentID { get; set; }
        public virtual Student Student { get; set; }
        public int DocumentID { get; set; }
        public virtual Document Document { get; set; }
        public DateTime ExpirationDate { get; set; }
        public bool IsExpired { get; set; }

    }
}