﻿using Web3MusicStore.API.Models;

namespace Web3MusicStore.API.Data.Repositories;

public interface IAlbumRepository
{
    Task<IEnumerable<Album>?> GetPagesAsync(
        int pageNumber = 1,
        int? userId = null);
    Task<IEnumerable<Album>?> GetRandomPagesAsync(
        int pageNumber = 1,
        Guid? guid = null
        );
    Task<Album?> FindById(int albumId = 0);
    Task InsertAsync(Album album);
    void Update(Album album);
    void Remove(Album album);
    void RemoveByIdAsync(int albumId);
    void RemoveRange(IEnumerable<Album> albums);
}