using FigoparaCaseStudyApi.Modules;
using Microsoft.EntityFrameworkCore;

namespace FigoparaCaseStudyApi.Entities.Db
{
    public class UserDbContext : DbContext
    {
        public UserDbContext() { }

        public UserDbContext(DbContextOptions<UserDbContext> options)
            : base(options) { }

        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite("Data Source=Application.db;Cache=Shared");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("user");
                entity.HasKey(cr => cr.Id);
                entity.Property(cr => cr.Email)
                      .IsRequired();
                entity.Property(cr => cr.Phone)
                      .IsRequired();
                entity.Property(cr => cr.Password)
                      .IsRequired();
                entity.Property(cr => cr.Surname)
                      .IsRequired();
                entity.Property(cr => cr.Name)
                      .IsRequired();
            });
        }
    }
}