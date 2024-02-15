using Charities.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CharityProject.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Creator> Owners { get; set; }
        public DbSet<Charity> Charities { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Comment>()
                .HasKey(x => new { x.CharityId, x.UserId });

            builder.Entity<Update>()
                .HasKey(x => new { x.CharityId, x.UserId });

            builder.Entity<Donation>()
                .HasKey(x => new { x.CharityId, x.UserId });

            builder.Entity<User>()
                .Property(u => u.UserName)
                .HasMaxLength(20)
                .IsRequired();

            builder.Entity<User>()
                .Property(u => u.Email)
                .IsRequired();

            builder.Entity<Category>()
                .HasData(new Category()
                {
                    Id = Guid.NewGuid(),
                    Name = "Healthy Food"
                },
                new Category()
                {
                    Id = Guid.NewGuid(),
                    Name = "Free Education"
                },
                new Category()
                {
                    Id = Guid.NewGuid(),
                    Name = "Clean Water"
                },
                new Category()
                {
                    Id = Guid.NewGuid(),
                    Name = "Helping Poor"
                },
                new Category()
                {
                    Id = Guid.NewGuid(),
                    Name = "Medical Facilities"
                });

            base.OnModelCreating(builder);
        }
    }
}