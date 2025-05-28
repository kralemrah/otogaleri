using BusinessLogic.Abstract;
using Core.Helpers;
using Entity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Malikinden.Areas.Admin.Controllers
{
    
    [Area("admin")]
    public class LoginController : Controller
    {
        private readonly IService<Kullanici> _service;
        private readonly IService<Rol> _Rolservice;

        public LoginController(IService<Kullanici> service, IService<Rol> rolservice)
        {
            _service = service;
            _Rolservice = rolservice;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/Admin/Login");
        }

        [HttpPost]
        public async Task<IActionResult> IndexAsync(string email, string password)
        {
            try
            {
                password=HashHelper.ComputeSha256Hash(password);
                var account = _service.Get(k=>k.Email==email && k.Sifre==password &&k.AktifMi==true);
                if (account==null)
                {
                    TempData["message"] = "Hata oluştu";
                }
                else
                {
                    var rol = _service.Get(r=>r.Id==account.RolId);
                    var claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.Name, account.Adi),
                       

                    };

                    if (rol is not null)
                    {
                        claims.Add(new Claim("Role", rol.Adi));
                    }
                    var userIdentity= new ClaimsIdentity(claims,"Login");
                    ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);
                    await HttpContext.SignInAsync(principal);
                    return Redirect("/Admin");
                }

            }
            catch (Exception)
            {

                TempData["message"] = "Hata oluştu";
            }
            return View();
        }
    }
}
