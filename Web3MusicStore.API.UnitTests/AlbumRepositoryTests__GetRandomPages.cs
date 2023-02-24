using MockQueryable.Moq;
using Web3MusicStore.API.Data.Repositories;
using Web3MusicStore.API.Models;

namespace Web3MusicStore.API.UnitTests;

public partial class AlbumRepositoryTests
{
    [Test]
    public async Task GetRandomPagesAsync_Returns_Random_Lists()
    {
        // Arrange
        var testSet = new List<Album>();
        var random = new Random(Guid.NewGuid().GetHashCode());

        for (var i = 0; i < 150; i++)
        {
            var album = new Album
            {
                Id = i,
                UserId = random.Next(0, 100)
            };
            testSet.Add(album);
        }

        var dbSet = testSet.AsQueryable().BuildMockDbSet().Object;
        
        _mockDbContext
            .Setup(context => context.Albums)
            .Returns(dbSet);
        _repository = new AlbumRepository(_mockDbContext.Object);

        // Act
        var results1 = await _repository.GetRandomPagesAsync();
        var results2 = await _repository.GetRandomPagesAsync();
        
        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(results1, Has.Count.EqualTo(100));
            Assert.That(results2, Has.Count.EqualTo(100));
        });

        CollectionAssert.AreNotEquivalent(results1, results2);
    }
    
    
    [Test]
    public async Task GetRandomPagesAsync_All_Elements_Returned_Are_Unique()
    {
        var testSet = new List<Album>();
        var random = new Random(Guid.NewGuid().GetHashCode());

        for (var i = 0; i < 150; i++)
        {
            var album = new Album
            {
                Id = i,
                UserId = random.Next(0, 100)
            };
            testSet.Add(album);
        }
        
        // Arrange
        var dbSet = testSet.AsQueryable().BuildMockDbSet().Object;
        
        _mockDbContext
            .Setup(context => context.Albums)
            .Returns(dbSet);
        _repository = new AlbumRepository(_mockDbContext.Object);

        // Act
        var results1 = await _repository.GetRandomPagesAsync();
        var results2 = await _repository.GetRandomPagesAsync();
        
        // Assert
        CollectionAssert.AllItemsAreUnique(results1);
        CollectionAssert.AllItemsAreUnique(results2);
    }
}