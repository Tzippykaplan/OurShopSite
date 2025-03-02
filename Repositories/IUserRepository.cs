using Entities;

namespace Repositories
{
    public interface IUserRepository
    {
        Task<User> addUser(User user);
        Task<User> getUserById(int id);
        Task<User> loginUser(string email, string password);
        Task<User> updateUser(int id, User userToUpdate);
    }
}