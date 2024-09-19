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
            modelBuilder.Entity<Bundle>()
                .HasMany(p => p.pipes)
                .WithOne(p => p.bundle)
                .HasForeignKey(p => p.bundleId);

            modelBuilder.Entity<Pipe>()
            .HasKey(p => p.id);

            modelBuilder.Entity<Pipe>()
                .Property(p => p.id)
                .ValueGeneratedOnAdd(); 
        }

        public DbSet<Pipe> Pipes { get; set; }
        public DbSet<Bundle> Bundles { get; set; }
    }
}
