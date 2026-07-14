using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SupportTracker.Data;

namespace SupportTracker.Controllers;

public class HomeController : Controller
{
    private readonly AppDbContext _db;

    public HomeController(AppDbContext db)
    {
        _db = db;
    }

    public async Task<IActionResult> Index()
    {
        var total = await _db.Tickets.CountAsync();
        var open = await _db.Tickets.CountAsync(t => t.Status == Models.TicketStatus.Open);
        var inProgress = await _db.Tickets.CountAsync(t => t.Status == Models.TicketStatus.InProgress);
        var resolved = await _db.Tickets.CountAsync(t => t.Status == Models.TicketStatus.Resolved);

        var recentTickets = await _db.Tickets
            .Include(t => t.AssignedTo)
            .OrderByDescending(t => t.UpdatedAt)
            .Take(5)
            .ToListAsync();

        ViewBag.TotalTickets = total;
        ViewBag.OpenTickets = open;
        ViewBag.InProgressTickets = inProgress;
        ViewBag.ResolvedTickets = resolved;
        ViewBag.RecentTickets = recentTickets;

        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        ViewData["RequestId"] = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
        return View();
    }
}
