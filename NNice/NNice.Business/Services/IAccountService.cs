using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NNice.Business.DTO;
using NNice.Common.Models;

namespace NNice.Business.Services
{
    public interface IAccountService
    {
        Task<ResponseObject<AccountDTO>> Authenticate(string username, string password, string secret);
        Task<IEnumerable<AccountDTO>> GetAllAsync();
    }
}
