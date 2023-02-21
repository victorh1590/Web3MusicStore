using Microsoft.EntityFrameworkCore;
using Web3MusicStore.API.Models;

namespace Web3MusicStore.API.Data;

public interface IStoreDbContext
{
    DbSet<Album> Albums { get; set; }
    DbSet<User> User { get; set; }
    DbSet<Song> Songs { get; set; }
}