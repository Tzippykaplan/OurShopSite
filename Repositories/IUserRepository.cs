using Entities;

namespace Repositories
{
    public interface IUserRepository
    {
        User addUser(User user);
        User getUserById(int id);
        User loginUser(string email, string password);
        User uppdateUser(int id, User userToUpdate);
    }
}