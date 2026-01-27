using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MoviesDB.Entities;

namespace MoviesDB
{
    public class AppDbContext : DbContext
    {
        public DbSet<Title> Titles { get; set; }
        public DbSet<User> Users { get; set; }

        private readonly string _connectionString;
        public AppDbContext()
        {
            var config = new ConfigurationBuilder().AddJsonFile("AppConfig.json").Build();

            _connectionString = config.GetConnectionString("DefaultConnection");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer(_connectionString);
        }
    }
}
