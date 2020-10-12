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
    public class InventoryService : IInventoryService
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public InventoryService(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task CreateAsync(InventoryDTO model)
        {
            var inventoryModel = _mapper.Map<InventoryDTO, InventoryModel>(model);
            var materialModel = _mapper.Map<MaterialModel>(model);

            var effectedMaterial = await _repository.CreateReturnAsync<MaterialModel>(materialModel);

            var effectedInventory = await _repository.CreateReturnAsync<InventoryModel>(inventoryModel);
            await _repository.SaveAsync();

            await _repository.AddAsync<InventoryDetailModel>(new InventoryDetailModel()
            {
                InventoryID = effectedInventory.ID,
                MaterialID = effectedMaterial.ID
            });

            await _repository.SaveAsync();
        }

        public async Task<IEnumerable<InventoryDTO>> GetAllAsync()
        {
            var inventories = await _repository.GetAllAsync<InventoryModel>();
            return _mapper.Map<IEnumerable<InventoryModel>, IEnumerable<InventoryDTO>>(inventories);
        }

        public async Task<InventoryDTO> GetByIdAsync(int id)
        {
            var inventory = await _repository.GetByIdAsync<InventoryModel>(id);

            return _mapper.Map<InventoryDTO>(inventory);
        }
    }
}
