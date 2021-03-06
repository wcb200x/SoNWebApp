﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SoNWebApp.Models
{
    public class Document
    {
        
        public int Id { get; set; }
        public int StudentID { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ExpirationDate { get; set; }
        public string DocumentType { get; set; }
        public bool ComplianceStatus { get; set; }
        public int StudentNumber { get; set; }
        public virtual Student Student { get; set; }
        public byte[] FileBytes { get; set; }
        public int ContentLength { get; set; }
        public string ContentType { get; set; }
        public string FileName { get; set; }
        public string UploadedBy { get; set; }
        public bool Active { get; set; }
    }
}