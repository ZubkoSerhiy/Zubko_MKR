using Auth0.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.OAuth;
using LR6.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace LR6.Controllers
{
    public class AccountController : Controller
    {
        private readonly IConfiguration _configuration;

        public AccountController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task Login(string returnUrl = "/")
        {
            var authenticationProperties = new LoginAuthenticationPropertiesBuilder()
                .WithRedirectUri(returnUrl)
                .Build();

            await HttpContext.ChallengeAsync(Auth0Constants.AuthenticationScheme, authenticationProperties);
        }

        [Authorize]
        public async Task Logout()
        {
            var authenticationProperties = new LogoutAuthenticationPropertiesBuilder()
                .WithRedirectUri(Url.Action("Index", "Home"))
                .Build();

            await HttpContext.SignOutAsync(Auth0Constants.AuthenticationScheme, authenticationProperties);
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var client = new HttpClient();
                var request = new
                {
                    email = model.EmailAddress,
                    username = model.Username,
                    password = model.Password,
                    connection = "Username-Password-Authentication",
                    user_metadata = new
                    {
                        full_name = model.FullName,
                        phone_number = model.PhoneNumber
                    }
                };
                var response = await client.PostAsJsonAsync($"https://{_configuration["Auth0:Domain"]}/dbconnections/signup", request);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Login");
                }
                else
                {
                    var errorResponse = await response.Content.ReadAsStringAsync();
                    ModelState.AddModelError("", $"Помилка реєстрації: {errorResponse}");
                }
            }
            return View(model);
        }

        [Authorize]
        public IActionResult Profile()
        {
            var userProfile = new ProfileViewModel
            {
                Username = User.Claims.FirstOrDefault(c => c.Type == "nickname")?.Value,
                EmailAddress = User.Claims.FirstOrDefault(c => c.Type == "email")?.Value,
                FullName = User.Claims.FirstOrDefault(c => c.Type == "https://localhost:3000/full_name")?.Value,
                PhoneNumber = User.Claims.FirstOrDefault(c => c.Type == "https://localhost:3000/phone_number")?.Value
            };
            return View(userProfile);
        }
    }
}
