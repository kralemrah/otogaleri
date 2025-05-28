using BusinessLogic.Abstract;
using BusinessLogic.Concrete;
using Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Malikinden.Controllers
{
    public class AracController : Controller
    {
        private readonly ICarService _Aracservice;
        private readonly IService<Musteri> _Musteriservice;
        private readonly IService<Talep> _Talepservice;

        public AracController(ICarService aracservice, IService<Musteri> musteriservice, IService<Talep> talepiservice)
        {
            _Aracservice = aracservice;
            _Musteriservice = musteriservice;
            _Talepservice = talepiservice;
        }

        public async Task<IActionResult> IndexAsync(int id)
        {
            var model = await _Aracservice.GetCustomCar(id);
            ViewBag.AracId = new SelectList(await _Aracservice.GetAllAsync(), "Id", "Model");
            ViewBag.TalepId = new SelectList(await _Talepservice.GetAllAsync(), "Id", "Tur");
            return View(model);
        }

        [Route("tum-araclarimiz")]
		public async Task<IActionResult> List()
		{
			var model = await _Aracservice.GetCustomCarList(c=>c.SatistaMi);
			return View(model);
		}
        public async Task<IActionResult> Ara(string q)
        {
            var model = await _Aracservice.GetCustomCarList(c => c.SatistaMi && c.Marka.Adi.Contains(q) || c.KasaTipi.Contains(q));
            return View(model);
        }
        public async Task<ActionResult> Musteri()
        {
           
            ViewBag.TalepId = new SelectList(await _Talepservice.GetAllAsync(), "Id", "Tur");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Musteri(Musteri musteri)
        {
            if (ModelState.IsValid)
            {

                try
                {
                    TempData["basarili"] = "Bilgi Gonderildi";
                    await _Musteriservice.AddAsync(musteri);
                    await _Musteriservice.SaveAsync();
                    return Redirect("/Arac/Index/" + musteri.AracId);

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
    }
}
