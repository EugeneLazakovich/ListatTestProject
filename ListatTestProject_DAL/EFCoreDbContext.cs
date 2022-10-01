using ListatTestProject_DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace ListatTestProject_DAL
{
    public class EFCoreDbContext : DbContext
    {
        public DbSet<Item> Items { get; set; }
        public DbSet<Sale> Sales { get; set; }

        protected EFCoreDbContext()
        {
        }

        public EFCoreDbContext(DbContextOptions<EFCoreDbContext> options) : base(options)
        {
            //Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
        }
    }
}
