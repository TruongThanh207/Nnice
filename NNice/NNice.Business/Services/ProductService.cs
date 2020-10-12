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
    public class ProductService : IProductService
    {
        private readonly IMapper _mapper;
        private readonly IRepository _repository;
        public ProductService(IMapper mapper, IRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }
        public async Task CreateAsync(ProductDTO model)
        {
            var addedProduct = _mapper.Map<ProductDTO, ProductModel>(model);
           
            var product = await _repository.CreateReturnAsync<ProductModel>(addedProduct);
            await _repository.SaveAsync();

            var cart = new CartModel()
            {
                CartId = Guid.NewGuid().ToString(),
                ProductID = product.ID,
            };
            await _repository.AddAsync<CartModel>(cart);
            await _repository.SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var ProductModel = await _repository.GetByIdAsync<ProductModel>(id);

            _repository.Delete<ProductModel>(ProductModel);
            await _repository.SaveAsync();
        }

        public async Task<IEnumerable<ProductDTO>> GetAllAsync()
        {
            var products = await _repository.GetAllAsync<ProductModel>();
            return _mapper.Map<IEnumerable<ProductModel>, IEnumerable<ProductDTO>>(products);
        }

        public async Task<ProductDTO> GetByIdAsync(int id)
        {
            var product = await _repository.GetByIdAsync<ProductModel>(id);

            return _mapper.Map<ProductDTO>(product);
        }

        public async Task UpdateAsync(ProductDTO model, int id)
        {
            var product = await _repository.GetByIdAsync<ProductModel>(id);

            product.Name = model.Name;
            product.UnitPrice = model.UnitPrice;

            _repository.Update<ProductModel>(product);
            await _repository.SaveAsync();
        }
    }
}
