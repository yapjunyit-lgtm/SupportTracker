using System.ComponentModel.DataAnnotations;

namespace SupportTracker.Models;

public class User
{
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    [Display(Name = "Full Name")]
    public string FullName { get; set; } = string.Empty;

    [Required]
    [MaxLength(200)]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [MaxLength(100)]
    public string Department { get; set; } = string.Empty;

    // Navigation property
    public List<Ticket> AssignedTickets { get; set; } = new();
}
