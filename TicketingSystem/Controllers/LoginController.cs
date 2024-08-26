using SecurityLayer;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TicketingSystem.Models;
using RepositoryLayer;
using ServiceLayer;

namespace APS.Web.Controllers
{
    public class LoginController(ISecurityService securityService, IRepositoryService repositoryService) : Controller
    {
        private readonly ISecurityService _securityService = securityService;
        private readonly IRepositoryService _repositoryService = repositoryService;

        public IActionResult Index()
        {
            return View("/Views/Users/Login.cshtml");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                bool result = await _securityService.AuthUserByEmailAsync(new RepositoryLayer.Models.User { Email = model.Email, Password = model.Password });

                if (result)
                {
                    var user = await _repositoryService.GetUserByEmail(model.Email);

                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, model.Email),
                    };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            }

            return View("/Views/Users/Login.cshtml", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            //HttpContext.Session.Clear(); // Limpiar la sesión al cerrar sesión
            return RedirectToAction("Index", "Login");
        }
    }
}    
