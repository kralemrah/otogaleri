using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Malikinden.Areas.Admin.Controllers
{
	public class MainController : Controller
	{
       [Area("admin"), Authorize(Policy = "AdminPolicy")]
        public IActionResult Index()
		{
			return View();
		}
	}
}
