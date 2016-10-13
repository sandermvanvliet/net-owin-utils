using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using net_owin_utils_sample.Models;

namespace net_owin_utils_sample.Controllers
{
    public class AccountController : Controller
    {
        public ViewResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            var claimsIdentity = new ClaimsIdentity();
            claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, "test"));
            var principal = new ClaimsPrincipal(claimsIdentity);

            HttpContext.Authentication.SignInAsync(AuthenticationSchemes.Basic.ToString(), principal).Wait();

            return RedirectToAction("Index", "Home");
        }

        public ViewResult Forbidden()
        {
            return View();
        }

        public ActionResult Logout()
        {
            HttpContext.Authentication.SignOutAsync(AuthenticationSchemes.Basic.ToString()).Wait();

            return RedirectToAction("Index", "Home");
        }
    }
}
