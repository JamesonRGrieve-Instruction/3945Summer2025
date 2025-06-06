using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProjectName.Models;
namespace ProjectName.Data
{

    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<Post> Posts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Post>(entity => // A Person
            {
                entity.HasOne(child => child.User) // Has One Job
                    .WithMany(parent => parent.Posts) // With Many People
                    .HasForeignKey(child => child.UserID)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName($"FK_{nameof(Post)}_{nameof(User)}");

                entity.HasIndex(entity => entity.UserID)
                    .HasDatabaseName($"FK_{nameof(Post)}_{nameof(User)}");

            });

            base.OnModelCreating(modelBuilder);
        }
    }
}