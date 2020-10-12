using NNice.Business.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NNice.Business.Services
{
    public interface IWorkShiftService
    {
        Task<IEnumerable<WorkShiftDTO>> GetAllAsync();
        Task<WorkShiftDTO> GetByIdAsync(int id);
        Task CreateAsync(WorkShiftDTO dto);
        Task UpdateAsync(WorkShiftDTO dto, int id);
        Task DeleteAsync(WorkShiftDTO dto);
    }
}
