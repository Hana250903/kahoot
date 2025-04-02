using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PRN222.Kahoot.Service.Services;
using PRN222.Kahoot.Service.Services.Interfaces;
using System.Threading.Tasks;

namespace PRN222.Kahoot.Razor.Pages.Account
{
    public class LogoutModel : PageModel
    {
        private readonly IUserService _userService;

        public LogoutModel(IUserService userService)
        {
            _userService = userService;
        }

        public async Task OnGet()
        {
            await _userService.Logout();
            Response.Redirect("/");
        }
    }
}
