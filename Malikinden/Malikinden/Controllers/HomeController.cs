using BusinessLogic.Abstract;
using Entity;
using Malikinden.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Data;
using Malikinden.Services;		
using System.Text;
using System.Linq;
using System;

namespace Malikinden.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly IService<Slider> _Sliderservice;
		private readonly ICarService _Aracservice;
		private readonly MalikindenDbContext _context;
		private readonly AracOneriSistemi _aracOneriSistemi;

		public HomeController(ILogger<HomeController> logger, IService<Slider> sliderservice, ICarService aracservice, MalikindenDbContext context)
		{
			_logger = logger;
			_Sliderservice = sliderservice;
			_Aracservice = aracservice;
			_context = context;
			_aracOneriSistemi = new AracOneriSistemi(_context);
		}

		public async Task<IActionResult> Index()
		{
			var model = new HomePageViewModel()
			{
				Sliderlar = await _Sliderservice.GetAllAsync(),
				Araclar = await _Aracservice.GetCustomCarList(a => a.Anasayfa && a.SatistaMi)
			};
			return View(model);
		}

		[HttpGet]
		public IActionResult AracOneri()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> AracOneriSonuc(AracOneriViewModel model)
		{
			var oneriler = await _aracOneriSistemi.AracOneriGetir(model);
			
			if (!oneriler.Any())
			{
				return Json(new { success = false, message = "Kriterlerinize uygun araç bulunamadı." });
			}

			var sonuclar = oneriler.Select(a => new
			{
				id = a.Id,
				marka = a.Marka.Adi,
				model = a.Model,
				fiyat = a.Fiyat.ToString("N0") + " TL",
				modelYili = a.ModelYili,
				kilometre = a.Kilometre.ToString("N0"),
				yakitTipi = a.YakitTipi,
				vitesTipi = a.VitesTipi,
				kasaTipi = a.KasaTipi,
				resimUrl = !string.IsNullOrEmpty(a.Resim1) ? "/Img/Cars/" + a.Resim1 : "/images/no-image.jpg",
				puan = Math.Round(_aracOneriSistemi.HesaplaPuan(a, model), 1)
			}).OrderByDescending(x => x.puan).ToList();

			return Json(new { success = true, oneriler = sonuclar });
		}

		public IActionResult Privacy()
		{
			return View();
		}

		public IActionResult Iletisim()
		{
			return View();
		}

		[Route("AccessDenied")]
		public IActionResult AccessDenied()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}