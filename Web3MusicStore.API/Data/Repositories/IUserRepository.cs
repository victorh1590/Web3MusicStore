using Web3MusicStore.API.Models;

namespace Web3MusicStore.API.Data.Repositories;

public interface IUserRepository
{
    Task<User?> FindUserById(int userId = 0);
    Task InsertUserAsync(User user);
    void UpdateUser(User user);
    void RemoveUser(User user);
    void RemoveUserRange(IEnumerable<User> users);
}