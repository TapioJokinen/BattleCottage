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
        public DbSet<GameRole> GameRoles => Set<GameRole>();
        public DbSet<LFGPost> LFGPosts => Set<LFGPost>();
        public DbSet<LFGPostGameRole> LFGPostGameRoles => Set<LFGPostGameRole>();

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

            builder.Entity<GameRole>()
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

            builder.Entity<GameRole>()
                .HasData(
                    new GameRole { Id = 1, Name = "Tank", DateAdded = DateTime.UtcNow, DateUpdated = DateTime.UtcNow },
                    new GameRole { Id = 2, Name = "Healer", DateAdded = DateTime.UtcNow, DateUpdated = DateTime.UtcNow },
                    new GameRole { Id = 3, Name = "DPS", DateAdded = DateTime.UtcNow, DateUpdated = DateTime.UtcNow },
                    new GameRole { Id = 4, Name = "Top Lane", DateAdded = DateTime.UtcNow, DateUpdated = DateTime.UtcNow },
                    new GameRole { Id = 5, Name = "Bottom Lane", DateAdded = DateTime.UtcNow, DateUpdated = DateTime.UtcNow },
                    new GameRole { Id = 6, Name = "Mid Lane", DateAdded = DateTime.UtcNow, DateUpdated = DateTime.UtcNow },
                    new GameRole { Id = 7, Name = "Jungle", DateAdded = DateTime.UtcNow, DateUpdated = DateTime.UtcNow },
                    new GameRole { Id = 8, Name = "Support", DateAdded = DateTime.UtcNow, DateUpdated = DateTime.UtcNow },
                    new GameRole { Id = 9, Name = "Entry Fragger", DateAdded = DateTime.UtcNow, DateUpdated = DateTime.UtcNow },
                    new GameRole { Id = 10, Name = "ReFragger", DateAdded = DateTime.UtcNow, DateUpdated = DateTime.UtcNow },
                    new GameRole { Id = 11, Name = "Strategy Caller", DateAdded = DateTime.UtcNow, DateUpdated = DateTime.UtcNow },
                    new GameRole { Id = 12, Name = "Lurker", DateAdded = DateTime.UtcNow, DateUpdated = DateTime.UtcNow },
                    new GameRole { Id = 13, Name = "Awper", DateAdded = DateTime.UtcNow, DateUpdated = DateTime.UtcNow },
                    new GameRole { Id = 14, Name = "Combat Support", DateAdded = DateTime.UtcNow, DateUpdated = DateTime.UtcNow },
                    new GameRole { Id = 15, Name = "Medic", DateAdded = DateTime.UtcNow, DateUpdated = DateTime.UtcNow },
                    new GameRole { Id = 16, Name = "Assault", DateAdded = DateTime.UtcNow, DateUpdated = DateTime.UtcNow },
                    new GameRole { Id = 17, Name = "Recon", DateAdded = DateTime.UtcNow, DateUpdated = DateTime.UtcNow },
                    new GameRole { Id = 18, Name = "Friendly", DateAdded = DateTime.UtcNow, DateUpdated = DateTime.UtcNow },
                    new GameRole { Id = 19, Name = "Funny", DateAdded = DateTime.UtcNow, DateUpdated = DateTime.UtcNow },
                    new GameRole { Id = 20, Name = "Serious", DateAdded = DateTime.UtcNow, DateUpdated = DateTime.UtcNow },
                    new GameRole { Id = 21, Name = "e-Girl", DateAdded = DateTime.UtcNow, DateUpdated = DateTime.UtcNow },
                    new GameRole { Id = 22, Name = "Silent", DateAdded = DateTime.UtcNow, DateUpdated = DateTime.UtcNow },
                    new GameRole { Id = 23, Name = "Carry", DateAdded = DateTime.UtcNow, DateUpdated = DateTime.UtcNow }
                );
        }
    }
}
