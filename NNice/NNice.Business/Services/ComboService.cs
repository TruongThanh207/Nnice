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
    public class ComboService : IComboService
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;
        
        public ComboService(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        } 
        public async Task CreateAsync(ComboDTO combo)
        {
            var addedCombo = _mapper.Map<ComboDTO, ComboModel>(combo);

            addedCombo.UnitPrice = ((await CaculateUnitPriceOfCombo(combo.ProductID)) *90)/100 ;

            var result = await _repository.CreateReturnAsync<ComboModel>(addedCombo);
            await _repository.SaveAsync();
            await CreateComboDetailAsync(combo.ProductID, result.ID);
        }

        public async Task DeleteAsync(int id)
        {
            var ProductModel = await _repository.GetByIdAsync<ComboModel>(id);

            _repository.Delete<ComboModel>(ProductModel);
            await _repository.SaveAsync();
        }

        public async Task<IEnumerable<ComboDTO>> GetAllAsync()
        {
            var combos = await _repository.GetAllAsync<ComboModel>();
            return _mapper.Map<IEnumerable<ComboModel>, IEnumerable<ComboDTO>>(combos);
        }

        public async Task<ComboDTO> GetByIdAsync(int id)
        {
            var combo = await _repository.GetByIdAsync<ComboModel>(id);

            return _mapper.Map<ComboDTO>(combo);
        }

        public async Task UpdateAsync(ComboDTO model, int id)
        {
            var combo = await _repository.GetByIdAsync<ProductModel>(id);

            combo.Name = model.Name;
            combo.UnitPrice = ((await CaculateUnitPriceOfCombo(model.ProductID)) * 90) / 100;

            _repository.Update<ProductModel>(combo);
            await _repository.SaveAsync();
        }

        private async Task CreateComboDetailAsync(int[] productIDs, int comboID)
        {
            foreach(var ID in productIDs)
            {
                await _repository.AddAsync<ComboDetailModel>(new ComboDetailModel() {
                    CBID = comboID,
                    ProductID = ID
                });
            }
            await _repository.SaveAsync();
        }

        private async Task UpdateComboDetailAsync(int[] productIDs, int comboID)
        {
            var comboDetails = await _repository.GetAllAsync<ComboDetailModel>(filter: x => x.CBID == comboID)
                as List<ComboDetailModel>;

            
            foreach (var ID in productIDs)
            {
                if (comboDetails.Count > productIDs.Length)
                {

                }
                else if (comboDetails.Count < productIDs.Length)
                {

                }
                else
                {

                }
            }
        }

        private async Task<double> CaculateUnitPriceOfCombo(int[] productIDs)
        {
            double unitPrice = 0;
            foreach(var ID in productIDs)
            {
                var product = await _repository.GetByIdAsync<ProductModel>(ID);
                unitPrice += product.UnitPrice;
            }

            return unitPrice;
        }
    }
}
