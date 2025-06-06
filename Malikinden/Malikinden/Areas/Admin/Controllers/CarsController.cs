﻿using BusinessLogic.Abstract;
using Entity;
using Malikinden.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Malikinden.Areas.Admin.Controllers
{
    [Area("admin"), Authorize(Policy = "AdminPolicy")]
    public class CarsController : Controller
    {
        private readonly ICarService _service;
        private readonly IService<Marka> _Markaservice;

        public CarsController(ICarService service, IService<Marka> markaservice)
        {
            _service = service;
            _Markaservice = markaservice;
        }

        // GET: CarsController
        public async Task<ActionResult> IndexAsync()
        {
            var model = await _service.GetCustomCarList();
            return View(model);
        }

        // GET: CarsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CarsController/Create
        public async Task<ActionResult> CreateAsync()
        {
            ViewBag.MarkaId = new SelectList(await _Markaservice.GetAllAsync(), "Id", "Adi");
            return View();
        }

        // POST: CarsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync(Arac arac, IFormFile? Resim1 , IFormFile? Resim2, IFormFile? Resim3)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    TempData["basarili"] = "Basariyla Eklendi";

                    arac.Resim1=await FileHelper.FileLoaderAsync(Resim1 , "/Img/Cars/");
                    arac.Resim1 = await FileHelper.FileLoaderAsync(Resim2, "/Img/Cars/");
                    arac.Resim1 = await FileHelper.FileLoaderAsync(Resim3 , "/Img/Cars/");

                    await _service.AddAsync(arac);
                    await _service.SaveAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    TempData["basarisiz"] = "Eksik Ya da Gecersiz Islem";
                }
            }
            ViewBag.MarkaId = new SelectList(await _Markaservice.GetAllAsync(), "Id", "Adi");
            return View(arac);
        }

        // GET: CarsController/Edit/5
        public async Task<ActionResult> EditAsync(int id)
        {
            var model = await _service.FindAsync(id);
            ViewBag.MarkaId = new SelectList(await _Markaservice.GetAllAsync(), "Id", "Adi");
            return View(model);
        }

        // POST: CarsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync(int id, Arac arac, IFormFile? Resim1, IFormFile? Resim2, IFormFile? Resim3)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    TempData["basarili"] = "Basariyla Guncellendi";

                    if(Resim1 is not null)
                    {
                        arac.Resim1 = await FileHelper.FileLoaderAsync(Resim1, "/Img/Cars/");
                    }
                    if (Resim2 is not null)
                    {
                        arac.Resim2 = await FileHelper.FileLoaderAsync(Resim2, "/Img/Cars/");
                    }
                    if (Resim3 is not null)
                    {
                        arac.Resim3 = await FileHelper.FileLoaderAsync(Resim3, "/Img/Cars/");
                    }

                    _service.Update(arac);
                    await _service.SaveAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    TempData["basarisiz"] = "Eksik Ya da Gecersiz Islem";
                }
            }
            ViewBag.RolId = new SelectList(await _Markaservice.GetAllAsync(), "Id", "Adi");
            return View(arac);
        }

        // GET: CarsController/Delete/5
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var model = await _service.FindAsync(id);
            return View(model);
        }

        // POST: CarsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Arac arac)
        {
            try
            {
                TempData["basarili"] = "Basariyla Silindi";
                _service.Delete(arac);
                _service.Save();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                TempData["basarisiz"] = "Eksik Ya da Gecersiz Islem";
            }
            return View(arac);
        }
    }
}
