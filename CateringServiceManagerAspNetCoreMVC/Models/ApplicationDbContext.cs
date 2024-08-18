using Microsoft.EntityFrameworkCore;

namespace CateringServiceManagerAspNetCoreMVC.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :base(options)
        {
            
        }
        public DbSet<Admin> tbl_admin { get; set; }
        public DbSet<Daig> tbl_daig {  get; set; }
    }
}
