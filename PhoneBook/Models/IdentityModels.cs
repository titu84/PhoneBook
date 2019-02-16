using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using PhoneBook.Models.Book;

namespace PhoneBook.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
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

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IPeopleContext
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }
        public DbSet<Person> People { get; set; }
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
        public void MarkAsModified(Person item)
        {
            Entry(item).State = EntityState.Modified;
        }
        public void MarkAsDeleted(Person item)
        {
            Entry(item).State = EntityState.Deleted;
        }
    }

    public interface IPeopleContext
    {
        DbSet<Person> People { get; set; }
        int SaveChanges();
        void MarkAsModified(Person item);
        void MarkAsDeleted(Person item);
    }
}