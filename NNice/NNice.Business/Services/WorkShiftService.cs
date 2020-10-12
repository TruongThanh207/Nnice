using AutoMapper;
using NNice.Business.DTO;
using NNice.Common.Models;
using NNice.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NNice.Business.Services
{
    public class WorkShiftService : IWorkShiftService
    {
        private readonly IMapper _mapper;
        private readonly IRepository _repository;
        public WorkShiftService(IMapper mapper, IRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }
        public async Task CreateAsync(WorkShiftDTO dto)
        {
            var addedWs = _mapper.Map<WorkShiftDTO, WorkShiftModel>(dto);
            if (addedWs == null)
            {
                throw new Exception();
            }
            if (addedWs.ShiftNumber < 0)
            {
                throw new Exception("Shift number cannot be negative");
            }
            if (dto.Employees == null || dto.Employees.Count() == 0)
            {
                throw new Exception("A shift must have at least 1 employee");
            }
            await _repository.AddAsync(addedWs);
            await _repository.SaveAsync();
            foreach (var emId in dto.Employees)
            {
                await _repository.AddAsync(new EmployeeShiftModel { EmployeeID = emId.ID, WorkShiftID = addedWs.ID });
            }
            await _repository.SaveAsync();
        }

        public async Task DeleteAsync(WorkShiftDTO dto)
        {
            var wsModel = await _repository.GetByIdAsync<WorkShiftModel>(dto.ID);
            var emShifts = await _repository.GetAllAsync<EmployeeShiftModel>(filter: x => x.WorkShiftID == dto.ID);
            foreach(var es in emShifts)
            {
                _repository.Delete(es);
            }
            _repository.Delete(wsModel);
            await _repository.SaveAsync();
        }

        public async Task<IEnumerable<WorkShiftDTO>> GetAllAsync()
        {
            var shiftsModel = await _repository.GetAllAsync<WorkShiftModel>();
            var shiftsDto = _mapper.Map<IEnumerable<WorkShiftModel>, IEnumerable<WorkShiftDTO>>(shiftsModel);
            foreach(var shift in shiftsDto)
            {
                var emShifts = await _repository.GetAllAsync<EmployeeShiftModel>(filter: x => x.WorkShiftID == shift.ID);
                shift.Employees = new List<EmployeeDTO>();
                foreach(var s in emShifts)
                {
                    var em = await _repository.GetByIdAsync<Employee>(s.EmployeeID);
                    var emDto = _mapper.Map<Employee, EmployeeDTO>(em);
                    if (emDto != null)
                        ((List<EmployeeDTO>)shift.Employees).Add(emDto);
                }
            }
            return shiftsDto;
        }

        public async Task<WorkShiftDTO> GetByIdAsync(int id)
        {
            var wsModel = await _repository.GetByIdAsync<WorkShiftModel>(id);
            var wsDto = _mapper.Map<WorkShiftDTO>(wsModel);
            var emShifts = await _repository.GetAllAsync<EmployeeShiftModel>(filter: x => x.WorkShiftID == id);
            wsDto.Employees = new List<EmployeeDTO>();
            foreach (var s in emShifts)
            {
                var em = await _repository.GetByIdAsync<Employee>(s.EmployeeID);
                var emDto = _mapper.Map<Employee, EmployeeDTO>(em);
                if (emDto != null)
                    ((List<EmployeeDTO>)wsDto.Employees).Add(emDto);
            }
            return wsDto;
        }

        public async Task UpdateAsync(WorkShiftDTO dto, int id)
        {
            if (dto.ShiftNumber < 0)
            {
                throw new Exception("Shift number cannot be negative");
            }
            if (dto.Employees == null || dto.Employees.Count() == 0)
            {
                throw new Exception("A shift must have at least 1 employee");
            }
            var wsModel = await _repository.GetByIdAsync<WorkShiftModel>(id);
            var emShifts = await _repository.GetAllAsync<EmployeeShiftModel>(filter: x => x.WorkShiftID == id);

            //remove old
            foreach (var es in emShifts)
            {
                _repository.Delete(es);
            }
            //add new
            foreach (var em in dto.Employees)
            {
                await _repository.AddAsync(new EmployeeShiftModel
                {
                    EmployeeID = em.ID,
                    WorkShiftID = wsModel.ID
                });
            }

            wsModel.ShiftNumber = dto.ShiftNumber;
            wsModel.WorkDate = dto.WorkDate;

            _repository.Update(wsModel);
            await _repository.SaveAsync();
        }
    }
}
