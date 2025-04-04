using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PRN222.Kahoot.Repository.Models;
using PRN222.Kahoot.Service.BusinessModels;
using PRN222.Kahoot.Service.Interfaces;
using PRN222.Kahoot.Service.Services.Interfaces;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PRN222.Kahoot.MVC.Controllers
{
	[Authorize]  // Yêu cầu đăng nhập để truy cập tất cả các action trong GameController

	[ApiController]
    [Route("participants")]
    public class ParticipantsController : Controller
    {
        private readonly IParticipantService _participantService;
        private readonly ISessionService _sessionService;

        public ParticipantsController(IParticipantService participantService, ISessionService sessionService)
        {
            _participantService = participantService;
            _sessionService = sessionService;
        }

        // ---------- API Endpoints ----------

        [HttpPost("join-page")]
        public async Task<IActionResult> JoinSession([FromForm] string codeRoom)
        {
            if (string.IsNullOrEmpty(codeRoom))
            {
                ViewData["Error"] = "Mã session không được để trống.";
                return View("Join");
            }

            var session = await _sessionService.GetSessionByCodeAsync(codeRoom);
            if (session == null)
            {
                ViewData["Error"] = "Mã session không hợp lệ.";
                return View("Join");
            }

            if (!User.Identity.IsAuthenticated)
            {
                ViewData["Error"] = "Người dùng chưa đăng nhập.";
                return View("Join");
            }

            // Lấy ClaimsPrincipal từ User
            var claimsPrincipal = User as ClaimsPrincipal;
            string userId = claimsPrincipal?.FindFirst(ClaimTypes.NameIdentifier)?.Value; // Sử dụng "UserId" đã lưu khi đăng nhập

            if (string.IsNullOrEmpty(userId))
            {
                ViewData["Error"] = "Không thể xác định ID người dùng.";
                return View("Join");
            }

            var participantModel = new ParticipantModel
            {
                SessionId = session.SessionId,
                UserId = int.Parse(userId),
                Team = codeRoom
            };

            var result = await _participantService.JoinSessionAsync(participantModel);
            ViewData["Success"] = $"Tham gia session thành công với ID: {result.ParticipantId}";
            return RedirectToAction("Waiting", "Waiting", new { code = codeRoom });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var participant = await _participantService.GetByIdAsync(id);
            if (participant == null)
            {
                return NotFound();
            }
            return Ok(participant);
        }

        [HttpPut("score/{participantId}")]
        public async Task<IActionResult> UpdateScore(int participantId, [FromQuery] int score)
        {
            await _participantService.UpdateScoreAsync(participantId, score);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteParticipant(int id)
        {
            await _participantService.DeleteAsync(id);
            return Ok("Participant deleted successfully");
        }

        [HttpPost]
        public async Task<IActionResult> AddParticipant([FromBody] ParticipantModel model)
        {
            await _participantService.AddAsync(model);
            return Ok("Participant added successfully");
        }

        [HttpGet]
        public async Task<IActionResult> GetAllParticipants()
        {
            var participants = await _participantService.GetAllAsync();
            return Ok(participants);
        }

        // ---------- MVC View Endpoints ----------

        [HttpGet("join-page")]
        public IActionResult Join()
        {
            return View();
        }

        [HttpGet("details/{id}")]
        public async Task<IActionResult> Details(int id)
        {
            var participant = await _participantService.GetByIdAsync(id);
            if (participant == null)
            {
                return NotFound();
            }
            return View(participant);
        }

        [HttpGet("list")]
        public async Task<IActionResult> ParticipantList()
        {
            var participants = await _participantService.GetAllAsync();
            return View(participants);
        }

    }
}
