using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using SaleMonitoring.Identity.Models.Account;
using SaleMonitoring.Identity.Models.Manager;
using SaleMonitoring.Identity.Models.Role;

namespace SaleMonitoring.Identity.Controllers
{

    public class AccountController : Controller
    {
        private IdentityUserManager UserManager => HttpContext.GetOwinContext().GetUserManager<IdentityUserManager>();
        private IAuthenticationManager AuthenticationManager => HttpContext.GetOwinContext().Authentication;
        private IdentityRoleManager RoleManager => HttpContext.GetOwinContext().GetUserManager<IdentityRoleManager>();

        public async Task<ActionResult> Register(string returnUrl)
        {
            var user = await UserManager.FindAsync("admin@mail.ru", "superuser");
            if (user != null)
            {
                ViewBag.returnUrl = returnUrl;
                return View();
            }
            else
            {
                var isRolesCreated = await CreateRole();
                if (isRolesCreated == true)
                { 
                    await CreateAdminUserAsync();
                    return View();
                }

                return View();
            }
        }

        [HttpPost]
        public async Task<ActionResult> Register(RegisterViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                AccountUser user = new AccountUser { UserName = model.Email, Email = model.Email, NickName = model.NickName};
                IdentityResult result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await UserManager.AddToRoleAsync(user.Id, "user");

                    ClaimsIdentity claim = await UserManager.CreateIdentityAsync(user,
                        DefaultAuthenticationTypes.ApplicationCookie);
                    AuthenticationManager.SignOut();
                    AuthenticationManager.SignIn(new AuthenticationProperties
                    {
                        IsPersistent = true
                    }, claim);
                    if (String.IsNullOrEmpty(returnUrl))
                        return RedirectToAction("Index", "Home");
                    return Redirect(returnUrl);
                }
                else
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }
            }
            ViewBag.returnUrl = returnUrl;
            return View(model);
        }

        public async Task<ActionResult> Login(string returnUrl)
        {
            var user = await UserManager.FindAsync("admin@mail.ru", "superuser");
            if (user != null)
            {
                ViewBag.returnUrl = returnUrl;
                return View();
            }
            else
            {
                var isRolesCreated = await CreateRole();
                if (isRolesCreated == true)
                {
                    await CreateAdminUserAsync();
                    return View();
                }

                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                AccountUser user = await UserManager.FindAsync(model.Email, model.Password);
                if (user == null)
                {
                    ModelState.AddModelError("", "Неверный логин или пароль.");
                }
                else
                {
                    ClaimsIdentity claim = await UserManager.CreateIdentityAsync(user,
                        DefaultAuthenticationTypes.ApplicationCookie);
                    AuthenticationManager.SignOut();
                    AuthenticationManager.SignIn(new AuthenticationProperties
                    {
                        IsPersistent = true
                    }, claim);
                    if (String.IsNullOrEmpty(returnUrl))
                        return RedirectToAction("Index", "Home");
                    return Redirect(returnUrl);
                }
            }
            ViewBag.returnUrl = returnUrl;
            return View(model);
        }
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Login");
        }

        public ActionResult LoginInfo()
        {
            return View();
        }

        private async Task<AccountUser> CreateAdminUserAsync()
        {
            var admin = new AccountUser() {UserName = "admin@mail.ru", Email = "admin@mail.ru", NickName = "Admin"};
            var result = await UserManager.CreateAsync(admin, "superuser");
            if (result.Succeeded)
            {
                await UserManager.AddToRoleAsync(admin.Id, "admin");
            }
            return admin;
        }

        public async Task<bool> CreateRole(string returnUrl = null)
        {
            AccountRole adminRole = await RoleManager.FindByNameAsync("admin");
            AccountRole userRole = await RoleManager.FindByNameAsync("user");
            if (adminRole == null || userRole == null)
            {
                IdentityResult resultAdminRole = await RoleManager.CreateAsync(new AccountRole()
                {
                    Name = "admin",
                    Description = "Super user in application",
                });
                IdentityResult resultUserRole = await RoleManager.CreateAsync(new AccountRole()
                {
                    Name = "user",
                    Description = "Simple user in application",
                });
                if (resultUserRole.Succeeded && resultAdminRole.Succeeded)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            return true;
        }
    }
}