using Web3MusicStore.API.Data.Repositories;
using Web3MusicStore.API.Models;
using Moq;
using Web3MusicStore.API.Data;

namespace Web3MusicStore.API.UnitTests;

[TestFixture]
public partial class AlbumRepositoryTests
{
    private IAlbumRepository _repository = default!;
    private Mock<IStoreDbContext> _mockDbContext = default!;
    
    private readonly List<Album> _testSet1 = new()
    {
        new() { Id = 1, UserId = 1 }, new() { Id = 2, UserId = 1 }, new() { Id = 3, UserId = 2 },
        new() { Id = 4, UserId = 1 }, new() { Id = 5, UserId = 1 },
        new() { Id = 6, UserId = 3 }, new() { Id = 7, UserId = 4 }, new() { Id = 8, UserId = 2 },
        new() { Id = 9, UserId = 5 }, new() { Id = 10, UserId = 6 },
        new() { Id = 11, UserId = 1 }, new() { Id = 12, UserId = 7 }, new() { Id = 13, UserId = 8 },
        new() { Id = 14, UserId = 1 }, new() { Id = 15, UserId = 2 },
        new() { Id = 16, UserId = 3 }, new() { Id = 17, UserId = 9 }, new() { Id = 18, UserId = 10 },
        new() { Id = 19, UserId = 6 }, new() { Id = 20, UserId = 11 },
        new() { Id = 21, UserId = 12 }, new() { Id = 22, UserId = 13 }, new() { Id = 23, UserId = 14 },
        new() { Id = 24, UserId = 4 }, new() { Id = 25, UserId = 5 }
    };

    [SetUp]
    public void Setup()
    {
        _mockDbContext = new Mock<IStoreDbContext>();
    }
}