using Microsoft.EntityFrameworkCore;
using SupportTracker.Models;

namespace SupportTracker.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Ticket> Tickets => Set<Ticket>();
    public DbSet<User> Users => Set<User>();
    public DbSet<Comment> Comments => Set<Comment>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure relationships
        modelBuilder.Entity<Ticket>()
            .HasOne(t => t.AssignedTo)
            .WithMany(u => u.AssignedTickets)
            .HasForeignKey(t => t.AssignedToId)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<Comment>()
            .HasOne(c => c.Ticket)
            .WithMany(t => t.Comments)
            .HasForeignKey(c => c.TicketId)
            .OnDelete(DeleteBehavior.Cascade);

        // Seed users
        modelBuilder.Entity<User>().HasData(
            new User { Id = 1, FullName = "Ahmad bin Ismail", Email = "ahmad@bassnet.com.my", Department = "Engineering" },
            new User { Id = 2, FullName = "Siti Nurhaliza", Email = "siti@bassnet.com.my", Department = "Support" },
            new User { Id = 3, FullName = "Rajesh Kumar", Email = "rajesh@bassnet.com.my", Department = "Engineering" },
            new User { Id = 4, FullName = "Mei Ling Tan", Email = "meiling@bassnet.com.my", Department = "Product" }
        );

        // Seed tickets
        modelBuilder.Entity<Ticket>().HasData(
            new Ticket
            {
                Id = 1,
                Title = "Login page returns 500 error after deploy",
                Description = "Users are seeing a blank page with 500 error when trying to log in. Started happening after the latest deployment to production. Browser console shows CORS error.",
                Status = TicketStatus.Open,
                Priority = TicketPriority.Critical,
                AssignedToId = 1,
                CreatedAt = new DateTime(2026, 7, 14, 9, 15, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 7, 14, 9, 15, 0, DateTimeKind.Utc)
            },
            new Ticket
            {
                Id = 2,
                Title = "Export to CSV times out for large datasets",
                Description = "When attempting to export more than 10,000 rows to CSV, the request times out after 30 seconds. Need to implement async streaming or pagination for the export.",
                Status = TicketStatus.InProgress,
                Priority = TicketPriority.High,
                AssignedToId = 3,
                CreatedAt = new DateTime(2026, 7, 13, 14, 30, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 7, 14, 8, 0, 0, DateTimeKind.Utc)
            },
            new Ticket
            {
                Id = 3,
                Title = "Add dark mode toggle to user settings",
                Description = "Several customers have requested a dark mode option. The design team has provided the color palette. Need to implement a toggle in user settings and persist the preference.",
                Status = TicketStatus.Open,
                Priority = TicketPriority.Medium,
                AssignedToId = null,
                CreatedAt = new DateTime(2026, 7, 12, 11, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 7, 12, 11, 0, 0, DateTimeKind.Utc)
            },
            new Ticket
            {
                Id = 4,
                Title = "Typo on pricing page — 'annually' spelled wrong",
                Description = "The pricing page says 'billed anually' instead of 'billed annually'. Simple text fix needed in the pricing component.",
                Status = TicketStatus.Resolved,
                Priority = TicketPriority.Low,
                AssignedToId = 1,
                CreatedAt = new DateTime(2026, 7, 11, 16, 45, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 7, 11, 17, 30, 0, DateTimeKind.Utc)
            },
            new Ticket
            {
                Id = 5,
                Title = "API rate limiting for third-party integrations",
                Description = "Need to implement rate limiting on the public API endpoints to prevent abuse. Should support configurable limits per API key tier (free, pro, enterprise).",
                Status = TicketStatus.Closed,
                Priority = TicketPriority.High,
                AssignedToId = 3,
                CreatedAt = new DateTime(2026, 7, 8, 10, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 7, 10, 15, 0, 0, DateTimeKind.Utc)
            }
        );

        // Seed comments
        modelBuilder.Entity<Comment>().HasData(
            new Comment
            {
                Id = 1,
                Content = "I checked the logs and it looks like the new auth middleware isn't forwarding CORS headers properly. Digging deeper.",
                AuthorName = "Ahmad bin Ismail",
                TicketId = 1,
                CreatedAt = new DateTime(2026, 7, 14, 9, 45, 0, DateTimeKind.Utc)
            },
            new Comment
            {
                Id = 2,
                Content = "Can we get the design team to prioritize the dark mode color tokens? Blocked on that.",
                AuthorName = "Siti Nurhaliza",
                TicketId = 3,
                CreatedAt = new DateTime(2026, 7, 13, 9, 30, 0, DateTimeKind.Utc)
            },
            new Comment
            {
                Id = 3,
                Content = "Fixed and deployed. Waiting for QA verification.",
                AuthorName = "Ahmad bin Ismail",
                TicketId = 4,
                CreatedAt = new DateTime(2026, 7, 11, 17, 30, 0, DateTimeKind.Utc)
            },
            new Comment
            {
                Id = 4,
                Content = "Implemented using the token bucket algorithm. Each API key tier has its own bucket configuration. PR is up for review.",
                AuthorName = "Rajesh Kumar",
                TicketId = 5,
                CreatedAt = new DateTime(2026, 7, 9, 11, 30, 0, DateTimeKind.Utc)
            }
        );
    }
}
