using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using todo_list.Models.Enums;

namespace todo_list.Models;

[Table("notes")]
public class NoteViewModel
{
    [Key] public long Id { get; set; }
    
    [MaxLength(100)]
    [MinLength(5)]
    [Required]
    public string Subject { get; set; } = string.Empty;
    
    [MaxLength(1000)]
    public string Content { get; set; } = string.Empty;
    public bool Done { get; set; } = false;
    
    public Priorities Priorities { get; set; }

    [DataType(DataType.Date)]
    public DateOnly DueDate { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public DateTime UpdatedAt { get; set; } = DateTime.Now;
    
    public NoteViewModel()
    {
    }
}