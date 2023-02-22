using MockQueryable.Moq;
using Web3MusicStore.API.Data.Repositories;
using Web3MusicStore.API.Models;

namespace Web3MusicStore.API.UnitTests;

public partial class AlbumRepositoryTests
{
    [Test, Sequential]
    public async Task GetPagesAsync_Returns_Expected_Albums_For_Page_And_User(
        [Values(1, 2, 3, 4, 5, 6, 7)] int userId,
        [Values(1, 1, 1, 1, 1, 1, 2)] int pageNumber,
        [Values(new[] { 1, 2, 4, 5, 11, 14 },
                new[] { 3, 8, 15 },
                new[] { 6, 16 },
                new[] { 7, 24 },
                new[] { 9, 25 },
                new[] { 10, 19 },
                new int[] { }
            )
        ]
        int[] expectedAlbumIds)
    {
        // Arrange
        _mockDbContext
            .Setup(context => context.Albums)
            .Returns(_testSet1.AsQueryable().BuildMockDbSet().Object);
        _repository = new AlbumRepository(_mockDbContext.Object);

        // Act
        var result = await _repository.GetPagesAsync(pageNumber, userId);

        // Assert
        Assert.That(result, Has.Count.EqualTo(expectedAlbumIds.Length));
        CollectionAssert.AreEquivalent(result.Select(a => a.Id).ToArray(), expectedAlbumIds);
    }

    [Test, Sequential]
    public async Task GetPagesAsync_Returns_Expected_Album_Pages_When_UserId_Not_Specified(
        [Values(1, 2, 3, 999, 'a')] int pageNumber)
    {
        var albums = new List<Album>(
            Enumerable
                .Range(1, 25)
                .Select(n => new Album { Id = n, UserId = n })
        );

        var expectedAlbums = pageNumber switch
        {
            1 => albums.Take(20).ToList(),
            2 => albums.Skip(20).ToList(),
            _ => new List<Album>()
        };

        // Arrange
        _mockDbContext
            .Setup(context => context.Albums)
            .Returns(albums.AsQueryable().BuildMockDbSet().Object);
        _repository = new AlbumRepository(_mockDbContext.Object);

        // Act
        var result = await _repository.GetPagesAsync(pageNumber);
        
        // Assert
        Assert.That(result, Has.Count.EqualTo(expectedAlbums.Count));
        CollectionAssert.AreEquivalent(result.Select(a => a).ToList(), expectedAlbums);
    }

    [Test, Sequential]
    public void GetPagesAsync_Throws_Exception_When_PageNumber_Invalid(
        [Values(0, -1, 'a' - 'z')] int pageNumber)
    {
        var albums = new List<Album>(
            Enumerable
                .Range(1, 25)
                .Select(n => new Album { Id = n, UserId = n })
        );

        // Arrange
        _mockDbContext
            .Setup(context => context.Albums)
            .Returns(albums.AsQueryable().BuildMockDbSet().Object);
        _repository = new AlbumRepository(_mockDbContext.Object);
        
        // Assert
        Assert.That(async Task<IReadOnlyCollection<Album>>()
                => await _repository.GetPagesAsync(pageNumber),
            Throws.Exception.TypeOf<ArgumentOutOfRangeException>());
    }
}