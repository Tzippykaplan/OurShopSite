﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Entities;
using Repositories;
using Zxcvbn;

namespace Services
{
    public class UserService : IUserService
    {
        IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public User getUserById(int id)
        {
            return _userRepository.getUserById(id);

        }
        public User AddUser(User user)
        {
            int PasswordStrength = checkPasswordStrength(user.Password);
            if (PasswordStrength > 2)
                return _userRepository.addUser(user);
            else
                throw new Exception(PasswordStrength.ToString());
        }
        public User loginUser(string email, string password)
        {
            return _userRepository.loginUser(email, password);
        }
        public User uppdateUser(int id, User userToUpdate)
        {//check password strength
            return _userRepository.uppdateUser(id, userToUpdate);
        }
        public int checkPasswordStrength(string password)
        {
            return Zxcvbn.Core.EvaluatePassword(password).Score;


        }



    }
}
