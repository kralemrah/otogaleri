using BusinessLogic.Abstract;
using Entity;
using Malikinden.ExtensionMethods;
using Microsoft.AspNetCore.Mvc;

namespace Malikinden.Controllers
{
    public class FavoritesController : Controller
    {
        private readonly ICarService _Aracservice;
        public FavoritesController(ICarService aracservice)
        {
            _Aracservice = aracservice;
        }
        public IActionResult Index()
        {
            var favoriler = GetFavorites();
            return View(favoriler);
        }
        private List<Arac>GetFavorites()
        {
            List<Arac>? araclar = new();
            araclar = HttpContext.Session.GetJson<List<Arac>>
                ("GetFavorites");
            return araclar ?? new List<Arac>();
        }
        public IActionResult Add(int aracId)
        {
            try
            {
                var arac = _Aracservice.Find(aracId);
                var favoriler = GetFavorites();
                if (favoriler != null && !favoriler.Any(a => a.Id == aracId))
                {
                    favoriler.Add(arac);
                    HttpContext.Session.SetJson("GetFavorites", favoriler);
                }
            }
            catch (Exception)
            {
                TempData["Message"] = "<div class='alert alert-danger'>HATA OLUŞTU</div>";
               
            }
           
            return RedirectToAction("Index");
        }
        public IActionResult Remove(int aracId)
        {
            try
            {
                var arac = _Aracservice.Find(aracId);
                var favoriler = GetFavorites();
                if (favoriler != null && favoriler.Any(a => a.Id == aracId))
                {
                    favoriler.RemoveAll(a => a.Id == aracId);
                    HttpContext.Session.SetJson("GetFavorites", favoriler);
                }
            }
            catch (Exception)
            {
                TempData["Message"] = "<div class='alert alert-danger'>HATA OLUŞTU</div>";

            }

            return RedirectToAction("Index");
        }
    }
}
