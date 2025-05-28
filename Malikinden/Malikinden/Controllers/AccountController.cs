using BusinessLogic.Abstract;
using BusinessLogic.Concrete;
using Core.Helpers;
using Entity;
using Malikinden.Models;
using Malikinden.Utility;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace Malikinden.Controllers
{
    public class AccountController : Controller
    {
        private readonly IService<Kullanici> _kullaniciService;
        private readonly IService<Rol> _rolService;
        public AccountController(IService<Kullanici> kullaniciService, IService<Rol> rolService)
        {
            _kullaniciService = kullaniciService;
            _rolService = rolService;
        }


        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RegisterAsync(Kullanici kullanici)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var rol = await _rolService.GetAsync(r => r.Adi == "Customer");
                    if (rol == null)
                    {
                        TempData["basarisiz"] = "Eksik Ya da Gecersiz Islem";
                        return View();
                    }
                    kullanici.RolId = rol.Id;
                    kullanici.AktifMi = true;
                    TempData["basarili"] = "Kayit Olundu";
                    kullanici.Sifre = Core.Helpers.HashHelper.ComputeSha256Hash(kullanici.Sifre);
                    await _kullaniciService.AddAsync(kullanici);
                    await _kullaniciService.SaveAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    TempData["basarisiz"] = "Eksik Ya da Gecersiz Islem";
                }
            }

            return View();

        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> LoginAsync(CustomerLoginViewModel customerLoginViewModel)
        {
            try
            {
                customerLoginViewModel.Sifre = Core.Helpers.HashHelper.ComputeSha256Hash(customerLoginViewModel.Sifre);
                var account = await _kullaniciService.GetAsync(k => k.Email == customerLoginViewModel.Email && k.Sifre == customerLoginViewModel.Sifre && k.AktifMi == true);
                if (account == null)
                {
                    ModelState.AddModelError("", "Giriş başarısız");
                }
                else
                {
                    var rol = _rolService.Get(r => r.Id == account.RolId);
                    var claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.Name, account.Adi),
                    };

                    if (rol is not null)
                    {
                        claims.Add(new Claim("Role", rol.Adi));
                    }
                    var userIdentity = new ClaimsIdentity(claims, "Login");
                    ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);
                    await HttpContext.SignInAsync(principal);
                    return Redirect("/Home/Index/");
                }

            }
            catch (Exception)
            {

                ModelState.AddModelError("", "Hata Oluştu");
            }
            return View();
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/");
        }
    }
}
