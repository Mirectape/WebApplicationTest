using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.ContextFolder
{
    public class DataContext : DbContext
    {
        public DbSet<Person> Persons { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                @"Server=DESKTOP-ECEJ6EC\SQLEXPRESS;
                  DataBase=PersonDB;
                  Trusted_Connection=True;
                  Encrypt=false"
                );
        }
    }
}
