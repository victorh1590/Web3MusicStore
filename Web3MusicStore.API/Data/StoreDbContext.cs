using Microsoft.EntityFrameworkCore;
using Web3MusicStore.API.Models;

namespace Web3MusicStore.API.Data;

public class StoreDbContext : DbContext
{
  public StoreDbContext(DbContextOptions<StoreDbContext> options)
    : base(options)
  {
  }
  
  public DbSet<Album> Albums { get; set; } = default!;
  public DbSet<Artist> Artists { get; set; } = default!;
  public DbSet<Song> Songs { get; set; } = default!;

  // protected override void OnModelCreating(ModelBuilder modelBuilder)
  // {
  //
  // }
}