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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(b =>
            {
                b.HasIndex(u => u.Username).IsUnique();
                b.HasIndex(u => u.Email).IsUnique();
                b.ToTable(t =>
                {
                    t.HasCheckConstraint("CK_Username", "LEN([Username]) > 0");
                    t.HasCheckConstraint("CK_UserEmail", "[Email] LIKE '%@%'");
                });

                b.HasMany(u => u.Titles).WithOne(t => t.User).HasForeignKey(t => t.UserId); 
            });

            modelBuilder.Entity<Title>(b =>
            {
                b.Property(t => t.Name).HasColumnType("NVARCHAR").HasMaxLength(50);
                b.Property(t => t.AddedDay).HasDefaultValueSql("GETDATE()");
                b.ToTable(t =>
                {
                    t.HasCheckConstraint("CK_TitleName", "LEN([Name]) > 0");
                    t.HasCheckConstraint("CK_TitleReleaseDate", "(YEAR(ReleaseDate)) > 0");
                });

                b.HasOne(t => t.User).WithMany(u => u.Titles).HasForeignKey(t => t.UserId);
            });
        }
    }
}
