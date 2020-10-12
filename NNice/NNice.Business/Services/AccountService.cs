using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using NNice.Business.DTO;
using NNice.Common.Models;
using NNice.DAL.Repositories;

namespace NNice.Business.Services
{
    public class AccountService : IAccountService
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;
        public AccountService(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ResponseObject<AccountDTO>> Authenticate(string username, string password, string secret)
        {
            var accounts = await _repository.GetAllAsync<AccountModel>
                (filter: x => x.Username == username && x.Password == password) as List<AccountModel>;
            var account = accounts.FirstOrDefault();
            if (account == null)
            {
                return new ResponseObject<AccountDTO>()
                {
                    Success = false,
                    Message = "the account not found",
                    Code = System.Net.HttpStatusCode.NotFound
                };
            }

            var tokenHandeler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, account.ID.ToString()),
                    new Claim(ClaimTypes.Role, account.Role.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandeler.CreateToken(tokenDescriptor);
            account.Token = tokenHandeler.WriteToken(token);

            account.Password = null;

            return new ResponseObject<AccountDTO>()
            {
                data = new List<AccountDTO>() { _mapper.Map<AccountDTO>(account) }
            };
        }

        public async Task<IEnumerable<AccountDTO>> GetAllAsync()
        {
            var accounts = await _repository.GetAllAsync<AccountModel>() as List<AccountModel>;

           return accounts.ToList().Select(x =>  new AccountDTO{
                Avatar = x.Avatar,
                Password = string.Empty,
                Token  = x.Token,
                Username = x.Username,
            }).ToList();

        }
    }
}
