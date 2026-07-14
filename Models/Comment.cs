using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SupportTracker.Models;

public class Comment
{
    public int Id { get; set; }

    [Required]
    [MaxLength(5000)]
    public string Content { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    [Display(Name = "Your Name")]
    public string AuthorName { get; set; } = string.Empty;

    public int TicketId { get; set; }

    [ForeignKey(nameof(TicketId))]
    public Ticket Ticket { get; set; } = null!;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
