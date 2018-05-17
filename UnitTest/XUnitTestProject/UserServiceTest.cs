using Data;
using Data.Entities;
using Data.Service;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace XUnitTestProject
{
    [Trait("业务功能测试", "用户模块")]
    public class UserServiceTest : IDisposable
    {
        private IUserService _userService { get; set; }
        public User _user { get; set; }

        private UserContext GetUerContext()
        {
            return new UserContext();
        }

        [Fact(DisplayName = "新增一个用户")]
        public async Task UserService_Add_WithExpectedParameters()
        {
            //UserContext userContext = GetUerContext();
            var mock = new Mock<UserContext>();

            User user = new User
            {
                NickName = "Jack",
                CreateTime = DateTime.Now,
                ModifyTime = DateTime.Now,
                IsDelete = false
            };

            User expectedUser = new User
            {
                NickName = "Jack",
                CreateTime = DateTime.Now,
                ModifyTime = DateTime.Now,
                IsDelete = false
            };

            //mock.Setup(c => c.Entry<User>(user)).Returns(;    //.ReturnsAsync(expectedUser);
            _userService = new UserService(mock.Object);

            User newUser = await _userService.AddUserAsync(user);
            Assert.Equal(expectedUser, newUser);

            //Assert.True(newUser.Id > 0);
        }

        public void Dispose()
        {
            //UserContext userContext = GetUerContext();
            //var entity = userContext.User.FindAsync(_user.Id).Result;
            //userContext.Remove(entity);
            //var success = userContext.SaveChangesAsync().Result > 0;
        }
    }
}
