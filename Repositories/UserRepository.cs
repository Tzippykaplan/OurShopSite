using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;




namespace Repositories
{
    public class UserRepository : IUserRepository
    {
        ShopContext _shopContext;
        public UserRepository(ShopContext shopContext)
        {
            _shopContext = shopContext;
        }
        const string filePath = "D:\\הנדסאים\\web api\\project\\Repositories\\Data.txt";
        public async Task<User> getUserById(int id)
        {
            User? user = await _shopContext.Users.FirstOrDefaultAsync(e => e.Id == id);
            return user;


        }
        public async Task<User> addUser(User user)
        {
            await _shopContext.Users.AddAsync(user);
            await _shopContext.SaveChangesAsync();
            return (user);
        }
        public  User loginUser(string email, string password)
        {
            var res = _shopContext.Users;
            var result=  _shopContext.Users.First(user => user.Email == email && user.Password == password);
            return result;

        }
        public async Task<User> uppdateUser(int id, User newUser)
        {
            var userToUpdate = await _shopContext.Users.FindAsync(id);
            if (userToUpdate == null)
            {
                return null;
            }
            newUser.Id = userToUpdate.Id;
            _shopContext.Entry(userToUpdate).CurrentValues.SetValues(newUser);
            await _shopContext.SaveChangesAsync();
            return (newUser);

        }

    }
}
