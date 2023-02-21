using Web3MusicStore.API.Data.Repositories;
using Web3MusicStore.API.Models;
using Moq;
using Web3MusicStore.API.Data;
using MockQueryable.Moq;

namespace Web3MusicStore.API.UnitTests;

[TestFixture]
public partial class AlbumRepositoryTests
{
    private IAlbumRepository _repository = default!;
    private Mock<IStoreDbContext> _mockDbContext = default!;

    [SetUp]
    public void Setup()
    {
        _mockDbContext = new Mock<IStoreDbContext>();
    }
}