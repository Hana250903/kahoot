using Microsoft.AspNetCore.Mvc;
using PRN222.Kahoot.Service.BusinessModels;
using PRN222.Kahoot.Service.Interfaces;
using PRN222.Kahoot.Service.Services.Interfaces;
using System.Threading.Tasks;

namespace PRN222.Kahoot.MVC.Controllers
{
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

        [HttpPost("join")]
        public async Task<IActionResult> JoinSession([FromBody] JoinRequestModel model)
        {
            var session = await _sessionService.GetSessionByCodeAsync(model.CodeRoom);
            if (session == null)
            {
                return BadRequest("Invalid session code.");
            }

            var participantModel = new ParticipantModel
            {
                SessionId = session.SessionId,
                UserId = 0, // Nếu có đăng nhập, lấy userId từ HttpContext
                Team = model.Team
            };

            var result = await _participantService.JoinSessionAsync(participantModel);
            return Ok(result);
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
