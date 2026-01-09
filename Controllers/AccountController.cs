using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;

using Microsoft.AspNetCore.Mvc;
//  Accesses all account models
using PUP_Online_Lagoon_System.Models.Account;
// Access to Data to Object (DTO's)
using PUP_Online_Lagoon_System.Models.DTO;
//  Accesses all services
using PUP_Online_Lagoon_System.Service;
using System.Diagnostics;
//  For cookies
using System.Security.Claims;

namespace PUP_Online_Lagoon_System.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;

        //  Handles methods for login and registration
        private readonly IAuthUser _authService;

        public AccountController(ILogger<AccountController> logger, IAuthUser authService)
        {
            _logger = logger;
            _authService = authService;
        }

        [Authorize]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult LandingPage()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            //  If statement both sets user as the return of the function and checks if it's null
            if (_authService.ValidateUser(username, password) is User user)
            {
                //  Add more claims when needed
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Email),
                    new Claim("UserId", user.User_ID),
                    new Claim("Role", user.Role.ToLower())
                };

                //  Contains the list of claims made declared (name: "email" & UserID: "user_ID")
                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);

                //  Creates the actual cookie
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                if(user.Role == "admin")
                {
                    return RedirectToAction("AdminDashboard", "Admin");
                }
                else if (user.Role == "vendor")
                {
                    return RedirectToAction("VendorDashboard", "Vendor");
                }
                else if (user.Role == "customer")
                {
                    return RedirectToAction("Dashboard", "Customer");
                }

                return RedirectToAction("Index");
            }
            else
            {
                Console.WriteLine("Login Failed.");
                return View(); 
            }

        }

        [HttpGet]
        public IActionResult Register()
        {
            return View(); 
        }

        [HttpPost]
        public IActionResult Register(CustomerRegisterDTO userInfo)
        {
            Console.WriteLine(userInfo.ToString());
            _authService.RegisterUser(userInfo);
            return RedirectToAction("LandingPage"); // Redirect to actual role home page
        }

        public async Task<IActionResult> Logout()
        {
            //  Deletes cookie
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Login");
        }
    }
}
