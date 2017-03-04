using System;
using System.Collections.Generic;
using System.Linq;

namespace SoNWebApp.Models.ViewModels
{
    public class SelectUserRolesViewModel
    {
        public SelectUserRolesViewModel()
        {
            this.Roles = new List<SelectRoleEditorViewModel>();
        }

        // Enable initialization with an instance of ApplicationUser:
        public SelectUserRolesViewModel(ApplicationUser user)
            : this()
        {
            this.UserName = user.UserName;
            //this.FirstName = user.FirstName;
            //this.LastName = user.LastName;

            var Db = new ApplicationDbContext();


            var allRolesForCurrentUser = Db.Users.FirstOrDefault(u => u.Id == user.Id).Roles;

            var roleNames = new List<string>();
            foreach (var role in allRolesForCurrentUser)
            {
                var roleName = Db.Roles.FirstOrDefault(r => r.Id == role.RoleId).Name;

                var rvm = new SelectRoleEditorViewModel()
                {
                    RoleId = role.RoleId,
                    RoleName = roleName,
                    Selected = true
                };

                this.Roles.Add(rvm);
            }
        }

        public string Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<SelectRoleEditorViewModel> Roles { get; set; }
    }
}