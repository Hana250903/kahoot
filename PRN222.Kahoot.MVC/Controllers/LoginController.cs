using Microsoft.AspNetCore.Mvc;
using PRN222.Kahoot.Service.Services.Interfaces;
using System.Threading.Tasks;

namespace PRN222.Kahoot.MVC.Controllers
{
	public class LoginController : Controller
	{
		private readonly IUserService _userService;

		public LoginController(IUserService userService)
		{
			_userService = userService;
		}

		[HttpGet]
		[Route("login")] // Định nghĩa đường dẫn /login
		public IActionResult Login()
		{
			return View("Login"); // Trả về Login.cshtml
		}

		[HttpPost]
		[Route("login")] // Định nghĩa đường dẫn /login cho POST
		public async Task<IActionResult> Login(string username, string password)
		{
			var user = await _userService.Login(username, password);

			if (user == null)
			{
				ViewData["Error"] = "❌ Sai tài khoản hoặc mật khẩu!";
				return View("Login");
			}

			if (user.Role != "Student")
			{
				ViewData["Error"] = "⚠️ Chỉ Student mới có thể đăng nhập!";
				return View("Login");
			}

			ViewData["Success"] = $"✅ Chào mừng {user.Username}! Bạn đã đăng nhập thành công.";

			// Sau khi đăng nhập thành công, chuyển hướng đến trang participants/join-page
			return RedirectToAction("join-page", "participants");
		}
	}
}
