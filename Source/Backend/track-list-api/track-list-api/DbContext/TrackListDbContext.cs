using api.Models;
using dotenv.net;
using Microsoft.EntityFrameworkCore;

namespace api.DbContext;

public class TrackListDbContext(DbContextOptions options) : Microsoft.EntityFrameworkCore.DbContext(options)
{
    private static readonly IDictionary<string, string> Env = DotEnv.Read();

    public DbSet<Comment> Comments { get; set; } = null!;
    public DbSet<CommentLike> CommentLike { get; set; } = null!;
    public DbSet<Follow> Follows { get; set; } = null!;
    public DbSet<Media> Media { get; set; } = null!;
    public DbSet<MediaTranslation> MediaTranslations { get; set; } = null!;
    public DbSet<Playlist> Playlists { get; set; } = null!;
    public DbSet<PlaylistAccess> PlaylistAccess { get; set; } = null!;
    public DbSet<PlaylistItem> PlaylistItems { get; set; } = null!;
    public DbSet<Report> Reports { get; set; } = null!;
    public DbSet<Review> Reviews { get; set; } = null!;
    public DbSet<ReviewLike> ReviewLikes { get; set; } = null!;
    public DbSet<TrackingStatus> TrackingStatuses { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<UserImage> UserImages { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(
                Env["CONNECTION_STRING"]
            )
            .LogTo(Console.WriteLine, LogLevel.Warning)
            //.EnableDetailedErrors()
            //.EnableSensitiveDataLogging();
            ;
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure Follow relationships
        modelBuilder.Entity<Follow>()
            .HasOne(f => f.Follower)
            .WithMany(u => u.Following)
            .HasForeignKey(f => f.FollowerId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Follow>()
            .HasOne(f => f.Following)
            .WithMany(u => u.Followers)
            .HasForeignKey(f => f.FollowingId)
            .OnDelete(DeleteBehavior.Restrict);

        // Optional: enforce unique follow relationship
        modelBuilder.Entity<Follow>()
            .HasIndex(f => new { f.FollowerId, f.FollowingId })
            .IsUnique();
    }
}