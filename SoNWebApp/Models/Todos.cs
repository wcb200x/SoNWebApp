using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoNWebApp.Models
{
    public class Todos
    {
        public int ID { get; set; }
        public string Message { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}