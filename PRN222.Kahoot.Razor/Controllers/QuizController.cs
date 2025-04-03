using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PRN222.Kahoot.Service.Services;
using PRN222.Kahoot.Service.Services.Interfaces;

namespace PRN222.Kahoot.Razor.Controllers
{
    [Route("api/quiz")]
    [ApiController]
    public class QuizController : ControllerBase
    {
        private readonly IQuizService _quizService;

        public QuizController(IQuizService quizService)
        {
            _quizService = quizService;
        }

        [HttpGet("get-details")]
        public IActionResult GetQuizDetails(int quizId)
        {
            //var quiz = _context.Quizzes
            //    .Where(q => q.Id == quizId)
            //    .Select(q => new
            //    {
            //        TotalQuestion = q.Questions.Count(),
            //        TotalScore = q.Questions.Sum(question => question.Score)
            //    })
            //    .FirstOrDefault();

            //if (quiz == null)
            //{
            //    return NotFound();
            //}

            //return Ok(quiz);
            throw new NotImplementedException();
        }
    }
}
