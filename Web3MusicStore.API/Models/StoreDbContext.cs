using Microsoft.EntityFrameworkCore;

namespace Web3MusicStore.API.Models;

public class StoreDbContext : DbContext
{
  public StoreDbContext(DbContextOptions<StoreDbContext> options)
    : base(options)
  {
  }
  
  public DbSet<Album> Albums { get; set; } = default!;
  public DbSet<Artist> Artists { get; set; } = default!;
  public DbSet<Song> Songs { get; set; } = default!;

  //  protected override void OnModelCreating(DbModelBuilder modelBuilder)
  //   {
  //       modelBuilder.Entity<Song>()
  //           .Property(song => song.Duration.ToString())
  //           .HasColumnType("TIME");
  //   }
}