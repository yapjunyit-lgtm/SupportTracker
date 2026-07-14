using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SupportTracker.Data;

namespace SupportTracker.Controllers;

public class UsersController : Controller
{
    private readonly AppDbContext _db;

    public UsersController(AppDbContext db)
    {
        _db = db;
    }

    // GET: /Users
    public async Task<IActionResult> Index()
    {
        var users = await _db.Users
            .Include(u => u.AssignedTickets)
            .OrderBy(u => u.FullName)
            .ToListAsync();

        return View(users);
    }
}
