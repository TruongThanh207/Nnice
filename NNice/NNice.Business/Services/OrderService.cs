using AutoMapper;
using NNice.Business.DTO;
using NNice.Common.Models;
using NNice.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NNice.Business.Services
{
    public class OrderService : IOrderService
    {
        private readonly IMapper _mapper;
        private readonly IRepository _repository;
        public OrderService(IRepository repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<ResponseObject> BookRoomAsync(OrderDTO order)
        {
            var invoiceModel = _mapper.Map<OrderDTO, InvoiceModel>(order);
            if (invoiceModel == null)
            {
                return new ResponseObject()
                {
                    Success = false,
                    Message = "OrderDTO is nullable",
                    Code = System.Net.HttpStatusCode.NotFound
                };
            }

            invoiceModel.UserID = order.UserID;

            var room = await _repository.GetByIdAsync<RoomModel>(invoiceModel.RoomID);
            if (room == null)
            {
                return new ResponseObject()
                {
                    Success = false,
                    Message = "Can not find the room",
                    Code = System.Net.HttpStatusCode.NotFound
                };
            }

            if (!room.IsAvailable)
            {
                return new ResponseObject()
                {
                    Success = false,
                    Message = "the room is not available",
                    Code = System.Net.HttpStatusCode.NotFound
                };
            }

            room.IsAvailable = false;
            _repository.Update<RoomModel>(room);

            var carts = await _repository.GetAllAsync<CartModel>();
            var produectAmount = await CaculateUsingProductAsync(carts);
            invoiceModel.TotalAmount = CaculateBookingRoomAmount(order.StartTime.Hour, order.EndTime.Hour) + produectAmount;

            if (order.BookingParty)
            {
                invoiceModel.PartyID = await CreateBookingParty(order);
            }
            else
            {
                invoiceModel.PartyID = null;
            }

            var addedInvoice = await _repository.CreateReturnAsync<InvoiceModel>(invoiceModel);
            await _repository.SaveAsync();

            await CreateInvoiceDetail(addedInvoice.ID, carts);

            await _repository.SaveAsync();
            return new ResponseObject();
        }

        public async Task<IEnumerable<OrderDTO>> GetAllAsync()
        {
            var invoices = await _repository.GetAllAsync<InvoiceModel>();
            var orderDto = _mapper.Map<IEnumerable<InvoiceModel>, IEnumerable<OrderDTO>>(invoices) as List<OrderDTO>;

            foreach (var item in orderDto)
            {
                var user = await _repository.GetByIdAsync<AccountModel>(item.UserID) as AccountModel;
                var room = await _repository.GetByIdAsync<RoomModel>(item.RoomID) as RoomModel;
                if (item.PartyID.HasValue)
                {
                    var party = await _repository.GetByIdAsync<PartyModel>(item.PartyID) as PartyModel;
                    item.PartyName = party.Name;
                }

                item.CreatedBy = user.Username;
                item.RoomName = room.Name;
            }
            return orderDto;
        }

        public async Task<OrderDTO> GetByIDAsync(int id)
        {
            var invoice = await _repository.GetByIdAsync<InvoiceModel>(id);
            var orderDto = _mapper.Map<InvoiceModel, OrderDTO>(invoice);

            var user = await _repository.GetByIdAsync<AccountModel>(orderDto.UserID) as AccountModel;
            var room = await _repository.GetByIdAsync<RoomModel>(orderDto.RoomID) as RoomModel;
            var invoiceDetails = await _repository.GetAllAsync<InvoiceDetailModel>(filter: x => x.InvoiceID == id);

            if(orderDto.BookingParty)
            {
                var party = await _repository.GetByIdAsync<PartyModel>(orderDto.PartyID) as PartyModel;
                orderDto.PartyName = party.Name;

            }

            orderDto.CreatedBy = user.Username;
            orderDto.RoomName = room.Name;

            var orderDetails = await _repository.GetAllAsync<InvoiceDetailModel>(filter: x => x.InvoiceID == id);
            var products = await _repository.GetAllAsync<ProductModel>();

            orderDto.OrderDetails = orderDetails.Join(
                products,
                inner => inner.ProductID,
                outer => outer.ID,
                (inner, outer) => new OrderDetailDTO()
                {
                    ProductID = outer.ID,
                    ProductName = outer.Name,
                    Quantity = inner.Quantity,
                    UnitPrice = outer.UnitPrice,
                    TotalPrice = inner.Quantity * outer.UnitPrice
                });

            return orderDto;
        }

        private double CaculateBookingRoomAmount(int startedHour, int endedHour)
        {
            if (startedHour <= 18 && endedHour <= 18)
            {
                return (endedHour - startedHour) * 25000;
            }
            else if (startedHour > 18 && endedHour > 18)
            {
                return (endedHour - startedHour) * 45000;
            }
            return (18 - startedHour) * 25000 + (endedHour - 18) * 45000;
        }
        private async Task<double> CaculateUsingProductAsync(IEnumerable<CartModel> carts)
        {
            double total = 0;
            foreach (var item in carts)
            {
                var product = await _repository.GetByIdAsync<ProductModel>(item.ProductID);

                total += (product.UnitPrice * item.Count);
            }
            return total;
        }

        private async Task CreateInvoiceDetail(int invoiceID, IEnumerable<CartModel> carts)
        {
            foreach (var item in carts)
            {
                if (item.Count < 1)
                {
                    continue;
                }

                await _repository.AddAsync<InvoiceDetailModel>(new InvoiceDetailModel()
                {
                    InvoiceID = invoiceID,
                    ProductID = item.ProductID,
                    Quantity = item.Count
                });
            }
        }

        private async Task<int> CreateBookingParty(OrderDTO order)
        {
            var partyModel = new PartyModel()
            {
                Name = order.PartyName,
                Deposit = 100,
                RoomID = order.RoomID
            };

            var result = await _repository.CreateReturnAsync<PartyModel>(partyModel);
            await _repository.SaveAsync();
            return result.ID;
        }
    }
}
