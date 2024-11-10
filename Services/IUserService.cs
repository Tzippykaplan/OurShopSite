using Entities;

namespace Services
{
    public interface IUserService
    {
        User AddUser(User user);
        int checkPasswordStrength(string password);
        User getUserById(int id);
        User loginUser(string email, string password);
        User uppdateUser(int id, User userToUpdate);
    }
}