using Entities;
using Repositories;
using System.Threading.Tasks;
using TestsProject;
using Xunit;

namespace Test
{
    public class UserRepositoryIntegrationTest : IClassFixture<DBFixture>
    {
        private readonly IUserRepository _userRepository;
        public  ShopApiContext _context;

        public UserRepositoryIntegrationTest(DBFixture fixture)
        {
            _context = fixture.Context;
            _userRepository = new UserRepository(_context);
        }

        [Fact]
        public async Task AddUser_ValidUser_ShouldSaveToDatabase()
        {
            var user = new User { FirstName = "John", LastName = "Doe", Email = "jdddll@exakkm", Password = "Pass123!" };

            var savedUser = await _userRepository.addUser(user);

            Assert.NotNull(savedUser);
            Assert.NotEqual(0, savedUser.UserId);
            Assert.Equal("jdddll@exakkm", savedUser.Email);
            _context.Dispose();
        }

        [Fact]
        public async Task LoginUser_ValidCredentials_ReturnUser()
        {
            var user = new User { FirstName = "Jane", LastName = "Doe", Email = "jane@example", Password = "JanePass!12" };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            var result = await _userRepository.loginUser(user.Email, user.Password);

            Assert.NotNull(result);
            Assert.Equal(user.Email, result.Email);
            _context.Dispose();

        }

        [Fact]
        public async Task LoginUser_InvalidCredentials_ReturnNull()
        {
            var result = await _userRepository.loginUser("invalid@ex", "WrongPass123");
            Assert.Null(result);
            _context.Dispose();

        }

        [Fact]
        public async Task GetUserById_ExistingUser_ReturnsUser()
        {
            var user = new User { FirstName = "Alice", LastName = "Smith", Email = "alice@ex", Password = "Alice1234!" };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            var foundUser = await _userRepository.getUserById(user.UserId);

            Assert.NotNull(foundUser);
            Assert.Equal(user.Email, foundUser.Email);
            _context.Dispose();

        }

        [Fact]
        public async Task GetUserById_NonExistingUser_ReturnsNull()
        {
            var result = await _userRepository.getUserById(9999);
            Assert.Null(result);
            _context.Dispose();

        }

        [Fact]
        public async Task UpdateUser_ValidUser_ShouldUpdateSuccessfully()
        {
            var user = new User { FirstName = "Tom", LastName = "Hanks", Email = "tolllll@ex", Password = "TomPass12!" };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            var updatedUser = new User { FirstName = "Tommy", LastName = "Hanks", Email = "tommy@ex", Password = "NewPass@34" };

            await _userRepository.updateUser(user.UserId, updatedUser);

            var result = await _userRepository.getUserById(user.UserId);

            Assert.NotNull(result);
            Assert.Equal(updatedUser.FirstName, result.FirstName);
            Assert.Equal(updatedUser.Email, result.Email);
            _context.Dispose();

        }

        [Fact]
        public async Task UpdateUser_NonExistingUser_ShouldNotThrowException()
        {
            var nonExistingUser = new User { FirstName = "Ghost", LastName = "User", Email = "ggggghost@ex", Password = "Ghost123!" };

          var result=  await _userRepository.updateUser(90099, nonExistingUser);
            Assert.Equal(result, null);
            _context.Dispose();

        }
    }
}
