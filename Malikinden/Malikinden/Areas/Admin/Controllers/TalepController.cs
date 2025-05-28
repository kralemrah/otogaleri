using BusinessLogic.Abstract;
using Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Malikinden.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TalepController : Controller
    {
        private readonly IService<Talep> _service;

        public TalepController(IService<Talep> service)
        {
            _service = service;
        }

        // GET: TalepController
        public async Task<ActionResult> IndexAsync()
        {
            var model = await _service.GetAllAsync();
            return View(model);
        }

        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: RolesController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RolesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Talep talep)
        {
            try
            {

                _service.Add(talep);
                _service.Save();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: RolesController/Edit/5
        public async Task<ActionResult> EditAsync(int id)
        {
            var model = await _service.FindAsync(id);
            return View(model);
        }

        // POST: RolesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Talep talep)
        {
            try
            {
                _service.Update(talep);
                _service.Save();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: RolesController/Delete/5
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var model = await _service.FindAsync(id);
            return View(model);
        }

        // POST: RolesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Talep talep)
        {
            try
            {
                _service.Delete(talep);
                _service.Save();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}

