using Microsoft.EntityFrameworkCore;

namespace Guru.Models;

public class GuruContext : DbContext
{
    public GuruContext(DbContextOptions<GuruContext> options)
        : base(options) { }

    public DbSet<GuruItem> GuruItem { get; set; } = null!;
}
