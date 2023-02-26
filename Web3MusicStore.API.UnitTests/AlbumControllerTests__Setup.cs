using Moq;
using Web3MusicStore.API.Controllers;
using Web3MusicStore.API.Data.Repositories;
using Web3MusicStore.API.Data.UnitOfWork;
using Web3MusicStore.API.Models;

namespace Web3MusicStore.API.UnitTests;

[TestFixture]
public partial class AlbumControllerTests
{
    private Mock<StoreRepository> _repository = default!;
    private Mock<IUnitOfWork> _unitOfWork = default!;
    private AlbumController _controller = default!;

    private readonly List<Album> _testSet1 = new()
    {
        new() { Id = 1, UserId = 1 }, new() { Id = 2, UserId = 1 }, new() { Id = 3, UserId = 2 },
        new() { Id = 4, UserId = 1 }, new() { Id = 5, UserId = 1 },
        new() { Id = 6, UserId = 3 }, new() { Id = 7, UserId = 4 }, new() { Id = 8, UserId = 2 },
        new() { Id = 9, UserId = 5 }, new() { Id = 10, UserId = 6 },
        new() { Id = 11, UserId = 1 }, new() { Id = 12, UserId = 7 }, new() { Id = 13, UserId = 8 },
        new() { Id = 14, UserId = 1 }, new() { Id = 15, UserId = 2 },
        new() { Id = 16, UserId = 3 }, new() { Id = 17, UserId = 9 }, new() { Id = 18, UserId = 10 },
        new() { Id = 19, UserId = 6 }, new() { Id = 20, UserId = 11 }
    };

    [SetUp]
    public void Setup()
    {
        _repository = new Mock<StoreRepository>();
        _unitOfWork = new Mock<IUnitOfWork>();
    }
}