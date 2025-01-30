namespace TestsProject;

using Entities;
using Moq;
using Moq.EntityFrameworkCore;
using Repositories;


public class UserReposetoryUnitTesting
{
    [Fact]
    public async void Login_ValidCredentialsREturnsUser()
    {
        var user = new User { FirstName = "aa", LastName = "bb", Email = "Tz@123cvv" };
        var users = new List<User>() { user };   
        var mockContext=new Mock<ShopApiContext>();
        mockContext.Setup(x => x.Users).ReturnsDbSet(users);
        var userRepository =new UserRepository(mockContext.Object);
        var result = await userRepository.loginUser(user.Email, user.Password);
        Assert.Equal(user,result);
    }
}