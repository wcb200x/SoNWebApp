using SoNWebApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SoNWebApp.Models
{
    [Authorize (Roles= ("Admin,SuperAdmin"))]
    public class Todos
    {
        public int ID { get; set; }
        public string Message { get; set; }
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }
    }
}