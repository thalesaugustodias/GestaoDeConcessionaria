using GestaoDeConcessionaria.Web.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using NToastNotify;

namespace GestaoDeConcessionaria.Web.Controllers
{
    public class AccountController(IHttpClientFactory httpClientFactory, IConfiguration configuration, IToastNotification toastNotification) : Controller
    {
        private readonly HttpClient _httpClient = httpClientFactory.CreateClient("ApiClient");
        private readonly IConfiguration _configuration = configuration;
        private readonly IToastNotification _toastNotification = toastNotification;

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var loginData = new { model.NomeUsuario, model.Senha };
            var content = new StringContent(JsonConvert.SerializeObject(loginData), System.Text.Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("api/auth/login", content);

            if (response.IsSuccessStatusCode)
            {
                var resultJson = await response.Content.ReadAsStringAsync();
                dynamic result = JsonConvert.DeserializeObject(resultJson);
                string token = result.token;
                var handler = new JwtSecurityTokenHandler();
                var jwtToken = handler.ReadJwtToken(token);
                var roleClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);
                var nomeClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "nomeUsuario");

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, nomeClaim?.Value ?? model.NomeUsuario),
                    new Claim(ClaimTypes.Role, roleClaim?.Value ?? "")
                };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                HttpContext.Session.SetString("JWToken", token);

                return RedirectToAction("Index", "Dashboard");
            }
            else
            {
                _toastNotification.AddErrorToastMessage("Credenciais inválidas.");
                return View(model);
            }
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("JWToken");
            return RedirectToAction("Login", "Account");
        }
    }
}