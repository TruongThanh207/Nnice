using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using NNice.Business.DTO;
using NNice.Common.Models;

namespace NNice.API.Extensions
{
    public class DomainProfile : Profile
    {
        public DomainProfile()
        {
            CreateMap<RoomModel, RoomDTO>();
            CreateMap<RoomDTO, RoomModel>().ForMember(x => x.ID, opt => opt.Ignore());

            CreateMap<OrderDTO, InvoiceModel>();
            CreateMap<InvoiceModel, OrderDTO>();

            CreateMap<ProductModel, ProductDTO>();
            CreateMap<ProductDTO, ProductModel>().ForMember(x=>x.ID, opt=>opt.Ignore());

            CreateMap<ComboModel, ComboDTO>();
            CreateMap<ComboDTO, ComboModel>();

            CreateMap<InventoryDTO, InventoryModel>();
            CreateMap<InventoryModel, InventoryDTO>();

            CreateMap<InventoryDTO, MaterialDTO>();
            CreateMap<MaterialDTO, InventoryDTO>();

            CreateMap<AccountDTO, AccountModel>();
            CreateMap<AccountModel, AccountDTO>();
            CreateMap<CartModel, CartDTO>();
            CreateMap<CartDTO, CartModel>();
            CreateMap<WorkShiftModel, WorkShiftDTO>().ForMember(x => x.Employees, opt => opt.Ignore());
        }
    }
}
