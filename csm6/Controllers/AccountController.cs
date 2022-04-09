using csm6.Models;
using csm6.ViewModels;
using csm6.Services;
using Microsoft.AspNetCore.Mvc;
using csm6.Data;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace csm6.Controllers
{
    public class AccountController : Controller
    {
        private readonly CSMContext db = new CSMContext();
        private readonly IConfiguration configuration;

        public AccountController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register(MemberViewModel member)
        {
            var registerMember = AccountServices.RegisterMember(member);

            if (ModelState.IsValid)
            {
                if (AccountServices.EmailCheck(member.Email))
                {
                    ViewBag.Message = "EmailDupe";
                    return View();
                }

                await db.Members.AddAsync(registerMember);
                await db.SaveChangesAsync();
                // AuthenticateUser(newCustomer.Email);
                return RedirectToAction("Index", "Home");
            }

            return View(member);
        }

        [HttpGet("register")]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(string password, string email)
        {
            if (!AccountServices.CanLogin(password, email))
                return BadRequest("wrong logindata");

            await CreateCookie(email);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet("login")]
        public ActionResult Login()
        {
            return View();
        }

        [HttpGet("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        [NonAction]
        public async Task CreateCookie(string email)
        {
            var claims = new List<Claim>
            {
            new Claim(ClaimTypes.Email, email),
            new Claim(ClaimTypes.Role, "User"),
            new Claim(ClaimTypes.Role, "Admin"),
            };

            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);


            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity));
        }
    }
}
