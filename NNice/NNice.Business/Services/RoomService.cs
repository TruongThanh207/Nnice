using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using NNice.Business.DTO;
using NNice.Common.Models;
using NNice.DAL.Repositories;

namespace NNice.Business.Services
{
    public class RoomService : IRoomService
    {
        private readonly IMapper _mapper;
        private readonly IRepository _repository;
        public RoomService(IRepository repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task CreateAsync(RoomDTO room)
        {
            var addedRoom = _mapper.Map<RoomDTO, RoomModel>(room);
           
            await _repository.AddAsync<RoomModel>(addedRoom);
            await _repository.SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var roomModel = await _repository.GetByIdAsync<RoomModel>(id);

            _repository.Delete<RoomModel>(roomModel);
            await _repository.SaveAsync();
        }

        public async Task<IEnumerable<RoomDTO>> GetAllAsync()
        {
            var rooms = await _repository.GetAllAsync<Common.Models.RoomModel>();
            return _mapper.Map<IEnumerable<Common.Models.RoomModel>, IEnumerable<DTO.RoomDTO>>(rooms);
        }

        public async Task<RoomDTO> GetByIdAsync(int id)
        {
            var roomModel = await _repository.GetByIdAsync<RoomModel>(id);

            return _mapper.Map<RoomDTO>(roomModel);
        }

        public async Task UpdateAsync(RoomDTO room, int id)
        {
            var roomModel = await _repository.GetByIdAsync<RoomModel>(id);
            if(roomModel == null)
            {
                throw new Exception();
            }

            roomModel.Name = room.Name;
            roomModel.Capacity = room.Capacity;
            roomModel.IsAvailable = room.IsAvailable;

            _repository.Update<RoomModel>(roomModel);
            await _repository.SaveAsync();
        }
    }
}
