using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PRN222.Kahoot.Repository.Models;
using PRN222.Kahoot.Service.BusinessModels;
using PRN222.Kahoot.Service.Services;
using PRN222.Kahoot.Service.Services.Interfaces;

namespace PRN222.Kahoot.Razor.Pages.Question
{
    public class DetailsModel : PageModel
    {
        private readonly IQuestionService _questionService;

        public DetailsModel(IQuestionService questionService)
        {
            _questionService = questionService;
        }

        public QuestionModel Question { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var question = await _questionService.GetById((int)id);

            if (question is not null)
            {
                Question = question;

                return Page();
            }

            return NotFound();
        }
    }
}
