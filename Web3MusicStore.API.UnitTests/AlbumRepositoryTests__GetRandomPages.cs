using MockQueryable.Moq;
using Web3MusicStore.API.Data.Repositories;

namespace Web3MusicStore.API.UnitTests;

public partial class AlbumRepositoryTests
{
    [Test]
    public async Task GetRandomPagesAsync_Always_Returns_Same_Page_Given_Guid()
    {
        const int pageNumber = 2;
        var exampleGuid = new Guid("8cdaa2d0-f7a7-4635-9a44-5a941d9282af");
        Console.WriteLine(exampleGuid);
        Console.WriteLine(exampleGuid.GetHashCode());

        // Arrange
        var dbSet = _testSet1.AsQueryable().BuildMockDbSet().Object;
        
        _mockDbContext
            .Setup(context => context.Albums)
            .Returns(dbSet);
        _repository = new AlbumRepository(_mockDbContext.Object);

        // Act
        var (expectedResults, _) = await _repository.GetRandomPagesAsync(pageNumber, exampleGuid);
        var (result, _) = await _repository.GetRandomPagesAsync(pageNumber, exampleGuid);
        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(expectedResults, Has.Count.EqualTo(5));
            Assert.That(result, Has.Count.EqualTo(expectedResults.Count));
        });
        CollectionAssert.AreEquivalent(result.Select(a => a.Id).ToArray(), 
            expectedResults.Select(a => a.Id).ToArray());
    }

    [Test]
    public async Task GetRandomPagesAsync_Returns_Random_Lists_And_Guids()
    {
        // Arrange
        var dbSet = _testSet1.AsQueryable().BuildMockDbSet().Object;
        
        _mockDbContext
            .Setup(context => context.Albums)
            .Returns(dbSet);
        _repository = new AlbumRepository(_mockDbContext.Object);

        // Act
        var (results1, result1Guid) = await _repository.GetRandomPagesAsync();
        var (results2, result2Guid) = await _repository.GetRandomPagesAsync();
        
        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(results1, Has.Count.EqualTo(20));
            Assert.That(results2, Has.Count.EqualTo(20));
        });
        CollectionAssert.AreNotEquivalent(results1, results2);
        Assert.That(result1Guid, Is.Not.EqualTo(result2Guid));
    }
}