using System.ComponentModel.DataAnnotations;
using Duende.IdentityServer.Services;
using IdentityServer.Application.Services;
using IdentityServer.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IdentityServer.UI.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly IAccountService _accountService;
        private readonly IIdentityServerInteractionService _interaction;
        private readonly ILogger<LoginModel> _logger;

        public LoginModel(
            IAccountService accountService,
            IIdentityServerInteractionService interaction,
            ILogger<LoginModel> logger)
        {
            _accountService = accountService;
            _interaction = interaction;
            _logger = logger;
        }

        [BindProperty]
        public LoginInputModel Input { get; set; } = new();

        [BindProperty(SupportsGet = true)]
        public string ReturnUrl { get; set; }

        public async Task<IActionResult> OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;

            // Check if we have a valid authorization context from IdentityServer
            var context = await _interaction.GetAuthorizationContextAsync(returnUrl);
            if (context != null)
            {
                // This is an IdentityServer authorization request
                ReturnUrl = returnUrl;
                return Page();
            }

            // If no context, just show the login page
            if (string.IsNullOrEmpty(ReturnUrl))
            {
                ReturnUrl = Url.Content("~/");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            _logger.LogInformation("Login POST received. ReturnUrl: {ReturnUrl}", ReturnUrl);

            // Check if this is an IdentityServer authorization request
            var context = await _interaction.GetAuthorizationContextAsync(ReturnUrl);

            if (context != null)
            {
                _logger.LogInformation("Valid IdentityServer context found. ClientId: {ClientId}", context.Client?.ClientId);
            }
            else
            {
                _logger.LogWarning("No IdentityServer context found for ReturnUrl: {ReturnUrl}", ReturnUrl);
            }

            // Use AccountService to sign in the user
            var result = await _accountService.SignInAsync(
                Input.Email,
                Input.Password,
                isPersistent: false,
                lockoutOnFailure: false
            );

            if (result.Success)
            {
                var userResult = await _accountService.GetUserByEmailAsync(Input.Email);
                if (userResult.Success && userResult.Data != null)
                {
                    _logger.LogInformation("User {Email} logged in successfully", Input.Email);

                    // If this is an IdentityServer authorization request, 
                    // redirect back to authorization endpoint
                    // IdentityServer middleware will detect authenticated user and continue flow
                    if (context != null && !string.IsNullOrEmpty(ReturnUrl))
                    {
                        _logger.LogInformation("Redirecting to authorization endpoint: {ReturnUrl}", ReturnUrl);
                        // Redirect back to /connect/authorize with the same parameters
                        // IdentityServer will detect the authenticated user cookie and issue authorization code
                        return Redirect(ReturnUrl);
                    }

                    // For non-IdentityServer requests, redirect to return URL or home
                    if (!string.IsNullOrEmpty(ReturnUrl) && Url.IsLocalUrl(ReturnUrl))
                    {
                        _logger.LogInformation("Redirecting to local return URL: {ReturnUrl}", ReturnUrl);
                        return LocalRedirect(ReturnUrl);
                    }
                }

                _logger.LogInformation("Redirecting to home page");
                return LocalRedirect("~/");
            }

            ModelState.AddModelError(string.Empty, result.Error ?? "Invalid login attempt.");
            return Page();
        }
    }

    public class LoginInputModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }
}
