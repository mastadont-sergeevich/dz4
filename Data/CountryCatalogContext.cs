using Microsoft.EntityFrameworkCore;
using CountryCatalogAPI.Models;

namespace CountryCatalogAPI.Data
{
    public class CountryCatalogContext : DbContext
    {
        public CountryCatalogContext(DbContextOptions<CountryCatalogContext> options) 
            : base(options) { }

        public DbSet<Country> Countries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Country>(entity =>
            {
                entity.HasIndex(c => c.Alpha2Code).IsUnique();
                entity.Property(c => c.FullName).IsRequired();
            });
        }
    }
}