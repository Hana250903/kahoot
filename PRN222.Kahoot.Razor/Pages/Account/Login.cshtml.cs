using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PRN222.Kahoot.Service.BusinessModels;
using PRN222.Kahoot.Service.Services;
using PRN222.Kahoot.Service.Services.Interfaces;
using System.Security.Claims;

namespace PRN222.Kahoot.Razor.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly IUserService _userService;

        public LoginModel(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            return Page();
        }

        [BindProperty]
        public UserModel UserModel { get; set; } = default!;

        public string ErrorMessage { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userService.Login(UserModel.Username,UserModel.Password);

            if(user == null)
            {
                ErrorMessage = "Invalid username or password";
                ModelState.AddModelError(string.Empty, ErrorMessage);
                return Page();
            }

            return RedirectToPage("/Question/Index");
        }

    }
}
