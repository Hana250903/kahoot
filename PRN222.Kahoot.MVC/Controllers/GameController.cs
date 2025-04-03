using Microsoft.AspNetCore.Mvc;
using PRN222.Kahoot.Service.Services.Interfaces;
using PRN222.Kahoot.Service.BusinessModels;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace PRN222.Kahoot.Web.Controllers
{
	[Authorize]  // Yêu cầu đăng nhập để truy cập tất cả các action trong GameController
	public class GameController : Controller
	{
		private readonly IQuestionService _questionService;  // Để lấy câu hỏi
		private readonly IQuizSessionService _quizSessionService; // Để quản lý QuizSession

		// Constructor injection các dịch vụ cần thiết
		public GameController(IQuizSessionService quizSessionService, IQuestionService questionService)
		{
			_quizSessionService = quizSessionService;
			_questionService = questionService;
		}

		// Hàm hiển thị câu hỏi
		public async Task<IActionResult> Game(int quizSessionId, int currentQuestionIndex = 0)
		{
			// Lấy thông tin quiz session bằng quizSessionId
			var quizSession = await _quizSessionService.GetById(quizSessionId);
			if (quizSession == null)
			{
				return NotFound("Quiz session không tồn tại.");
			}

			// Lấy danh sách câu hỏi theo quizSessionId
			var questions = await _questionService.GetQuestionByQuizId(quizSessionId);

			// Kiểm tra xem câu hỏi có đủ hay không
			if (questions == null || !questions.Any())
			{
				return NotFound("Không có câu hỏi cho quiz này.");
			}

			// Kiểm tra xem currentQuestionIndex có hợp lệ không
			if (currentQuestionIndex >= questions.Count())
			{
				return RedirectToAction("GameOver");
			}

			// Lấy câu hỏi theo currentQuestionIndex
			var currentQuestion = questions.ElementAtOrDefault(currentQuestionIndex);
			if (currentQuestion == null)
			{
				return RedirectToAction("GameOver");
			}

			// Truyền dữ liệu qua ViewBag để hiển thị trong View
			ViewBag.CurrentQuestionIndex = currentQuestionIndex;
			ViewBag.QuizSessionId = quizSessionId;
			ViewBag.Duration = currentQuestion.Duration;  // Thời gian câu hỏi
			ViewBag.QuestionCount = questions.Count();  // Tổng số câu hỏi

			// Trả về View với câu hỏi hiện tại
			return View(currentQuestion);
		}

		// Hàm xử lý câu trả lời
		[HttpPost]
		public async Task<IActionResult> SubmitAnswer(int quizSessionId, string answer, int currentQuestionIndex)
		{
			// Xử lý câu trả lời ở đây, ví dụ: kiểm tra đúng sai, ghi điểm,...

			// Chuyển tới câu hỏi tiếp theo
			var nextQuestionIndex = currentQuestionIndex + 1;

			// Lấy danh sách câu hỏi
			var questions = await _questionService.GetQuestionByQuizId(quizSessionId);

			// Kiểm tra xem còn câu hỏi không
			if (nextQuestionIndex < questions.Count())
			{
				return RedirectToAction("Game", new { quizSessionId = quizSessionId, currentQuestionIndex = nextQuestionIndex });
			}

			// Nếu không còn câu hỏi, chuyển tới GameOver
			return RedirectToAction("GameOver");
		}
	}
}
