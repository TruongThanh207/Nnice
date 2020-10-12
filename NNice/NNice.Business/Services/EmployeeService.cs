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
    public class EmployeeService : IEmployeeService
    {
        private readonly IMapper _mapper;
        private readonly IRepository _repository;
        public EmployeeService(IRepository repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task CreateAsync(EmployeeDTO dto)
        {
            if (dto.Email != null && !IsValidEmail(dto.Email))
            {
                throw new Exception("Invalid email format");
            }

            if (dto.AccountID.HasValue && !await IsValidAccount(dto.AccountID.Value))
            {
                throw new Exception("Invalid account id");
            }

            var addedUser = _mapper.Map<EmployeeDTO, Employee>(dto);
            if(addedUser == null)
            {
                throw new Exception();
            }

            await _repository.AddAsync(addedUser);
            await _repository.SaveAsync();
        }

        public async Task DeleteAsync(EmployeeDTO user)
        {
            var userModel = await _repository.GetByIdAsync<Employee>(user.ID);
            if (userModel == null)
            {
                throw new Exception();
            }

            _repository.Delete(userModel);
            await _repository.SaveAsync();
        }

        public async Task<IEnumerable<EmployeeDTO>> GetAllAsync()
        {
            var users = await _repository.GetAllAsync<Employee>();
            return _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeDTO>>(users);
        }

        public async Task<EmployeeDTO> GetByIdAsync(int id)
        {
            var user = await _repository.GetByIdAsync<Employee>(id);
            return _mapper.Map<Employee, EmployeeDTO>(user);
        }

        public async Task UpdateAsync(EmployeeDTO dto)
        {
            if (dto.Email != null && !IsValidEmail(dto.Email))
            {
                throw new Exception("Invalid email format");
            }
            if (dto.AccountID.HasValue && !await IsValidAccount(dto.AccountID.Value))
            {
                throw new Exception("Invalid account id");
            }
            var em = await _repository.GetByIdAsync<Employee>(dto.ID);
            if (em == null)
            {
                throw new Exception();
            }

            em.FirstName = dto.FirstName;
            em.Email = dto.Email;
            em.AccountID = dto.AccountID;
            em.LastName = dto.LastName;
            em.Salary = dto.Salary;
            em.Address = dto.Address;

            _repository.Update(em);
            await _repository.SaveAsync();
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private async Task<bool> IsValidAccount(int id)
        {
            return true;
            /*var acc = await _repository.GetByIdAsync<AccountModel>(id);
            if (acc != null)
                return true;
            return false;*/
        }
    }
}
