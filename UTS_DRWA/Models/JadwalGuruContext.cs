using Microsoft.EntityFrameworkCore;

namespace JadwalGuru.Models;

public class JadwalGuruContext : DbContext
{
    public JadwalGuruContext(DbContextOptions<JadwalGuruContext> options)
        : base(options)
    {
    }

    public DbSet<JadwalGuruItem> JadwalGuruItem { get; set; } = null!;
}