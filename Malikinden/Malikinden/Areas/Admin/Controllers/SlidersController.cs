using BusinessLogic.Abstract;
using Entity;
using Malikinden.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Malikinden.Areas.Admin.Controllers
{
    [Area("admin"), Authorize]
    public class SlidersController : Controller
    {
        private readonly IService<Slider> _service;

        public SlidersController(IService<Slider> service)
        {
            _service = service;
        }

        // GET: SlidersController
        public async Task<ActionResult> Index()
        {
            var model =  await _service.GetAllAsync();
            return View(model);
        }

        // GET: SlidersController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: SlidersController/Create
        public  ActionResult Create()
        {
            return View();
        }

        // POST: SlidersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync(Slider slider, IFormFile resim)
        {
            try
            {
                TempData["basarili"] = "Basariyla Eklendi";
                slider.Resim = await FileHelper.FileLoaderAsync(resim, "/Img/Slider/");
                await _service.AddAsync(slider);
                await _service.SaveAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                TempData["basarisiz"] = "Eksik Ya da Gecersiz Islem";
            }
            return View();
        }

        // GET: SlidersController/Edit/5
        public async Task<ActionResult> EditAsync(int id)
        {
           var model= await _service.FindAsync(id);
            return View(model);
        }

        // POST: SlidersController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync(int id, Slider slider, IFormFile resim)
        {
            try
            {
                if (resim is not null)
                {
                    slider.Resim = await FileHelper.FileLoaderAsync(resim, "/Img/Slider/");
                }
                TempData["basarili"] = "Basariyla Guncellendi";
                _service.Update(slider);
                await _service.SaveAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                TempData["basarisiz"] = "Eksik Ya da Gecersiz Islem";               
            }
            return View();
        }

        // GET: SlidersController/Delete/5
        public async Task<ActionResult> DeleteAsync(int id)
        {
          var model = await _service.FindAsync(id);
            return View(model);
        }

        // POST: SlidersController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteAsync(int id, Slider slider)
        {
            try
            {
                TempData["basarili"] = "Basariyla Silindi";
                _service.Delete(slider);
                await _service.SaveAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                TempData["basarisiz"] = "Eksik Ya da Gecersiz Islem";
            }
            return View(slider);
        }
    }
}
