using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SupportTracker.Data;
using SupportTracker.Models;

namespace SupportTracker.Controllers;

public class TicketsController : Controller
{
    private readonly AppDbContext _db;

    public TicketsController(AppDbContext db)
    {
        _db = db;
    }

    // GET: /Tickets?search=&status=&priority=
    public async Task<IActionResult> Index(string? search, string? status, string? priority)
    {
        var query = _db.Tickets
            .Include(t => t.AssignedTo)
            .AsQueryable();

        // Search by title
        if (!string.IsNullOrWhiteSpace(search))
        {
            query = query.Where(t => t.Title.Contains(search));
        }

        // Filter by status
        if (!string.IsNullOrWhiteSpace(status) && Enum.TryParse<TicketStatus>(status, out var statusEnum))
        {
            query = query.Where(t => t.Status == statusEnum);
        }

        // Filter by priority
        if (!string.IsNullOrWhiteSpace(priority) && Enum.TryParse<TicketPriority>(priority, out var priorityEnum))
        {
            query = query.Where(t => t.Priority == priorityEnum);
        }

        var tickets = await query
            .OrderByDescending(t => t.CreatedAt)
            .ToListAsync();

        ViewBag.Search = search;
        ViewBag.StatusFilter = status;
        ViewBag.PriorityFilter = priority;

        return View(tickets);
    }

    // GET: /Tickets/Detail/5
    public async Task<IActionResult> Detail(int id)
    {
        var ticket = await _db.Tickets
            .Include(t => t.AssignedTo)
            .Include(t => t.Comments.OrderByDescending(c => c.CreatedAt))
            .FirstOrDefaultAsync(t => t.Id == id);

        if (ticket == null)
            return NotFound();

        return View(ticket);
    }

    // GET: /Tickets/Create
    public async Task<IActionResult> Create()
    {
        ViewBag.UserList = new SelectList(await _db.Users.ToListAsync(), "Id", "FullName");
        return View();
    }

    // POST: /Tickets/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Title,Description,Priority,AssignedToId")] Ticket ticket)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.UserList = new SelectList(await _db.Users.ToListAsync(), "Id", "FullName");
            return View(ticket);
        }

        ticket.Status = TicketStatus.Open;
        ticket.CreatedAt = DateTime.UtcNow;
        ticket.UpdatedAt = DateTime.UtcNow;

        _db.Tickets.Add(ticket);
        await _db.SaveChangesAsync();

        TempData["Success"] = $"Ticket \"{ticket.Title}\" created successfully.";
        return RedirectToAction(nameof(Index));
    }

    // GET: /Tickets/Edit/5
    public async Task<IActionResult> Edit(int id)
    {
        var ticket = await _db.Tickets.FindAsync(id);
        if (ticket == null)
            return NotFound();

        ViewBag.UserList = new SelectList(await _db.Users.ToListAsync(), "Id", "FullName", ticket.AssignedToId);
        ViewBag.StatusList = new SelectList(Enum.GetValues<TicketStatus>());
        ViewBag.PriorityList = new SelectList(Enum.GetValues<TicketPriority>());

        return View(ticket);
    }

    // POST: /Tickets/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,Status,Priority,AssignedToId")] Ticket ticket)
    {
        if (id != ticket.Id)
            return NotFound();

        if (!ModelState.IsValid)
        {
            ViewBag.UserList = new SelectList(await _db.Users.ToListAsync(), "Id", "FullName", ticket.AssignedToId);
            ViewBag.StatusList = new SelectList(Enum.GetValues<TicketStatus>());
            ViewBag.PriorityList = new SelectList(Enum.GetValues<TicketPriority>());
            return View(ticket);
        }

        var existing = await _db.Tickets.FindAsync(id);
        if (existing == null)
            return NotFound();

        existing.Title = ticket.Title;
        existing.Description = ticket.Description;
        existing.Status = ticket.Status;
        existing.Priority = ticket.Priority;
        existing.AssignedToId = ticket.AssignedToId;
        existing.UpdatedAt = DateTime.UtcNow;

        await _db.SaveChangesAsync();

        TempData["Success"] = $"Ticket \"{ticket.Title}\" updated successfully.";
        return RedirectToAction(nameof(Index));
    }

    // POST: /Tickets/AddComment
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddComment(int ticketId, string content, string authorName)
    {
        var ticket = await _db.Tickets.FindAsync(ticketId);
        if (ticket == null)
            return NotFound();

        if (string.IsNullOrWhiteSpace(content) || string.IsNullOrWhiteSpace(authorName))
        {
            TempData["Error"] = "Comment and name are required.";
            return RedirectToAction(nameof(Detail), new { id = ticketId });
        }

        var comment = new Comment
        {
            Content = content.Trim(),
            AuthorName = authorName.Trim(),
            TicketId = ticketId,
            CreatedAt = DateTime.UtcNow
        };

        ticket.UpdatedAt = DateTime.UtcNow;
        _db.Comments.Add(comment);
        await _db.SaveChangesAsync();

        TempData["Success"] = "Comment posted.";
        return RedirectToAction(nameof(Detail), new { id = ticketId });
    }
}
