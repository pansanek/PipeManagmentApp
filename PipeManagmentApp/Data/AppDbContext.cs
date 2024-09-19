using Microsoft.EntityFrameworkCore;
using PipeManagmentApp.Data.Models;

namespace PipeManagmentApp.Data
{
    public class AppDbContext : DbContext
    {
        protected readonly IConfiguration Configuration;

        public AppDbContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // connect to postgres with connection string from app settings
            options.UseNpgsql(Configuration.GetConnectionString("WebApiDatabase"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Package>()
                .HasMany(p => p.pipes)
                .WithOne(p => p.package)
                .HasForeignKey(p => p.packageId);
        }

        public DbSet<Pipe> Pipes { get; set; }
        public DbSet<Package> Packages { get; set; }
    }
}
