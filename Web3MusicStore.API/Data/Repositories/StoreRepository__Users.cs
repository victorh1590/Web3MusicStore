using Web3MusicStore.API.Models;

namespace Web3MusicStore.API.Data.Repositories;

public partial class StoreRepository : IUserRepository
{
    public async Task<User?> FindUserById(int userId)
    {
        return await _context.User.FindAsync(userId);
    }

    public async Task InsertUserAsync(User user)
    {
        await _context.User.AddAsync(user);
    }

    public void UpdateUser(User user)
    {
        _context.User.Update(user);
    }

    //TODO disable this method for production.
    public void RemoveUser(User user)
    {
        _context.User.Remove(user);
    }

    //TODO disable this method for production.
    public void RemoveUserRange(IEnumerable<User> users)
    {
        _context.User.RemoveRange(users);
    }
}