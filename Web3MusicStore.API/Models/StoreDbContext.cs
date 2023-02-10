using System.Data.Entity;

namespace Web3MusicStore.API.Models;

public class StoreDbContext : DbContext
{
  public DbSet<Album> Albums { get; set; } = default!;
  public DbSet<Artist> Artists { get; set; } = default!;
  public DbSet<Song> Songs { get; set; } = default!;
}