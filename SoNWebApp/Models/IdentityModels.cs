using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace SoNWebApp.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
    
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {

#if DEBUG
            // This will create database if one doesn't exist.
            Database.SetInitializer(new CreateDatabaseIfNotExists<ApplicationDbContext>());
            //This will drop and re-create the database if model changes.
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<ApplicationDbContext>());
#endif
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Compliance>().HasKey(c => c.ID);
            modelBuilder.Entity<Compliance>().Property(p => p.DocumentID).IsOptional();
            modelBuilder.Entity<Compliance>().Property(p => p.ExpirationDate).IsOptional();
            modelBuilder.Entity<Compliance>().Property(p => p.StudentID).IsRequired();
            modelBuilder.Entity<Compliance>().Property(p => p.Name).IsRequired();


            modelBuilder.Entity<Compliance>().HasOptional(c => c.Document);
            modelBuilder.Entity<Compliance>().HasRequired(c => c.Student);

            //modelBuilder.Entity<Compliance>().HasOptional(c => c.ExpirationDate);

            base.OnModelCreating(modelBuilder);
        }


        public DbSet <Student> Students { get; set; }
        public DbSet <Program> Programs { get; set; }
        public DbSet <Courses> Courses  { get; set; }
        public DbSet <Enrollment> Enrollments { get; set; }
        public DbSet <Campus> Campuses { get; set; }
        public DbSet <Event> Events { get; set; }
        public DbSet<Document> Documents { get; set; }

        public System.Data.Entity.DbSet<SoNWebApp.Models.Todos> Todos { get; set; }

        public System.Data.Entity.DbSet<SoNWebApp.Models.UDApplication> UDApplications { get; set; }
        public DbSet<POS> POS { get; set; }

        public System.Data.Entity.DbSet<SoNWebApp.Models.Alerts> Alerts { get; set; }

        public System.Data.Entity.DbSet<SoNWebApp.Models.Compliance> Compliances { get; set; }
    }
}