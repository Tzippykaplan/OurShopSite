using Entities;
using Microsoft.EntityFrameworkCore;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestsProject
{
    public class UserRepositoryIntegrationTest
    {
        private readonly DBFixture _DBFixture;

        public UserRepositoryIntegrationTest()
        {
            _DBFixture = new DBFixture();
     
        }

        //[Fact]
        //public async Task CreateUser_Should_Add_User_To_Database()
        //{
        //    // Arrange
       
        //    var repository = new UserRepository(_DBFixture.Context);

        //    var user = new User { FirstName = "aa", LastName = "bb", Email = "Tz@123cvv" ,Password="Rzfdsxf!@2"};
        //    var DbUser = await repository.addUser(user);

        //    // Assert
        //    Assert.NotNull(DbUser);
        //    Assert.NotEqual(0, DbUser.UserId);
        //    Assert.Equal("Tz@123cvv", DbUser.Email);
        //    _DBFixture.Dispose();
        //}
        //[Fact]
        //public async Task CreateUser_And_LoginUser_Should_Work_Correctly()
        //{
        //    // Arrange

        //    var repository = new UserRepository(_DBFixture.Context);

        //    var user = new User { FirstName = "aa", LastName = "bb", Email = "Tz@123cvv", Password = "Rzfdsxf!@2" };
        //    var DbUser = await repository.addUser(user);

        //    var loggedInUser = await repository.loginUser("Tz@123cvv", "Rzfdsxf!@2");

      
        //    Assert.NotNull(loggedInUser);
        //    Assert.Equal("Tz@123cvv", loggedInUser.Email);
        //    Assert.Equal(DbUser.UserId, loggedInUser.UserId);

        //    _DBFixture.Dispose();
        //}
        //[Fact]
        //public async Task Update_Should_Work_Correctly()
        //{
        //    // Arrange
        //    var repository = new UserRepository(_DBFixture.Context);

        //    var user = new User
        //    {
        //        FirstName = "aab",
        //        LastName = "bb",
        //        Email = "Tz@123cvv",
        //        Password = "Rzfdsxf!@2"
        //    };

        //    var DbUser = await repository.addUser(user);

        //    var updatedUser = new User
        //    {
        //        FirstName = "newFirstName",
        //        LastName = "newLastName",
        //        Email = "Tz@123cvv",
        //        Password = "newPassword123!"
        //    };
        //   var currentuser= await repository.getUserById(DbUser.UserId);

           
        //    Assert.NotNull(retrievedUser);
        //    Assert.Equal(updatedUser.FirstName, retrievedUser.FirstName);
        //    Assert.Equal(updatedUser.LastName, retrievedUser.LastName);
        //    Assert.Equal(updatedUser.Password, retrievedUser.Password);

        //    _DBFixture.Dispose();
        }


    }
