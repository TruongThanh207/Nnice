using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NNice.Business.DTO;

namespace NNice.Business.Services
{
    public interface IRoomService
    {
        Task<IEnumerable<DTO.RoomDTO>> GetAllAsync();
        Task<DTO.RoomDTO> GetByIdAsync(int id);
        Task CreateAsync(RoomDTO room);
        Task UpdateAsync(RoomDTO room, int id);
        Task DeleteAsync(int id);
    }
}
