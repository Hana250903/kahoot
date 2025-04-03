using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PRN222.Kahoot.MVC.Controllers
{
	[Authorize]  // Yêu cầu đăng nhập để truy cập tất cả các action trong GameController
	public class WaitingController : Controller
	{
		public IActionResult Index()
		{
			return View("Waiting"); // Load view Waiting.cshtml
		}
	}
}
