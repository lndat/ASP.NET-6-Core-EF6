using csm6.Models;
using csm6.ViewModels;
using csm6.Services;
using Microsoft.AspNetCore.Mvc;
using csm6.Data;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

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
        public ActionResult Login(string password, string email)
        {
            if (!AccountServices.CanLogin(password, email))
                return BadRequest("wrong logindata");

            string token = CreateToken(email);
            return Ok(token);
        }

        [HttpGet("login")]
        public ActionResult Login()
        {
            return View();
        }

        [NonAction]
        private string CreateToken(string email)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, email)
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                configuration.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
    }
}
