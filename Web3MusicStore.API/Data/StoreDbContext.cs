#nullable disable

using Microsoft.EntityFrameworkCore;
using Web3MusicStore.API.Models;

namespace Web3MusicStore.API.Data;

public class StoreDbContext : DbContext
{
  public StoreDbContext(DbContextOptions<StoreDbContext> options)
    : base(options)
  {
  }
  
  public DbSet<Album> Albums { get; set; }
  public DbSet<User> User { get; set; }
  public DbSet<Song> Songs { get; set; }

  // protected override void OnModelCreating(ModelBuilder modelBuilder)
  // {
  //
  // }
}