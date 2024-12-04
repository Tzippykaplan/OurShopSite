using Entities;

namespace Repositories
{
    public interface IUserRepository
    {
        Task<User> addUser(User user);
        Task<User> getUserById(int id);
        User loginUser(string email, string password);
        Task uppdateUser(int id, User userToUpdate);
    }
}