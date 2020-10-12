using AutoMapper;
using Moq;
using NNice.Business.DTO;
using NNice.Business.Services;
using NNice.Business.Test.Helpers;
using NNice.Common.Models;
using NNice.DAL.Repositories;
using System.Threading.Tasks;
using Xunit;

namespace NNice.Business.Test
{
    public class UserServiceTest
    {
        private Mock<IRepository> _mockRepo;
        private IUserService _userService;
        private IMapper _mapper;

        public UserServiceTest()
        {
            var myProfile = new HelperMapper();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
            _mapper = new Mapper(configuration);
            _mockRepo = new Mock<IRepository>();
            _userService = new UserService(_mockRepo.Object, _mapper);
        }
        [Fact]
        public async Task Test_CreateAsync()
        {
            var userDto = new UserDTO()
            {
                ID = 1,
                Name = "Nguyen Van A",
                Address = "TPHCM",
                Avatar = "avalink",
                Email = "email1@example.com",
                Salary = 100,
                AccountID = 1,
            };

            //Arrange
            _mockRepo.Setup(r => r.AddAsync(It.IsAny<UserModel>()));
            _mockRepo.Setup(r => r.SaveAsync()).ReturnsAsync(1);

            //Act
            var success = await _userService.CreateAsync(userDto);

            //Assert
            Assert.True(success);
        }

        [Fact]
        public async Task Test_UpdateAsync()
        {
            var userDto = new UserDTO()
            {
                ID = 1,
                Name = "Nguyen Thi B",
                Address = "TPHCM",
                Avatar = "avalink",
                Email = "email1@example.com",
                Salary = 100,
                AccountID = 1,
            };

            //Arrange
            _mockRepo.Setup(r => r.Update(It.IsAny<UserModel>()));
            _mockRepo.Setup(r => r.SaveAsync()).ReturnsAsync(1);

            //Act
            var success = await _userService.UpdateAsync(userDto);

            //Assert
            Assert.True(success);
        }

        [Fact]
        public async Task Test_DeleteAsync()
        {
            var userDto = new UserDTO()
            {
                ID = 1,
                Name = "Nguyen Thi B",
                Address = "TPHCM",
                Avatar = "avalink",
                Email = "email1@example.com",
                Salary = 100,
                AccountID = 1,
            };

            //Arrange
            _mockRepo.Setup(r => r.Delete(It.IsAny<UserModel>()));
            _mockRepo.Setup(r => r.SaveAsync()).ReturnsAsync(1);
            _mockRepo.Setup(r => r.GetByIdAsync<UserModel>(It.IsAny<object>())).ReturnsAsync(() =>
            {
                return new UserModel()
                {
                    ID = 1,
                    Name = "Nguyen Thi B",
                    Address = "TPHCM",
                    Avatar = "avalink",
                    Email = "email1@example.com",
                    Salary = 100,
                    AccountID = 1,
                };
            });

            //Act
            var success = await _userService.DeleteAsync(userDto);

            //Assert
            Assert.True(success);
        }
    }
}
