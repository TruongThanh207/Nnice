using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using NNice.API.Helpers;
using NNice.Business.DTO;
using NNice.Business.Services;

namespace NNice.API.Controllers
{


    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly AppSettings _appSettings;
        public AccountsController(IAccountService accountService, IOptions<AppSettings> appSettings)
        {
            _accountService = accountService;
            _appSettings = appSettings.Value;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody]AccountDTO account)
        {
            var user = await _accountService.Authenticate(account.Username, account.Password, _appSettings.Secret);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(user);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllAsync()
        {
            var users = await _accountService.GetAllAsync();
            return Ok(users);
        }
    }
}