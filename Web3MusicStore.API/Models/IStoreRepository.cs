namespace Web3MusicStore.API.Models;

public interface IStoreRepository
{
  IQueryable<Album> Albums { get; }
  IQueryable<Artist> Artists { get; }
  IQueryable<Song> Songs { get; }
}