using BattleCottage.Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BattleCottage.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        protected readonly IConfiguration _configuration;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }

        public DbSet<Game> Games => Set<Game>();
        public DbSet<GameMode> GameModes => Set<GameMode>();
        public DbSet<GameStyle> GameStyles => Set<GameStyle>();
        public DbSet<LFGPost> LFGPosts => Set<LFGPost>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseNpgsql(_configuration.GetConnectionString("DefaultConnection"));
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Game>()
                .HasIndex(x => x.Name)
                .IsUnique();

            builder.Entity<GameMode>()
                .HasIndex(x => x.Name)
                .IsUnique();

            builder.Entity<GameStyle>()
                .HasIndex(x => x.Name)
                .IsUnique();

            builder.Entity<GameMode>()
                .HasData(
                    new GameMode { Id = 1, Name = "PvP", DateAdded = DateTime.UtcNow, DateUpdated = DateTime.UtcNow },
                    new GameMode { Id = 2, Name = "PvE", DateAdded = DateTime.UtcNow, DateUpdated = DateTime.UtcNow }
                );

            builder.Entity<GameStyle>()
                .HasData(
                    new GameStyle { Id = 1, Name = "Casual", DateAdded = DateTime.UtcNow, DateUpdated = DateTime.UtcNow },
                    new GameStyle { Id = 2, Name = "Competitive", DateAdded = DateTime.UtcNow, DateUpdated = DateTime.UtcNow }
                );
        }
    }
}
