using BusinessLogic.Abstract;
using Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Malikinden.Areas.Admin.Controllers
{
    [Area("admin"), Authorize(Policy = "AdminPolicy")]

    public class CustomersController : Controller
    {
        private readonly IService<Musteri> _service;
        private readonly IService<Arac> _Aracservice;
        private readonly IService<Talep> _Talepservice;



        public CustomersController(IService<Musteri> service, IService<Arac> aracservice, IService<Talep> talepservice)
        {
            _service = service;
            _Aracservice = aracservice;
            _Talepservice = talepservice;
        }

        // GET: CustomersController
        public async Task<ActionResult> IndexAsync()
        {
            var model = await _service.GetAllAsync();
            return View(model);
        }

        // GET: CustomersController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CustomersController/Create
        public async Task<ActionResult> CreateAsync()
        {
            ViewBag.AracId = new SelectList(await _Aracservice.GetAllAsync(), "Id", "Model");
            ViewBag.TalepId = new SelectList(await _Talepservice.GetAllAsync(), "Id", "Tur");
            return View();
        }

        // POST: CustomersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync(Musteri musteri)
        {
            if (ModelState.IsValid)
            {

                try
                {
                    TempData["basarili"] = "Basariyla Eklendi";
                    await _service.AddAsync(musteri);
                    await _service.SaveAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    TempData["basarisiz"] = "Eksik Ya da Gecersiz Islem";
                }
            }
            ViewBag.AracId = new SelectList(await _Aracservice.GetAllAsync(), "Id", "Model");
            ViewBag.TalepId = new SelectList(await _Talepservice.GetAllAsync(), "Id", "Tur");
            return View(musteri);
        }

        // GET: CustomersController/Edit/5
        public async Task<ActionResult> EditAsync(int id)
        {
            var model = await _service.FindAsync(id);
            ViewBag.AracId = new SelectList(await _Aracservice.GetAllAsync(), "Id", "Model");
            ViewBag.TalepId = new SelectList(await _Talepservice.GetAllAsync(), "Id", "Tur");
            return View(model);
        }

        // POST: CustomersController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync(int id , Musteri musteri)
        {
            if (ModelState.IsValid)
            {

                try
                {
                    TempData["basarili"] = "Basariyla Guncellendi";
                   _service.Update(musteri);
                    await _service.SaveAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    TempData["basarisiz"] = "Eksik Ya da Gecersiz Islem";
                }
            }
            ViewBag.AracId = new SelectList(await _Aracservice.GetAllAsync(), "Id", "Model");
            ViewBag.TalepId = new SelectList(await _Talepservice.GetAllAsync(), "Id", "Tur");
            return View(musteri);
        }

        // GET: CustomersController/Delete/5
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var model = await _service.FindAsync(id);
            return View(model);
        }

        // POST: CustomersController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Musteri musteri)
        {
            try
            {
                TempData["basarili"] = "Basariyla Silindi";
                _service.Delete(musteri);
                _service.Save();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                TempData["basarisiz"] = "Eksik Ya da Gecersiz Islem";
            }
            return View(musteri);
        }
    }
}
