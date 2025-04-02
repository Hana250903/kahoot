using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PRN222.Kahoot.Razor.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }

    public IActionResult OnGet()
    {
        _logger.LogInformation($"User authenticated: {User.Identity!.IsAuthenticated}");
        if (User.Identity!.IsAuthenticated)
        {
            return RedirectToPage("/Index");
        }
        return RedirectToPage("/Account/Register");
    }
}
