using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NNice.Business.DTO;

namespace NNice.Business.Services
{
    public interface IShoppingCartService
    {
        Task AddToCartAsync(CartDTO model);
        Task<int> RemoveFromCartAsync(CartDTO model);
        Task EmptyCartAsync();
        Task<IEnumerable<CartDTO>> GetCartItemsAsync();
        Task<int> GetCountAsync();
        Task<decimal> GetTotalAsync();
    }
}
