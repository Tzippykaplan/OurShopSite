﻿using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;




namespace Repositories
{
    public class UserRepository : IUserRepository
    {
        ShopApiContext _shopContext;
        public UserRepository(ShopApiContext shopContext)
        {
            _shopContext = shopContext;
        }

        public async Task<User> getUserById(int id)
        {
            User user = await _shopContext.Users.Include(u=>u.Orders).FirstOrDefaultAsync(e => e.UserId == id);
            return user;


        }
        public async Task<User> addUser(User user)
        {
            bool b = await _shopContext.Users.AnyAsync(currentUser => currentUser.Email == user.Email);
            if (b)
            {
                return null;
            }
            await _shopContext.Users.AddAsync(user);
            await _shopContext.SaveChangesAsync();
            return (user);
        }
        public async Task<User> loginUser(string email, string password)
        {
            return await _shopContext.Users.FirstOrDefaultAsync(user => user.Email == email && user.Password == password);
             
        }
        public async Task updateUser(int id, User newUser)
        {
            newUser.UserId = id;
            _shopContext.Update(newUser);
            await _shopContext.SaveChangesAsync();


        }

    }
}
