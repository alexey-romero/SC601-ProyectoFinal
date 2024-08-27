using SecurityLayer;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TicketingSystem.Models;
using RepositoryLayer;
using ServiceLayer;

namespace TicketingSystem.Controllers
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

                    var userRole = await _repositoryService.GetUserRole(user.Id);

                    var claims = new List<Claim>
                    {
                        new Claim("UserId", user.Id.ToString()),
                        new Claim(ClaimTypes.Email, model.Email),
                        new Claim(ClaimTypes.Name, user.FirstName),
                        new Claim(ClaimTypes.Role, userRole.RoleName),
                        new Claim("ManagerId", user.IdManager.ToString())
                    };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                    if (userRole.Id == 2)
                    {
                        return RedirectToAction("IndexAdmin", "Home");
                    }
                    else if (userRole.Id == 1)
                    {
                        return RedirectToAction("Index", "Home");
                    }
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
