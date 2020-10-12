using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NNice.Web.Helpers;
using NNice.Web.Models;

namespace NNice.Web.Controllers
{
    public class AccountController : Controller
    {
        private Connector _connector;
        public AccountController()
        {
            var client = new HttpClient();
            // Update port # in the following line.
            client.BaseAddress = new Uri(Routes.BaseUrl);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            _connector = new Connector(client);
        }
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login(string ReturnUrl)
        {
            ViewData["ReturnUrl"] = ReturnUrl;
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(AccountViewModel account, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            returnUrl = returnUrl ?? "room/RoomManagement";
            if (!ModelState.IsValid)
            {
                return View("Login", account);
            }

            var user = await _connector.Authencate(account);
            var userData = user.data.FirstOrDefault();
            if (user.Success)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, userData.ID.ToString()),
                    new Claim("access_token", userData.Token)
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties()
                {
                    IsPersistent = account.RememberMe,
                    ExpiresUtc = DateTime.UtcNow.AddMinutes(20)
                };
                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                  new ClaimsPrincipal(claimsIdentity),
                  authProperties);

                return Redirect(returnUrl);
            }

            //var cookie = new System.Web.HttpCookie()
            return View("Login", account);
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme);
            return LocalRedirect("/");
        }
    }
}