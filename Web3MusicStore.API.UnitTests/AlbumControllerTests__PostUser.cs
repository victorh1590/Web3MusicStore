using Microsoft.AspNetCore.Mvc;
using Web3MusicStore.API.Controllers;
using Web3MusicStore.API.Models;

namespace Web3MusicStore.API.UnitTests;

public partial class AlbumControllerTests
{
    [Test]
    public async Task PostUser_Adds_New_User_To_Database_Then_Deletes_It_Successfully()
    {
        //Arrange
        var user = new User { Name = "Test" };
        _repository.Setup(repo => repo);
        _controller = new AlbumController(_repository.Object, _unitOfWork.Object);
        
        //Act
        var result = await _controller.PostUser(user);
        
        //Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.TypeOf<OkObjectResult>());

        var okResult = result as OkObjectResult;
        var userAdded = okResult.Value as User;
        
        Assert.That(userAdded, Is.Not.Null);
        Assert.That(userAdded?.Name, Is.EqualTo(user.Name));
        if (userAdded != null)
        {
            var objectSaved = await _repository.Object.FindUserById(userAdded.Id);
            Assert.That(objectSaved, Is.EqualTo(userAdded));
        }

        var deletionResult = _controller.DeleteUser(userAdded);
        
        Assert.That(deletionResult, Is.Not.Null);
        Assert.That(deletionResult, Is.TypeOf<NoContentResult>());

        Assert.That(userDeleted, Is.Not.Null);
        if (userAdded != null)
        {
            var objectDeleted = await _repository.Object.FindUserById(userAdded.Id);
            Assert.That(objectDeleted, Is.Null);
        }
    }
}