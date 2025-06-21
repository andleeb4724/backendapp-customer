using Customer_Crud.Models;
using Microsoft.EntityFrameworkCore;

namespace Customer_Crud.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
                
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<CallDetail> CallDetails { get; set; }


    }
}
