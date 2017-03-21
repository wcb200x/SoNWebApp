using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity.EntityFramework;

namespace SoNWebApp.Models.ViewModels
{
    public class SelectRoleEditorViewModel
    {
        public SelectRoleEditorViewModel() { }
        public SelectRoleEditorViewModel(IdentityRole role)
        {
            this.RoleId = role.Id;
            this.RoleName = role.Name;
          
        }

        public bool Selected { get; set; }

        [Required]
        public string RoleName { get; set; }

        public string RoleId { get; set; }
    }
}