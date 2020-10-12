using AutoMapper;
using Moq;
using NNice.Business.DTO;
using NNice.Business.Services;
using NNice.Business.Test.Helpers;
using NNice.Common.Models;
using NNice.DAL.Repositories;
using System;
using System.Threading.Tasks;
using Xunit;

namespace NNice.Business.Test
{
    public class OrderServiceTest
    {
        private Mock<IRepository> _mockRepo;
        private IOrderService _orderService;
        private IMapper _mapper;

        public OrderServiceTest()
        {
            var myProfile = new HelperMapper();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
            _mapper = new Mapper(configuration);
            _mockRepo = new Mock<IRepository>();
            _orderService = new OrderService(_mockRepo.Object, _mapper);
        }

        [Fact]
        public async Task Test_BookRoom()
        {
            //Arrange
            var orderDTO = new OrderDTO
            {
                RoomID = 1,
                UserID = 1,
                StartTime = new DateTime(2020, 1, 1, 8, 0, 0),
                EndTime = new DateTime(2020, 1, 1, 10, 0, 0),
            };

            _mockRepo.Setup(x => x.GetByIdAsync<RoomModel>(It.IsAny<int>())).ReturnsAsync((int id) =>
                new RoomModel
                {
                    ID = id,
                    IsAvailable = true
                }
            );
            _mockRepo.Setup(x => x.Update(It.IsAny<RoomModel>())).Verifiable();
            _mockRepo.Setup(x => x.AddAsync(It.IsAny<OrderModel>())).Verifiable();
            _mockRepo.Setup(x => x.SaveAsync()).ReturnsAsync(2);

            //Act
            await _orderService.BookRoomAsync(orderDTO);

            //Assert
            _mockRepo.Verify();
        }

        [Fact]
        public async Task Test_BookParty()
        {
            //Arrange
            var orderDTO = new OrderDTO
            {
                RoomID = 1,
                UserID = 1,
                StartTime = new DateTime(2020, 1, 1, 8, 0, 0),
                EndTime = new DateTime(2020, 1, 1, 10, 0, 0)
            };

            var partyDTO = new PartyDTO
            {
                Name = "partyname"
            };

            _mockRepo.Setup(x => x.AddAsync(It.IsAny<PartyModel>())).Verifiable();
            _mockRepo.Setup(x => x.GetByIdAsync<RoomModel>(It.IsAny<int>())).ReturnsAsync((int id) =>
                new RoomModel
                {
                    ID = id,
                    IsAvailable = true
                }
            );
            _mockRepo.Setup(x => x.Update(It.IsAny<RoomModel>())).Verifiable();
            _mockRepo.Setup(x => x.AddAsync(It.IsAny<OrderModel>())).Verifiable();
            _mockRepo.Setup(x => x.SaveAsync()).ReturnsAsync(2);

            //Act
            await _orderService.BookPartyAsync(orderDTO, partyDTO);

            //Assert
            _mockRepo.Verify();
        }

        [Fact]
        public async Task Test_Delete()
        {
            //Arrange
            _mockRepo.Setup(x => x.GetByIdAsync<OrderModel>(It.IsAny<int>())).ReturnsAsync(
                new OrderModel 
                {
                    ID = 1,
                    PartyID = 1,
                    RoomID = 1,
                    UserID = 1 
                }
            );

            _mockRepo.Setup(x => x.GetByIdAsync<PartyModel>(1)).ReturnsAsync(
                new PartyModel
                {
                    ID = 1,
                    OrderID = 1
                }
            ).Verifiable();

            _mockRepo.Setup(x => x.Delete(It.Is<PartyModel>(p => p.ID == 1))).Verifiable();
            _mockRepo.Setup(x => x.GetByIdAsync<RoomModel>(1)).ReturnsAsync(
                new RoomModel
                {
                    ID = 1,
                    IsAvailable = false,
                }
            );

            _mockRepo.Setup(x => x.Update(It.IsAny<RoomModel>())).Verifiable();
            _mockRepo.Setup(x => x.Delete(It.IsAny<OrderModel>())).Verifiable();

            _mockRepo.Setup(x => x.SaveAsync()).ReturnsAsync(3);

            //Act
            await _orderService.DeleteAsync(1);

            //Assert
            _mockRepo.Verify();
        }

        [Fact]
        public async Task Test_Update()
        {

            //Arrange
            var orderDto = new OrderDTO
            {
                ID = 1,
                PartyID = 2,
                RoomID = 2
            };

            var partyDTO = new PartyDTO
            {
                Name = "partyname"
            };

            _mockRepo.Setup(x => x.GetByIdAsync<OrderModel>(orderDto.ID)).ReturnsAsync(
                new OrderModel
                {
                    ID = 1,
                    PartyID = 1,
                    RoomID = 1,
                }
            );

            _mockRepo.Setup(x => x.GetByIdAsync<PartyModel>(1)).Verifiable();
            _mockRepo.Setup(x => x.Delete(It.IsAny<PartyModel>())).Verifiable();
            _mockRepo.Setup(x => x.AddAsync(It.IsAny<PartyModel>())).Verifiable();

            _mockRepo.Setup(x => x.GetByIdAsync<RoomModel>(2)).ReturnsAsync(
                new RoomModel
                {
                    ID = 2,
                    IsAvailable = true,
                }
            ).Verifiable();
            _mockRepo.Setup(x => x.GetByIdAsync<RoomModel>(1)).ReturnsAsync(
                new RoomModel
                {
                    ID = 1,
                    IsAvailable = false,
                }
            ).Verifiable();

            _mockRepo.Setup(x => x.Update(It.Is<RoomModel>(r => r.ID == 1))).Verifiable();
            _mockRepo.Setup(x => x.Update(It.Is<RoomModel>(r => r.ID == 2))).Verifiable();

            _mockRepo.Setup(x => x.Update(It.IsAny<OrderModel>()));
            _mockRepo.Setup(x => x.SaveAsync()).ReturnsAsync(5);

            //Act
            await _orderService.UpdateAsync(orderDto, partyDTO);

            //Assert
            _mockRepo.Verify();
        }
    }
}
