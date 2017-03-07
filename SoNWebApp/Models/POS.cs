using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoNWebApp.Models
{
    public class POS
    {
        public int ID { get; set; }
        public string Course1 { get; set; }
        public string Course2 { get; set; }
        public string Course3 { get; set; }
        public string Course4 { get; set; }
        public string Course5 { get; set; }
        public string Course6 { get; set; }
        public string Course7 { get; set; }
        public string Course8 { get; set; }
        public string Course9 { get; set; }
        public string Course10 { get; set; }
        public string Course11 { get; set; }
        public string Course12 { get; set; }
        public int StudentID { get; set; }
        public virtual Student Student { get; set; }
        
    }
}