using Azure.Core.Pipeline;
using IdentityServer.Application.Dtos;
using IdentityServer.Application.Services;
using IdentityServer.Domain.Entities;
using IdentityServer.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace IdentityServer.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class AuthController : ControllerBase
{
    private readonly ILogger<AuthController> _logger;
    private readonly IUserService _userService;


    public AuthController(
        IUserService userService,
        ILogger<AuthController> logger)
    {
        _userService = userService;
        _logger = logger;
    }

    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<IActionResult> Register(CreateUserRequest request)
    {
        /*
        var user = new ApplicationUser
        {
            UserName = request.Email,
            Email = request.Email,
            FirstName = request.FirstName,
            LastName = request.LastName,
            PhoneNumber = request.PhoneNumber,
            EmailConfirmed = true,
            IsActive = true,
            CreatedAt = DateTime.Now
        };

        var result = await _userManager.CreateAsync(user, request.Password);

        if (!result.Succeeded)
            return BadRequest(result.Errors);

        if (!await _roleManager.RoleExistsAsync(request.Role))
            await _roleManager.CreateAsync(new IdentityRole(request.Role));

        await _userManager.AddToRoleAsync(user, request.Role);
        */
        var createResult = await _userService.CreateUserAsync(request);
        _logger?.LogInformation("Register called for {Email}", request?.Email);

        if (!createResult.Success)
        {
            _logger?.LogWarning("User registration failed for {Email}: {Error}", request?.Email, createResult.Error);
            return BadRequest(createResult.Error);
        }

        _logger?.LogInformation("User registered successfully: {Email}", request?.Email);

        return Ok("User registered successfully.");
    }

    [HttpPut("update")]
    public async Task<IActionResult> Update([FromBody] UpdateUserRequest request)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? User.FindFirstValue("sub");
        if (string.IsNullOrEmpty(userId))
        {
            _logger?.LogWarning("Update called but userId not found in claims");
            return Unauthorized("User id not present in token.");
        }

        // var getOp = await _userService.GetUserAsync(userId);
        // if (!getOp.Success || getOp.Data is null)
        //     return NotFound(getOp.Error ?? "User not found.");

        _logger?.LogInformation("Update called for userId={UserId}", userId);

        var updateOp = await _userService.UpdateUserAsync(request, userId);
        if (!updateOp.Success)
        {
            _logger?.LogWarning("User update failed for userId={UserId}: {Error}", userId, updateOp.Error);
            return BadRequest(updateOp.Error);
        }

        _logger?.LogInformation("User updated successfully: userId={UserId}", userId);

        return Ok("User updated successfully.");
    }

    [HttpPut("change-password")]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest request)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? User.FindFirstValue("sub");
        if (string.IsNullOrEmpty(userId))
        {
            _logger?.LogWarning("ChangePassword called but userId not found in claims");
            return Unauthorized("User id not present in token.");
        }

        _logger?.LogInformation("ChangePassword called for userId={UserId}", userId);

        // Call service using values from the request body
        var changeOp = await _userService.ChangePassword(userId, request.OldPassword, request.NewPassword);

        if (!changeOp.Success)
        {
            _logger?.LogWarning("ChangePassword failed for userId={UserId}: {Error}", userId, changeOp.Error);
            if (changeOp.Error?.Contains("NotFound") ?? false)
                return NotFound(changeOp.Error);
            return BadRequest(changeOp.Error);
        }

        _logger?.LogInformation("ChangePassword successful for userId={UserId}", userId);
        return Ok("Your password is changed successfully!");
    }

    [HttpGet("user/{userId}")]
    [Authorize]
    public async Task<IActionResult> GetUserById(string userId)
    {
        _logger?.LogInformation("GetUserById called for {UserId}", userId);
        var result = await _userService.GetUserAsync(userId);
        if (!result.Success)
        {
            _logger?.LogWarning("GetUserById: user {UserId} not found: {Error}", userId, result.Error);
            return NotFound(result.Error);
        }

        _logger?.LogInformation("GetUserById: user {UserId} found", userId);
        // return user data only
        return Ok(result.Data);
    }

    // Diagnostic helper: lookup by email to confirm repository returns user data
    // This is AllowAnonymous for quick testing; remove or protect in production
    [HttpGet("user/by-email")]
    [AllowAnonymous]
    public async Task<IActionResult> GetUserByEmail([FromQuery] string email)
    {
        _logger?.LogInformation("GetUserByEmail called for {Email}", email);
        var result = await _userService.GetUserByEmailAsync(email);
        if (!result.Success)
        {
            _logger?.LogWarning("GetUserByEmail: user with email {Email} not found: {Error}", email, result.Error);
            return NotFound(result.Error);
        }

        _logger?.LogInformation("GetUserByEmail: user with email {Email} found", email);
        return Ok(result.Data);
    }
}