using SoNWebApp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace SoNWebApp.Mapping
{
    public class ClinicalComplianceMapping : EntityTypeConfiguration<Compliance>
    {
        public ClinicalComplianceMapping()
        {

            //Property(p => p.DocumentID).IsOptional();
            Property(p => p.ExpirationDate).IsOptional();
    
    }
    }
}