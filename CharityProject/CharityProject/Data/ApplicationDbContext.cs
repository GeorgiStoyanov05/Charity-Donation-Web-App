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
        public DbSet<Creator> Owner { get; set; }
        public DbSet<Charity> Charity { get; set; }
        public DbSet<Category> Category { get; set; }

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

            base.OnModelCreating(builder);
        }
    }
}