using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Moq;
using NNice.Business.DTO;
using NNice.Business.Services;
using NNice.Business.Test.Helpers;
using NNice.Common.Models;
using NNice.DAL.Repositories;
using Xunit;

namespace NNice.Business.Test
{
    public class RoomServiceTest
    {
        private Mock<IRepository> _mockRepo;
        private IRoomService _roomService;
        private IMapper _mapper;

        public RoomServiceTest()
        {
            var myProfile = new HelperMapper();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
            _mapper = new Mapper(configuration);
            _mockRepo = new Mock<IRepository>();
            _roomService = new RoomService(_mockRepo.Object, _mapper);
        }
        //[Fact]
        //public async Task Test_CreateAsync()
        //{
        //    var roomDto = new RoomDTO()
        //    {
        //        ID = 1,
        //        Capacity = 12,
        //        IsAvailable = true,
        //        Name = "Room 1"
        //    };

        //    var roomModel = _mapper.Map<RoomModel>(roomDto);
        //    //Arrange

        //    _mockRepo.Setup(r => r.AddAsync<RoomModel>(It.IsAny<RoomModel>()));
        //    _mockRepo.Setup(r => r.GetByIdAsync<RoomModel>(It.IsAny<object>())).ReturnsAsync(()=> new RoomModel{
        //        ID = 1,
        //        Capacity = 12,
        //        IsAvailable = true,
        //        Name = "Room 1"
        //    });


        //    //Act
        //    await _roomService.CreateAsync(roomDto);
        //    var result = await _roomService.GetByIdAsync(1);
        //    //Assert
        //    Assert.Equal(1, result.ID);
        //}

        [Fact]
        public async Task Test_CreateAsync()
        {
            var roomDto = new RoomDTO()
            {
                ID = 1,
                Capacity = 12,
                IsAvailable = true,
                Name = "Room 1"
            };

            var roomModel = _mapper.Map<RoomModel>(roomDto);
            //Arrange

            //_mockRepo.Setup(r => r.AddAsync<RoomModel>(It.Is<RoomModel>(x => AreEqual(x, new RoomModel
            //{
            //    ID = 1,
            //    Capacity = 12,
            //    IsAvailable = true,
            //    Name = "Room 1"
            //}))));

            //_mockRepo.Setup(r => r.SaveAsync());
            //_mockRepo.Setup(r => r.GetByIdAsync<RoomModel>(It.IsAny<object>())).ReturnsAsync(() => new RoomModel
            //{
            //    ID = 1,
            //    Capacity = 12,
            //    IsAvailable = true,
            //    Name = "Room 1"
            //});


            //Act
            await _roomService.CreateAsync(roomDto);
            //var result = await _roomService.GetByIdAsync(1);
            //Assert
            // Assert.Equal(1, result.ID);
            //_mockRepo.VerifyAll();
        }

        [Fact]
        public async Task Test_UpdateAsync()
        {
            var roomDto = new RoomDTO()
            {
                ID = 1,
                Capacity = 12,
                IsAvailable = true,
                Name = "Room 1"
            };

            var roomModel = _mapper.Map<RoomModel>(roomDto);
            //Arrange

            _mockRepo.Setup(r => r.Update<RoomModel>(It.IsAny<RoomModel>()));
            _mockRepo.Setup(r => r.GetByIdAsync<RoomModel>(It.IsAny<object>())).ReturnsAsync(() =>
            {
                return new RoomModel()
                {
                    ID = 1,
                    Capacity = 15,
                    IsAvailable = true,
                    Name = "Room 1"
                };
            });


            //Act
            await _roomService.UpdateAsync(roomDto);
            var result = await _roomService.GetByIdAsync(1);

            //Assert
            Assert.Equal(roomDto,result);
        }

        [Fact]
        public async Task Test_Delete()
        {
            var roomDto = new RoomDTO()
            {
                ID = 1,
                Capacity = 12,
                IsAvailable = true,
                Name = "Room 1"
            };

            var roomModel = _mapper.Map<RoomModel>(roomDto);
            //Arrange

            _mockRepo.Setup(r => r.Delete<RoomModel>(It.IsAny<RoomModel>()));
            _mockRepo.Setup(r => r.GetByIdAsync<RoomModel>(It.IsAny<object>())).ReturnsAsync(() =>
            {
                return new RoomModel()
                {
                    ID = 1,
                    Capacity = 15,
                    IsAvailable = true,
                    Name = "Room 1"
                };
            });


            //Act
            await _roomService.DeleteAsync(roomDto);
            var roomDTO = await _roomService.GetByIdAsync(1);

            //Assert
            Assert.NotNull(roomDTO);
        }

        private bool AreEqual(RoomModel actual,  RoomModel expected)
        {
            return actual.ID == expected.ID &&
                actual.Name == expected.Name;
        }
    }
}
