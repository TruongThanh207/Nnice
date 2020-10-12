using NNice.Business.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NNice.Business.Services
{
    public interface IOrderService
    {
        Task<ResponseObject> BookRoomAsync(OrderDTO order);
        //Task<bool> BookPartyAsync(OrderDTO order, PartyDTO party);
        Task<IEnumerable<OrderDTO>> GetAllAsync();
        Task<OrderDTO> GetByIDAsync(int id);
    }
}
