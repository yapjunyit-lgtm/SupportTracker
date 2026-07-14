using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SupportTracker.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FullName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Department = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tickets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 5000, nullable: false),
                    Status = table.Column<int>(type: "INTEGER", nullable: false),
                    Priority = table.Column<int>(type: "INTEGER", nullable: false),
                    AssignedToId = table.Column<int>(type: "INTEGER", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tickets_Users_AssignedToId",
                        column: x => x.AssignedToId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Content = table.Column<string>(type: "TEXT", maxLength: 5000, nullable: false),
                    AuthorName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    TicketId = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_Tickets_TicketId",
                        column: x => x.TicketId,
                        principalTable: "Tickets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Tickets",
                columns: new[] { "Id", "AssignedToId", "CreatedAt", "Description", "Priority", "Status", "Title", "UpdatedAt" },
                values: new object[] { 3, null, new DateTime(2026, 7, 12, 11, 0, 0, 0, DateTimeKind.Utc), "Several customers have requested a dark mode option. The design team has provided the color palette. Need to implement a toggle in user settings and persist the preference.", 1, 0, "Add dark mode toggle to user settings", new DateTime(2026, 7, 12, 11, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Department", "Email", "FullName" },
                values: new object[,]
                {
                    { 1, "Engineering", "ahmad@bassnet.com.my", "Ahmad bin Ismail" },
                    { 2, "Support", "siti@bassnet.com.my", "Siti Nurhaliza" },
                    { 3, "Engineering", "rajesh@bassnet.com.my", "Rajesh Kumar" },
                    { 4, "Product", "meiling@bassnet.com.my", "Mei Ling Tan" }
                });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "Id", "AuthorName", "Content", "CreatedAt", "TicketId" },
                values: new object[] { 2, "Siti Nurhaliza", "Can we get the design team to prioritize the dark mode color tokens? Blocked on that.", new DateTime(2026, 7, 13, 9, 30, 0, 0, DateTimeKind.Utc), 3 });

            migrationBuilder.InsertData(
                table: "Tickets",
                columns: new[] { "Id", "AssignedToId", "CreatedAt", "Description", "Priority", "Status", "Title", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2026, 7, 14, 9, 15, 0, 0, DateTimeKind.Utc), "Users are seeing a blank page with 500 error when trying to log in. Started happening after the latest deployment to production. Browser console shows CORS error.", 3, 0, "Login page returns 500 error after deploy", new DateTime(2026, 7, 14, 9, 15, 0, 0, DateTimeKind.Utc) },
                    { 2, 3, new DateTime(2026, 7, 13, 14, 30, 0, 0, DateTimeKind.Utc), "When attempting to export more than 10,000 rows to CSV, the request times out after 30 seconds. Need to implement async streaming or pagination for the export.", 2, 1, "Export to CSV times out for large datasets", new DateTime(2026, 7, 14, 8, 0, 0, 0, DateTimeKind.Utc) },
                    { 4, 1, new DateTime(2026, 7, 11, 16, 45, 0, 0, DateTimeKind.Utc), "The pricing page says 'billed anually' instead of 'billed annually'. Simple text fix needed in the pricing component.", 0, 2, "Typo on pricing page — 'annually' spelled wrong", new DateTime(2026, 7, 11, 17, 30, 0, 0, DateTimeKind.Utc) },
                    { 5, 3, new DateTime(2026, 7, 8, 10, 0, 0, 0, DateTimeKind.Utc), "Need to implement rate limiting on the public API endpoints to prevent abuse. Should support configurable limits per API key tier (free, pro, enterprise).", 2, 3, "API rate limiting for third-party integrations", new DateTime(2026, 7, 10, 15, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "Id", "AuthorName", "Content", "CreatedAt", "TicketId" },
                values: new object[,]
                {
                    { 1, "Ahmad bin Ismail", "I checked the logs and it looks like the new auth middleware isn't forwarding CORS headers properly. Digging deeper.", new DateTime(2026, 7, 14, 9, 45, 0, 0, DateTimeKind.Utc), 1 },
                    { 3, "Ahmad bin Ismail", "Fixed and deployed. Waiting for QA verification.", new DateTime(2026, 7, 11, 17, 30, 0, 0, DateTimeKind.Utc), 4 },
                    { 4, "Rajesh Kumar", "Implemented using the token bucket algorithm. Each API key tier has its own bucket configuration. PR is up for review.", new DateTime(2026, 7, 9, 11, 30, 0, 0, DateTimeKind.Utc), 5 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_TicketId",
                table: "Comments",
                column: "TicketId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_AssignedToId",
                table: "Tickets",
                column: "AssignedToId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "Tickets");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
