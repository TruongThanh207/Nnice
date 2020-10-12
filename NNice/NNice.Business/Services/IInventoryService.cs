using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NNice.Business.DTO;

namespace NNice.Business.Services
{
    public interface IInventoryService
    {
        Task<IEnumerable<InventoryDTO>> GetAllAsync();
        Task<InventoryDTO> GetByIdAsync(int id);
        Task CreateAsync(InventoryDTO model);
    }
}
