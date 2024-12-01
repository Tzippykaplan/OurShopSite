using Entities;

namespace Services
{
    public interface IUserService
    {
        Task<User> AddUser(User user);
        int checkPasswordStrength(string password);
        Task<User> getUserById(int id);
        User loginUser(string email, string password);
        Task<User> uppdateUser(int id, User userToUpdate);
    }
}