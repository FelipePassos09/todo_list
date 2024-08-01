using System.Reflection;
using Microsoft.EntityFrameworkCore;
using todo_list.Models;

namespace todo_list.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<NoteViewModel> Notes { get; set; }

}