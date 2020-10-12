using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NNice.Business.DTO;
using NNice.Common.Models;
using NNice.DAL.Repositories;
using System.Linq;
using AutoMapper;

namespace NNice.Business.Services
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;
        public ShoppingCartService(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task AddToCartAsync(CartDTO model)
        {
            var cartItems = await _repository.GetAllAsync<CartModel>
                (filter: x => x.CartId == model.CartId && x.ProductID == model.ProductID);

            var cartItem = cartItems.FirstOrDefault();
            cartItem.Count = model.Count;
            cartItem.DateCreated = DateTime.Now;
            await _repository.SaveAsync();
        }

        public async Task EmptyCartAsync()
        {
            var cartItems = await _repository.GetAllAsync<CartModel>();

            foreach (var cartItem in cartItems)
            {
                cartItem.Count = 0;
            }
            // Save changes
            await _repository.SaveAsync();
        }

        public async Task<IEnumerable<CartDTO>> GetCartItemsAsync()
        {
            var cartItems = await _repository.GetAllAsync<CartModel>();
            return _mapper.Map<IEnumerable<CartDTO>>(cartItems);
        }

        public async Task<int> GetCountAsync()
        {

            //// Get the count of each item in the cart and sum them up
            //int? count = (from cartItems in storeDB.Carts
            //              where cartItems.CartId == ShoppingCartId
            //              select (int?)cartItems.Count).Sum();
            //// Return 0 if all entries are null
            //return count ?? 0;
            await _repository.SaveAsync();
            return 1;
        }

        public Task<decimal> GetTotalAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<int> RemoveFromCartAsync(CartDTO model)
        {
            var cartItems = await _repository
                .GetAllAsync<CartModel>(filter: x => x.CartId == model.CartId && x.ProductID == model.ProductID);

            int itemCount = 0;
            var cartItem = cartItems.FirstOrDefault();
            if (cartItem != null)
            {
                if (cartItem.Count > 1)
                {
                    cartItem.Count--;
                    itemCount = cartItem.Count;
                }
                else
                {
                    _repository.Delete<CartModel>(cartItem);
                }
                // Save changes
                await _repository.SaveAsync();
            }
            return itemCount;
        }
    }
}
