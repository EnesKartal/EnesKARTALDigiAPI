using Microsoft.EntityFrameworkCore;

namespace EnesKARTALDigiAPI.Data.Models
{
    public partial class DigiBlogDBContext : DbContext
    {
        public DigiBlogDBContext()
        {
        }

        public DigiBlogDBContext(DbContextOptions<DigiBlogDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<Post> Post { get; set; }
        public virtual DbSet<Comment> Comment { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Comment>(entity =>
            {
                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.PostId)
                    .IsRequired();
            });

            modelBuilder.Entity<Post>(entity =>
            {
                entity.Property(e => e.Title)
                   .IsRequired()
                   .HasMaxLength(100);

                entity.Property(e => e.SubTitle)
                .HasMaxLength(250);

                entity.Property(e => e.Description)
                    .IsRequired();
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(120);

                entity.Property(e => e.Password)
                    .IsRequired();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}