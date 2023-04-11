using Microsoft.EntityFrameworkCore;

namespace Mapel.Models;

public class MapelContext : DbContext
{
    public MapelContext(DbContextOptions<MapelContext> options)
        : base(options)
    {
    }

    public DbSet<MapelItem> MapelItem { get; set; } = null!;
}