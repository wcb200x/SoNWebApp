using Microsoft.Owin;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Owin;
using SoNWebApp.Models;

[assembly: OwinStartupAttribute(typeof(SoNWebApp.Startup))]
namespace SoNWebApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateRolesAndUsers();
        }
        public void CreateRolesAndUsers()
        {
            ApplicationDbContext context = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            ////If Admin role doesn't exist, create first Super Admin Role and a default Super Admin User    
            //if (!roleManager.RoleExists("SuperAdmin"))
            //{
            //    //First we create Admin role   
            //    var role = new IdentityRole();
            //    role.Name = "SuperAdmin";
            //    roleManager.Create(role);

            //    //Then we create a Admin user                
            //    var user = new ApplicationUser();
            //    user.UserName = "admin@email.com"; //Use same UserName and Email for simplicity. 
            //    user.Email = "admin@email.com";    //Else you will need to modify the login action in the AccountController
            //    string userPWD = "Welcome1";

            //    var chkUser = UserManager.Create(user, userPWD);

            //    //Add default User to Role Admin   
            //    if (chkUser.Succeeded)
            //    {
            //        var result = UserManager.AddToRole(user.Id, "SuperAdmin");
            //    }
            //}
            ////// creating Creating Manager role    
            //if (!roleManager.RoleExists("Admin"))
            //{
            //    var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
            //    role.Name = "Admin";
            //    roleManager.Create(role);

            //}

            ////// creating Creating Employee role    
            //if (!roleManager.RoleExists("Advisor"))
            //{
            //    var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
            //    role.Name = "Advisor";
            //    roleManager.Create(role);

            //}
            //if (!roleManager.RoleExists("Student"))
            //{
            //    var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
            //    role.Name = "Student";
            //    roleManager.Create(role);

            //}

        }
    }

}

