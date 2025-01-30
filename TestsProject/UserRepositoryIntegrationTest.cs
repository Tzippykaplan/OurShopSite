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

        [Fact]
        public async Task CreateUser_Should_Add_User_To_Database()
        {
            // Arrange
       
            var repository = new UserRepository(_DBFixture.Context);

            var user = new User { FirstName = "aa", LastName = "bb", Email = "Tz@123cvv" ,Password="Rzfdsxf!@2"};
            var DbUser = await repository.addUser(user);

            // Assert
            Assert.NotNull(DbUser);
            Assert.NotEqual(0, DbUser.UserId);
            Assert.Equal("Tz@123cvv", DbUser.Email);
            _DBFixture.Dispose();
        }

    }
}
