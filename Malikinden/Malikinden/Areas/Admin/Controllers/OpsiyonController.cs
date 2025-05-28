using BusinessLogic.Abstract;
using Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Malikinden.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class OpsiyonController : Controller
    {
        private readonly IService<Opsiyon> _service;
        private readonly IService<Arac> _Aracservice;
        private readonly IService<Musteri> _Musteriservice;

        public OpsiyonController(IService<Opsiyon> service, IService<Arac> Aracservice, IService<Musteri> Musteriservice)
        {
            _service = service;
            _Aracservice = Aracservice;
            _Musteriservice = Musteriservice;
        }

        // GET: SalesController
        public async Task<ActionResult> IndexAsync()
        {
            var model = await _service.GetAllAsync();
            return View(model);
        }

        // GET: SalesController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: SalesController/Create
        public async Task<ActionResult> CreateAsync()
        {
            ViewBag.AracId = new SelectList(await _Aracservice.GetAllAsync(), "Id", "Model");
            ViewBag.MusteriId = new SelectList(await _Musteriservice.GetAllAsync(), "Id", "Adi");
            return View();
        }

        // POST: SalesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync(Opsiyon opsiyon)
        {
            try
            {
                TempData["basarili"] = "Basariyla Eklendi";
                await _service.AddAsync(opsiyon);
                await _service.SaveAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                TempData["basarisiz"] = "Eksik Ya da Gecersiz Islem";
            }
            ViewBag.AracId = new SelectList(await _Aracservice.GetAllAsync(), "Id", "Model");
            ViewBag.MusteriId = new SelectList(await _Musteriservice.GetAllAsync(), "Id", "Adi");
            return View();
        }

        // GET: SalesController/Edit/5
        public async Task<ActionResult> EditAsync(int id)
        {
            var model = await _service.FindAsync(id);
            ViewBag.AracId = new SelectList(await _Aracservice.GetAllAsync(), "Id", "Model");
            ViewBag.MusteriId = new SelectList(await _Musteriservice.GetAllAsync(), "Id", "Adi");
            return View(model);
        }

        // POST: SalesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync(int id, Opsiyon opsiyon)
        {
            try
            {
                TempData["basarili"] = "Basariyla Guncellendi";
                _service.Update(opsiyon);
                await _service.SaveAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                TempData["basarisiz"] = "Eksik Ya da Gecersiz Islem";
            }
            ViewBag.AracId = new SelectList(await _Aracservice.GetAllAsync(), "Id", "Model");
            ViewBag.MusteriId = new SelectList(await _Musteriservice.GetAllAsync(), "Id", "Adi");
            return View(opsiyon);
        }

        // GET: SalesController/Delete/5
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var model = await _service.FindAsync(id);
            return View(model);
        }

        // POST: SalesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Opsiyon opsiyon)
        {
            try
            {

                TempData["basarili"] = "Basariyla Silindi";
                _service.Delete(opsiyon);
                _service.Save();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                TempData["basarisiz"] = "Eksik Ya da Gecersiz Islem";
            }
            return View(opsiyon);
        }
    }
}