﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoNWebApp.Models.ViewModels
{
    public class AdvisorDefaultViewModel
    {
        public IEnumerable<string> AlertList { get; set; }
        public IEnumerable<Todos> TodosList { get; set; }
    }
}