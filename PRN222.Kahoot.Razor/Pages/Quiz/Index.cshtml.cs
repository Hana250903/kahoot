using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PRN222.Kahoot.Service.BusinessModels;
using PRN222.Kahoot.Service.Services.Interfaces;

namespace PRN222.Kahoot.Razor.Pages.Quiz
{
    public class IndexModel : PageModel
    {
		private IQuizService _quizService;

		public IndexModel(IQuizService quizService)
        {
            _quizService = quizService;
		}

        public IList<QuizModel> Quiz { get;set; } = default!;

		public PaginationModel PaginationModel { get; set; }

        public async Task OnGetAsync()
        {
            var list = await _quizService.GetQuizs(PaginationModel);
            Quiz = list;
        }
    }
}
