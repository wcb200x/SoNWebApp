using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using SoNWebApp.Models;
using SoNWebApp.Models.ViewModels;
using System;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;


namespace SoNWebApp.Controllers
{
    public class UserManagementController : Controller
    {
        //Refer to this github repo for another example of how to do this.
        //https://github.com/TypecastException/AspNetRoleBasedSecurityExample/blob/master/AspNetRoleBasedSecurity/Views/Account/Edit.cshtml

        readonly ApplicationDbContext db = new ApplicationDbContext();
        



        [Authorize(Roles = "SuperAdmin, Admin")]
        public ActionResult Index(string searchString1)
        {

            var users = from x in db.Users
            select x;
            if (!String.IsNullOrEmpty(searchString1))
            {
                users = users.Where(x => x.Email.Contains(searchString1));
            }
            var model = new List<SelectUserRolesViewModel>();
            foreach (var user in users)
            {
                var u = new SelectUserRolesViewModel(user);
                u.Id = user.Id;
                model.Add(u);
            }
         
            return View(model);
        }


        [Authorize(Roles = "SuperAdmin, Admin")]
        public ActionResult Edit(string id)
        {
            var Db = new ApplicationDbContext();
            var user = Db.Users.FirstOrDefault(u => u.Id == id);
            
            var roleList = new List<SelectRoleEditorViewModel>();
        
            var allRoles = Db.Roles;

            foreach (var role in allRoles)
            {
                //var roleName = allRoles.FirstOrDefault(r => r.Id == role.Id).Name.ToList();

                var rvm = new SelectRoleEditorViewModel()
                {
                    RoleId = role.Id,
                    RoleName = role.Name
                };
                roleList.Add(rvm);
            }

            ViewBag.Name = new SelectList(db.Roles.ToList(), "Name", "Name");
            var model = new EditUserViewModel(user);
            model.Roles = roleList;
            var roles = Roles();
            model.RoleNames = GetRolesListItems(roles);
            return View(model);
        }


        [HttpPost]
        [Authorize(Roles = "SuperAdmin, Admin")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EditUserViewModel model)
        {
            var roles = Roles();
            model.RoleNames = GetRolesListItems(roles);

            if (ModelState.IsValid)
            {
                var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
                var Db = new ApplicationDbContext();
                var user = Db.Users.First(u => u.UserName == model.UserName);

                if (model.AddRoleName == null)
                {
                    model.AddRoleName = "none";
                }
                else
                {
                    userManager.AddToRole(user.Id, model.AddRoleName.ToLower());
                }
                if (model.RemoveRoleName == null)
                {
                    model.RemoveRoleName = "none";
                }
                else
                {
                    userManager.RemoveFromRole(user.Id, model.RemoveRoleName.ToLower());
                }
              
               
               
                user.Email = model.Email;
                
                Db.Entry(user).State = System.Data.Entity.EntityState.Modified;
                 await Db.SaveChangesAsync();
                

                return RedirectToAction("Index");
            }
            // If we got this far, something failed, redisplay form

            return View(model);
        }
        public IEnumerable<string> Roles()
        {
            return new List<string>
            {
                "SuperAdmin",
                "Admin",
                "Advisor",
                "Student",
                "None",

            };

        }
        public IEnumerable<SelectListItem> GetRolesListItems(IEnumerable<string> items)
        {
            var selectList = new List<SelectListItem>();
            foreach (var element in items)
            {
                selectList.Add(new SelectListItem
                {
                    Value = element,
                    Text = element
                });
            }
            return selectList;
        }

        //[Authorize(Roles = "Admin")]
        //public ActionResult Delete(string id = null)
        //{
        //    var Db = new ApplicationDbContext();
        //    var user = Db.Users.First(u => u.UserName == id);
        //    var model = new EditUserViewModel(user);
        //    if (user == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return System.Web.UI.WebControls.View(model);
        //}


        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //[Authorize(Roles = "Admin")]
        //public ActionResult DeleteConfirmed(string id, string roleName)
        //{
        //    //    var Db = new ApplicationDbContext();
        //        var user = db.Users.First(u => u.Id == id);
        //    var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
        //    userManager.RemoveFromRolesAsync(user.Id, roleName);
        //    //    Db.Users.Remove(user);
        //    //    Db.SaveChanges();
        //    return RedirectToAction("Index");
        //}


        //[Authorize(Roles = "Admin")]
        //public ActionResult UserRoles(string id)
        //{
        //    var Db = new ApplicationDbContext();
        //    var user = Db.Users.First(u => u.UserName == id);
        //    var model = new SelectUserRolesViewModel(user);
        //    return System.Web.UI.WebControls.View(model);
        //}


        //[HttpPost]
        //[Authorize(Roles = "Admin")]
        //[ValidateAntiForgeryToken]
        //public ActionResult UserRoles(SelectUserRolesViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var idManager = new IdentityManager();
        //        var Db = new ApplicationDbContext();
        //        var user = Db.Users.First(u => u.UserName == model.UserName);
        //        idManager.ClearUserRoles(user.Id);
        //        foreach (var role in model.Roles)
        //        {
        //            if (role.Selected)
        //            {
        //                idManager.AddUserToRole(user.Id, role.RoleName);
        //            }
        //        }
        //        return RedirectToAction("index");
        //    }
        //    return View();
    }
    }
