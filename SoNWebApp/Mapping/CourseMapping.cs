using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using SoNWebApp.Models;
using SoNWebApp.Models.ViewModels;

namespace SoNWebApp.Mapping
{
    public class CourseMapping:EntityTypeConfiguration<Courses>
    {
        public CourseMapping()
        {
            HasKey(p => p.Id);

    }
    }
}