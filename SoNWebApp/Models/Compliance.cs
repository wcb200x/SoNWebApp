using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SoNWebApp.Models
{
    public class Compliance
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int StudentID { get; set; }
        public virtual Student Student { get; set; }
        public int DocumentID { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Expiration Date")]
        public DateTime ExpirationDate { get; set; }
        public bool IsExpired { get; set; }
        public IEnumerable<SelectListItem> Names { get; set; }
        public bool IsCompliant { get; set; }

    }
}