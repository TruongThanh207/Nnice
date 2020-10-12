using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NNice.Business.DTO;

namespace NNice.Business.Services
{
    public interface IComboService
    {
        Task<IEnumerable<ComboDTO>> GetAllAsync();
        Task<ComboDTO> GetByIdAsync(int id);
        Task CreateAsync(ComboDTO combo);
        Task UpdateAsync(ComboDTO combo, int id);
        Task DeleteAsync(int id);
    }
}
