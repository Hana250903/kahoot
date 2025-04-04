using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PRN222.Kahoot.Service.Interfaces;
using PRN222.Kahoot.Service.Services;
using System.Security.Claims;

namespace PRN222.Kahoot.MVC.Controllers
{
    [Authorize] // Yêu cầu đăng nhập để truy cập tất cả các action trong WaitingController
    public class WaitingController : Controller
    {
        private readonly IParticipantService _participantService;

        public WaitingController(IParticipantService participantService)
        {
            _participantService = participantService;
        }

        [HttpGet]
        [Route("waiting")]
        public async Task<IActionResult> Waiting(string code)
        {
            // Lấy ClaimsPrincipal từ User
            var claimsPrincipal = User as ClaimsPrincipal;
            string userId = claimsPrincipal?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Join", "Participants");
            }

            var model = new 
            {
                Code = code,
                PlayerId = int.Parse(userId),
            };

            return View("Waiting", model);
        }
    }
}
