using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PRN222.Kahoot.MVC.Controllers
{
    [Authorize]  // Yêu cầu đăng nhập để truy cập tất cả các action trong GameController
    public class GameController : Controller
    {
        public async Task<IActionResult> Index(string code, int playerId)
        {
            ViewBag.Code = code;
            ViewBag.PlayerId = playerId;
            return View();
        }
    }
}
