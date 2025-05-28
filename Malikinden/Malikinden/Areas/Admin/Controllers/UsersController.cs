using BusinessLogic.Abstract;
using Core.Helpers;
using Entity;
using Malikinden.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Malikinden.Areas.Admin.Controllers
{
    [Area("admin"), Authorize(Policy = "AdminPolicy")]
    public class UsersController : Controller
    {
        private readonly IService<Kullanici> _service;
        private readonly IService<Rol> _Rolservice;

        public UsersController(IService<Kullanici> service, IService<Rol> Rolservice)
        {
            _service = service;
            _Rolservice = Rolservice;
        }

        // GET: UsersController
        public async Task<ActionResult> IndexAsync()
        {
            var model = await _service.GetAllAsync();
            return View(model);
        }

        // GET: UsersController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UsersController/Create
        public async Task<ActionResult> CreateAsync()
        {
            ViewBag.RolId = new SelectList(await _Rolservice.GetAllAsync(), "Id", "Adi");
            return View();
        }

        // POST: UsersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync(Kullanici kullanici)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    kullanici.Sifre = Core.Helpers.HashHelper.ComputeSha256Hash(kullanici.Sifre);
                    TempData["basarili"] = "Basariyla Eklendi";
                    await _service.AddAsync(kullanici);
                    await _service.SaveAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    TempData["basarisiz"] = "Eksik Ya da Gecersiz Islem";
                }
            }
            ViewBag.RolId = new SelectList(await _Rolservice.GetAllAsync(), "Id", "Adi");
            return View();
        }

        // GET: UsersController/Edit/5
        public async Task<ActionResult> EditAsync(int id)
        {
            var model = await _service.FindAsync(id);
            ViewBag.RolId = new SelectList(await _Rolservice.GetAllAsync(), "Id", "Adi");
            return View(model);
        }

        // POST: UsersController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync(int id, Kullanici kullanici)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    TempData["basarili"] = "Basariyla Guncellendi";
                    _service.Update(kullanici);
                    await _service.SaveAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    TempData["basarisiz"] = "Eksik Ya da Gecersiz Islem";
                }
            }
            ViewBag.RolId = new SelectList(await _Rolservice.GetAllAsync(), "Id", "Adi");
            return View();
        }

        // GET: UsersController/Delete/5
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var model = await _service.FindAsync(id);
            return View(model);
        }

        // POST: UsersController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Kullanici kullanici)
        {

            try
            {
                TempData["basarili"] = "Basariyla Silindi";
                _service.Delete(kullanici);
                _service.Save();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                TempData["basarisiz"] = "Eksik Ya da Gecersiz Islem";
            }
            return View();
        }
    }
}
