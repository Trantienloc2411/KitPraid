using Duende.IdentityServer.Services;
using IdentityServer.Application.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IdentityServer.UI.Pages.Account;

public class LogoutModel : PageModel
{
    private readonly IAccountService _accountService;
    private readonly IIdentityServerInteractionService _interaction;
    private readonly ILogger<LogoutModel> _logger;

    public LogoutModel(
        IAccountService accountService,
        IIdentityServerInteractionService interaction,
        ILogger<LogoutModel> logger)
    {
        _accountService = accountService;
        _interaction = interaction;
        _logger = logger;
    }

    public string LogoutId { get; set; }
    public bool ShowLogoutPrompt { get; set; } = true;

    public async Task<IActionResult> OnGetAsync(string logoutId = null)
    {
        LogoutId = logoutId;

        // Check if we have a logout context from IdentityServer
        var logoutContext = await _interaction.GetLogoutContextAsync(logoutId);
        
        // In Duende IdentityServer 7.x, ShowSignOutPrompt is not available
        // Show logout prompt if user is authenticated and no automatic logout is requested
        if (logoutContext != null && !string.IsNullOrEmpty(logoutContext.PostLogoutRedirectUri))
        {
            // If there's a post-logout redirect URI, we might want to skip the prompt
            // But for security, we'll still show it unless explicitly configured otherwise
            ShowLogoutPrompt = true;
        }

        // If user is not authenticated, redirect to home
        if (!User.Identity?.IsAuthenticated ?? true)
        {
            // If there's a logout context with redirect URI, redirect there
            if (logoutContext?.PostLogoutRedirectUri != null)
            {
                return Redirect(logoutContext.PostLogoutRedirectUri);
            }
            return RedirectToPage("/");
        }

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(string logoutId = null)
    {
        LogoutId = logoutId;

        // Get logout context from IdentityServer
        var logoutContext = await _interaction.GetLogoutContextAsync(logoutId);
        
        // Sign out the user
        await _accountService.SignOutAsync();
        
        _logger.LogInformation("User logged out");

        // If we have a post logout redirect URI from IdentityServer, redirect there
        if (logoutContext?.PostLogoutRedirectUri != null)
        {
            return Redirect(logoutContext.PostLogoutRedirectUri);
        }

        // Otherwise redirect to home
        return RedirectToPage("/");
    }
}

