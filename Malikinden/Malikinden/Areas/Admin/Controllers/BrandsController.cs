using BusinessLogic.Abstract;
using Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Malikinden.Areas.Admin.Controllers
{
    [Area("admin"), Authorize(Policy = "AdminPolicy")]
    public class BrandsController : Controller
    {
        private readonly IService<Marka> _service;

        public BrandsController(IService<Marka> service)
        {
            _service = service;
        }

        // GET: BrandsController
        public async Task<ActionResult> IndexAsync()
        {
            var model = await _service.GetAllAsync();
            return View(model);
        }

        // GET: BrandsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: BrandsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BrandsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync(Marka marka)
        {
            try
            {
                TempData["basarili"] = "Basariyla Eklendi";
                await _service.AddAsync(marka);
                await _service.SaveAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                TempData["basarisiz"] = "Eksik Ya da Gecersiz Islem Yapıldı.";
            }
            return View(marka);
        }

        // GET: BrandsController/Edit/5
        public async Task<ActionResult> EditAsync(int id)
        {
            var model= await _service.FindAsync(id);
            return View(model);
        }

        // POST: BrandsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync(int id, Marka marka)
        {
            try
            {
                TempData["basarili"] = "Basariyla Guncellendi";
                _service.Update(marka);
                await _service.SaveAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                TempData["basarisiz"] = "Eksik Ya da Gecersiz Islem Yapıldı.";
            }
            return View();
        }

        // GET: BrandsController/Delete/5
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var model = await _service.FindAsync(id);
            return View(model);
        }

        // POST: BrandsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Marka marka)
        {
            try
            {
                TempData["basarili"] = "Basariyla Silindi";
                _service.Delete(marka);
                _service.Save();
                return RedirectToAction(nameof(Index));
            }

            catch
            {
                TempData["basarisiz"] = "Eksik Ya da Gecersiz Islem Yapıldı.";
            }
            return View();
        }
    }
}
