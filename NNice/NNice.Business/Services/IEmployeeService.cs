using NNice.Business.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IEmployeeService
{
    Task<IEnumerable<EmployeeDTO>> GetAllAsync();
    Task<EmployeeDTO> GetByIdAsync(int id);
    Task CreateAsync(EmployeeDTO dto);
    Task UpdateAsync(EmployeeDTO dto);
    Task DeleteAsync(EmployeeDTO dto);
}