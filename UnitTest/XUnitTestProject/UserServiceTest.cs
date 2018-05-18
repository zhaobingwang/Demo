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
    public class UserServiceTest
    {
        private readonly Mock<UserContext> _userContentMock;
        private readonly Mock<IUserRepository> _userRepositoryMock;

        public UserServiceTest()
        {
            _userContentMock = new Mock<UserContext>();
            _userRepositoryMock = new Mock<IUserRepository>();
        }

        [Fact(DisplayName = "新增一个用户-正常参数测试")]
        public async Task UserService_Add_WithExpectedParameters()
        {
            // Arrange
            User user = GetFakeUser(2);
            _userRepositoryMock.Setup(a => a.AddAsync(It.IsAny<User>()))
                .Returns(Task.FromResult(true));

            // Act
            var userService = new UserService(_userContentMock.Object, _userRepositoryMock.Object);
            bool success = await userService.AddUserAsync(user);

            // Assert
            Assert.True(success);
        }
        [Fact(DisplayName = "新增一个用户-异常参数测试")]
        public async Task UserService_Add_WithUnExpectedParameters()
        {
            // Arrange
            User user = GetFakeUser(1);
            _userRepositoryMock.Setup(a => a.AddAsync(It.IsAny<User>()))
                .Returns(Task.FromResult(true));

            // Act
            var userService = new UserService(_userContentMock.Object, _userRepositoryMock.Object);
            bool success = await userService.AddUserAsync(user);

            // Assert
            Assert.False(success);
        }
        private User GetFakeUser(byte status)
        {
            return new User
            {
                NickName = "Jack",
                CreateTime = DateTime.Now,
                ModifyTime = DateTime.Now,
                UserStatus = status,
                IsDelete = false
            };
        }
    }
}
