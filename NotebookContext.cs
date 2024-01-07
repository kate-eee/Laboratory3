using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;
namespace ConsoleApp1;

public class NotebookContext : DbContext
{
    public DbSet<NotebookEntry> Archive { get; set; }

    private string _dbPath;

    public NotebookContext(string path)
    {
        _dbPath = path;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite($"Data Source={_dbPath}");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<NotebookEntry>()
            .HasKey(e => e.Id);
    }
}