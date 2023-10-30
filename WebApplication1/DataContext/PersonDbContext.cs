using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApplication1.AuthPersonApp;
using WebApplication1.Models;

namespace WebApplication1.DataContext
{
    public class PersonDbContext : IdentityDbContext<User>
    {
        public PersonDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Person> People { get; set; }
    }
}
