using Microsoft.AspNetCore.Mvc;

namespace TelnetTest.Controllers
{
	public class RootController : Controller
	{
		// GET
		public IActionResult Index()
		{
			return View();
		}
	}
}