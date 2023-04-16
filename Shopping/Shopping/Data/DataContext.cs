using Microsoft.EntityFrameworkCore;
using Shopping.Data.Entities;

namespace Shopping.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Category> Categories { get; set; }
		public DbSet<City> Cities { get; set; }
		public DbSet<State> States { get; set; }
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Country>().HasIndex(x => x.CountryName).IsUnique();
            modelBuilder.Entity<Category>().HasIndex(x => x.CategoryName).IsUnique();
            modelBuilder.Entity<State>().HasIndex("StateName", "CountryIdCountry").IsUnique();
            modelBuilder.Entity<City>().HasIndex("CityName", "StateIdState").IsUnique();
        }
    }
}
