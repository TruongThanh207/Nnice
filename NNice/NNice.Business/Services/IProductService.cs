using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NNice.Business.DTO;

namespace NNice.Business.Services
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDTO>> GetAllAsync();
        Task<ProductDTO> GetByIdAsync(int id);
        Task CreateAsync(ProductDTO model);
        Task UpdateAsync(ProductDTO model, int id);
        Task DeleteAsync(int id);
    }
}
