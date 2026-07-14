# 🎫 SupportTracker

A ticket management system built with **ASP.NET Core 8 MVC** — a portfolio project demonstrating C# and .NET fundamentals for a software engineering internship interview.

**🔗 Live Demo:** [supporttracker-demo.azurewebsites.net](https://supporttracker-demo.azurewebsites.net)  
**📂 GitHub:** [github.com/yapjunyit-lgtm/SupportTracker](https://github.com/yapjunyit-lgtm/SupportTracker)

---

## Features

| Feature | Description |
|---------|-------------|
| **Dashboard** | Summary cards (total, open, in-progress, resolved tickets) + recent activity table |
| **Ticket CRUD** | Create, list, view detail, edit tickets with full form validation |
| **Search & Filter** | Search by title, filter by status and priority |
| **Status Workflow** | Open → In Progress → Resolved → Closed |
| **Priority Levels** | Low, Medium, High, Critical |
| **User Assignment** | Assign tickets to team members via dropdown |
| **Comments** | Add and view comments on ticket detail page |
| **Seed Data** | Pre-loaded with 4 users, 5 tickets, and 4 comments for demo |

---

## Tech Stack

| Layer | Technology |
|-------|-----------|
| Framework | ASP.NET Core 8 MVC |
| Database | SQLite + Entity Framework Core 8 |
| Frontend | Razor Views + Bootstrap 5 |
| Validation | Data Annotations (server + client-side) |
| Deployment | Azure App Service (Free Tier — East Asia) |

---

## C# & .NET Concepts Demonstrated

- **OOP:** Classes, enums, navigation properties, data annotations
- **LINQ:** `Where`, `Contains`, `Count`, `OrderByDescending`, `Include`, `IQueryable` composition
- **Entity Framework Core:** DbContext, migrations, Fluent API relationships, seed data
- **MVC Pattern:** Controllers, Razor Views, Tag Helpers, model binding, `ViewBag`, `TempData`
- **Dependency Injection:** Constructor injection of `AppDbContext`
- **Validation:** `[Required]`, `[MaxLength]`, `[EmailAddress]`, `[Display]`, anti-forgery tokens

---

## Project Structure

```
SupportTracker/
├── Controllers/
│   ├── HomeController.cs         — Dashboard
│   ├── TicketsController.cs      — Ticket CRUD + search/filter + comments
│   └── UsersController.cs        — User listing
├── Models/
│   ├── Ticket.cs                 — Ticket entity with navigation properties
│   ├── User.cs                   — User entity
│   ├── Comment.cs                — Comment entity
│   ├── TicketStatus.cs           — Enum: Open, InProgress, Resolved, Closed
│   └── TicketPriority.cs         — Enum: Low, Medium, High, Critical
├── Data/
│   └── AppDbContext.cs           — EF Core context with Fluent API + seed data
├── Views/
│   ├── Home/Index.cshtml         — Dashboard page
│   ├── Tickets/
│   │   ├── Index.cshtml          — Ticket list with search/filter
│   │   ├── Detail.cshtml         — Ticket detail with comments
│   │   ├── Create.cshtml         — Create ticket form
│   │   └── Edit.cshtml           — Edit ticket form
│   ├── Users/Index.cshtml        — User listing
│   └── Shared/_Layout.cshtml     — Layout with navbar
├── Migrations/                   — EF Core migration files
├── wwwroot/                      — Static assets (CSS, JS, libs)
├── Program.cs                    — App entry point + DI + middleware
└── SupportTracker.csproj         — Project file
```

---

## Getting Started

### Prerequisites
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)

### Run Locally

```bash
# Clone the repo
git clone https://github.com/yapjunyit-lgtm/SupportTracker.git
cd SupportTracker

# Restore packages and run
dotnet restore
dotnet run
```

Open **http://localhost:{PORT}** in your browser. The database is auto-created with seed data on first run.

---

## Deploy to Azure

This app is deployed on Azure App Service (Free F1 tier, Linux, East Asia).

```bash
# Build
dotnet publish -c Release -o ./publish

# Zip and deploy
cd publish && zip -r ../deploy.zip . && cd ..
az webapp deploy --name supporttracker-demo --resource-group SupportTrackerRG --src-path deploy.zip --type zip
```

### Azure Configuration
- **Connection string** set via `ConnectionStrings__DefaultConnection` app setting → `Data Source=/home/SupportTracker.db` (persisted storage)
- **Runtime:** `DOTNETCORE|8.0`
- **SKU:** F1 (Free)

---

## Seed Data

The app comes with realistic demo data:

| ID | Ticket | Status | Priority | Assigned To |
|----|--------|--------|----------|-------------|
| #1 | Login page returns 500 error after deploy | Open | Critical | Ahmad bin Ismail |
| #2 | Export to CSV times out for large datasets | In Progress | High | Rajesh Kumar |
| #3 | Add dark mode toggle to user settings | Open | Medium | — |
| #4 | Typo on pricing page — 'annually' spelled wrong | Resolved | Low | Ahmad bin Ismail |
| #5 | API rate limiting for third-party integrations | Closed | High | Rajesh Kumar |

---

Built as a portfolio project © 2025
