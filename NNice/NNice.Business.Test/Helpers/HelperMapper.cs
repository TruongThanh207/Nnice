using AutoMapper;
using NNice.Business.DTO;
using NNice.Common.Models;

namespace NNice.Business.Test.Helpers
{
    public class HelperMapper : Profile
    {
        public HelperMapper()
        {
            CreateMap<RoomModel, RoomDTO>();
            CreateMap<RoomDTO, RoomModel>();
            CreateMap<UserModel, UserDTO>();
            CreateMap<UserDTO, UserModel>();
            CreateMap<PartyDTO, PartyModel>();
            CreateMap<PartyModel, PartyDTO>();
        }
    }
}
