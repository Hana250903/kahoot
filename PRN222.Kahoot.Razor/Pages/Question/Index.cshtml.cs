using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PRN222.Kahoot.Repository.Models;
using PRN222.Kahoot.Service.BusinessModels;
using PRN222.Kahoot.Service.Services.Interfaces;

namespace PRN222.Kahoot.Razor.Pages.Question
{
    [Authorize(Roles = "Admin")]
    public class IndexModel : PageModel
    {
        private readonly IQuestionService _questionService;

        public IndexModel(IQuestionService questionService)
        {
            _questionService = questionService;
        }

        public IList<QuestionModel> Question { get;set; } = default!;
        public PaginationModel PaginationModel { get; set; }

        public async Task OnGetAsync(int pageIndex = 1)
        {
            PaginationModel ??= new PaginationModel();

            PaginationModel.PageIndex = pageIndex;
            PaginationModel.PageSize = 5;
            PaginationModel.TotalPages = 0;

            var list = await _questionService.GetQuestions(PaginationModel);
            PaginationModel.TotalPages += (int)Math.Ceiling((double)list.TotalCount / PaginationModel.PageSize);

            Question = list;
        }
    }
}
