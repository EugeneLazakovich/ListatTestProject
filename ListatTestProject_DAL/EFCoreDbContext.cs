using Microsoft.EntityFrameworkCore;
using System;

namespace ListatTestProject_DAL
{
    public class EFCoreDbContext : DbContext
    {
        public DbSet<Location> Locations { get; set; }
        public DbSet<Book> Books { get; set; }

        protected EFCoreDbContext()
        {
        }

        public EFCoreDbContext(DbContextOptions<EFCoreDbContext> options) : base(options)
        {
            //Database.EnsureCreated();
        }

        protected override void OnModelCreating()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }
    }
}
