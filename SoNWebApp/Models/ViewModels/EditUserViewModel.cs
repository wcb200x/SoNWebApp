﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace SoNWebApp.Models.ViewModels
{
    public class EditUserViewModel
    {
        public EditUserViewModel() { }

        // Allow Initialization with an instance of ApplicationUser:
        public EditUserViewModel(ApplicationUser user)
        {
            UserName = user.UserName;
            //this.FirstName = user.FirstName;
            //this.LastName = user.LastName;
            Email = user.Email;
            //RoleName = user.Roles.ToString();
            //RemoveRoleName = user.Roles.ToString();
        }

        [Required]
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }

        public List<SelectRoleEditorViewModel> Roles { get; set; }
        public string AddRoleName { get; set; }
        public string RemoveRoleName { get; set; }
        public IEnumerable<SelectListItem> RoleNames { get; set; }

    }
}