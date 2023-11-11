using BasketballClubAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BasketballClubAPI.Data {
    public class DataContext: DbContext {
        public DbSet<Player> Player { get; set; }
        public DbSet<Coach> Coach { get; set; }
        public DbSet<Team> Team { get; set; }
        public DbSet<Match> Match { get; set; }
        public DbSet<Statistic> Statistic { get; set; }
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder) {

            modelBuilder.Entity<Player>()
                .HasKey(p => p.Id);

            modelBuilder.Entity<Player>()
                .Property(p => p.Position)
                .HasConversion<string>();

            modelBuilder.Entity<Player>()
                .Property(p => p.TeamId)
                .IsRequired(false);

            modelBuilder.Entity<Player>()
                .HasOne(p => p.Team)  // Define the navigation property
                .WithMany(t => t.Players)
                .HasForeignKey(p => p.TeamId)
                .OnDelete(DeleteBehavior.Restrict);  // Specify the cascade behavior here

            modelBuilder.Entity<Team>()
                .HasKey(t => t.Id);
            modelBuilder.Entity<Team>()
                .Property(t => t.HeadCoachId)
                .IsRequired(false);

            modelBuilder.Entity<Match>()
                .HasOne(m => m.HomeTeam)
                .WithMany()
                .HasForeignKey(m => m.HomeTeamId)
                .OnDelete(DeleteBehavior.Restrict); // Restrict the delete action for HomeTeam
            modelBuilder.Entity<Match>()
                .HasOne(m => m.AwayTeam)
                .WithMany()
                .HasForeignKey(m => m.AwayTeamId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Match>()
                .Property(m => m.Status)
                .HasConversion<string>();

            // Configure the Statistic entity
            modelBuilder.Entity<Statistic>()
                .HasKey(s => new { s.MatchId, s.PlayerId });

            // Define the many-to-many relationship between Match and Player through Statistic
            modelBuilder.Entity<Statistic>()
                .HasOne(s => s.Match)
                .WithMany(m => m.Statistics)
                .HasForeignKey(s => s.MatchId);

            modelBuilder.Entity<Statistic>()
                .HasOne(s => s.Player)
                .WithMany(p => p.Statistics)
                .HasForeignKey(s => s.PlayerId);
        }
    }
}
